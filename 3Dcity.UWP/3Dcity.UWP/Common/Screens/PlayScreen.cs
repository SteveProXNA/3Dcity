using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class PlayScreen : BaseScreenPlay, IScreen
	{
		private Boolean isGodMode;
		private Boolean currRumble;
		private Boolean prevRumble;

		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;
			PrevScreen = ScreenType.Quit;
			currRumble = false;
			prevRumble = false;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();
			NextScreen = CurrScreen;
			MyGame.Manager.RenderManager.SetGridDelay(LevelConfigData.GridDelay);

			isGodMode = MyGame.Manager.StateManager.CheatGame || LevelConfigData.BonusLevel;
			currRumble = false;
			prevRumble = false;
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			// Check to go back first.
			Boolean back = MyGame.Manager.InputManager.Back();
			if (back)
			{
				MyGame.Manager.SoundManager.PauseMusic();
				return (Int32)PrevScreen;
			}


			// Log delta to monitor performance!
#if DEBUG
			//string time = gameTime.ElapsedGameTime.TotalSeconds.ToString();
			//MyGame.Manager.Logger.Info(time);
			//Console.WriteLine(time);
			//System.Diagnostics.Debug.WriteLine(time);
#endif


			// Begin common code...
			CheckLevelComplete = false;

			// Target.
			DetectTarget(gameTime);

			// Bullets.
			DetectBullets();
			UpdateBullets(gameTime);
			VerifyBullets();

			// Explosions.
			UpdateExplosions(gameTime);
			VerifyExplosions();

			// Enemies.
			UpdateEnemies(gameTime);
			if (!isGodMode)
			{
				currRumble = MyGame.Manager.EnemyManager.EnemyController > 0;
				if (currRumble)
				{
					Single rightMotor = MyGame.Manager.EnemyManager.EnemyController;
					MyGame.Manager.InputManager.SetMotors(0, rightMotor);
				}
				else
				{
					if (currRumble != prevRumble)
					{
						MyGame.Manager.InputManager.ResetMotors();
					}
				}
				prevRumble = currRumble;
			}

			VerifyEnemies();
			if (NextScreen != CurrScreen)
			{
				// Edge case: reset shooting icon if dead.
				MyGame.Manager.IconManager.UpdateFireIcon(0);
				return (Int32) NextScreen;
			}

			// Icons.
			UpdateIcons();

			// Score.
			UpdateScore(gameTime);

			// Summary.
			UpdateLevel();
			if (NextScreen != CurrScreen)
			{
				return (Int32) NextScreen;
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			DrawTextCommon();
			MyGame.Manager.ScoreManager.DrawBlink();
		}

	}
}
