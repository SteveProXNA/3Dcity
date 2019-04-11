using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class PlayScreen : BaseScreen, IScreen
	{
		private LevelType levelType;
		private Byte levelIndex;
		private LevelConfigData levelConfigData;
		private Boolean invincibile;
		private Boolean checkLevelComplete;

		public override void Initialize()
		{
			MyGame.Manager.DebugManager.Reset();
			base.Initialize();
			PrevScreen = ScreenType.Quit;
		}

		public override void LoadContent()
		{
			// Load the configuration for level type + index.
			levelType = MyGame.Manager.LevelManager.LevelType;
			levelIndex = MyGame.Manager.LevelManager.LevelIndex;
			MyGame.Manager.LevelManager.LoadLevelConfigData(levelType, levelIndex);
			levelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			Boolean isGodMode = MyGame.Manager.StateManager.IsGodMode;
			invincibile = isGodMode || levelConfigData.BonusLevel;

			// Not bad settings for default.
			//MyGame.Manager.BulletManager.Reset(10, 200, 100);
			MyGame.Manager.BulletManager.Reset(5, 50, 100);
			//MyGame.Manager.BulletManager.Reset(100, 20, 50);		// TODO remove - extreme!



			//const Byte enemySpawn = 1;
			//const Byte enemyTotal = 3;
			Byte enemySpawn = MyGame.Manager.ConfigManager.GlobalConfigData.EnemySpawn;	// 1;  // TODO level config
			Byte enemyTotal = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyTotal;	// 1;  // TODO level config
			MyGame.Manager.EnemyManager.Reset(levelType, enemySpawn, 1000, 5000, enemyTotal);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.ExplosionManager.Reset(enemySpawn, MyGame.Manager.ConfigManager.GlobalConfigData.ExplodeDelay);

			MyGame.Manager.SoundManager.PlayMusic(SongType.GameMusic);
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			Boolean escape = MyGame.Manager.InputManager.Escape();
			if (escape)
			{
				return (Int32)PrevScreen;
			}

			checkLevelComplete = false;

			// Log delta to monitor performance!
#if DEBUG
			//string time = gameTime.ElapsedGameTime.TotalSeconds.ToString();
			//MyGame.Manager.Logger.Info(time);
			//Console.WriteLine(time);
			//System.Diagnostics.Debug.WriteLine(time);
#endif

			// TODO delete
			//MyGame.Manager.CollisionManager.ClearCollisionList();

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);


			// Test shooting enemy ships.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				SByte bulletIndex = MyGame.Manager.BulletManager.CheckBullets();
				if (Constants.INVALID_INDEX != bulletIndex)
				{
					// TODO check!
					MyGame.Manager.SoundManager.PlaySoundEffect();

					Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
					MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
				}
			}


			// BULLETS.
			// Update bullets and test collision.
			MyGame.Manager.BulletManager.Update(gameTime);
			if (MyGame.Manager.BulletManager.BulletTest.Count > 0)
			{
				IList<Bullet> bulletTest = MyGame.Manager.BulletManager.BulletTest;
				for (Byte testIndex = 0; testIndex < bulletTest.Count; testIndex++)
				{
					Bullet bullet = bulletTest[testIndex];
					SByte testID = MyGame.Manager.CollisionManager.DetermineEnemySlot(bullet.Position);

					// Check to ensure bullet will something.
					if (Constants.INVALID_INDEX == testID)
					{
						continue;
					}

					// Check to ensure bullet in same slot as enemy.
					Byte slotID = (Byte)testID;
					if (!MyGame.Manager.EnemyManager.EnemyDict.ContainsKey(slotID))
					{
						continue;
					}

					// It could be possible that the enemy in this slot is already dead!
					Enemy enemy = MyGame.Manager.EnemyManager.EnemyDict[slotID];
					if (EnemyType.Dead == enemy.EnemyType)
					{
						continue;
					}

					// Can kill initial enemy [at frame count = 0] because enemy will be "hidden".
					Byte enemyCount = enemy.FrameCount;
					if (0 == enemy.FrameCount)
					{
						continue;
					}

					// Check if bullet collides with enemy.
					Byte enemyFrame = enemy.FrameIndex;
					Boolean collide = MyGame.Manager.CollisionManager.BulletCollideEnemy(enemy.Position, bullet.Position, levelType, enemyFrame);
					if (!collide)
					{
						continue;
					}

					// Collision!	Enemy dead and trigger explode...
					MyGame.Manager.ScoreManager.UpdateGameScore(enemyFrame);

					// TODO if DiffType == HARD and enemy.FrameCount = 9 OR 11 then enemy dead?
					ExplodeType explodeType = enemy.FrameIndex < 4 ? ExplodeType.Small : ExplodeType.Big;
					Byte enemyID = enemy.ID;
					MyGame.Manager.ExplosionManager.LoadContent(enemyID, explodeType);
					MyGame.Manager.ExplosionManager.Explode(enemyID, explodeType, enemy.Position);
					enemy.Dead();
				}
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
					else
					{
						checkLevelComplete = true;
					}
				}
			}

			// ENEMYS.
			// Update enemies and test collisions.
			MyGame.Manager.EnemyManager.Update(gameTime);
			if (MyGame.Manager.EnemyManager.EnemyTest.Count > 0)
			{
				// TODO delete
				//checkLevelComplete = true;

				// Enemy has maxed out frames so check for collision.
				LargeTarget target = MyGame.Manager.SpriteManager.LargeTarget;
				IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;

				for (Byte testIndex = 0; testIndex < enemyTest.Count; testIndex++)
				{
					Enemy enemy = enemyTest[testIndex];
					if (!invincibile)
					{
						// First check if enemy instantly kills target.
						Boolean test = CheckEnemyKillTarget(enemy, target);

						// Instant death!	Game Over.
						if (test)
						{
							// Do NOT reset enemy here as we want to see Target killed by Enemy!
							return (Int32)ScreenType.Dead;
						}

						// Enemy not kill target but missed so increment miss total.
						MyGame.Manager.ScoreManager.IncrementMisses();
						if (MyGame.Manager.ScoreManager.MissesTotal >= Constants.MAX_MISSES)
						{
							// If maximum misses then game over.
							enemy.Reset();
							return (Int32)ScreenType.Dead;
						}
					}

					// Finally, check if anymore enemies to spawn...
					Byte enemyID = enemy.ID;
					Boolean check = MyGame.Manager.EnemyManager.CheckThisEnemy(enemyID);
					if (!check)
					{
						MyGame.Manager.EnemyManager.SpawnOneEnemy(enemyID);
					}
					else
					{
						checkLevelComplete = true;
					}
				}
			}


			// Update fire icon.
			Byte canShootIndex = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			MyGame.Manager.IconManager.UpdateFireIcon(canShootIndex);


			// Finally, check to see if all enemies finished = level complete.
			if (checkLevelComplete)
			{
				Boolean noMoreEnemies = MyGame.Manager.EnemyManager.CheckEnemiesNone();
				if (noMoreEnemies)
				{
					return (Int32)ScreenType.Finish;
				}
			}

			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32)CurrScreen;
		}

		private static Boolean CheckEnemyKillTarget(Enemy enemy, LargeTarget target)
		{
			Boolean test = MyGame.Manager.CollisionManager.BoxesCollision(enemy.Position, target.Position);
			if (!test)
			{
				return false;
			}

			test = MyGame.Manager.CollisionManager.ColorCollision(enemy.Position, target.Position);
			if (!test)
			{
				return false;
			}

			return true;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Individual texture.
			//MyGame.Manager.DebugManager.Draw();		// TODO delete

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
