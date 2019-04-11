using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IBulletManager 
	{
		void Initialize();
		void LoadContent();
		void Reset(Byte theBulletShoot, UInt16 frameDelay, UInt16 shootDelay);
		SByte CheckBullets();
		void Update(GameTime gameTime);
		
		void Shoot(Byte bulletIndex, Vector2 position);
		void Draw();

		IList<Bullet> BulletTest { get; }
		Boolean CanShoot { get; }
	}

	public class BulletManager : IBulletManager 
	{
		private Byte allBulletShoot;
		private Byte maxBulletShoot;

		public void Initialize()
		{
			allBulletShoot = MyGame.Manager.ConfigManager.GlobalConfigData.MaxBullets;

			BulletList = new List<Bullet>(allBulletShoot);
			BulletTest = new List<Bullet>(allBulletShoot);

			for (Byte index = 0; index < allBulletShoot; index++)
			{
				Bullet bullet = new Bullet();
				bullet.SetID(index);
				bullet.Initialize(Constants.MAX_BULLET_FRAME);
				BulletList.Add(bullet);
			}

			maxBulletShoot = allBulletShoot;
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < allBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				bullet.LoadContent(MyGame.Manager.ImageManager.BulletRectangles);
			}
		}

		public void Reset(Byte theBulletShoot, UInt16 frameDelay, UInt16 shootDelay)
		{
			maxBulletShoot = theBulletShoot;
			if (maxBulletShoot > allBulletShoot)
			{
				maxBulletShoot = allBulletShoot;
			}

			for (Byte index = 0; index < maxBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				bullet.Reset(frameDelay);
			}

			BulletTest.Clear();
			ShootDelay = shootDelay;
			ShootTimer = 0;
			CanShoot = true;
		}

		public SByte CheckBullets()
		{
			SByte bulletIndex = Constants.INVALID_INDEX;
			if (!CanShoot)
			{
				return bulletIndex;
			}

			for (Byte testerIndex = 0; testerIndex < maxBulletShoot; testerIndex++)
			{
				Bullet bullet = BulletList[testerIndex];
				if (!bullet.IsFiring)
				{
					CanShoot = false;
					bulletIndex = (SByte)testerIndex;
					break;
				}
			}

			return bulletIndex;
		}

		public void Update(GameTime gameTime)
		{
			if (!CanShoot)
			{
				ShootTimer += gameTime.ElapsedGameTime.Milliseconds;
				if (ShootTimer >= ShootDelay)
				{
					CanShoot = true;
					ShootTimer -= ShootDelay;
				}
			}

			BulletTest.Clear();
			for (Byte index = 0; index < maxBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				if (bullet.IsFiring)
				{
					bullet.Update(gameTime);
					if (!bullet.IsFiring)
					{
						BulletTest.Add(bullet);
					}
				}
			}
		}

		public void Shoot(Byte bulletIndex, Vector2 position)
		{
			Bullet bullet = BulletList[bulletIndex];
			bullet.Shoot(position);
		}

		public void Draw()
		{
			for (Byte index = 0; index < maxBulletShoot; index++)
			{
				Bullet bullet = BulletList[index];
				if (bullet.IsFiring)
				{
					bullet.Draw();
				}
			}
		}

		public IList<Bullet> BulletList { get; private set; }
		public IList<Bullet> BulletTest { get; private set; }
		public Boolean CanShoot { get; private set; }
		public UInt16 ShootDelay { get; private set; }
		public Single ShootTimer { get; private set; }

	}
}
