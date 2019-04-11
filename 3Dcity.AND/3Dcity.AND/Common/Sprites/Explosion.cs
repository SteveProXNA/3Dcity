using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Sprites
{
	public class Explosion : BaseSprite
	{
		public void Reset(UInt16 frameDelay)
		{
			// Constant throughout level.
			FrameDelay = frameDelay;
			IsExploding = false;
			FrameIndex = 0;
			FrameTimer = 0;
			EnemyID = Constants.INVALID_INDEX;
		}

		public void Explode(Byte enemyID)
		{
			IsExploding = true;
			FrameIndex = 0;
			FrameTimer = 0;
			EnemyID = (SByte)enemyID;
		}

		public override void Update(GameTime gameTime)
		{
			if (!IsExploding)
			{
				return;
			}

			FrameTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (FrameTimer >= FrameDelay)
			{
				FrameTimer -= FrameDelay;
				FrameIndex++;

				if (FrameIndex >= MaxFrames)
				{
					IsExploding = false;
				}
			}
		}

		public Boolean IsExploding { get; private set; }
		private UInt16 FrameDelay { get; set; }
		public SByte EnemyID { get; private set; }
	}
}
