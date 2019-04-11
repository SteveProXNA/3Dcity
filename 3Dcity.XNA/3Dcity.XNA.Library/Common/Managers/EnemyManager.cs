using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface IEnemyManager 
	{
		void Initialize();
		void LoadContent();
		void Reset(LevelType theLevelType, LevelConfigData theLevelConfigData);
		void Clear();
		void SpawnAllEnemies();
		void SpawnOneEnemy(Byte index);
		Boolean CheckThisEnemy(Byte index);
		Boolean CheckEnemiesNone();
		void Update(GameTime gameTime);
		void Draw();
		void DrawProgress();

		IList<Enemy> EnemyTest { get; }
		IDictionary<Byte, Enemy> EnemyDict { get; }
		IList<Rectangle> EnemyBounds { get; }

		Byte EnemyTotal { get; }
		Single EnemyPercentage { get; }
		Single EnemyController { get; }
	}

	public class EnemyManager : IEnemyManager
	{
		private LevelType levelType;
		private LevelConfigData levelConfigData;
		private IDictionary<Byte, UInt16> enemyDelays;
		private IList<Boolean> enemyRotates;
		private IList<MoveType> enemyMoves;
		private Byte maxEnemySpawn;
		private Vector2[] progressPosition;

		public void Initialize()
		{
			maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;

			EnemyList = new List<Enemy>(maxEnemySpawn);
			EnemyTest = new List<Enemy>(maxEnemySpawn);
			EnemyDict = new Dictionary<Byte, Enemy>(maxEnemySpawn);
			EnemyBounds = GetEnemyBounds();

			EnemyOffsetX = new UInt16[maxEnemySpawn];
			EnemyOffsetY = new UInt16[maxEnemySpawn];

			Boolean enemyBlink = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyBlink;
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyOffsetX[index] = (UInt16)(Constants.ENEMY_OFFSET_X[index] + Constants.GameOffsetX);
				EnemyOffsetY[index] = (UInt16)(Constants.ENEMY_OFFSET_Y[index] + Constants.GameOffsetY);

				Enemy enemy = new Enemy();
				enemy.Initialize(Constants.MAX_ENEMYS_FRAME);
				enemy.SetID(index);
				enemy.SetSlotID();
				enemy.SetBlinkd(enemyBlink);
				EnemyList.Add(enemy);
			}

			enemyDelays = new Dictionary<Byte, UInt16>(Constants.MAX_ENEMYS_TOTAL);
			enemyRotates = new List<Boolean>(Constants.MAX_ENEMYS_TOTAL);
			enemyMoves = new List<MoveType>(Constants.MAX_ENEMYS_TOTAL);

			progressPosition = new Vector2[3];
			progressPosition[0] = MyGame.Manager.TextManager.GetTextPosition(25, 23);
			progressPosition[1] = MyGame.Manager.TextManager.GetTextPosition(28, 23);
			progressPosition[2] = MyGame.Manager.TextManager.GetTextPosition(29, 23);
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.LoadContent(MyGame.Manager.ImageManager.EnemyRectangles);
			}
		}

		public void Reset(LevelType theLevelType, LevelConfigData theLevelConfigData)
		{
			levelType = theLevelType;
			levelConfigData = theLevelConfigData;

			maxEnemySpawn = levelConfigData.EnemySpawn;
			if (maxEnemySpawn <= 0)
			{
				maxEnemySpawn = 1;
			}
			if (maxEnemySpawn > Constants.MAX_ENEMYS_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
			}

			EnemyTotal = levelConfigData.EnemyTotal;
			if (EnemyTotal <= 0)
			{
				EnemyTotal = 1;
			}

			// Ensure max total takes precedence over spawn.
			if (maxEnemySpawn > EnemyTotal)
			{
				maxEnemySpawn = EnemyTotal;
			}

			// Reset all enemies but not the list as will clear.
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Reset();
			}

			// Important: collections MUST be cleared on Reset() to ensure stability!
			Clear();
			enemyDelays.Clear();
			enemyRotates.Clear();
			enemyMoves.Clear();

			EnemyStart = 0;
			EnemySpawn = 0;
			EnemyStartText = EnemyStart.ToString().PadLeft(3, '0');
			EnemyTotalText = EnemyTotal.ToString().PadLeft(3, '0');
			EnemyPercentage = 0.0f;
		}

		public void Clear()
		{
			EnemyTest.Clear();
			EnemyDict.Clear();
		}

		public void SpawnAllEnemies()
		{
			// Calculate frame delays for all enemy ships.
			MyGame.Manager.DelayManager.ResetEnemyDelays(enemyDelays, levelConfigData);
			MyGame.Manager.DelayManager.CalcdEnemyDelays(enemyDelays, levelConfigData);

			// Reset rotates and moves.
			ResetEnemyRotates();
			ResetEnemyMoves();

			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				SpawnOneEnemy(index);
			}

			const Byte first = 0;
			UInt16 startDelay = MyGame.Manager.DelayManager.GetStartDelay(first, levelConfigData.EnemyStartDelay, levelConfigData.EnemyStartDelta);
			UInt16 totalDelay = MyGame.Manager.DelayManager.GetTotalDelay(EnemyList[first].FrameDelay);
			UInt16 parseDelay = (UInt16) (totalDelay / maxEnemySpawn);

			// Displace evenly spawned ships from start delay...
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Start((UInt16)(index * parseDelay + startDelay));
			}
		}

		public void SpawnOneEnemy(Byte index)
		{
			UInt16 frameDelay = enemyDelays[EnemySpawn];
			Boolean enemyRotate = enemyRotates[EnemySpawn];
			MoveType moveType = enemyMoves[EnemySpawn];
			Byte theEnemySpeed = levelConfigData.EnemyVelocity;

			Byte slotID;
			while (true)
			{
				slotID = (Byte)MyGame.Manager.RandomManager.Next(Constants.MAX_ENEMYS_SPAWN);
				if (!EnemyDict.ContainsKey(slotID))
				{
					break;
				}
			}

			// TODO delete
			//slotID = 0;		// hard code slotID to test.
			//MyGame.Manager.Logger.Info((slotID+1).ToString());

			// Retrieve the enemy from list.
			Enemy enemy = EnemyList[index];

			Vector2 position = enemy.Position;
			Byte randomX = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_X);
			Byte randomY = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_Y);
			UInt16 offsetX = EnemyOffsetX[slotID];
			UInt16 offsetY = EnemyOffsetY[slotID];

			// This fits within [160,200] = [40,80] = [32,72] => [32 = 160-120-(2*4)]
			// [32,72] => [32 = 160-120-(2*4), 72 = 200-120-(2*4)]
			position.X = randomX + offsetX + Constants.BorderSize;
			position.Y = randomY + offsetY + Constants.BorderSize;

			// Implement bounds checking...
			Rectangle bounds = EnemyBounds[slotID];
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

			enemy.Spawn(slotID, frameDelay, position, bounds, levelType, enemyRotate, moveType, theEnemySpeed);
			EnemyDict.Add(slotID, enemy);

			EnemySpawn++;
		}

		public Boolean CheckThisEnemy(Byte index)
		{
			Boolean check = false;

			Enemy enemy = EnemyList[index];
			if (EnemyType.Idle == enemy.EnemyType)
			{
				return false;
			}

			SByte testID = enemy.SlotID;
			if (testID >= 0 && testID < Constants.MAX_ENEMYS_SPAWN)
			{
				Byte slotID = (Byte) testID;
				if (EnemyDict.ContainsKey(slotID))
				{
					EnemyDict.Remove(slotID);
				}
			}

			enemy.Reset();

			// Check this is last enemy!!
			if (EnemySpawn >= EnemyTotal)
			{
				enemy.None();
				check = true;
			}

			return check;
		}

		public Boolean CheckEnemiesNone()
		{
			// Assume no more to spawn.
			Boolean test = true;
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				Enemy enemy = EnemyList[index];
				if (EnemyType.None != enemy.EnemyType)
				{
					test = false;
					break;
				}
			}

			return test;
		}

		public void Update(GameTime gameTime)
		{
			EnemyController = 0.0f;
			Boolean launchCheck = false;
			EnemyTest.Clear();
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.Update(gameTime);

				// Check if controller should rumble.
				if (enemy.EnemyMotor > 0)
				{
					Single enemyMotor = enemy.EnemyMotor;
					if (EnemyController < enemyMotor)
					{
						EnemyController = enemyMotor;
					}
				}

				// Check if should move enemy ship...
				if (enemy.EnemyChange)
				{
					if (MoveType.None != enemy.MoveType)
					{
						Boolean enemyMoving = MyGame.Manager.MoverManager.ShouldEnemyMove(enemy.FrameIndex, levelType);
						enemy.SetEnemyMoving(enemyMoving);

						if (enemyMoving)
						{
							Boolean updateVelocity = MyGame.Manager.MoverManager.UpdateVelocity(enemy.FrameIndex, enemy.MoveType, levelType);
							if (updateVelocity)
							{
								Vector2 velocity = MyGame.Manager.MoverManager.GetEnemyVelocity(enemy.MoveType);
								enemy.SetEnemyVelocity(velocity);
							}
						}
						else
						{
							enemy.SetEnemyVelocity(Vector2.Zero);
						}
					}
				}

				if (enemy.EnemyLaunch)
				{
					launchCheck = true;
					EnemyStart++;
					enemy.ResetLaunch();
				}
				if (EnemyType.Test == enemy.EnemyType)
				{
					EnemyTest.Add(enemy);
				}
			}

			if (launchCheck)
			{
				EnemyStartText = EnemyStart.ToString().PadLeft(3, '0');
				EnemyPercentage = (EnemyStart / (Single)EnemyTotal) * 100.0f;
			}
		}

		public void Draw()
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Draw();
			}
		}

		public void DrawProgress()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, EnemyStartText, progressPosition[0], Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, Globalize.SEPARATOR, progressPosition[1], Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, EnemyTotalText, progressPosition[2], Color.White);
		}

		private static IList<Rectangle> GetEnemyBounds()
		{
			IList<Rectangle> enemyBounds = new List<Rectangle>(Constants.MAX_ENEMYS_SPAWN);
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Rectangle bounds = GetEnemyBound(index);
				enemyBounds.Add(bounds);
			}

			return enemyBounds;
		}
		private static Rectangle GetEnemyBound(Byte index)
		{
			// High + wide max enemy.
			const Byte size = 120;
			const Byte wide = 160;
			const Byte high = 200;

			const Byte inflate = 4;
			const Byte deflate = 8;

			UInt16 offsetY = 80;
			offsetY += Constants.GameOffsetY;

			const Byte uppr = Constants.BOTTOM_SECTOR;
			if (index < uppr)
			{
				return new Rectangle((wide * index) + inflate, offsetY + inflate, wide - size - deflate, high - size - deflate);
			}
			else
			{
				index -= uppr;
				offsetY = Constants.HALFWAY_DOWN;
				offsetY += Constants.GameOffsetY;
				const Byte offsetX = Constants.BOTTOM_OFFSET;
				return new Rectangle(offsetX + (wide * index) + inflate, offsetY + inflate, wide - size - deflate, high - size - deflate);
			}
		}
		private void ResetEnemyRotates()
		{
			Byte enemyTotal = levelConfigData.EnemyTotal;
			for (Byte index = 0; index < enemyTotal; index++)
			{
				enemyRotates.Add(false);
			}

			const Byte first = 1;
			Byte iterations = (Byte) (levelConfigData.EnemyRotates / 100.0f * enemyTotal);
			for (Byte index = 0; index < iterations; index++)
			{
				while (true)
				{
					// Always want first [0th] enemy to be None so random starts >= first.
					Byte key = (Byte)MyGame.Manager.RandomManager.Next(first, enemyTotal);
					if (!enemyRotates[key])
					{
						enemyRotates[key] = true;
						break;
					}
				}
			}
		}
		private void ResetEnemyMoves()
		{
			Byte enemyTotal = levelConfigData.EnemyTotal;
			for (Byte index = 0; index < enemyTotal; index++)
			{
				enemyMoves.Add(MoveType.None);
			}

			MyGame.Manager.MoverManager.ResetEnemyMoves(enemyMoves, levelConfigData.EnemyMoverHorz, enemyTotal, MoveType.Horz);
			MyGame.Manager.MoverManager.ResetEnemyMoves(enemyMoves, levelConfigData.EnemyMoverVert, enemyTotal, MoveType.Vert);
			MyGame.Manager.MoverManager.ResetEnemyMoves(enemyMoves, levelConfigData.EnemyMoverBoth, enemyTotal, MoveType.Both);
		}

		public IList<Enemy> EnemyList { get; private set; }
		public IList<Enemy> EnemyTest { get; private set; }
		public IDictionary<Byte, Enemy> EnemyDict { get; private set; }
		public IList<Rectangle> EnemyBounds { get; private set; }
		public UInt16[] EnemyOffsetX { get; private set; }
		public UInt16[] EnemyOffsetY { get; private set; }

		public Byte EnemySpawn { get; private set; }
		public Byte EnemyTotal { get; private set; }
		public Byte EnemyStart { get; private set; }
		public Single EnemyPercentage { get; private set; }
		public Single EnemyController { get; private set; }
		public String EnemyTotalText { get; private set; }
		public String EnemyStartText { get; private set; }
	}
}
