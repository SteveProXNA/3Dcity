using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface IBossManager 
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);
		void DrawMedBoss();
		void DrawBigBoss();
		void DrawProgress();
	}

	public class BossManager : IBossManager
	{
		private Vector2 progressPosition;
		private String bossProgressText;

		private Vector2 bossMedPosition;
		private Vector2 bossBigPosition;

		public void Initialize()
		{
			progressPosition = MyGame.Manager.TextManager.GetTextPosition(25, 23);
			bossProgressText = "[100%]";

			bossMedPosition = new Vector2(480, 120);
			bossBigPosition = new Vector2(200, 80);
		}

		public void LoadContent()
		{
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw()
		{
		}

		public void DrawMedBoss()
		{
			DrawMedBoss(0);
		}
		public void DrawMedBoss(Byte index)
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, bossMedPosition, MyGame.Manager.ImageManager.BossMedRectangles[index], Color.White);
		}
		public void DrawBigBoss()
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, bossBigPosition, MyGame.Manager.ImageManager.BossBigRectangle, Color.White);
		}
		public void DrawProgress()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, bossProgressText, progressPosition, Color.White);
		}

	}
}
