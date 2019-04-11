using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TitleScreen : BaseScreen, IScreen
	{
		private Vector2 promptPosition;
		private UInt16 promptDelay;
		private UInt16 selectDelay;
		private Byte iconIndex;
		private Byte localCount;
		private Boolean isGodMode;
		private Boolean localCheat;
		private Boolean flag1, flag2;

		public override void Initialize()
		{
			base.Initialize();

			promptPosition = MyGame.Manager.TextManager.GetTextPosition(13, 11);
			//promptPosition.X -= 7.5f;
			promptDelay = MyGame.Manager.ConfigManager.GlobalConfigData.FlashDelay;
			selectDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(255, 213, 365, 217);

			isGodMode = MyGame.Manager.ConfigManager.GlobalConfigData.IsGodMode;
			MyGame.Manager.StateManager.SetIsGodMode(isGodMode);

			NextScreen = ScreenType.Diff;
			PrevScreen = ScreenType.Exit;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.TextManager.ResetTitle();
			MyGame.Manager.ScoreManager.ResetAll();

			localCheat = false;
			MyGame.Manager.StateManager.SetCheatGame(localCheat);
			if (isGodMode)
			{
				MyGame.Manager.TextManager.CheatTitle();
			}

			localCount = 0;
			iconIndex = 0;
			flag1 = false;
			flag2 = true;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			// Check to go back first.
			Boolean back = MyGame.Manager.InputManager.BackAll();
			if (back)
			{
				return (Int32) PrevScreen;
			}

			UpdateTimer(gameTime);
			if (flag1)
			{
				if (Timer > selectDelay * 2)
				{
					flag1 = false;
					iconIndex = Convert.ToByte(flag1);
					MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
					return (Int32)NextScreen;
				}

				iconIndex = Convert.ToByte(flag1);
				MyGame.Manager.IconManager.UpdateFireIcon(iconIndex);
				return (Int32) CurrScreen;
			}

			// Check for cheat detection.
			if (!isGodMode)
			{
				if (!localCheat)
				{
					Boolean titleMode = MyGame.Manager.InputManager.TitleModeAll();
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

			// Check to go forward second.
			if (!flag1)
			{
				Boolean fire = false;
#if WINDOWS
				fire = MyGame.Manager.InputManager.SelectJoystick();
#endif
				if (!fire)
				{
					fire = MyGame.Manager.InputManager.SelectWithout();
				}

				Boolean left = MyGame.Manager.InputManager.LeftsSide();
				Boolean rght = MyGame.Manager.InputManager.RightSide();

				if (fire || left || rght)
				{
					MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Right);
					flag1 = true;
					flag2 = true;
					Timer = 0;
				}
			}

			if (Timer > promptDelay)
			{
				flag2 = !flag2;
				Timer -= promptDelay;
			}

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.RenderManager.DrawTitle();
			MyGame.Manager.RenderManager.DrawBorderPosition(BackedPositions);

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawGameInfo(0);
			MyGame.Manager.ScoreManager.Draw();

			if (flag2)
			{
				Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.INSERT_COINS, promptPosition, Color.White);
			}
		}

	}
}
