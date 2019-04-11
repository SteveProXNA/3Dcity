using System;
using WindowsGame.Common.Screens;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class ResumeScreenBAK2 : BaseScreenPlay, IScreen
	{
		private UInt16 delay1, delay2, timer;
		private Boolean flag;

		public override void Initialize()
		{
			MyGame.Manager.DebugManager.Reset();
			base.Initialize();
			UpdateGrid = true;

			// TODO make delay values configurable!
			delay1 = 200;
			delay2 = 5000;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			// Load the configuration for level type + index.
			LevelType = MyGame.Manager.LevelManager.LevelType;
			LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			MyGame.Manager.LevelManager.LoadLevelConfigData(LevelType, LevelIndex);
			LevelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			// Bullets.
			Byte bulletMaxim = LevelConfigData.BulletMaxim;
			UInt16 bulletFrame = LevelConfigData.BulletFrame;
			UInt16 bulletShoot = LevelConfigData.BulletShoot;
			MyGame.Manager.BulletManager.Reset(bulletMaxim, bulletFrame, bulletShoot);
			// TODO delete
			MyGame.Manager.BulletManager.Reset(2, 100, 200);

			// Enemies.
			Byte enemySpawn = LevelConfigData.EnemySpawn;
			Byte enemyTotal = LevelConfigData.EnemyTotal;
			MyGame.Manager.EnemyManager.Reset(LevelType, enemySpawn, 1000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			// Explosions.
			UInt16 explodeDelay = LevelConfigData.ExplodeDelay;
			MyGame.Manager.ExplosionManager.Reset(LevelConfigData.EnemySpawn, explodeDelay);

			// Resume screen cannot die
			//Boolean isGodMode = MyGame.Manager.StateManager.IsGodMode;
			//Invincibile = isGodMode || LevelConfigData.BonusLevel;
			Invincibile = true;
			timer = 0;
			flag = true;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);
			if (Timer > delay1)
			{
				timer += Timer;
				if (timer > delay2)
				{
					return (Int32)ScreenType.Ready;
				}

				Timer -= delay1;
				flag = !flag;
			}

			CheckLevelComplete = false;

			// Target.
			DetectTarget(gameTime);

			// Bullets.
			//DetectBullets();
			UpdateBullets(gameTime);
			VerifyBullets();

			// Explosions.
			UpdateExplosions(gameTime);
			VerifyExplosions();

			// Enemies.
			UpdateEnemies(gameTime);
			VerifyEnemies();
			if (NextScreen != CurrScreen)
			{
				return (Int32)NextScreen;
			}

			// Icons.
			UpdateIcons();

			// Score.
			UpdateScore(gameTime);

			// Summary.
			UpdateLevel();
			if (NextScreen != CurrScreen)
			{
				return (Int32)NextScreen;
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02(flag);
			if (flag)
			{
				MyGame.Manager.SpriteManager.LargeTarget.Draw();
			}

			// Text data last!
			DrawText();
		}

	}
}
