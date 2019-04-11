using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreenPlay : BaseScreen
	{
		protected LevelType LevelType { get; set; }
		protected Byte LevelIndex { get; set; }
		protected LevelConfigData LevelConfigData { get; set; }

		protected Boolean Invincibile { get; set; }

		//protected Byte EnemySpawn { get; private set; }
		protected Byte EnemyTotal { get; private set; }

		//protected UInt16 ExplodeDelay { get; private set; }

		protected Boolean CheckLevelComplete { private get; set; }


		public override void LoadContent()
		{
			// Load the configuration for level type + index.
			LevelType = MyGame.Manager.LevelManager.LevelType;
			LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			LevelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			Invincibile = MyGame.Manager.StateManager.IsGodMode ||
			              MyGame.Manager.StateManager.CheatGame || LevelConfigData.BonusLevel;

			//EnemySpawn = MyGame.Manager.EnemyManager.EnemySpawn;
			EnemyTotal = MyGame.Manager.EnemyManager.EnemyTotal;

			base.LoadContent();
		}


		// Target.
		protected static void DetectTarget(GameTime gameTime)
		{
			// Move target unconditionally.
			Boolean fast = MyGame.Manager.InputManager.Accelerate();
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(fast, horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);
		}


		// Bullets.
		protected static void DetectBullets()
		{
			// Test shooting enemy ships.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				SByte bulletIndex = MyGame.Manager.BulletManager.CheckBullets();
				if (Constants.INVALID_INDEX == bulletIndex)
				{
					return;
				}

				Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
				MyGame.Manager.SoundManager.PlayBulletSoundEffect();
				MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
			}
		}
		protected static void UpdateBullets(GameTime gameTime)
		{
			MyGame.Manager.BulletManager.Update(gameTime);
		}

		protected void VerifyBullets()
		{
			VerifyBullets(true);
		}

		protected void VerifyBullets(Boolean updateScore)
		{
			if (0 == MyGame.Manager.BulletManager.BulletTest.Count)
			{
				return;
			}

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
				if (0 == enemy.FrameCount)
				{
					continue;
				}

				// Check if bullet collides with enemy.
				Byte enemyFrame = enemy.FrameIndex;
				Boolean collide = MyGame.Manager.CollisionManager.BulletCollideEnemy(enemy.Position, bullet.Position, LevelType, enemyFrame);
				if (!collide)
				{
					continue;
				}

				// Collision!	Enemy dead and trigger explode...
				if (updateScore)
				{
					MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Ship);
					MyGame.Manager.ScoreManager.UpdateGameScore(enemyFrame);
				}

				ExplodeType explodeType = enemy.FrameIndex < 4 ? ExplodeType.Small : ExplodeType.Big;
				Byte enemyID = enemy.ID;
				MyGame.Manager.ExplosionManager.LoadContent(enemyID, explodeType);

				//Vector2 explosionPosition = enemy.Position;		//Use this to centralize explosion around enemy.
				Vector2 explosionPosition = bullet.Position;		//Use this to centralize explosion around bullet.
				MyGame.Manager.ExplosionManager.Explode(enemyID, explodeType, explosionPosition);
				enemy.Dead();
			}
		}


		// Explosions.
		protected static void UpdateExplosions(GameTime gameTime)
		{
			MyGame.Manager.ExplosionManager.Update(gameTime);
		}
		protected void VerifyExplosions()
		{
			if (0 == MyGame.Manager.ExplosionManager.ExplosionTest.Count)
			{
				return;
			}

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
					CheckLevelComplete = true;
				}
			}
		}


		// Enemies.
		protected static void UpdateEnemies(GameTime gameTime)
		{
			MyGame.Manager.EnemyManager.Update(gameTime);
		}
		protected void VerifyEnemies()
		{
			if (0 == MyGame.Manager.EnemyManager.EnemyTest.Count)
			{
				return;
			}

			// Enemy has maxed out frames so check for collision.
			LargeTarget target = MyGame.Manager.SpriteManager.LargeTarget;
			IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;

			for (Byte testIndex = 0; testIndex < enemyTest.Count; testIndex++)
			{
				Enemy enemy = enemyTest[testIndex];
				if (!Invincibile)
				{
					// First check if enemy instantly kills target.
					Boolean test = CheckEnemyKillTarget(enemy, target);

					// Instant death!	Game Over.
					if (test)
					{
						// Do NOT reset enemy here as CheckThisEnemy() will remove from dictionary and reset there.
						// Set the enemy position so can be rendered by the additional "kill" enemy to show death!
						MyGame.Manager.StateManager.SetKillSpace(enemy.Position);
						NextScreen = ScreenType.Dead;
					}
					else
					{
						// Enemy not kill target but missed so increment miss total.
						MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Wrong);
						MyGame.Manager.ScoreManager.IncrementMisses();
						if (MyGame.Manager.ScoreManager.MissesTotal >= Constants.MAX_MISSES)
						{
							// If maximum misses then game over.
							// Do NOT reset enemy here as CheckThisEnemy() will remove from dictionary and reset there.
							NextScreen = ScreenType.Dead;
						}
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
					CheckLevelComplete = true;
				}
			}
		}
		private static Boolean CheckEnemyKillTarget(BaseSprite enemy, BaseSprite target)
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


		// Icons.
		protected static void UpdateIcons()
		{
			Byte canShootIndex = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			MyGame.Manager.IconManager.UpdateFireIcon(canShootIndex);
		}


		// Score.
		protected static void UpdateScore(GameTime gameTime)
		{
			MyGame.Manager.ScoreManager.Update(gameTime);
		}


		// Summary.
		protected void UpdateLevel()
		{
			if (!CheckLevelComplete)
			{
				return;
			}

			Boolean noMoreEnemies = MyGame.Manager.EnemyManager.CheckEnemiesNone();
			if (noMoreEnemies)
			{
				NextScreen = ScreenType.Finish;
			}
		}


		protected void DrawSheet01()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
		}

		protected void DrawSheet02()
		{
			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
		}

		protected void DrawSheet02(Boolean flag)
		{
			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.SmallTarget.Draw();
			if (flag)
			{
				MyGame.Manager.SpriteManager.LargeTarget.Draw();
			}
		}

		protected void DrawBacked()
		{
			MyGame.Manager.RenderManager.DrawBorderPosition(BackedPositions);
		}

		protected static void DrawText()
		{
			// Text data last!
			DrawTextCommon();
			MyGame.Manager.ScoreManager.Draw();
		}

		protected static void DrawTextCommon()
		{
			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
		}

	}
}