using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Back
{
	public interface IEnemyManagerBAK
	{
		void Initialize();
		void LoadContent();
		void Reset(LevelType theLevelType, Byte theEnemySpawn, UInt16 minDelay, UInt16 maxDelay, Byte enemyTotal);
		void SpawnAllEnemies();
		void SpawnOneEnemy(Byte index);
		//void CheckAllEnemies();					// TODO delete
		Boolean CheckThisEnemy(Byte index);
		//void CheckThisEnemy(Byte index);
		//void CheckThisEnemy(Enemy enemy);			// TODO delete
		Boolean CheckEnemiesNone();

		//void Spawn(UInt16 frameDelay, Vector2 position);
		void Update(GameTime gameTime);
		void Draw();

		IList<Enemy> EnemyList { get; }
		IList<Enemy> EnemyTest { get; }
		IDictionary<Byte, Enemy> EnemyDict { get; }

		IList<Rectangle> EnemyBounds { get; }
		UInt16[] EnemyOffsetX { get; }
		UInt16[] EnemyOffsetY { get; }
		
		UInt16 MinDelay { get; }
		UInt16 MaxDelay { get; }
		Byte EnemySpawn { get; }
		Byte EnemyTotal { get; }
	}

	public class EnemyManagerBAK : IEnemyManagerBAK
	{
		private LevelType levelType;
		private Byte maxEnemySpawn;

		public void Initialize()
		{
			maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;

			EnemyList = new List<Enemy>(maxEnemySpawn);
			EnemyTest = new List<Enemy>(maxEnemySpawn);
			EnemyDict = new Dictionary<Byte, Enemy>(maxEnemySpawn);
			EnemyBounds = GetEnemyBounds();

			EnemyOffsetX = new UInt16[maxEnemySpawn];
			EnemyOffsetY = new UInt16[maxEnemySpawn];

			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyOffsetX[index] = (UInt16)(Constants.ENEMY_OFFSET_X[index] + Constants.GameOffsetX);
				EnemyOffsetY[index] = (UInt16)(Constants.ENEMY_OFFSET_Y[index] + Constants.GameOffsetY);

				Enemy enemy = new Enemy();
				enemy.Initialize(Constants.MAX_ENEMYS_FRAME);
				enemy.SetID(index);
				enemy.SetSlotID();
				EnemyList.Add(enemy);
			}
		}

		public void LoadContent()
		{
			for (Byte index = 0; index < Constants.MAX_ENEMYS_SPAWN; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.LoadContent(MyGame.Manager.ImageManager.EnemyRectangles);
			}
		}

		public void Reset(LevelType theLevelType, Byte theEnemySpawn, UInt16 minDelay, UInt16 maxDelay, Byte enemyTotal)
		{
			levelType = theLevelType;
			maxEnemySpawn = theEnemySpawn;
			if (maxEnemySpawn > Constants.MAX_ENEMYS_SPAWN)
			{
				maxEnemySpawn = Constants.MAX_ENEMYS_SPAWN;
			}

			MinDelay = minDelay;
			MaxDelay = maxDelay;

			// Reset all enemies but not the list as will clear.
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Reset();
			}

			EnemyTest.Clear();
			EnemyDict.Clear();

			// Ensure total at least the max.
			if (enemyTotal < maxEnemySpawn)
			{
				enemyTotal = maxEnemySpawn;
			}
			EnemyTotal = enemyTotal;
			EnemySpawn = 0;
		}

		public void SpawnAllEnemies()
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				SpawnOneEnemy(index);
				//EnemyList[index].Start((UInt16)(Constants.TestFrameDelay + 3* Constants.TestFrameDelay * index));		// TODO tweak configurable numbers

				//REPEAT - initial "algorithm" but changed because this did not space out enemy ships evenly!
				// Set the start delay for the initial spawned enemies.
				UInt16 startFrameDelay = GetStartFrameDelay(index, levelConfigData.EnemyStartDelay, levelConfigData.EnemyStartDelta);
				EnemyList[index].Start(startFrameDelay);
			}
		}
		private UInt16 GetStartFrameDelay(Byte index, UInt16 enemyStartDelay, UInt16 enemyStartDelta)
		{
			UInt16 delay = (UInt16)((index + 1) * enemyStartDelay);
			UInt16 delta = (UInt16)MyGame.Manager.RandomManager.Next(enemyStartDelta);

			return (UInt16)(delay - delta);
		}

		public void SpawnOneEnemy(Byte index)
		{
			// TODO work out better the frame delay.
			UInt16 frameDelay = Constants.TestFrameDelay;
			//UInt16 frameDelay = MyGame.Manager.RandomManager.Next(MinDelay, MaxDelay);

			SByte slotID = Constants.INVALID_INDEX;
			while (true)
			{
				slotID = (SByte)MyGame.Manager.RandomManager.Next(Constants.MAX_ENEMYS_SPAWN);
				if (!EnemyDict.ContainsKey((Byte)slotID))
				{
					break;
				}
			}

			// TODO delete
			//slotID = 0;		// hard code slotID to test.
			MyGame.Manager.Logger.Info((slotID+1).ToString());

			Enemy enemy = EnemyList[index];

			Vector2 position = enemy.Position;
			Byte randomX = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_X);
			Byte randomY = (Byte)MyGame.Manager.RandomManager.Next(Constants.ENEMY_RANDOM_Y);
			UInt16 offsetX = EnemyOffsetX[(Byte)slotID];
			UInt16 offsetY = EnemyOffsetY[(Byte)slotID];
			position.X = randomX + offsetX;
			position.Y = randomY + offsetY;

			Rectangle bounds = EnemyBounds[(Byte)slotID];
			enemy.Spawn((Byte)slotID, frameDelay, position, bounds);
			EnemyDict.Add((Byte)slotID, enemy);

			EnemySpawn++;
		}

		// TODO delete
		//public void CheckAllEnemies()
		//{
		//    Boolean test = false;
		//    for (Byte index = 0; index < maxEnemySpawn; index++)
		//    {
		//        Enemy enemy = EnemyList[index];
		//        if (EnemyType.Test != enemy.EnemyType)
		//        {
		//            continue;
		//        }

		//        SByte slotID = enemy.SlotID;
		//        if (EnemyDict.ContainsKey((Byte)slotID))
		//        {
		//            EnemyDict.Remove((Byte)slotID);
		//        }

		//        enemy.Reset();

		//        //EnemyCount++;
		//        //if (EnemyCount > EnemyTotal)
		//        //{
		//        //    test = true;
		//        //    enemy.None();
		//        //}
		//        //SpawnOneEnemy(index);
		//    }

		////	return test;
		//}

		public Boolean CheckThisEnemy(Byte index)
		{
			Boolean check = false;

			Enemy enemy = EnemyList[index];
			if (EnemyType.Idle == enemy.EnemyType)
			{
				return false;
			}

			SByte slotID = enemy.SlotID;
			if (EnemyDict.ContainsKey((Byte)slotID))
			{
				EnemyDict.Remove((Byte)slotID);
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

		//public void CheckThisEnemy(Byte index)
		//{
		//    Enemy enemy = EnemyList[index];
		//    if (EnemyType.Test == enemy.EnemyType || EnemyType.Dead == enemy.EnemyType)
		//    {
		//        SByte slotID = enemy.SlotID;
		//        if (EnemyDict.ContainsKey((Byte)slotID))
		//        {
		//            EnemyDict.Remove((Byte)slotID);
		//        }

		//        enemy.Reset();
		//    }
		//}

		// TODO delete
		//public void CheckThisEnemy(Enemy enemy)
		//{
		//    if (EnemyType.Test != enemy.EnemyType)
		//    {
		//        return;
		//    }

		//    SByte slotID = enemy.SlotID;
		//    if (EnemyDict.ContainsKey((Byte)slotID))
		//    {
		//        EnemyDict.Remove((Byte)slotID);
		//    }

		//    enemy.Reset();
		//}

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
			EnemyTest.Clear();
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				Enemy enemy = EnemyList[index];
				enemy.Update(gameTime);

				if (EnemyType.Test == enemy.EnemyType)
				{
					EnemyTest.Add(enemy);
				}
			}
		}

		public void Draw()
		{
			for (Byte index = 0; index < maxEnemySpawn; index++)
			{
				EnemyList[index].Draw();
			}
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
			const Byte uppr = 5;

			if (index < uppr)
			{
				return new Rectangle((wide * index), 80 + Constants.GameOffsetY, (wide - size), (high - size));
			}
			else
			{
				index -= uppr;
				const Byte offset = 190;
				return new Rectangle(offset + wide * index, 280 + Constants.GameOffsetY, (wide - size), (high - size));
			}
		}

		public IList<Enemy> EnemyList { get; private set; }
		public IList<Enemy> EnemyTest { get; private set; }
		public IDictionary<Byte, Enemy> EnemyDict { get; private set; }
		public IList<Rectangle> EnemyBounds { get; private set; }
		public UInt16[] EnemyOffsetX { get; private set; }
		public UInt16[] EnemyOffsetY { get; private set; }
		public Byte EnemySpawn { get; private set; }
		public Byte EnemyTotal { get; private set; }
		public UInt16 MinDelay { get; private set; }
		public UInt16 MaxDelay { get; private set; }
	}
}
