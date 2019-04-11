using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class OverScreen : BaseScreen, IScreen
	{
		private Vector2 outputPos;
		private Vector2 enemysPos;
		private Vector2 targetPos;
		private Rectangle enemysRect;
		private Rectangle targetRect;
		private String[] outputText;

		public override void Initialize()
		{
			MyGame.Manager.DebugManager.Reset(CurrScreen);
			base.Initialize();
			LoadTextData();

			// TODO delete!
			outputText = new string[2] { "FALSE", "TRUE" };

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			outputPos = MyGame.Manager.TextManager.GetTextPosition(0, 4);
			GlobalConfigData data = MyGame.Manager.ConfigManager.GlobalConfigData;
			enemysPos = new Vector2(data.EnemysX, data.EnemysY);

			Single x = (120 - 64) / 2.0f + data.EnemysX;
			Single y = (120 - 64) / 2.0f + data.EnemysY;
			targetPos = new Vector2(x - 0, y - 0);

			enemysRect = MyGame.Manager.ImageManager.EnemyRectangles[7];
			targetRect = MyGame.Manager.ImageManager.TargetLargeRectangle;
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			Boolean gameState = MyGame.Manager.InputManager.GameState();
			if (gameState)
			{
				MyGame.Manager.SoundManager.StopMusic();
				//MyGame.Manager.SoundManager.PlayMusic(SongType.BossMusic);
				MyGame.Manager.SoundManager.PlayMusic(SongType.CoolMusic, false);
			}
			else
			{
				Boolean gameSound = MyGame.Manager.InputManager.GameSound();
				if (gameSound)
				{
					MyGame.Manager.SoundManager.StopMusic();
					MyGame.Manager.SoundManager.PlayMusic(SongType.ContMusic, false);
				}
				else
				{
					Boolean fire = MyGame.Manager.InputManager.Fire();
					if (fire)
					{
						MyGame.Manager.SoundManager.StopMusic();
						MyGame.Manager.SoundManager.PlayMusic(SongType.GameOver, false);
					}
					else
					{
						Single horz = MyGame.Manager.InputManager.Horizontal();
						Single vert = MyGame.Manager.InputManager.Vertical();
						if (0 == horz && 0 == vert)
						{
							return (Int32)CurrScreen;
						}
						else
						{
							MyGame.Manager.SoundManager.StopMusic();
							MyGame.Manager.SoundManager.PlayMusic(SongType.GameTitle, false);
						}
					}
				}
			}

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, enemysPos, enemysRect, Color.White);
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, targetPos, targetRect, Color.White);

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();

			// TODO delete
			//Engine.SpriteBatch.DrawString(Assets.EmulogicFont, outputText[Convert.ToByte(collision)], outputPos, Color.White);
		}

		private void Process()
		{
			enemysMid = GetMidPoint(enemysPos, 120);
			targetMid = GetMidPoint(targetPos, 64);
			float dist = Vector2.Distance(enemysMid, targetMid);
			float diSq = Vector2.DistanceSquared(enemysMid, targetMid);

			String msg = String.Format("({0},{1})  ({2},{3})  {4}  {5}", enemysMid.X, enemysMid.Y, targetMid.X, targetMid.Y, dist,
				diSq);
			MyGame.Manager.Logger.Info(msg);
		}

		public Vector2 GetMidPoint(Vector2 pos, Single size)
		{
			Single half = size / 2.0f;
			return new Vector2(pos.X + half, pos.Y + half);
		}

	}
}
