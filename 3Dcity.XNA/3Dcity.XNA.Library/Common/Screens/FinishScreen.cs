using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class FinishScreen : BaseScreenSelect, IScreen
	{
		private Vector2 completePosition;
		private Vector2 hitRatioPosition;
		private String hitRatioText;
		private UInt16 timer1, timer2, timer3;
		private UInt16 pauseDelay;
		private UInt16 startDelay;
		private UInt16 finishDelay;
		private UInt16 promptDelay;
		private Vector2 homeSpot;
		private Single deltaX, deltaY;
		private Boolean flag, flag3;
		private FinishState finishState;
		private const Single offset = 1.0f;
		private const Single multiplier = 0.6f;

		public override void Initialize()
		{
			base.Initialize();
			UpdateGrid = MyGame.Manager.ConfigManager.GlobalConfigData.UpdateGrid;

			pauseDelay = Constants.LONGER_PAUSE;
			startDelay = 2000;
			finishDelay = MyGame.Manager.ConfigManager.GlobalConfigData.FinishDelay;
			promptDelay = MyGame.Manager.ConfigManager.GlobalConfigData.FlashDelay;
			homeSpot = MyGame.Manager.SpriteManager.LargeTarget.HomeSpot;

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(250, 195, 370, 217);
			completePosition = MyGame.Manager.TextManager.GetTextPosition(13, 10);
			hitRatioPosition = MyGame.Manager.TextManager.GetTextPosition(23, 11);
			NextScreen = ScreenType.Load;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();
			base.LoadContent();

			MyGame.Manager.IconManager.UpdateFireIcon(0);
			MyGame.Manager.RenderManager.SetGridDelay(MyGame.Manager.LevelManager.LevelConfigData.GridDelay);

			Byte scoreKills = MyGame.Manager.ScoreManager.ScoreKills;
			Byte enemyTotal = MyGame.Manager.EnemyManager.EnemyTotal;
			Byte hitRatio = (Byte)(scoreKills / (Single)enemyTotal * 100);
			hitRatioText = hitRatio.ToString().PadLeft(3, '0');
			hitRatioText += Globalize.PERCENTAGE;

			deltaX = homeSpot.X - MyGame.Manager.SpriteManager.LargeTarget.Position.X;
			deltaY = homeSpot.Y - MyGame.Manager.SpriteManager.LargeTarget.Position.Y;

			timer1 = 0;
			timer2 = 0;
			timer3 = 0;
			Flag2 = true;
			flag = false;
			flag3 = false;
			finishState = FinishState.PauseSml;
		}

		private enum FinishState
		{
			PauseSml,
			MusicSfx,
			PauseMed,
			AutoMove,
			WaitingX,
			Complete
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32) CurrScreen;
			}

			// Update bullets to finish off..
			MyGame.Manager.BulletManager.Update(gameTime);

			timer3 += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (timer3 >= finishDelay)
			{
				CompleteScreen();
				return (Int32) NextScreen;
			}

			timer1 += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (FinishState.PauseSml == finishState && timer1 <= pauseDelay)
			{
				return (Int32) CurrScreen;
			}

			if (FinishState.PauseSml == finishState && timer1 > pauseDelay)
			{
				timer1 -= pauseDelay;
				finishState = FinishState.MusicSfx;
				MyGame.Manager.SoundManager.StopMusic();
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Finish);
				finishState = FinishState.PauseMed;
				return (Int32) CurrScreen;
			}

			if (FinishState.PauseMed == finishState)
			{
				flag3 = true;
				timer2 += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
				if (timer2 > promptDelay)
				{
					Flag2 = !Flag2;
					timer2 -= promptDelay;
				}
			}
			if (FinishState.PauseMed == finishState && timer1 <= startDelay)
			{
				return (Int32) CurrScreen;
			}
			if (FinishState.PauseMed == finishState && timer1 > startDelay)
			{
				timer1 -= startDelay;
				finishState = FinishState.AutoMove;
				return (Int32) CurrScreen;
			}

			// After initial slight pause and 2s wait check if player wants to skip auto move...
			UpdateFlag1(gameTime);
			if (Selected)
			{
				CompleteScreen();
				return (Int32)NextScreen;
			}

			Boolean fire = MyGame.Manager.InputManager.Select();
			Boolean left = MyGame.Manager.InputManager.LeftsSide();
			Boolean rght = MyGame.Manager.InputManager.RightSide();
			if (fire || left || rght)
			{
				Flag1 = true;
			}
			if (Flag1)
			{
				MyGame.Manager.SoundManager.StopSoundEffect(SoundEffectType.Finish);
				PlaySoundEffect();
				return (Int32)CurrScreen;
			}

			if (FinishState.AutoMove == finishState)
			{
				if (!flag)
				{
					Single deltaT = (Single)gameTime.ElapsedGameTime.TotalSeconds;
					Vector2 targetPosition = MyGame.Manager.SpriteManager.LargeTarget.Position;

					if (Math.Abs(homeSpot.X - targetPosition.X) > offset)
					{
						targetPosition.X += deltaX * deltaT * multiplier;
					}
					if (Math.Abs(homeSpot.Y - targetPosition.Y) > offset)
					{
						targetPosition.Y += deltaY * deltaT * multiplier;
					}

					MyGame.Manager.SpriteManager.LargeTarget.SetPosition(targetPosition);
					if (Math.Abs(homeSpot.X - targetPosition.X) <= offset && Math.Abs(homeSpot.Y - targetPosition.Y) <= offset)
					{
						finishState = FinishState.WaitingX;
						UpdateGrid = false;
						flag = true;
					}

					return (Int32) CurrScreen;
				}
			}

			if (FinishState.WaitingX == finishState && timer1 <= startDelay)
			{
				return (Int32) CurrScreen;
			}
			if (FinishState.WaitingX == finishState && timer1 > startDelay)
			{
				timer1 -= startDelay;
				MyGame.Manager.SoundManager.StopMusic();
				MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Cheat);
				finishState = FinishState.Complete;
				return (Int32) CurrScreen;
			}

			return (Int32) CurrScreen;
		}

		private void CompleteScreen()
		{
			Byte levelIndex = (Byte)(MyGame.Manager.LevelManager.LevelIndex + 1);
			if (levelIndex >= MyGame.Manager.LevelManager.MaximLevel)
			{
				MyGame.Manager.ScoreManager.ResetMisses();
				NextScreen = ScreenType.Beat;
			}
			else
			{
				MyGame.Manager.LevelManager.SetLevelIndex(levelIndex);
			}
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			DrawSheet01();

			// Sprite sheet #02.
			MyGame.Manager.EnemyManager.Draw();
			MyGame.Manager.RenderManager.DrawStatusOuter();
			MyGame.Manager.RenderManager.DrawStatusInner(StatusType.Yellow, MyGame.Manager.EnemyManager.EnemyPercentage);

			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.BulletManager.Draw();

			if (flag3)
			{
				DrawBacked();
			}
			MyGame.Manager.SpriteManager.Draw();

			// Text data last!
			if (flag3)
			{
				MyGame.Manager.TextManager.Draw(TextDataList);
			}
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawProgress();
			MyGame.Manager.EnemyManager.DrawProgress();
			MyGame.Manager.LevelManager.DrawTextData();

			if (flag3)
			{
				MyGame.Manager.TextManager.DrawText(hitRatioText, hitRatioPosition);
				if (Flag1 || Flag2)
				{
					MyGame.Manager.TextManager.DrawText(Globalize.FINISH_TEXT1, completePosition);
				}
			}

		}

	}
}
