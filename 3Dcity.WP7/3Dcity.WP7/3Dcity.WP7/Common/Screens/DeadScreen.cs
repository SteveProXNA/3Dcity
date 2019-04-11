using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class DeadScreen : BaseScreenSelect, IScreen
	{
		private UInt16 bigDelay, medDelay, smlDelay;

		private Vector2 killspace;
		private Vector2 deathPosition;
		private String deathText;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(290, 195, 305, 217);

			deathPosition = MyGame.Manager.TextManager.GetTextPosition(15, 11);
			bigDelay = MyGame.Manager.ConfigManager.GlobalConfigData.DeadDelay;
			medDelay = 1500;

			Boolean unlimitedCont = MyGame.Manager.ConfigManager.GlobalConfigData.UnlimitedCont;
			NextScreen = unlimitedCont ? ScreenType.Cont : ScreenType.Over;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SoundManager.StopMusic();
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();

			killspace = MyGame.Manager.StateManager.KillSpace;
			MyGame.Manager.SpriteManager.KillEnemy.SetPosition(killspace);

			Boolean miss = Constants.MAX_MISSES == MyGame.Manager.ScoreManager.MissesTotal;
			deathText = miss ? Globalize.DEAD_OPTION1 : Globalize.DEAD_OPTION2;
			smlDelay = miss ? Constants.LONGER_PAUSE : Constants.SLIGHT_PAUSE;

			Boolean dead = Constants.MAX_MISSES != MyGame.Manager.ScoreManager.MissesTotal;
			deathText = dead ? Globalize.DEAD_OPTION2 : Globalize.DEAD_OPTION1;
			smlDelay = dead ? Constants.SLIGHT_PAUSE : (UInt16) 600;

			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			UpdateTimer(gameTime);

			// Initial pause.
			if (Timer <= smlDelay)
			{
				return (Int32) CurrScreen;
			}

			if (!Flag1)
			{
				// Ensure sound effect once.
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Aaargh);
				MyGame.Manager.InputManager.SetMotors(1, 0);
				Flag1 = true;
			}

			if (Timer <= medDelay)
			{
				return (Int32) CurrScreen;
			}

			// Now can check to pro actively goto next screen.
			MyGame.Manager.InputManager.ResetMotors();
			Boolean status = MyGame.Manager.InputManager.StatusBar();

			// Time expired so advance.
			if (status || Timer > bigDelay)
			{
				return (Int32) NextScreen;
			}

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			if (Vector2.Zero != killspace)
			{
				// Draw dead enemy on instant death only.
				MyGame.Manager.SpriteManager.KillEnemy.Draw();
			}

			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			DrawSheet02();
			MyGame.Manager.SpriteManager.LargeTarget.Draw();

			if (Flag1)
			{
				DrawBacked();
			}

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();

			if (!Flag1)
			{
				return;
			}

			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(deathText, deathPosition);
		}

	}
}
