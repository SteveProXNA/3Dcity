using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Sprites
{
	public class BaseSprite
	{
		private Rectangle[] rectangles;

		public virtual void Initialize(Byte maxFrames)
		{
			MaxFrames = maxFrames;
			FrameIndex = 0;
		}

		public virtual void Initialize(Vector2 position)
		{
			Initialize(position, Rectangle.Empty);
		}

		public virtual void Initialize(Vector2 position, Rectangle bounds)
		{
			Initialize(position, Rectangle.Empty, bounds);
		}

		public virtual void Initialize(Vector2 position, Rectangle collision, Rectangle bounds)
		{
			BaseX = (UInt16)(position.X);
			BaseY = (UInt16)(position.Y);
			Position = position;
			HomeSpot = position;
			//Collision = collision;
			Bounds = bounds;

			// Default one frame;
			Initialize(1);
		}

		public virtual void LoadContent(Rectangle theRectangle)
		{
			Rectangle[] theRectangles = new Rectangle[] { theRectangle };
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

		public void SetPosition(Vector2 position)
		{
			Position = position;
		}

		public void SetBounds(Rectangle bounds)
		{
			Bounds = bounds;
		}

		public void SetHomeSpot()
		{
			Position = HomeSpot;
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw()
		{
			Draw(FrameIndex);
		}
		protected void DrawRotate(Single rotate, Vector2 origin)
		{
			DrawRotate(FrameIndex, rotate, origin);
		}

		private void Draw(Byte theFrameIndex)
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, Position, rectangles[theFrameIndex], Color.White);
		}
		private void DrawRotate(Byte theFrameIndex, Single rotate, Vector2 origin)
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, Position, rectangles[theFrameIndex], Color.White, rotate, origin, 1.0f, SpriteEffects.None, 1.0f);
		}

		public void SetID(Byte index)
		{
			ID = index;
		}

		public Byte ID { get; protected set; }
		public Rectangle Bounds { get; protected set; }

		public Byte MaxFrames { get; protected set; }
		public Byte FrameIndex { get; protected set; }
		public Single FrameTimer { get; protected set; }

		public UInt16 BaseX { get; private set; }
		public UInt16 BaseY { get; private set; }
		public UInt16 SizeW { get; private set; }
		public UInt16 SizeH { get; private set; }
		public Vector2 Position { get; protected set; }
		public Vector2 Midpoint { get; private set; }
		public Vector2 HomeSpot { get; protected set; }
		//public Rectangle Collision { get; private set; }

	}
}
