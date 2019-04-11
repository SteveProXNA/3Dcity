using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;
using WindowsGame.Master.Objects;

namespace WindowsGame.Common.Screens
{
	public abstract class BaseScreen
	{
		protected UInt16 Timer { get; set; }
		protected Vector2[] BackedPositions { get; set; }
		protected ScreenType CurrScreen { get; set; }
		protected ScreenType NextScreen { get; set; }
		protected ScreenType PrevScreen { get; set; }
		protected Boolean GamePause { get; set; }
		protected Boolean UpdateGrid { get; set; }

		protected IList<TextData> TextDataList { get; private set; }
		protected Boolean UpdateStar { get; private set; }

		public virtual void Initialize()
		{
			String screenName = GetType().Name.ToLower();
			screenName = screenName.Replace("screen", String.Empty);
			CurrScreen = (ScreenType)Enum.Parse(typeof(ScreenType), screenName, true);
			NextScreen = CurrScreen;
			PrevScreen = CurrScreen;

			UpdateStar = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateStar;
			UpdateGrid = false;
		}

		public virtual void LoadContent()
		{
			Timer = 0;
		}

		public virtual Int32 Update(GameTime gameTime)
		{
			// Check if game is paused.
			Boolean gameState = MyGame.Manager.InputManager.GameState();
			if (gameState)
			{
				MyGame.Manager.StateManager.ToggleGameState();
				GamePause = MyGame.Manager.StateManager.GamePause;
				MyGame.Manager.SoundManager.GamePause(GamePause);

				BaseObject icon = MyGame.Manager.IconManager.GameState;
				MyGame.Manager.IconManager.ToggleIcon(icon);

				return (Int32)CurrScreen;
			}

			// If game paused then do not check for sound.
			if (GamePause)
			{
				MyGame.Manager.InputManager.ResetMotors();
				return (Int32)CurrScreen;
			}

			// Enable / disable sound.
			Boolean gameSound = MyGame.Manager.InputManager.GameSound();
			if (gameSound)
			{
				MyGame.Manager.StateManager.ToggleGameSound();
				Boolean gameQuiet = MyGame.Manager.StateManager.GameQuiet;
				MyGame.Manager.SoundManager.GameQuiet(gameQuiet);

				BaseObject icon = MyGame.Manager.IconManager.GameSound;
				MyGame.Manager.IconManager.ToggleIcon(icon);
			}

			// Update grid + stars.
			if (UpdateStar)
			{
				MyGame.Manager.RenderManager.UpdateStar(gameTime);
			}

			if (UpdateGrid)
			{
				MyGame.Manager.RenderManager.UpdateGrid(gameTime);
			}

			return (Int32)CurrScreen;
		}


		protected void UpdateTimer(GameTime gameTime)
		{
			Timer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
		}

		public virtual void Draw()
		{
			//MyGame.Manager.DeviceManager.DrawTitle(GetType().Name);
			MyGame.Manager.DeviceManager.DrawTitle();

			if (MyGame.Manager.ConfigManager.GlobalConfigData.RenderBack)
			{
				MyGame.Manager.RenderManager.Draw();
			}

			if (MyGame.Manager.ConfigManager.GlobalConfigData.RenderIcon)
			{
				MyGame.Manager.IconManager.Draw();
			}
		}

		protected void ResetTimer()
		{
			Timer = 0;
		}

		protected void LoadTextData()
		{
			LoadTextData(GetType().Name);
		}

		private void LoadTextData(String screen)
		{
			TextDataList = MyGame.Manager.TextManager.LoadTextData(screen);
		}

	}
}