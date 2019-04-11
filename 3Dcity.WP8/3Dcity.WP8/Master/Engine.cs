using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Master
{
	public static class Engine
	{
		public static void Initialize(Game game, GraphicsDeviceManager graphics)
		{
			Game = game;
			Content = game.Content;
			GraphicsDeviceManager = graphics;
			GraphicsDevice = GraphicsDeviceManager.GraphicsDevice;
			SpriteBatch = new SpriteBatch(GraphicsDevice);
		}

		public static Game Game { get; private set; }
		public static ContentManager Content { get; private set; }
		public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
		public static GraphicsDevice GraphicsDevice { get; private set; }
		public static SpriteBatch SpriteBatch { get; private set; }
	}
}