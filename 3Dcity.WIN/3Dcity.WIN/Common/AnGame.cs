using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;
using WindowsGame.Master;

namespace WindowsGame.Common
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class AnGame : Game
	{
		private readonly GraphicsDeviceManager graphics;

		public AnGame()
		{
			graphics = new GraphicsDeviceManager(this) {SupportedOrientations = DisplayOrientation.LandscapeLeft};
			Registration.Initialize();

			var manager = GameFactory.Resolve();
			MyGame.Construct(manager);
		}

		protected override void Initialize()
		{
			Engine.Initialize(this, graphics);
			MyGame.Initialize();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			MyGame.LoadContent();
			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			MyGame.UnloadContent();
			base.UnloadContent();
		}

		protected override void Update(GameTime gameTime)
		{
			MyGame.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			MyGame.Draw();
			base.Draw(gameTime);
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			MyGame.OnActivated();
			base.OnActivated(sender, args);
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			MyGame.OnDeactivated();
			base.OnDeactivated(sender, args);
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			MyGame.OnExiting();
			base.OnExiting(sender, args);
		}

	}
}
