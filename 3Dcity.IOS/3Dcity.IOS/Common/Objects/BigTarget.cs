using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Common.Objects
{
	public class BigTarget : BaseSprite
	{
		private const Single PIXEL = 200.0f;		// TODO tweak this constant
		private const Single RATIO = 1.05f;			// TODO tweak this acceleration
		private Single accX;
		private Single accY;

		public BigTarget() : base()
		{
			accX = 1.0f;
			accY = 1.0f;
		}

		public override void Update(GameTime gameTime, Single horz, Single vert)
		{
			Vector2 position = Position;
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

			const Single max = 2.5f;					// TODO tweak maxiumum acceleration
			if (accX > max)
			{
				accX = max;
			}
			if (accY > max)
			{
				accY = max;
			}

			Single delta = (Single)gameTime.ElapsedGameTime.TotalSeconds;
			Single moveX = horz * delta * PIXEL * accX;
			Single moveY = vert * delta * PIXEL * accY;

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
