using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class BeatScreen : BaseScreenSelect, IScreen
	{
		private IList<Rectangle> enemyBounds;
		private Vector2[] positions;
		private UInt16[] delays;
		private UInt16 timer;
		private UInt16 delay;
		private UInt16 beatDelay;
		private Boolean[] flags;
		private Byte maxExplode;
		private Byte wide;
		private Byte high;

		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();

			BackedPositions = MyGame.Manager.StateManager.SetBackedPositions(235, 195, 385, 217);
			beatDelay = MyGame.Manager.ConfigManager.GlobalConfigData.BeatDelay;

			// Seems good explosion delay 300ms
			timer = 0;
			delay = 100;

			maxExplode = Constants.MAX_EXPLODE_SPAWN;
			enemyBounds = MyGame.Manager.EnemyManager.EnemyBounds;

			positions = new Vector2[maxExplode];
			delays = new UInt16[maxExplode];
			flags = new Boolean[maxExplode];

			wide = (Byte)enemyBounds[0].Width;
			high = (Byte)enemyBounds[0].Width;
			NextScreen = ScreenType.Title;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override void LoadContent()
		{
			MyGame.Manager.SpriteManager.LargeTarget.SetHomeSpot();
			MyGame.Manager.SpriteManager.SmallTarget.SetHomeSpot();
			MyGame.Manager.ExplosionManager.Reset(maxExplode, delay);
			base.LoadContent();

			for (Byte index = 0; index < maxExplode; index++)
			{
				positions[index] = GetRandomPosition(index);
				flags[index] = false;

				while (true)
				{
					Byte value = (Byte)MyGame.Manager.RandomManager.Next(maxExplode);
					if (0 == delays[value])
					{
						delays[value] = index;
						break;
					}
				}
			}

			for (Byte index = 0; index < maxExplode; index++)
			{
				delays[index] = (UInt16) (delays[index] * delay + delay);
			}

			flags[1] = true;
			flags[3] = true;
			flags[6] = true;

			MyGame.Manager.SoundManager.StopMusic();
			MyGame.Manager.SoundManager.PlayMusic(SongType.GameTitle);
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}


			timer += (UInt16) gameTime.ElapsedGameTime.Milliseconds;
			if (timer > beatDelay)
			{
				// Reset back to start.
				Complete();
				return (Int32) NextScreen;
			}

			for (Byte index = 0; index < maxExplode; index++)
			{
				if (flags[index])
				{
					continue;
				}

				if (timer > delays[index])
				{
					flags[index] = true;
					Byte enemyID = index;
					ExplodeType explodeType = GetRandomExplodeType();

					MyGame.Manager.ExplosionManager.LoadContent(enemyID, explodeType);
					MyGame.Manager.ExplosionManager.Explode(enemyID, explodeType, positions[index]);
				}
			}

			MyGame.Manager.ExplosionManager.Update(gameTime);
			if (0 != MyGame.Manager.ExplosionManager.ExplosionTest.Count)
			{
				IList<Byte> explosionTest = MyGame.Manager.ExplosionManager.ExplosionTest;
				for (Byte testIndex = 0; testIndex < explosionTest.Count; testIndex++)
				{
					Byte enemyID = explosionTest[testIndex];
					ExplodeType explodeType = GetRandomExplodeType();

					MyGame.Manager.ExplosionManager.LoadContent(enemyID, explodeType);
					positions[testIndex] = GetRandomPosition(enemyID);
					MyGame.Manager.ExplosionManager.Explode(enemyID, explodeType, positions[testIndex]);
				}
			}


			UpdateFlag1(gameTime);
			if (Selected)
			{
				// Reset back to start.
				//MyGame.Manager.LevelManager.SetLevelIndex(0);
				Complete();
				return (Int32) NextScreen;
			}
			if (Flag1)
			{
				return (Int32) CurrScreen;
			}

			DetectSelect();
			if (Flag1)
			{
				MyGame.Manager.SoundManager.StopMusic();
				PlaySoundEffect();
				return (Int32) CurrScreen;
			}

			#region Events
			//MyGame.Manager.EventManager.ClearEvents();
			
			//// Move target unconditionally.
			//Vector2 pos1 = MyGame.Manager.SpriteManager.LargeTarget.Position;
			//Vector2 pos2 = MyGame.Manager.SpriteManager.SmallTarget.Position;

			//Single horz = MyGame.Manager.InputManager.Horizontal();
			//Single vert = MyGame.Manager.InputManager.Vertical();
			//MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			//MyGame.Manager.SpriteManager.Update(gameTime);

			//Vector2 pos3 = MyGame.Manager.SpriteManager.LargeTarget.Position;
			//Vector2 pos4 = MyGame.Manager.SpriteManager.SmallTarget.Position;

			//if (pos1 != pos3)
			//{
			//    MyGame.Manager.EventManager.AddLargeTargetMoveEvent(pos3);
			//}
			//if (pos2 != pos4)
			//{
			//    MyGame.Manager.EventManager.AddSmallTargetMoveEvent(pos4);
			//}

			////// Events.
			//MyGame.Manager.EventManager.ProcessEvents(gameTime);
			#endregion

			return (Int32)CurrScreen;
		}

		private void Complete()
		{
			MyGame.Manager.SoundManager.StopMusic();
			MyGame.Manager.LevelManager.SetLevelIndex(0);
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.Draw();
			MyGame.Manager.ExplosionManager.Draw();
			MyGame.Manager.SpriteManager.Draw();
			DrawBacked();

			// Text data last!
			MyGame.Manager.TextManager.Draw(TextDataList);
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.ScoreManager.Draw();
		}

		private Vector2 GetRandomPosition(Byte index)
		{
			const Byte off = 100;
			Byte spot = high;
			if (2 == index)
			{
				spot /= 2;
			}
			Byte ex = (Byte) MyGame.Manager.RandomManager.Next(wide);
			Byte ey = (Byte) MyGame.Manager.RandomManager.Next(spot);

			Rectangle rect = enemyBounds[index];
			Int32 rx = rect.X;
			Int32 ry = rect.Y;

			if (0 == index)
			{
				rx += off;
			}
			if (4 == index)
			{
				rx -= off;
			}

			Vector2 position = positions[index];
			position.X = rx + ex;
			position.Y = ry + ey;

			
			return position;
		}
		private static ExplodeType GetRandomExplodeType()
		{
			Byte type = (Byte) MyGame.Manager.RandomManager.Next(2);
			return (ExplodeType) type;
		}

	}
}
