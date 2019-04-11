using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Sprites;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Back
{
	public interface ICollisionManagerBAK 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadContentEnemys();
		void LoadContentTarget();

		Boolean ColorCollision(Vector2 enemysPosition, Vector2 targetPosition);
		Boolean CheckOne();
		Boolean CheckOne(Vector2 targetPosition, IList<Enemy> enemyTest);

		void ClearCollisionList();
		void AddToBulletCollisionList(Byte bulletIndex);
		void AddToEnemysCollisionList(Byte enemysIndex);

		Boolean EnemyCollideTarget(Vector2 enemysPosition, Vector2 targetPosition);

		IList<Byte> BulletCollisionList { get; }
		IList<Byte> EnemysCollisionList { get; }

		IList<UInt16> EnemysList { get; }
		IList<UInt16> TargetList { get; }
		Color[] EnemysColor { get; }
		Color[] TargetColor { get; }
	}

	public class CollisionManagerBAK : ICollisionManagerBAK
	{
		private const Byte borderSize = 4;
		private String collisionRoot;
		private Byte enemysSize;
		//private Byte targetSize;
		private Byte offsetSize;

		private const String SPRITE_DIRECTORY = "Sprite";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			BulletCollisionList = new List<Byte>(Constants.MAX_BULLET_SHOOT);
			EnemysCollisionList = new List<Byte>(Constants.MAX_ENEMYS_SPAWN);

			collisionRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, SPRITE_DIRECTORY);
		}

		public void LoadContent()
		{
			LoadContentEnemys();
			LoadContentTarget();

			//enemysSize = (Byte)(Assets.Enemy120.Width);
			enemysSize = Constants.EnemySize;

			//targetSize = (Byte)(Assets.Target56.Width);
			//targetSize = (Byte)(Assets.Target64.Width);
			//targetSize = Constants.TargetSize;

			//targetSize = 56;
			offsetSize = Constants.TargetSize - 2 * borderSize;

			//EnemysColor = new Color[enemysSize * enemysSize];
			//Assets.Enemy120.GetData(EnemysColor);

			//TargetColor = new Color[Assets.Target56.Width * Assets.Target56.Height];
			//TargetColor = new Color[targetSize * targetSize];
			//Assets.Target56.GetData(TargetColor);
			//Assets.Target64.GetData(TargetColor);
			//Assets.Target56.GetData(TargetColor);


			//IList<UInt16> list1 = Process(EnemysColor, Byte.MinValue);
			//foreach (UInt16 item in list1)
			//{
			//    //Console.WriteLine(item);
			//}
			//IList<UInt16> list2 = Process(TargetColor, Byte.MinValue);
			//foreach (UInt16 item in list2)
			//{
			//    //Console.WriteLine(item);
			//}
		}

		public void LoadContentEnemys()
		{
			EnemysList = LoadContentData("Enemys");
		}
		public void LoadContentTarget()
		{
			TargetList = LoadContentData("Target");
		}
		private IList<UInt16> LoadContentData(String file)
		{
			file = String.Format("{0}/{1}.txt", collisionRoot, file);
			return MyGame.Manager.FileManager.LoadTxt<UInt16>(file);
		}


		public Boolean ColorCollision(Vector2 enemysPosition, Vector2 targetPosition)
		{
			// Use 56 x 56 as target collision thus border offset is 4px on all sides.
			Vector2 offsetPosition = targetPosition;
			offsetPosition.X += borderSize;
			offsetPosition.Y += borderSize;

			Int16 enemysPosX = (Int16)enemysPosition.X;
			Int16 enemysPosY = (Int16)enemysPosition.Y;
			Int16 targetPosX = (Int16)targetPosition.X;
			Int16 targetPosY = (Int16)targetPosition.Y;
			Int16 offsetPosX = (Int16) offsetPosition.X;
			Int16 offsetPosY = (Int16) offsetPosition.Y;

			//Int16 lft = Math.Max(enemysPosX, targetPosX);
			//Int16 top = Math.Max(enemysPosY, targetPosY);
			//Int16 rgt = Math.Min((Int16)(enemysPosX + enemysSize), (Int16)(targetPosX + targetSize));
			//Int16 bot = Math.Min((Int16)(enemysPosY + enemysSize), (Int16)(targetPosY + targetSize));

			Int16 lft = Math.Max(enemysPosX, offsetPosX);
			Int16 top = Math.Max(enemysPosY, offsetPosY);
			Int16 rgt = Math.Min((Int16)(enemysPosX + enemysSize), (Int16)(offsetPosX + offsetSize));
			Int16 bot = Math.Min((Int16)(enemysPosY + enemysSize), (Int16)(offsetPosY + offsetSize));


			// Check every point within the intersection bounds.
			for (Int16 y = top; y < bot; y++)
			{
				for (Int16 x = lft; x < rgt; x++)
				{
					//UInt16 targetIndex = (UInt16)((x - targetPosX) + (y - targetPosY) * targetSize);
					UInt16 targetIndex = (UInt16)((x - offsetPosX) + (y - offsetPosY) * offsetSize);
					if (!TargetList.Contains(targetIndex))
					{
						continue;
					}
					UInt16 enemysIndex = (UInt16)((x - enemysPosX) + (y - enemysPosY) * enemysSize);
					if (!EnemysList.Contains(enemysIndex))
					{
						continue;
					}

					// An intersection has been found.
					return true;
				}
			}

			// No intersection found.
			return false;
		}

		/*
		public Boolean ColorCollisionX(Vector2 enemysPosition, Vector2 targetPosition)
		{
			// Use 56 x 56 as target collision thus border offset is 4px on all sides.
			Vector2 offsetPosition = targetPosition;
			offsetPosition.X += borderSize;
			offsetPosition.Y += borderSize;

			Int16 enemysPosX = (Int16)enemysPosition.X;
			Int16 enemysPosY = (Int16)enemysPosition.Y;
			Int16 targetPosX = (Int16)targetPosition.X;
			Int16 targetPosY = (Int16)targetPosition.Y;
			Int16 offsetPosX = (Int16)offsetPosition.X;
			Int16 offsetPosY = (Int16)offsetPosition.Y;

			Int16 lft = Math.Max(enemysPosX, targetPosX);
			Int16 top = Math.Max(enemysPosY, targetPosY);
			Int16 rgt = Math.Min((Int16)(enemysPosX + enemysSize), (Int16)(targetPosX + targetSize));
			Int16 bot = Math.Min((Int16)(enemysPosY + enemysSize), (Int16)(targetPosY + targetSize));

			//Int16 rgt = Math.Min((Int16)(enemysPosX + enemysSize), (Int16)(offsetPosX + offsetSize));
			//Int16 bot = Math.Min((Int16)(enemysPosY + enemysSize), (Int16)(offsetPosY + offsetSize));


			// Check every point within the intersection bounds.
			for (Int16 y = top; y < bot; y++)
			{
				for (Int16 x = lft; x < rgt; x++)
				{
					UInt16 targetIndex = (UInt16)((x - targetPosX) + (y - targetPosY) * targetSize);
					//UInt16 offsetIndex = (UInt16)((x - offsetPosX) + (y - offsetPosY) * offsetSize);
					Color targetColor = TargetColor[targetIndex];
					//Color offsetColor = TargetColor[offsetIndex];
					if (0 == targetColor.A)
					{
						continue;
					}

					UInt16 enemysIndex = (UInt16)((x - enemysPosX) + (y - enemysPosY) * enemysSize);
					Color enemysColor = EnemysColor[enemysIndex];
					if (0 == enemysColor.A)
					{
						continue;
					}

					// An intersection has been found.
					return true;
				}
			}

			// No intersection found.
			return false;
		}
		*/

		public Boolean EnemyCollideTarget(Vector2 enemysPosition, Vector2 targetPosition)
		{
			return false;
		}

		// TODO give a better name!
		public Boolean CheckOne()
		{
			Vector2 targetPosition = MyGame.Manager.SpriteManager.LargeTarget.Position;
			IList<Enemy> enemyTest = MyGame.Manager.EnemyManager.EnemyTest;

			return CheckOne(targetPosition, enemyTest);
		}

		public Boolean CheckOne(Vector2 targetPosition, IList<Enemy> EnemyTest)
		{
			// TODO implement collision logic between enemies to test and target.
			return false;
		}

		public void ClearCollisionList()
		{
			BulletCollisionList.Clear();
			EnemysCollisionList.Clear();
		}

		public void AddToBulletCollisionList(Byte bulletIndex)
		{
			BulletCollisionList.Add(bulletIndex);
		}

		public void AddToEnemysCollisionList(Byte enemysIndex)
		{
			EnemysCollisionList.Add(enemysIndex);
		}

		// TODO delete
		private IList<UInt16> Process(Color[] array, Byte test)
		{
			IList<UInt16> list = new List<UInt16>();
			for (UInt16 index = 0; index < array.Length; index++)
			{
				Color color = array[index];
				Byte alpha = color.A;
				if (test == alpha)
				{
					continue;
				}

				list.Add(index);
			}

			return list;
		}

		public IList<Byte> BulletCollisionList { get; private set; }
		public IList<Byte> EnemysCollisionList { get; private set; }

		public IList<UInt16> EnemysList { get; private set; }
		public IList<UInt16> TargetList { get; private set; }
		public Color[] EnemysColor { get; private set; }
		public Color[] TargetColor { get; private set; }
	}
}

