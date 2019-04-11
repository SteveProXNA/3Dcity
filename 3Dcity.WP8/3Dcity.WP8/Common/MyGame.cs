using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;
using WindowsGame.Master;

namespace WindowsGame.Common
{
	public static class MyGame
	{
		public static void Construct(IGameManager manager)
		{
			Manager = manager;
		}

		public static void Initialize()
		{
			Manager.Logger.Initialize();

			Manager.ConfigManager.Initialize();
			Manager.ConfigManager.LoadContent();

			Manager.ContentManager.Initialize();
			Manager.ContentManager.LoadContentSplash();

			Manager.ResolutionManager.Initialize();
			Manager.ScreenManager.Initialize();
			Manager.SoundManager.Initialize();
			Manager.StateManager.Initialize();
			Manager.StorageManager.Initialize();
			Manager.ThreadManager.Initialize();

			Manager.DeviceManager.Initialize();
			Manager.InputManager.Initialize();

			// Initialize default values.
			Manager.LevelManager.SetLevelType(LevelType.Easy);
			Manager.LevelManager.SetLevelIndex(0);
		}

		public static void LoadContent()
		{
			Byte framesPerSecond = Manager.ConfigManager.GlobalConfigData.FramesPerSecond;
			Engine.Game.IsFixedTimeStep = Constants.IsFixedTimeStep;
			Engine.Game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / framesPerSecond);
			Engine.Game.IsMouseVisible = Constants.IsMouseVisible;
			Manager.ResolutionManager.LoadContent(Constants.IsFullScreen, Constants.ScreenWide, Constants.ScreenHigh, Constants.UseExposed, Constants.ExposeWide, Constants.ExposeHigh);
			Manager.InputManager.LoadContent();
		}

		public static void LoadContentAsync()
		{
			// Load all the content first!
			Manager.ContentManager.LoadContent();
			Manager.ImageManager.LoadContent();

			Manager.BossManager.Initialize();
			Manager.BulletManager.Initialize();
			Manager.CollisionManager.Initialize();
			Manager.CommandManager.Initialize();
			Manager.ControlManager.Initialize();
			Manager.DebugManager.Initialize();
			Manager.DelayManager.Initialize();
			Manager.EnemyManager.Initialize();
			Manager.EventManager.Initialize();
			Manager.ExplosionManager.Initialize();
			Manager.IconManager.Initialize();
			Manager.LevelManager.Initialize();
			Manager.MoverManager.Initialize();
			Manager.RandomManager.Initialize();
			Manager.RenderManager.Initialize();
			Manager.ScoreManager.Initialize();
			Manager.SpriteManager.Initialize();
			Manager.StopwatchManager.Initialize();
			Manager.TextManager.Initialize();

			Manager.BossManager.LoadContent();
			Manager.BulletManager.LoadContent();
			Manager.CollisionManager.LoadContent();
			Manager.ControlManager.LoadContent();
			Manager.CommandManager.LoadContent();
			Manager.DelayManager.LoadContent();
			Manager.EnemyManager.LoadContent();
			
			Manager.LevelManager.LoadContent();
			Manager.RenderManager.LoadContent();
			Manager.ScoreManager.LoadContent();
			Manager.ScreenManager.LoadContent();
			Manager.SpriteManager.LoadContent();
			Manager.StorageManager.LoadContent();

			// Invoke icon manager last...!
			Manager.IconManager.LoadContent();

			GC.Collect();
		}

		public static void UnloadContent()
		{
			Engine.Game.Content.Unload();
		}

		public static void Update(GameTime gameTime)
		{
			// 50fps = 20ms = 20 / 1000 = 0.02
			// Single delta = (Single) gameTime.ElapsedGameTime.TotalSeconds;

			Manager.InputManager.Update(gameTime);

#if WINDOWS && DEBUG
			Boolean escape = Manager.InputManager.Escape();
			if (escape)
			{
				if (Manager.ConfigManager.GlobalConfigData.QuitsToExit)
				{
					Engine.Game.Exit();
					return;
				}
			}
#endif

			Manager.ScreenManager.Update(gameTime);
		}

		public static void Draw()
		{
			Manager.ScreenManager.Draw();
		}

		public static void OnActivated()
		{
			Manager.StorageManager.LoadContent();
		}

		public static void OnDeactivated()
		{
			Manager.InputManager.ResetMotors();
			Manager.StorageManager.SaveContent();

#if ANDROID
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
			System.Environment.Exit(0);
#endif
		}

		public static void OnExiting()
		{
			Manager.SoundManager.StopMusic();
			Manager.DeviceManager.Abort();
		}

		public static IGameManager Manager { get; private set; }
	}

}