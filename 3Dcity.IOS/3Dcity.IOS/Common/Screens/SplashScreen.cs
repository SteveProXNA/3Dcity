using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class SplashScreen : IScreen
	{
		private Vector2 bannerPosition;
		private Boolean flag;

		public void Initialize()
		{
			Single wide = (Constants.ScreenWide - Assets.SplashTexture.Width) / 2.0f;
			Single high = (Constants.ScreenHigh - Assets.SplashTexture.Height) / 2.0f;

			bannerPosition = new Vector2(wide, high);
			flag = false;
		}

		public void LoadContent()
		{
		}

		public Int32 Update(GameTime gameTime)
		{
			return flag ? (Int32)ScreenType.Init : (Int32)ScreenType.Splash;
		}

		public void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SplashTexture, bannerPosition, Color.White);
			flag = true;
		}

	}
}
