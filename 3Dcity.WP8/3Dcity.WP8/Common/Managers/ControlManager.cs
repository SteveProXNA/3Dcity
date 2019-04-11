using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IControlManager 
	{
		void Initialize();
		void LoadContent();

		Single CheckJoyPadTiny(Vector2 position);
		Single CheckJoyPadHorz(Vector2 position);
		Single CheckJoyPadVert(Vector2 position);
		Boolean CheckJoyPadFire(Vector2 position);
		Boolean CheckGameState(Vector2 position);
		Boolean CheckGameSound(Vector2 position);
		Boolean CheckCenterPos(Vector2 position);
		Boolean CheckTitleMode(Vector2 position);
		Boolean CheckStatusBar(Vector2 position);
		Boolean CheckLeftsSide(Vector2 position);
		Boolean CheckRightSide(Vector2 position);
		Boolean CheckAcclerate(Vector2 position);

		Boolean CheckPosInRect(Vector2 position, Rectangle collision);
		Vector2 ClampPosInRect(Vector2 position, Rectangle bounds);
		Single CalcJoyPadPosn(Single space, Single coord, Single bound);
	}

	public class ControlManager : IControlManager 
	{
		private Rectangle joypadTinyCollision;
		private Rectangle joypadMoveCollision;
		private Rectangle joypadMoveBounds;
		private Rectangle joyButtonCollision;
		private Rectangle gameStateCollision;
		private Rectangle gameSoundCollision;
		private Rectangle centerPosCollision;
		private Rectangle titleModeCollision;
		private Rectangle statusBarCollision;
		private Rectangle leftsSideCollision;
		private Rectangle rightSideCollision;
		private Rectangle acclerateCollision;

		public void Initialize()
		{
		}

		public void LoadContent()
		{
			const Byte tinyWide = 200;
			joypadTinyCollision = new Rectangle(0, Constants.ScreenHigh - Constants.GameOffsetY - tinyWide, tinyWide, tinyWide);

			joypadMoveCollision = MyGame.Manager.IconManager.JoypadMove.Collision;
			joypadMoveBounds = MyGame.Manager.IconManager.JoypadMove.Bounds;
			joyButtonCollision = MyGame.Manager.IconManager.JoyButton.Collision;
			gameStateCollision = MyGame.Manager.IconManager.GameState.Collision;
			gameSoundCollision = MyGame.Manager.IconManager.GameSound.Collision;

			const UInt16 half = Constants.ScreenWide / 2;
			const Byte qtr = Constants.ScreenWide / 4;
			centerPosCollision = new Rectangle(qtr, Constants.BaseSize + Constants.GameOffsetY, half, half);

			Vector2 titlePosition = Constants.TitlePosition;
			titleModeCollision = new Rectangle((UInt16)titlePosition.X, (UInt16)titlePosition.Y, 224, 160);

			const UInt16 left = 250;
			const UInt16 wide = 300;
			const UInt16 high = 75;
			statusBarCollision = new Rectangle(left, Constants.GameOffsetY, wide, high);

			leftsSideCollision = GetMidSectionCollision(0, 180);
			rightSideCollision = GetMidSectionCollision(410, 180);

			//acclerateCollision = {X:600 Y:160 Width:200 Height:160}
			acclerateCollision = new Rectangle(Constants.ScreenWide - Constants.FIRE_OFFSET_X, Constants.FIRE_OFFSET_Y + Constants.GameOffsetY, Constants.FIRE_OFFSET_X, Constants.FIRE_OFFSET_Y);
		}

		public Single CheckJoyPadTiny(Vector2 position)
		{
			// Step 01. check collision.
			Boolean contains = CheckPosInRect(position, joypadTinyCollision);
			if (!contains)
			{
				return 0.0f;
			}

			// Step 02. clamp position.
			position = ClampPosInRect(position, joypadMoveBounds);

			// Step 03. calcd value.
			return CalcJoyPadPosn(joypadMoveBounds.Width, position.X, joypadMoveBounds.Left);
		}

		public Single CheckJoyPadHorz(Vector2 position)
		{
			// Step 01. check collision.
			Boolean contains = CheckPosInRect(position, joypadMoveCollision);
			if (!contains)
			{
				return 0.0f;
			}

			// Step 02. clamp position.
			position = ClampPosInRect(position, joypadMoveBounds);

			// Step 03. calcd value.
			return CalcJoyPadPosn(joypadMoveBounds.Width, position.X, joypadMoveBounds.Left);
		}

		public Single CheckJoyPadVert(Vector2 position)
		{
			// Step 01. check collision.
			Boolean contains = CheckPosInRect(position, joypadMoveCollision);
			if (!contains)
			{
				return 0.0f;
			}

			// Step 02. clamp position.
			position = ClampPosInRect(position, joypadMoveBounds);

			// Step 03. calcd value.
			return CalcJoyPadPosn(joypadMoveBounds.Height, position.Y, joypadMoveBounds.Top);
		}

		public Boolean CheckPosInRect(Vector2 position, Rectangle collision)
		{
			return position.X >= collision.Left && 
				position.X <= collision.Right && 
				position.Y >= collision.Top && 
				position.Y <= collision.Bottom;
		}

		public Vector2 ClampPosInRect(Vector2 position, Rectangle bounds)
		{
			if (position.X < bounds.Left)
			{
				position.X = bounds.Left;
			}
			if (position.X > bounds.Right)
			{
				position.X = bounds.Right;
			}
			if (position.Y < bounds.Top)
			{
				position.Y = bounds.Top;
			}
			if (position.Y > bounds.Bottom)
			{
				position.Y = bounds.Bottom;
			}

			return position;
		}

		public Single CalcJoyPadPosn(Single space, Single coord, Single bound)
		{
			Single value = 0.0f;

			Single halve = space / 2.0f;
			Single calcd = coord - bound - halve;

			if (Math.Abs(halve) > Single.Epsilon)
			{
				value = calcd / halve;
			}

			return value;
		}

		public Boolean CheckJoyPadFire(Vector2 position)
		{
			return CheckPosInRect(position, joyButtonCollision);
		}

		public Boolean CheckGameState(Vector2 position)
		{
			return CheckPosInRect(position, gameStateCollision);
		}

		public Boolean CheckGameSound(Vector2 position)
		{
			return CheckPosInRect(position, gameSoundCollision);
		}

		public Boolean CheckCenterPos(Vector2 position)
		{
			return CheckPosInRect(position, centerPosCollision);
		}

		public Boolean CheckTitleMode(Vector2 position)
		{
			return CheckPosInRect(position, titleModeCollision);
		}

		public Boolean CheckStatusBar(Vector2 position)
		{
			return CheckPosInRect(position, statusBarCollision);
		}

		public Boolean CheckLeftsSide(Vector2 position)
		{
			return CheckPosInRect(position, leftsSideCollision);
		}

		public Boolean CheckRightSide(Vector2 position)
		{
			return CheckPosInRect(position, rightSideCollision);
		}

		public Boolean CheckAcclerate(Vector2 position)
		{
			return CheckPosInRect(position, acclerateCollision);
		}

		private static Rectangle GetMidSectionCollision(UInt16 lft, UInt16 top)
		{
			const UInt16 wide = 390;
			const Byte high = 80;

			return new Rectangle(lft, top + Constants.GameOffsetX, wide, high);
		}
	}
}
