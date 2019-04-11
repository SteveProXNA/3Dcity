using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Sprites
{
	public class Bullet : BaseSprite
	{
		public void Reset(UInt16 frameDelay)
		{
			// Constant throughout level.
			FrameDelay = frameDelay;
			IsFiring = false;
			FrameIndex = 0;
			FrameTimer = 0;
		}

		public void Shoot(Vector2 position)
		{
			IsFiring = true;
			FrameTimer = 0;
			FrameIndex = 0;
			Position = position;
		}

		public override void Update(GameTime gameTime)
		{
			if (!IsFiring)
			{
				return;
			}

			FrameTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (FrameTimer >= FrameDelay)
			{
				FrameTimer -= FrameDelay;
				FrameIndex++;

				// Check for collision after final frame complete!
				if (FrameIndex >= MaxFrames)
				{
					IsFiring = false;
				}
			}
		}

		public Boolean IsFiring { get; private set; }
		public UInt16 FrameDelay { get; private set; }

	}
}
