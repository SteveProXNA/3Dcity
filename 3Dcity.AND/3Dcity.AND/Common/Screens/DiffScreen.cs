using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class DiffScreen : BaseScreenSelect, IScreen
	{
		private Boolean isGodMode;
		private Boolean localCheat;
		private Byte localCount;

		public override void Initialize()
		{
			base.Initialize();

			CursorPositions = new Vector2[2];
			CursorPositions[0] = MyGame.Manager.TextManager.GetTextPosition(12, 11);
			CursorPositions[1] = MyGame.Manager.TextManager.GetTextPosition(23, 11);

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(235, 195, 405, 217);

			NextScreen = ScreenType.Level;
			PrevScreen = ScreenType.Title;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();
			isGodMode = MyGame.Manager.ConfigManager.GlobalConfigData.IsGodMode;
			localCheat = MyGame.Manager.StateManager.CheatGame;
			localCount = 0;
			base.LoadContent();

			SelectType = (Byte)MyGame.Manager.LevelManager.LevelType;
			MyGame.Manager.LevelManager.CheckLevelOrbs();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Check to go back first.
			Boolean back = MyGame.Manager.InputManager.Back();
			if (back)
			{
				return (Int32)PrevScreen;
			}

			IsMoving = false;
			UpdateFlag1(gameTime);
			if (Selected)
			{
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
				MyGame.Manager.LevelManager.SetLevelType((LevelType)SelectType);
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
				MyGame.Manager.LevelManager.SetLevelType((LevelType)SelectType);
			}

			return (Int32) CurrScreen;
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
			MyGame.Manager.TextManager.DrawCursor(CursorPositions[SelectType]);
			MyGame.Manager.TextManager.DrawInstruct();
		}

	}
}
