using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class IntroScreen : BaseScreen, IScreen
	{
		private Vector2 startPosition;
		private Vector2 titlePosition;
		private Vector2 moverPosition;
		private Single startY;
		private Single titleY;
		private Single deltaY;
		private Boolean coolMusic;
		private UInt16 delay1, delay2, timer;
		private Byte index;
		private Boolean flag;

		public override void Initialize()
		{
			base.Initialize();

			titlePosition = Constants.TitlePosition;
			startPosition = new Vector2(titlePosition.X, Constants.ScreenHigh - Constants.GameOffsetY + 10);

			startY = startPosition.Y;
			titleY = titlePosition.Y;

			timer = 0;
			delay1 = 4000;
			delay2 = MyGame.Manager.ConfigManager.GlobalConfigData.IntroDelay;
			flag = false;
			NextScreen = ScreenType.Title;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			const UInt16 gapDelay = 660;//	good guess
			deltaY = startY - titleY;
			deltaY = gapDelay / deltaY;
			moverPosition = startPosition;

			//coolMusic = MyGame.Manager.ConfigManager.GlobalConfigData.CoolMusic;
			coolMusic = MyGame.Manager.StateManager.CoolMusic;
			SongType song = coolMusic ? SongType.CoolMusic : SongType.GameTitle;

			MyGame.Manager.SoundManager.PlayMusic(song, false);
			index = 0;
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
				// Exit on Title.
				return (Int32) NextScreen;
			}
			// Check to go forward second.
			Boolean test = MyGame.Manager.InputManager.SelectAll();
			Boolean mode = MyGame.Manager.InputManager.TitleModeAll();
			if (test || mode)
			{
				return (Int32) NextScreen;
			}

			UpdateTimer(gameTime);
			if (startY > titleY)
			{
				Single delta = (Single) gameTime.ElapsedGameTime.TotalSeconds;
				startY -= delta * deltaY * 24;
				moverPosition.Y = startY;
			}
			else
			{
				flag = true;
			}

			if (flag)
			{
				timer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
				if (timer > delay1)
				{
					timer -= delay1;
					index++;
					if (index >= Constants.INFO_LINES)
					{
						index = 0;
						return (Int32)NextScreen;
					}
				}
			}
			
			if (Timer > delay2)
			{
				return (Int32) NextScreen;
			}

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.RenderManager.DrawTitle(moverPosition);
			MyGame.Manager.RenderManager.DrawBottom();

			// Text data last!
			MyGame.Manager.TextManager.DrawBuild();
			MyGame.Manager.TextManager.DrawTitle();

			if (flag)
			{
				MyGame.Manager.TextManager.DrawGameInfo(index);
			}
		}

	}
}
