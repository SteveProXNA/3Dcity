using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class LevelScreenBAK : BaseScreen, IScreen
	{
		private readonly String[] cursorOptions = new string[3] { Globalize.CURSOR_LEFTS, "  ", Globalize.CURSOR_RIGHT };
		private Vector2 cursorPosition;
		private Vector2 spritePosition;
		private Vector2 levelNamePosition;
		private Vector2 levelTextPosition;

		private Byte levelIndex;
		private Byte maximLevel;
		private String levelName;
		private String levelValu;

		private UInt16 selectDelay;
		private Byte iconIndex, moveIndex;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			UpdateGrid = false;
			NextScreen = ScreenType.Ready;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();

			iconIndex = 0;
			moveIndex = 1;

			cursorPosition = MyGame.Manager.TextManager.GetTextPosition(16, 11);
			spritePosition = MyGame.Manager.SpriteManager.SmallTarget.Position;
			spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];

			levelNamePosition = MyGame.Manager.TextManager.GetTextPosition(19, 11);
			levelTextPosition = MyGame.Manager.TextManager.GetTextPosition(12, 11);

			maximLevel = MyGame.Manager.LevelManager.MaximLevel;
			//levelNames = MyGame.Manager.LevelManager.LevelNames;		// TODO delete
			levelIndex = MyGame.Manager.LevelManager.LevelIndex;
			PopulateLevelData(levelIndex);

			selectDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;
			flag1 = flag2 = false;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// TODO add check for Back button on Windows and Android
			// this would navigate back to the previous Diff screen!
			if (flag1)
			{
				UpdateTimer(gameTime);
				if (Timer > selectDelay * 2)
				{
					flag1 = false;
					iconIndex = Convert.ToByte(flag1);
					MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
					MyGame.Manager.LevelManager.SetLevelIndex(levelIndex);
					return (Int32)NextScreen;
				}

				iconIndex = Convert.ToByte(flag1);
				MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
				return (Int32)CurrScreen;
			}

			if (flag2)
			{
				UpdateTimer(gameTime);
				if (Timer > selectDelay)
				{
					moveIndex = 1;
					spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];
					MyGame.Manager.SpriteManager.SmallTarget.SetPosition(spritePosition);

					Timer = 0;
					flag2 = false;
				}

				return (Int32)CurrScreen;
			}

			// Check fire first.
			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				flag1 = true;
				return (Int32)CurrScreen;
			}

			// Check move second.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			if (0 == horz)
			{
				return (Int32)CurrScreen;
			}

			if (horz < 0)
			{
				levelIndex--;
				if (levelIndex >= Byte.MaxValue)
				{
					levelIndex = (Byte) (maximLevel - 1);
				}

				PopulateLevelData(levelIndex);
				moveIndex = 0;
			}
			if (horz > 0)
			{
				levelIndex++;
				if (levelIndex >= maximLevel)
				{
					levelIndex = 0;
				}

				PopulateLevelData(levelIndex);
				moveIndex = 2;
			}

			spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];
			MyGame.Manager.SpriteManager.SmallTarget.SetPosition(spritePosition);

			//levelType = (Byte)(1 - levelType);
			flag2 = true;
			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.RenderManager.DrawTitle();

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.SpriteManager.DrawCursor();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(cursorOptions[moveIndex], cursorPosition);
			MyGame.Manager.TextManager.DrawText(levelName, levelNamePosition);
			MyGame.Manager.TextManager.DrawText(levelValu, levelTextPosition);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawInstruct();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

		private void PopulateLevelData(Byte theLevelIndex)
		{
			MyGame.Manager.LevelManager.SetLevelIndex(theLevelIndex);

			levelName = MyGame.Manager.LevelManager.LevelName;
			levelValu = MyGame.Manager.LevelManager.LevelValu;
		}

	}
}
