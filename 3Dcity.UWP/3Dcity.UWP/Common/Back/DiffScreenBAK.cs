using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class DiffScreenBAK : BaseScreen, IScreen
	{
		private Vector2[] cursorPositions;
		private Vector2 spritePosition;
		private Byte levelType;

		private UInt16 selectDelay;
		private Byte iconIndex, moveIndex;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			UpdateGrid = false;
			NextScreen = ScreenType.Level;
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();

			iconIndex = 0;
			moveIndex = 1; 

			cursorPositions = new Vector2[2];
			cursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(12, 11);
			cursorPositions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);

			spritePosition = MyGame.Manager.SpriteManager.SmallTarget.Position;
			spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];
			levelType = (Byte)MyGame.Manager.LevelManager.LevelType;

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

			if (flag1)
			{
				UpdateTimer(gameTime);
				if (Timer > selectDelay * 2)
				{
					flag1 = false;
					iconIndex = Convert.ToByte(flag1);
					MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
					MyGame.Manager.LevelManager.SetLevelType((LevelType)levelType);
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
				moveIndex = 0;
			}
			if (horz > 0)
			{
				moveIndex = 2;
			}

			spritePosition.X = Constants.CURSOR_OFFSET_X[moveIndex];
			MyGame.Manager.SpriteManager.SmallTarget.SetPosition(spritePosition);

			levelType = (Byte) (1 - levelType);
			MyGame.Manager.LevelManager.SetLevelType((LevelType)levelType);

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
			MyGame.Manager.TextManager.DrawCursor(cursorPositions[levelType]);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawInstruct();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
