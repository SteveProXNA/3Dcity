using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Managers
{
	public interface IScreenManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void Draw();
	}

	public class ScreenManager : IScreenManager 
	{
		private IDictionary<Int32, IScreen> screens;
		private Int32 currScreen = (Int32)ScreenType.Splash;
		private Int32 nextScreen = (Int32)ScreenType.Splash;
		private readonly Color color = Color.Black;

		public void Initialize()
		{
			screens = GetScreens();
			screens[(Int32)ScreenType.Splash].Initialize();
			screens[(Int32)ScreenType.Init].Initialize();
		}

		public void LoadContent()
		{
			foreach (var key in screens.Keys)
			{
				if ((Int32)ScreenType.Splash == key || (Int32)ScreenType.Init == key)
				{
					continue;
				}

				screens[key].Initialize();
			}
		}

		public void Update(GameTime gameTime)
		{
			if (currScreen != nextScreen)
			{
				currScreen = nextScreen;
				screens[currScreen].LoadContent();
			}

			nextScreen = screens[currScreen].Update(gameTime);
		}

		public void Draw()
		{
			MyGame.Manager.ResolutionManager.BeginDraw(color);
			Engine.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MyGame.Manager.ResolutionManager.TransformationMatrix);
			screens[currScreen].Draw();
			Engine.SpriteBatch.End();
		}

		private static Dictionary<Int32, IScreen> GetScreens()
		{
			return new Dictionary<Int32, IScreen>
			{
				{(Int32)ScreenType.Splash, new SplashScreen()},
				{(Int32)ScreenType.Init, new InitScreen()},
				{(Int32)ScreenType.Title, new TitleScreen()},
				{(Int32)ScreenType.Intro, new IntroScreen()},
				{(Int32)ScreenType.Begin, new BeginScreen()},
				{(Int32)ScreenType.Diff, new DiffScreen()},
				{(Int32)ScreenType.Level, new LevelScreen()},
				{(Int32)ScreenType.Load, new LoadScreen()},
				{(Int32)ScreenType.Ready, new ReadyScreen()},
				{(Int32)ScreenType.Play, new PlayScreen()},
				{(Int32)ScreenType.Quit, new QuitScreen()},
				{(Int32)ScreenType.Finish, new FinishScreen()},
				{(Int32)ScreenType.Boss, new BossScreen()},
				{(Int32)ScreenType.Dead, new DeadScreen()},
				{(Int32)ScreenType.Cont, new ContScreen()},
				{(Int32)ScreenType.Over, new OverScreen()},
				{(Int32)ScreenType.Resume, new ResumeScreen()},
				{(Int32)ScreenType.Beat, new BeatScreen()},
				{(Int32)ScreenType.Demo, new DemoScreen()},
				{(Int32)ScreenType.Exit, new ExitScreen()},
				{(Int32)ScreenType.Test, new TestScreen()},
			};
		}

	}
}
