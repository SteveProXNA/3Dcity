using System;
using WindowsGame.Common.Screens;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class ResumeScreenBAK : BaseScreen, IScreen
	{
		private Vector2[] boundsPos;
		private Vector2 targetPos;
		private Vector2 enemysPos;
		private Vector2 statusPos;

		//private UInt16 delay;
		//private Byte goal;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
			UpdateGrid = false;

			//delay = 1000;
			//goal = 100;

			int x = 200;
			int y = 200;
			int s = 120;
			int u = 80;
			int v = 80;
			boundsPos = new Vector2[4];
			boundsPos[0] = new Vector2(x - u, y - v);
			boundsPos[1] = new Vector2(x + s, y - v);
			boundsPos[2] = new Vector2(x - u, y + s);
			boundsPos[3] = new Vector2(x + s, y + s);


			enemysPos = new Vector2(x, y);
			MyGame.Manager.EnemyManager.EnemyList[0].SetPosition(enemysPos);
			MyGame.Manager.EnemyManager.EnemyList[0].Reset();

			targetPos = new Vector2(MyGame.Manager.ConfigManager.GlobalConfigData.TargetX, MyGame.Manager.ConfigManager.GlobalConfigData.TargetY);
			MyGame.Manager.BulletManager.BulletList[0].SetPosition(targetPos);
			MyGame.Manager.BulletManager.BulletList[0].Reset(0);
			MyGame.Manager.SpriteManager.LargeTarget.SetPosition(targetPos);

			statusPos = new Vector2(15 * 20 - 2, Constants.ScreenHigh - 20 - Constants.GameOffsetY - 4);
		}

		public override void LoadContent()
		{
			MyGame.Manager.DebugManager.Reset();
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

#if DEBUG
			MyGame.Manager.Logger.Info(gameTime.ElapsedGameTime.TotalSeconds.ToString());
#endif

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			//MyGame.Manager.IconManager.DrawControls();

			for (int i = 0; i < 4; i++)
			{
				Engine.SpriteBatch.Draw(Assets.SpriteSheet01Texture, boundsPos[i], MyGame.Manager.ImageManager.JoyButtonRectangles[0], Color.White);
			}

			MyGame.Manager.EnemyManager.EnemyList[0].Draw();
			MyGame.Manager.BulletManager.BulletList[0].Draw();

			if (MyGame.Manager.ConfigManager.GlobalConfigData.EnemyIndex > 0)
			{
				MyGame.Manager.SpriteManager.LargeTarget.Draw();
			}
			//MyGame.Manager.LevelManager.Draw();

			//MyGame.Manager.RenderManager.DrawStatusOuter();
			//MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, 0);

			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Red, 70);
			//MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, statusPos, MyGame.Manager.ImageManager.ProgressRectangles[0], Color.White);
			//Engine.SpriteBatch.Draw(Assets.BlackBar, statusPos, Color.White);
			//Engine.SpriteBatch.Draw(Assets.YellowBar, statusPos, Color.White);

			//const byte color = 2;
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, statusPos, MyGame.Manager.ImageManager.ProgressRectangles[0], Color.White);
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(statusPos.X + 100.0f, statusPos.Y), MyGame.Manager.ImageManager.ProgressRectangles[0], Color.White);

			//Rectangle rect = MyGame.Manager.ImageManager.ProgressRectangles[color];
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(statusPos.X + 0.0f, statusPos.Y), rect, Color.White);
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(statusPos.X + 50.0f, statusPos.Y), rect, Color.White);
			//rect.Width = 50;
			//Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, new Vector2(statusPos.X + 100.0f, statusPos.Y), rect, Color.White);

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
