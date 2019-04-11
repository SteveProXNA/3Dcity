using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class TestScreenBAK : BaseScreen, IScreen
	{
		//private Rectangle orbRectangle;
		//private Vector2 orbPosition;
		//private SByte number;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			UpdateGrid = false;
			//number = Constants.INVALID_INDEX;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();

			LevelType levelType = MyGame.Manager.LevelManager.LevelType;
			Byte enemySpawn = MyGame.Manager.ConfigManager.GlobalConfigData.EnemySpawn;	// 1;  // TODO level config
			Byte enemyTotal = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyTotal;	// 1;  // TODO level config
			MyGame.Manager.EnemyManager.Reset(levelType, enemySpawn, 2000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.ExplosionManager.Reset(8, 100);
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

			// ENEMYS.
			// Update enemies and test collisions.
			MyGame.Manager.EnemyManager.Update(gameTime);
			if (MyGame.Manager.EnemyManager.EnemyTest.Count > 0)
			{
				Boolean collision = MyGame.Manager.CollisionManager.CheckOne();
				if (collision)
				{
					return (Int32)ScreenType.Over;
				}

				// Iterate all enemy ships to test and add to misses.
				IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;
				for (Byte testIndex = 0; testIndex < enemyTest.Count; testIndex++)
				{
					// TODO update score manager => 1x miss per enemy here!
					// if (misses >= 4) then return game over

					Enemy enemy = enemyTest[testIndex];
					//MyGame.Manager.EnemyManager.CheckThisEnemy(enemy);
					Byte enemyID = enemy.ID;

					Boolean check = MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
					if (!check)
					{
						MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
					}
				}
			}


			// TODO - is this redundant because Enemy "kill" target [ABOVE]
			//MyGame.Manager.EnemyManager.CheckAllEnemies();
			//if (MyGame.Manager.CollisionManager.EnemysCollisionList.Count > 0)
			//{
			//    // Check collisions here.
			//}


			// TODO - implement bullet logic... for now simulate slotID of bullet on last frame
			SByte number = MyGame.Manager.InputManager.Number();
			if (Constants.INVALID_INDEX != number)
			{
				// Retrieve slotID from bullet position
				// (determine if collision!)
				Byte slotID = (Byte)number;
				if (MyGame.Manager.EnemyManager.EnemyDict.ContainsKey(slotID))
				{
					// Can kill initial enemy [at frame count = 0] because enemy will be "hidden".
					Enemy enemy = MyGame.Manager.EnemyManager.EnemyDict[slotID];
					if (0 != enemy.FrameCount)
					{
						// Collision!	Enemy dead and trigger explode...
						MyGame.Manager.ScoreManager.UpdateGameScore(enemy.FrameIndex);

						// TODO if DiffType == HARD and enemy.FrameCount = 9 OR 11 then enemy dead?
						ExplodeType explodeType = enemy.FrameIndex < 4 ? ExplodeType.Small : ExplodeType.Big;
						Byte enemyID = enemy.ID;
						MyGame.Manager.ExplosionManager.LoadContent(enemyID, explodeType);
						MyGame.Manager.ExplosionManager.Explode(enemyID, explodeType, enemy.Position);
						enemy.Dead();
					}
				}
				//else
				//{
				//    MyGame.Manager.Logger.Info("miss " + (slotID).ToString());		// TODO remove logging
				//}
			}


			// EXPLOSIONS.
			MyGame.Manager.ExplosionManager.Update(gameTime);
			if (MyGame.Manager.ExplosionManager.ExplosionTest.Count > 0)
			{
				// Iterate all enemy ships to test and add to misses.
				IList<Byte> explosionTest = MyGame.Manager.ExplosionManager.ExplosionTest;
				for (Byte testIndex = 0; testIndex < explosionTest.Count; testIndex++)
				{
					Byte enemyID = explosionTest[testIndex];

					Boolean check = MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
					if (!check)
					{
						MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
					}
				}
			}

			// Finally, check to see if all enemies finished = level complete.
			Boolean gameover = MyGame.Manager.EnemyManager.CheckEnemiesNone();
			if (gameover)
			{
				return (Int32)ScreenType.Cont; // TODO actually finished level!
			}


			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.SpriteManager.Draw();
			MyGame.Manager.ExplosionManager.Draw();

			// Individual texture.
			MyGame.Manager.DebugManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}