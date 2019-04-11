using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class SmallTarget : BaseSprite
	{
		private Single pixel;
		private const Single RATIO = 0.15f;

		public virtual void Reset(Single thePixel)
		{
			pixel = thePixel;
		}

		public void Update(GameTime gameTime, Single horz, Single vert)
		{
			Vector2 position = Position;

			if (0 == horz && 0 == vert)
			{
				position.X = BaseX;
				position.Y = BaseY;

				Position = position;
				return;
			}


			// Tolerance
			if (Math.Abs(horz) < Constants.GeneralTolerance)
			{
				horz = 0.0f;
			}
			if (Math.Abs(vert) < Constants.GeneralTolerance)
			{
				vert = 0.0f;
			}
			if (0 == horz && 0 == vert)
			{
				return;
			}

			Single val1 = horz * pixel;
			Single val2 = val1 / 2.0f;
			//Single val3 = val2 + BaseX;
			Single val3 = val2 * RATIO;

			Single val5 = vert * pixel;
			Single val6 = val5 / 2.0f;
			//Single val7 = val6 + BaseY;
			Single val7 = val6 * RATIO;


			position.X += val3;
			position.Y += val7;

			if (position.X <= Bounds.Left)
			{
				position.X = Bounds.Left;
			}
			if (position.X >= Bounds.Right)
			{
				position.X = Bounds.Right;
			}
			if (position.Y <= Bounds.Top)
			{
				position.Y = Bounds.Top;
			}
			if (position.Y >= Bounds.Bottom)
			{
				position.Y = Bounds.Bottom;
			}

			Position = position;
		}

	}
}
