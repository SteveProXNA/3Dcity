using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Screens
{
	public class BaseScreenSelect : BaseScreen
	{
		protected Vector2[] CursorPositions { get; set; }
		//protected Vector2[] BackedPositions { get; set; }
		protected Vector2 Killspace { get; set; }
		protected Boolean IsMoving { get; set; }
		protected Byte SelectType { get; set; }

		protected Boolean Selected { get; private set; }
		protected Byte MoveIndex { get; private set; }
		protected Single MoveValue { get; private set; }
		protected Boolean Flag1 { get; set; }
		protected Boolean Flag2 { get; set; }
		protected Boolean Lefts { get; set; }
		protected Boolean Right { get; set; }

		private UInt16 SelectDelay;
		private Vector2 SpritePosition;
		
		private Byte IconIndex;
		private Vector2 spritePosition;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			SelectDelay = MyGame.Manager.ConfigManager.GlobalConfigData.SelectDelay;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			IconIndex = 0;
			MoveIndex = 1;
			MoveValue = 0.0f;

			spritePosition = MyGame.Manager.SpriteManager.SmallTarget.Position;
			spritePosition.X = Constants.CURSOR_OFFSET_X[MoveIndex];
			SpritePosition = spritePosition;

			Selected = false;
			IsMoving = false;
			Flag1 = Flag2 = false;
			Lefts = Right = false;
		}

		protected void UpdateFlag1(GameTime gameTime)
		{
			if (!Flag1)
			{
				return;
			}

			UpdateTimer(gameTime);
			if (Timer > SelectDelay * 2)
			{
				Flag1 = false;
				IconIndex = Convert.ToByte(Flag1);
				MyGame.Manager.IconManager.UpdateFireIcon(IconIndex);
				Selected = true;
				return;
			}

			IconIndex = Convert.ToByte(Flag1);
			MyGame.Manager.IconManager.UpdateFireIcon(IconIndex);
		}

		protected void UpdateFlag2(GameTime gameTime)
		{
			if (!Flag2)
			{
				return;
			}

			IsMoving = true;
			UpdateTimer(gameTime);
			if (Timer <= SelectDelay)
			{
				return;
			}

			MoveIndex = 1;

			spritePosition = SpritePosition;
			spritePosition.X = Constants.CURSOR_OFFSET_X[MoveIndex];
			SpritePosition = spritePosition;
			MyGame.Manager.SpriteManager.SmallTarget.SetPosition(SpritePosition);

			Timer = 0;
			Flag2 = false;
		}

		protected static void PlaySoundEffect()
		{
			MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Right);
		}

		protected void DetectLefts()
		{
			// Check fire first.
			Boolean lefts = MyGame.Manager.InputManager.LeftsSide();
			if (lefts)
			{
				Lefts = true;
				Flag1 = true;
			}
		}

		protected void DetectRight()
		{
			// Check fire first.
			Boolean right = MyGame.Manager.InputManager.RightSide();
			if (right)
			{
				Right = true;
				Flag1 = true;
			}
		}

		protected void DetectSelect()
		{
			// Check fire first.
			Boolean select = MyGame.Manager.InputManager.Select();
			if (select)
			{
				Flag1 = true;
			}
		}

		protected void DetectMove()
		{
			// Check move second.
			MoveValue = MyGame.Manager.InputManager.LittleHorz();
			if (0 == MoveValue)
			{
				return;
			}

			if (MoveValue < 0)
			{
				MoveIndex = 0;
			}
			if (MoveValue > 0)
			{
				MoveIndex = 2;
			}

			spritePosition = SpritePosition;
			spritePosition.X = Constants.CURSOR_OFFSET_X[MoveIndex];
			SpritePosition = spritePosition;
			MyGame.Manager.SpriteManager.SmallTarget.SetPosition(SpritePosition);

			Flag2 = true;
		}

		protected static void DrawSheet01()
		{
			MyGame.Manager.IconManager.DrawControls();
		}

		protected static void DrawSheet02()
		{
			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.DrawCursor();
		}

		protected void DrawBacked()
		{
			MyGame.Manager.RenderManager.DrawBorderPosition(BackedPositions);
		}

		protected void DrawText()
		{
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}