using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LevelScreen : BaseScreenSelect, IScreen
	{
		private readonly String[] cursorOptions = { Globalize.CURSOR_LEFTS, "  ", Globalize.CURSOR_RIGHT };
		private Vector2 levelNamePosition;
		private Vector2 levelTextPosition;

		private Byte levelIndex;
		private Byte maximLevel;
		private String levelName;
		private String levelValu;
		private Boolean isGodMode;
		private Boolean localCheat;
		private Byte localCount;

		public override void Initialize()
		{
			base.Initialize();

			CursorPositions = new Vector2[1];
			CursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(16, 11);

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(235, 195, 405, 217);

			NextScreen = ScreenType.Load;
			PrevScreen = ScreenType.Diff;

			levelNamePosition = MyGame.Manager.TextManager.GetTextPosition(19, 11);
			levelTextPosition = MyGame.Manager.TextManager.GetTextPosition(12, 11);

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();
			isGodMode = MyGame.Manager.ConfigManager.GlobalConfigData.IsGodMode;
			localCheat = MyGame.Manager.StateManager.CheatGame;
			localCount = 0;
			base.LoadContent();

			maximLevel = MyGame.Manager.LevelManager.MaximLevel;
			levelIndex = MyGame.Manager.LevelManager.LevelIndex;
			PopulateLevelData(levelIndex);
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
				return (Int32) PrevScreen;
			}

			IsMoving = false;
			UpdateFlag1(gameTime);
			if (Selected)
			{
				MyGame.Manager.LevelManager.SetLevelIndex(levelIndex);
				MyGame.Manager.ScoreManager.ResetAll();
				return (Int32) NextScreen;
			}
			if (Flag1)
			{
				return (Int32) CurrScreen;
			}

			// Check for cheat detection.
			if (!isGodMode)
			{
				if (!localCheat)
				{
					Boolean titleMode = MyGame.Manager.InputManager.TitleMode();
					if (titleMode)
					{
						localCount++;
						if (localCount >= Constants.MAX_CHEATS)
						{
							localCheat = true;
							MyGame.Manager.StateManager.SetCheatGame(localCheat);
							MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Cheat);
							MyGame.Manager.TextManager.CheatTitle();
						}
					}
				}
			}

			if (Lefts || Right)
			{
				return (Int32) CurrScreen;
			}
			DetectLefts();
			DetectRight();
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
				return (Int32) CurrScreen;
			}

			DetectMove();
			if (MoveValue < 0)
			{
				levelIndex--;
				if (levelIndex >= Byte.MaxValue)
				{
					levelIndex = (Byte)(maximLevel - 1);
				}

				PopulateLevelData(levelIndex);
			}
			if (MoveValue > 0)
			{
				levelIndex++;
				if (levelIndex >= maximLevel)
				{
					levelIndex = 0;
				}

				PopulateLevelData(levelIndex);
			}

			return (Int32) CurrScreen;
		}

		private void PopulateLevelData(Byte theLevelIndex)
		{
			MyGame.Manager.LevelManager.SetLevelIndex(theLevelIndex);

			levelName = MyGame.Manager.LevelManager.LevelName;
			levelValu = MyGame.Manager.LevelManager.LevelValu;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();
			MyGame.Manager.RenderManager.DrawTitle();

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.SpriteManager.DrawCursor();
			DrawBacked();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.DrawText(cursorOptions[MoveIndex], CursorPositions[0]);
			MyGame.Manager.TextManager.DrawText(levelName, levelNamePosition);
			MyGame.Manager.TextManager.DrawText(levelValu, levelTextPosition);
			MyGame.Manager.TextManager.DrawInstruct();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
