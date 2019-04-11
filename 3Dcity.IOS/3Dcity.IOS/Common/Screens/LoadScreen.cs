using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class LoadScreen : BaseScreenPlay, IScreen
	{
		private Vector2 enemyTotalPosition;
		private String enemyTotalText;

		private Vector2 levelNamePosition;
		private Vector2 levelTextPosition;
		private String levelName;
		private String levelValu;
		private UInt16 loadDelay;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(235, 195, 385, 217);

			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;
			NextScreen = ScreenType.Ready;

			enemyTotalPosition = MyGame.Manager.TextManager.GetTextPosition(25, 10);
			levelNamePosition = MyGame.Manager.TextManager.GetTextPosition(19, 11);
			levelTextPosition = MyGame.Manager.TextManager.GetTextPosition(12, 11);
			loadDelay = MyGame.Manager.ConfigManager.GlobalConfigData.LoadDelay;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			// Adjust for testing.
			LevelType levelType = MyGame.Manager.ConfigManager.GlobalConfigData.LevelType;
			if (LevelType.Test == levelType)
			{
				// Load level configuration data.
				Byte levelNo = Constants.TEST_LEVEL_NUM - 1;
				MyGame.Manager.LevelManager.LoadLevelConfigData(levelType, levelNo);
				LevelConfigData levelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

				levelType = (LevelType)Enum.Parse(typeof(LevelType), levelConfigData.LevelType, true);
				levelNo = Convert.ToByte(levelConfigData.LevelNo);
				MyGame.Manager.LevelManager.SetLevelType(levelType);
				MyGame.Manager.LevelManager.SetLevelNo(levelNo);
				LevelType = MyGame.Manager.LevelManager.LevelType;
				LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			}
			else
			{
				LevelType = MyGame.Manager.LevelManager.LevelType;
				LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
				MyGame.Manager.LevelManager.LoadLevelConfigData(LevelType, LevelIndex);
			}

			LevelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			// Resets all relevant score level info,
			MyGame.Manager.ScoreManager.ResetLevel();

			// Bullets.
			MyGame.Manager.BulletManager.Reset(LevelConfigData.BulletMaxim, LevelConfigData.BulletFrame, LevelConfigData.BulletShoot);

			// Enemies.
			MyGame.Manager.EnemyManager.Reset(LevelType, LevelConfigData);

			// Explosions.
			MyGame.Manager.ExplosionManager.Reset(LevelConfigData.EnemySpawn, LevelConfigData.ExplodeDelay);

			// Sprites.
			MyGame.Manager.SpriteManager.Reset(LevelType, MyGame.Manager.LevelManager.LevelNo);

			levelName = MyGame.Manager.LevelManager.LevelName;
			levelValu = MyGame.Manager.LevelManager.LevelValu;
			base.LoadContent();

			// Must set this after base load content.
			enemyTotalText = EnemyTotal.ToString().PadLeft(3, '0');

			MyGame.Manager.RenderManager.SetGridDelay(LevelConfigData.GridDelay);
			MyGame.Manager.StateManager.SetKillSpace(Vector2.Zero);
			MyGame.Manager.EnemyManager.SpawnAllEnemies();

			MyGame.Manager.SpriteManager.LargeTarget.SetHomeSpot();
			MyGame.Manager.SoundManager.StopMusic();
			SongType songType = MyGame.Manager.SoundManager.GetGameMusic(LevelIndex);
			MyGame.Manager.SoundManager.PlayGameMusic(songType);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			UpdateTimer(gameTime);
			if (Timer >= loadDelay)
			{
				return (Int32)NextScreen;
			}

			Boolean select = MyGame.Manager.InputManager.Select();
			if (select)
			{
				return (Int32) NextScreen;
			}

			// Target.
			DetectTarget(gameTime);
			return (Int32) CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			DrawSheet01();

			// Sprite sheet #02.
			DrawSheet02();
			DrawBacked();
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			DrawText();
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawText(levelName, levelNamePosition);
			MyGame.Manager.TextManager.DrawText(levelValu, levelTextPosition);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, enemyTotalText, enemyTotalPosition, Color.White);
		}

	}
}
