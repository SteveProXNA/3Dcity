using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class TestScreen : BaseScreenPlay, IScreen
	{
		private Rectangle rect;
		private Vector2 enemy;
		private Vector2 large;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			PrevScreen = ScreenType.Exit;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			rect = MyGame.Manager.ImageManager.EnemyRectangles[7];
			enemy = new Vector2(200, 150);
			large = new Vector2(100, 150);
			MyGame.Manager.SpriteManager.LargeTarget.SetPosition(large);

			base.LoadContent();
			MyGame.Manager.RenderManager.SetGridDelay(LevelConfigData.GridDelay);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			Boolean back = MyGame.Manager.InputManager.Back();
			if (back)
			{
				return (Int32)PrevScreen;
			}

			// Target.
			DetectTarget(gameTime);

			// Bullets.
			DetectBullets();
			UpdateBullets(gameTime);

			// Explosions.
			UpdateExplosions(gameTime);
			VerifyExplosions();

			// Icons.
			UpdateIcons();

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			DrawSheet02();
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemy, rect, Color.White);
			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
