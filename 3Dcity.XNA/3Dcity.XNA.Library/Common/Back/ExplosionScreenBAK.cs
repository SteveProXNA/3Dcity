using System;
using WindowsGame.Common.Screens;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class ExplosionScreenBAK : BaseScreen, IScreen
	{
		private Vector2[] boxPositions;
		private SByte number;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			number = Constants.INVALID_INDEX;
		}

		public override void LoadContent()
		{
			boxPositions = GetBoxPositions();

			LevelType levelType = MyGame.Manager.LevelManager.LevelType;
			const Byte enemySpawn = 1;
			const Byte enemyTotal = 3;
			MyGame.Manager.EnemyManager.Reset(levelType, enemySpawn, 2000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			//MyGame.Manager.EnemyManager.SpawnOneEnemy(0);
			//MyGame.Manager.EnemyManager.Spawn(1500);
			//MyGame.Manager.EnemyManager.Start(2000);
			MyGame.Manager.ExplosionManager.Reset(8, 100);

			MyGame.Manager.ScoreManager.Reset();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Log delta to monitor performance!
#if DEBUG
			//MyGame.Manager.Logger.Info(gameTime.ElapsedGameTime.TotalSeconds.ToString());
#endif

			// Move enemies.
			MyGame.Manager.EnemyManager.Update(gameTime);
			//MyGame.Manager.EnemyManager.CheckAllEnemies();		// TODO delete

			number = MyGame.Manager.InputManager.Number();
			if (Constants.INVALID_INDEX != number)
			{
				Byte explodeIndex = (Byte) number;
				Explosion explosion = MyGame.Manager.ExplosionManager.ExplosionList[explodeIndex];
				if (!explosion.IsExploding)
				{
					Vector2 position = GetPosition(explodeIndex);
					Byte diff = (Byte)(number % 2);
					ExplodeType explodeType = (ExplodeType)diff;
					MyGame.Manager.ExplosionManager.LoadContent(explodeIndex, explodeType);
					MyGame.Manager.ExplosionManager.Explode(explodeIndex, 5, explodeType, position);
				}
			}

			MyGame.Manager.ExplosionManager.Update(gameTime);
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();

			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			//MyGame.Manager.SpriteManager.Draw();

			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Engine.SpriteBatch.Draw(Assets.ZZindigoTexture, boxPositions[index], Color.Black);
			}

			//Vector2 enemyPos = new Vector2(350-120, 280 + 80);
			//Rectangle enemyRect = MyGame.Manager.ImageManager.EnemyRectangles[3];
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemyPos, enemyRect, Color.White);

			MyGame.Manager.ExplosionManager.Draw();


			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private static Vector2 GetPosition(Byte theNumber)
		{
			if (theNumber < 5)
			{
				Single x = theNumber*140 + 50;
				return new Vector2(x, 60 + (theNumber * 10));
			}
			else
			{
				Single x = (theNumber - 5)* 140 + 150;
				return new Vector2(x, 200 + (theNumber * 10));
			}
		}

		private Vector2[] GetBoxPositions()
		{
			const Single hi = 80 + Constants.GameOffsetY;
			const Single lo = 280 + Constants.GameOffsetY;

			boxPositions = new Vector2[Constants.MAX_ENEMYS_SPAWN];
			boxPositions[0] = new Vector2(160 * 0, hi);
			boxPositions[1] = new Vector2(160 * 1, hi);
			boxPositions[2] = new Vector2(160 * 2, hi);
			boxPositions[3] = new Vector2(160 * 3, hi);
			boxPositions[4] = new Vector2(160 * 4, hi);

			const Byte offset = 190;
			boxPositions[5] = new Vector2(160 * 0 + offset, lo);
			boxPositions[6] = new Vector2(160 * 1 + offset, lo);
			boxPositions[7] = new Vector2(160 * 2 + offset, lo);

			return boxPositions;
		}

	}
}
