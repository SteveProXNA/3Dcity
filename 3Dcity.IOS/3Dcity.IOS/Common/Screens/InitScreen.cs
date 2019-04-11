using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class InitScreen : IScreen
	{
		private Vector2 bannerPosition;
		private Vector2 annualPosition;
		private Vector2 musicPosition;

		private Int32 nextScreen;
		private UInt16 splashDelay;
		private UInt16 splashTimer;
		private Boolean join;
		private Boolean flag;

		public void Initialize()
		{
			Single wide = (Constants.ScreenWide - Assets.SplashTexture.Width) / 2.0f;
			Single high = (Constants.ScreenHigh - Assets.SplashTexture.Height) / 2.0f;
			bannerPosition = new Vector2(wide, high);

			nextScreen = GetNextScreen();
			splashDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SplashDelay;
			splashTimer = 0;
			join = false;
			flag = false;
		}

		public void LoadContent()
		{
			annualPosition = MyGame.Manager.TextManager.GetTextPosition(32, 23);
			musicPosition = MyGame.Manager.TextManager.GetTextPosition(0, 23);

			MyGame.Manager.ThreadManager.LoadContentAsync();
		}

		public Int32 Update(GameTime gameTime)
		{
			splashTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;

			// Do not attempt to progress until join.
			join = MyGame.Manager.ThreadManager.Join(1);
			if (!join)
			{
				return (Int32)ScreenType.Init;
			}

			if (splashTimer > splashDelay)
			{
				return nextScreen;
			}

			if (!flag)
			{
				Boolean test = MyGame.Manager.InputManager.SelectAll();
				if (test)
				{
					flag = true;
					MyGame.Manager.StateManager.SetCoolMusic(!MyGame.Manager.StateManager.CoolMusic);
				}
			}

			Boolean next = MyGame.Manager.InputManager.CenterPos();
			if (next)
			{
				return nextScreen;
			}
			
			return (Int32)ScreenType.Init;
		}

		public void Draw()
		{
			//MyGame.Manager.DeviceManager.DrawTitle(GetType().Name);
			MyGame.Manager.DeviceManager.DrawTitle();

			Engine.SpriteBatch.Draw(Assets.SplashTexture, bannerPosition, Color.White);
			if (0 == splashDelay)
			{
				return;
			}

			if (!join)
			{
				return;
			}

			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.YEAR_TITLE, annualPosition, Color.White);

			if (!flag)
			{
				return;
			}

			String text = MyGame.Manager.StateManager.CoolMusic ? Globalize.CURSOR_LEFTS : Globalize.CURSOR_RIGHT;
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, text, musicPosition, Color.White);
		}

		private static Int32 GetNextScreen()
		{
			ScreenType screenType = MyGame.Manager.ConfigManager.GlobalConfigData.ScreenType;
			if (ScreenType.Splash == screenType || ScreenType.Init == screenType)
			{
				screenType = ScreenType.Intro;
			}

			return (Int32)screenType;
		}

	}
}
