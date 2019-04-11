using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class QuitScreen : BaseScreenSelect, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();

			CursorPositions = new Vector2[2];
			CursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(14, 11);
			CursorPositions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(275, 195, 385, 217);

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.InputManager.ResetMotors();
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();
			base.LoadContent();

			NextScreen = CurrScreen;
			SelectType = 1;
		}

		public override Int32 Update(GameTime gameTime)
		{
			// IMPORTANT - customize pause / sound on Quit screen
			// because would like to unconditionally pause music.
			// Plus do not want to be able to resume so don't check sound button tap!

			// Check if game is paused.
			Boolean gameState = MyGame.Manager.InputManager.GameState();
			if (gameState)
			{
				MyGame.Manager.StateManager.ToggleGameState();
				GamePause = MyGame.Manager.StateManager.GamePause;

				BaseObject icon = MyGame.Manager.IconManager.GameState;
				MyGame.Manager.IconManager.ToggleIcon(icon);

				return (Int32)CurrScreen;
			}

			// If game paused then do not check for sound.
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}
			if (UpdateGrid)
			{
				MyGame.Manager.RenderManager.UpdateGrid(gameTime);
			}


			Boolean statusBar = MyGame.Manager.InputManager.Back();
			if (statusBar)
			{
				NextScreen = ScreenType.Play;
				MyGame.Manager.SoundManager.ResumeMusic();
				return (Int32) NextScreen;
			}

			IsMoving = false;
			UpdateFlag1(gameTime);
			if (Selected)
			{
				NextScreen = SelectType == 0 ? ScreenType.Over : ScreenType.Play;
				if (ScreenType.Over == NextScreen)
				{
					return (Int32) NextScreen;
				}

				MyGame.Manager.SoundManager.ResumeMusic();
				return (Int32) NextScreen;
			}
			if (Flag1)
			{
				return (Int32) CurrScreen;
			}

			if (Lefts || Right)
			{
				return (Int32) CurrScreen;
			}
			DetectLefts();
			if (Lefts)
			{
				SelectType = 0;
			}
			DetectRight();
			if (Right)
			{
				SelectType = 1;
			}
			if (Lefts || Right)
			{
				PlaySoundEffect();
				return (Int32) CurrScreen;
			}

			DetectSelect();
			if (Flag1)
			{
				PlaySoundEffect();
				return (Int32) CurrScreen;
			}

			UpdateFlag2(gameTime);
			if (IsMoving)
			{
				return (Int32)CurrScreen;
			}

			DetectMove();
			if (0 != MoveValue)
			{
				SelectType = (Byte)(1 - SelectType);
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
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			DrawSheet02();
			MyGame.Manager.SpriteManager.LargeTarget.Draw();
			DrawBacked();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawCursor(CursorPositions[SelectType]);
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
		}

	}
}
