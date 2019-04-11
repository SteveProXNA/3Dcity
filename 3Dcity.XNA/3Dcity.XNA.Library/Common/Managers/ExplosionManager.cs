using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IExplosionManager 
	{
		void Initialize();
		void LoadContent(Byte enemyID, ExplodeType explodeType);
		void Reset(Byte theBombsExplode, UInt16 frameDelay);
		void Clear();
		void Explode(Byte enemyID, ExplodeType explodeType, Vector2 position);
		void Update(GameTime gameTime);
		void Draw();

		IList<Byte> ExplosionTest { get; }
	}

	public class ExplosionManager : IExplosionManager 
	{
		private IList<Byte> keys;
		private Byte maxBombsExplode;

		public void Initialize()
		{
			ExplosionList = new List<Explosion>(Constants.MAX_ENEMYS_SPAWN);
			ExplosionTest = new List<Byte>();
			ExplosionDict = new Dictionary<Byte, Explosion>(Constants.MAX_ENEMYS_SPAWN);

			for (Byte index = 0; index < Constants.MAX_EXPLODE_SPAWN; index++)
			{
				Explosion explode = new Explosion();
				explode.SetID(index);
				explode.Initialize(Constants.MAX_EXPLODE_FRAME);
				ExplosionList.Add(explode);
			}

			keys = new List<Byte>();
			maxBombsExplode = Constants.MAX_EXPLODE_SPAWN;
		}

		public void LoadContent(Byte enemyID, ExplodeType explodeType)
		{
			// Load the explosion rectangles on demand.
			Explosion explosion = ExplosionList[enemyID];
			explosion.LoadContent(MyGame.Manager.ImageManager.ExplodeRectangles[(Byte)explodeType]);
		}

		public void Reset(Byte theBombsExplode, UInt16 frameDelay)
		{
			maxBombsExplode = theBombsExplode;
			if (maxBombsExplode > Constants.MAX_EXPLODE_SPAWN)
			{
				maxBombsExplode = Constants.MAX_EXPLODE_SPAWN;
			}

			for (Byte index = 0; index < maxBombsExplode; index++)
			{
				Explosion explode = ExplosionList[index];
				explode.Reset(frameDelay);
			}

			// Important: collections MUST be cleared on Reset() to ensure stability!
			Clear();
			keys.Clear();
		}

		public void Clear()
		{
			ExplosionTest.Clear();
			ExplosionDict.Clear();
		}

		public void Explode(Byte enemyID, ExplodeType explodeType, Vector2 position)
		{
			Explosion explosion = ExplosionList[enemyID];
			if (explosion.IsExploding)
			{
				return;
			}

			Vector2 newPosition = position;
			newPosition.X += Constants.EXPLODE_OFFSET_X[(Byte)explodeType];
			newPosition.Y += Constants.EXPLODE_OFFSET_Y[(Byte)explodeType];
			explosion.SetPosition(newPosition);
			explosion.Explode(enemyID);

			ExplosionDict.Add(enemyID, explosion);
		}

		public void Update(GameTime gameTime)
		{
			ExplosionTest.Clear();
			if (0 == ExplosionDict.Count)
			{
				return;
			}

			keys.Clear();
			foreach (var key in ExplosionDict.Keys)
			{
				Explosion explosion = ExplosionDict[key];
				if (null == explosion || !explosion.IsExploding)
				{
					continue;
				}

				// Update explosion but check to see if finished.
				explosion.Update(gameTime);
				if (!explosion.IsExploding)
				{
					ExplosionTest.Add((Byte)explosion.EnemyID);
					keys.Add(explosion.ID);
				}
			}

			// https://stackoverflow.com/questions/9892790/deleting-while-iterating-over-a-dictionary
			foreach (var key in keys)
			{
				ExplosionDict.Remove(key);
			}
		}

		public void Draw()
		{
			if (0 == ExplosionDict.Count)
			{
				return;
			}

			foreach (var key in ExplosionDict.Keys)
			{
				Explosion explosion = ExplosionDict[key];
				if (null != explosion && explosion.IsExploding)
				{
					explosion.Draw();
				}
			}
		}

		public IList<Explosion> ExplosionList { get; private set; }
		public IList<Byte> ExplosionTest { get; private set; }
		public IDictionary<Byte, Explosion> ExplosionDict { get; private set; }
	}
}
