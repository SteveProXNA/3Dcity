using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ReadyScreen : BaseScreenPlay, IScreen
	{
		private UInt16 readyDelay;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(285, 213, 330, 217);

			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;
			readyDelay = MyGame.Manager.ConfigManager.GlobalConfigData.ReadyDelay;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			base.LoadContent();

			NextScreen = CurrScreen;
			MyGame.Manager.RenderManager.SetGridDelay(LevelConfigData.GridDelay);
			MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Ready);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			UpdateTimer(gameTime);
			if (Timer >= readyDelay)
			{
				NextScreen = ScreenType.Play;
				return (Int32) NextScreen;
			}

			// Begin common code...
			CheckLevelComplete = false;

			// Target.
			DetectTarget(gameTime);

			// Bullets.
			DetectBullets();
			UpdateBullets(gameTime);
			VerifyBullets(false);

			// Explosions.
			UpdateExplosions(gameTime);
			VerifyExplosions();

			// Enemies.
			UpdateEnemies(gameTime);
			VerifyEnemies();
			if (NextScreen != CurrScreen)
			{
				return (Int32) NextScreen;
			}

			// Icons.
			UpdateIcons();

			// Score.
			UpdateScore(gameTime);

			// Summary.
			UpdateLevel();
			if (NextScreen != CurrScreen)
			{
				return (Int32) NextScreen;
			}

			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02();
			DrawBacked();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			DrawTextCommon();
			MyGame.Manager.ScoreManager.DrawBlink();
			MyGame.Manager.TextManager.Draw(TextDataList);
		}

	}
}
