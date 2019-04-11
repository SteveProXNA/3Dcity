using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Objects
{
	public class BaseObject
	{
		private Rectangle[] rectangles;

		public virtual void Initialize(Vector2 position, Rectangle collision)
		{
			Initialize(position, collision, Rectangle.Empty);
		}

		public virtual void Initialize(Vector2 position, Rectangle collision, Rectangle bounds)
		{
			BaseX = (UInt16)(position.X);
			BaseY = (UInt16)(position.Y);
			Position = position;
			Collision = collision;
			Bounds = bounds;
			Index = 0;
		}

		public virtual void LoadContent(Rectangle theRectangle)
		{
			Rectangle[] theRectangles = { theRectangle };
			LoadContent(theRectangles);
		}

		public virtual void LoadContent(Rectangle[] theRectangles)
		{
			rectangles = theRectangles;

			// Assume all textures in array are same size!
			UInt16 width = (UInt16)(theRectangles[0].Width);
			UInt16 height = (UInt16)(theRectangles[0].Height);
			SizeW = width;
			SizeH = height;

			Single midX = width / 2.0f + BaseX;
			Single midY = height / 2.0f + BaseY;
			Midpoint = new Vector2(midX, midY);
		}

		public void ToggleIcon()
		{
			Index = (Byte)(1 - Index);
		}
		public void UpdateIcon(Byte index)
		{
			Index = index;
		}

		public virtual void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, Position, rectangles[0], Color.White);
		}

		protected virtual void Draw(Byte index)
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, Position, rectangles[index], Color.White);
		}

		public UInt16 BaseX { get; private set; }
		public UInt16 BaseY { get; private set; }
		public UInt16 SizeW { get; private set; }
		public UInt16 SizeH { get; private set; }
		public Vector2 Position { get; private set; }
		public Vector2 Midpoint { get; private set; }
		public Rectangle Collision { get; private set; }
		public Rectangle Bounds { get; private set; }
		protected Byte Index { get; private set; }
	}
}
