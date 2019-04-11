using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Screens;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	// Testing saving events.
	public class EventScreenBAK : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			MyGame.Manager.ScoreManager.Reset();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);

			MyGame.Manager.EventManager.ClearEvents();

			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);

			MoveTarget(gameTime);


			//MyGame.Manager.EventManager.Update(gameTime);
			//MyGame.Manager.ExplosionManager.Update(gameTime);
			//MyGame.Manager.BulletManager.Update(gameTime);

			MyGame.Manager.EventManager.ProcessEvents(gameTime);
			MyGame.Manager.ScoreManager.Update(gameTime);
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();

			// Sprite sheet #02.
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

		private static void MoveTarget(GameTime gameTime)
		{
			Vector2 largeTargetPosBEF = MyGame.Manager.SpriteManager.LargeTarget.Position;
			Vector2 smallTargetPosBEF = MyGame.Manager.SpriteManager.SmallTarget.Position;
			MyGame.Manager.SpriteManager.Update(gameTime);
			Vector2 largeTargetPosAFT = MyGame.Manager.SpriteManager.LargeTarget.Position;
			Vector2 smallTargetPosAFT = MyGame.Manager.SpriteManager.SmallTarget.Position;

			if (largeTargetPosBEF != largeTargetPosAFT)
			{
				MyGame.Manager.EventManager.AddLargeTargetMoveEvent(largeTargetPosAFT);
			}
			if (smallTargetPosBEF != smallTargetPosAFT)
			{
				MyGame.Manager.EventManager.AddSmallTargetMoveEvent(smallTargetPosAFT);
			}
		}

	}
}
