using System;
using WindowsGame.Define;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Objects
{
	public class BaseSprite
	{
		private Texture2D texture;

		public virtual void Initialize(Vector2 position, Rectangle bounds)
		{
			Initialize(position, Rectangle.Empty, bounds);
		}

		public virtual void Initialize(Vector2 position, Rectangle collision, Rectangle bounds)
		{
			BaseX = (UInt16)(position.X);
			BaseY = (UInt16)(position.Y);
			Position = position;
			Collision = collision;
			Bounds = bounds;
		}

		public virtual void LoadContent(Texture2D theTexture)
		{
			UInt16 width = (UInt16)(theTexture.Width);
			UInt16 height = (UInt16)(theTexture.Height);

			LoadContent(theTexture, width, height);
		}

		protected virtual void LoadContent(Texture2D theTexture, UInt16 width, UInt16 height)
		{
			texture = theTexture;

			SizeW = (UInt16)(texture.Width);
			SizeH = (UInt16)(texture.Height);

			Single midX = width / 2.0f + BaseX;
			Single midY = height / 2.0f + BaseY;
			Midpoint = new Vector2(midX, midY);
		}

		public virtual void Update(GameTime gameTime, Single horz, Single vert)
		{
		}

		public virtual void Draw()
		{
			Engine.SpriteBatch.Draw(texture, Position, Color.White);
		}

		public UInt16 BaseX { get; private set; }
		public UInt16 BaseY { get; private set; }
		public UInt16 SizeW { get; private set; }
		public UInt16 SizeH { get; private set; }
		public Vector2 Position { get; protected set; }
		public Vector2 Midpoint { get; private set; }
		public Rectangle Collision { get; private set; }
		public Rectangle Bounds { get; private set; }
	}
}
