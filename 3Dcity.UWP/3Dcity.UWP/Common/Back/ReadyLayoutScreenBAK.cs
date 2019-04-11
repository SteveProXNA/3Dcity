using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Screens;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class ReadyLayoutScreenBAK : BaseScreen, IScreen
	{
		private Vector2[] positions;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			positions = new Vector2[4];
			positions[0] = MyGame.Manager.TextManager.GetTextPosition(39, 11);
			positions[1] = MyGame.Manager.TextManager.GetTextPosition(39, 10);
			positions[2] = MyGame.Manager.TextManager.GetTextPosition(39, 9);
			positions[3] = MyGame.Manager.TextManager.GetTextPosition(39, 8);

			MyGame.Manager.ScoreManager.Reset();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);

			// Events
			//MyGame.Manager.EventManager.ClearEvents();

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			// Events.
			//MyGame.Manager.EventManager.ProcessEvents(gameTime);

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

			//MyGame.Manager.LevelManager.Draw();		// not needed
			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, "X", positions[0], Color.White);
			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, "X", positions[1], Color.White);
			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, "X", positions[2], Color.White);
			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, "X", positions[3], Color.White);

		}

	}
}
