using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class LargeTarget : BaseSprite
	{
		private Single pixel;
		private const Single RATIO = 1.05f;
		private const Single MAXIM = 2.5f;

		private Single accX;
		private Single accY;

		public LargeTarget()
		{
			accX = 1.0f;
			accY = 1.0f;
		}

		public virtual void Reset(Single thePixel)
		{
			pixel = thePixel;
		}

		public void Update(GameTime gameTime, Boolean fast, Single horz, Single vert)
		{
			Vector2 position = Position;
			Single mult = 1.0f;
			if (fast)
			{
				mult *= 2.0f;
			}

			// Tolerance
			if (Math.Abs(horz) < Constants.GeneralTolerance)
			{
				horz = 0.0f;
			}
			if (Math.Abs(vert) < Constants.GeneralTolerance)
			{
				vert= 0.0f;
			}
			if (0 == horz && 0 == vert)
			{
				return;
			}

			if (Math.Abs(horz) < Single.Epsilon && Math.Abs(vert) < Single.Epsilon)
			{
				accX = 1.0f;
				accY = 1.0f;
			}
			else if (!(Math.Abs(horz) < Single.Epsilon) && Math.Abs(vert) < Single.Epsilon)
			{
				accX *= RATIO;
				accY = 1.0f;
			}
			else if (Math.Abs(horz) < Single.Epsilon && (!(Math.Abs(vert) < Single.Epsilon)))
			{
				accX = 1.0f;
				accY *= RATIO;
			}
			else
			{
				accX *= RATIO;
				accY *= RATIO;
			}

			if (accX > MAXIM)
			{
				accX = MAXIM;
			}
			if (accY > MAXIM)
			{
				accY = MAXIM;
			}

			Single delta = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			Single moveX = horz * delta * pixel * accX * mult;
			Single moveY = vert * delta * pixel * accY * mult;

			position.X += moveX;
			position.Y += moveY;

			if (position.X <= Bounds.Left)
			{
				position.X = Bounds.Left;
				accX = 1.0f;
			}
			if (position.X >= Bounds.Right)
			{
				position.X = Bounds.Right;
				accX = 1.0f;
			}

			if (position.Y <= Bounds.Top)
			{
				position.Y = Bounds.Top;
				accY = 1.0f;
			}
			if (position.Y >= Bounds.Bottom)
			{
				position.Y = Bounds.Bottom;
				accY = 1.0f;
			}

			Position = position;
		}

	}
}
