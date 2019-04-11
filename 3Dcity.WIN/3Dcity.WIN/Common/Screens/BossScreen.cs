using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class BossScreen : BaseScreenPlay, IScreen
	{
		private Boolean bossBig;

		public override void Initialize()
		{
			base.Initialize();

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.BulletManager.Reset(10, 100, 100);
			bossBig = MyGame.Manager.ConfigManager.GlobalConfigData.BossBig;

			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Target.
			DetectTarget(gameTime);

			// Bullets.
			DetectBullets();
			UpdateBullets(gameTime);

			// Icons.
			UpdateIcons();

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Red, 100.0f);

			if (bossBig)
			{
				MyGame.Manager.BossManager.DrawBigBoss();
			}
			else
			{
				MyGame.Manager.BossManager.DrawMedBoss();
			}

			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.TextManager.DrawProgress(ShipType.Boss);
			MyGame.Manager.BossManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
