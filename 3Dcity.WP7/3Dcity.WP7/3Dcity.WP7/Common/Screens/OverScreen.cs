using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class OverScreen : BaseScreenSelect, IScreen
	{
		private UInt16 bigDelay, medDelay, smlDelay;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(280, 213, 325, 217);

			bigDelay = MyGame.Manager.ConfigManager.GlobalConfigData.OverDelay;
			medDelay = 3000;
			smlDelay = Constants.SLIGHT_PAUSE*4;

			NextScreen = ScreenType.Title;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();
			base.LoadContent();

			flag1 = false;
			flag2 = false;

			Killspace = MyGame.Manager.StateManager.KillSpace;
			MyGame.Manager.SpriteManager.KillEnemy.SetPosition(Killspace);
			MyGame.Manager.SoundManager.StopMusic();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			if (Selected)
			{
				Complete();
				return (Int32) NextScreen;
			}

			if (Flag1)
			{
				UpdateFlag1(gameTime);
			}
			else
			{
				UpdateTimer(gameTime);
			}

			// Initial pause.
			if (Timer <= smlDelay)
			{
				return (Int32)CurrScreen;
			}

			if (!flag1)
			{
				// Ensure sound effect once.
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Over);
				flag1 = true;
				return (Int32) CurrScreen;
			}

			if (Timer <= medDelay)
			{
				return (Int32) CurrScreen;
			}

			if (!flag2)
			{
				// Ensure sound effect once.
				MyGame.Manager.SoundManager.PlayMusic(SongType.GameOver);
				flag2 = true;
				return (Int32) CurrScreen;
			}

			// Now can check to pro actively goto next screen.
			Boolean back = MyGame.Manager.InputManager.Back();
			if (back)
			{
				Complete();
				return (Int32)NextScreen;
			}

			DetectLefts();
			DetectRight();
			DetectSelect();
			if (Flag1)
			{
				Timer = 0;
				MyGame.Manager.SoundManager.StopMusic();
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Right);
				return (Int32)CurrScreen;
			}

			// Time expired so advance.
			//if (status || center || Timer > bigDelay)
			if (Timer > bigDelay)
			{
				Complete();
				return (Int32) NextScreen;
			}

			return (Int32)CurrScreen;
		}

		private static void Complete()
		{
			// If game over then leave things as they are...
			MyGame.Manager.EnemyManager.Clear();
			MyGame.Manager.ExplosionManager.Clear();

			MyGame.Manager.StateManager.SetKillSpace(Vector2.Zero);
			MyGame.Manager.ScoreManager.ResetMisses();
			MyGame.Manager.SoundManager.StopMusic();
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			if (Vector2.Zero != Killspace)
			{
				// Draw dead enemy on instant death only.
				MyGame.Manager.SpriteManager.KillEnemy.Draw();
			}

			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			DrawSheet02();
			MyGame.Manager.SpriteManager.LargeTarget.Draw();
			DrawBacked();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
		}

	}
}
