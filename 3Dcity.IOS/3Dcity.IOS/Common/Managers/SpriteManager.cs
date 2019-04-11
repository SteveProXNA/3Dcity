using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ISpriteManager 
	{
		void Initialize();
		void LoadContent();
		void Reset(LevelType levelType, Byte levelNo);

		void SetMovement(Boolean fast, Single horz, Single vert);
		void SetPosition(SpriteType type, Vector2 position);
		void Update(GameTime gameTime);

		void Draw();
		void DrawCursor();

		// Properties.
		SmallTarget SmallTarget { get; }
		LargeTarget LargeTarget { get; }
		Enemy KillEnemy { get; }
	}

	public class SpriteManager : ISpriteManager
	{
		private Vector2 smallPosition;
		private Vector2 largePosition;
		private Single targetHorz, targetVert;
		private Boolean targetFast;

		public void Initialize()
		{
			smallPosition = new Vector2(80, 360 + Constants.GameOffsetY);
			largePosition = new Vector2((Constants.ScreenWide - 64) / 2.0f, 250 + Constants.GameOffsetY);
			TheInit();

			KillEnemy = new Enemy();
			KillEnemy.SetDeath();
			KillEnemy.SetBlinkd(false);
			targetFast = false;
		}

		public void LoadContent()
		{
			LargeTarget.LoadContent(MyGame.Manager.ImageManager.TargetLargeRectangle);
			SmallTarget.LoadContent(MyGame.Manager.ImageManager.TargetSmallRectangle);
			KillEnemy.LoadContent(MyGame.Manager.ImageManager.EnemyRectangles);
			targetFast = false;
		}

		public void Reset(LevelType levelType, Byte levelNo)
		{
			Byte ratio = (Byte) (levelNo / 2.0f);
			Byte largeTargetPB = Constants.LARGE_TARGET_PB[(Byte) levelType];
			Byte smallTargetPB = Constants.SMALL_TARGET_PB[(Byte) levelType];

			LargeTarget.Reset(largeTargetPB + ratio);
			SmallTarget.Reset(smallTargetPB + ratio);

			//SmallTarget.SetPosition(smallPosition);
			//LargeTarget.SetPosition(largePosition);
		}

		public void SetMovement(Boolean fast, Single horz, Single vert)
		{
			targetFast = fast;
			targetHorz = horz;
			targetVert = vert;
		}

		public void SetPosition(SpriteType type, Vector2 position)
		{
			switch (type)
			{
				case SpriteType.LargeTarget:
					LargeTarget.SetPosition(position);
					break;

				case SpriteType.SmallTarget:
					SmallTarget.SetPosition(position);
					break;
			}
		}

		public void Update(GameTime gameTime)
		{
			LargeTarget.Update(gameTime, targetFast, targetHorz, targetVert);
			SmallTarget.Update(gameTime, targetHorz, targetVert);
		}

		public void Draw()
		{
			SmallTarget.Draw();
			LargeTarget.Draw();
		}

		public void DrawCursor()
		{
			SmallTarget.Draw();
		}

		private void TheInit()
		{
			//Vector2 stPos = new Vector2(80, 360 + Constants.GameOffsetY);
			Rectangle stBounds = new Rectangle(30, 310 + Constants.GameOffsetY, 100, 100);
			SmallTarget = new SmallTarget();
			SmallTarget.Initialize(smallPosition, stBounds);

			const Byte targetTop = 74;
			const Byte targetSize = 64;
			//Vector2 bgPos = new Vector2((Constants.ScreenWide - 64) / 2.0f, 250 + Constants.GameOffsetY);
			Rectangle bgBounds = new Rectangle(-2, targetTop + Constants.GameOffsetY, Constants.ScreenWide - targetSize + 2, Constants.ScreenHigh - (2 * Constants.GameOffsetY) - targetTop - targetSize + 2);
			LargeTarget = new LargeTarget();
			LargeTarget.Initialize(largePosition, Rectangle.Empty, bgBounds);
		}

		public SmallTarget SmallTarget { get; private set; }
		public LargeTarget LargeTarget { get; private set; }
		public Enemy KillEnemy { get; private set; }
	}
}
