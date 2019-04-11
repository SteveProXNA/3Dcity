using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IMoverManager
	{
		void Initialize();
		void ResetEnemyMoves(IList<MoveType> enemyMoves, Byte percentage, Byte enemyTotal, MoveType moveType);
		Boolean ShouldEnemyMove(Byte item, LevelType levelType);
		Boolean UpdateVelocity(Byte frameIndex, MoveType moveType, LevelType levelType);
		Vector2 GetEnemyVelocity(MoveType moveType);
	}

	public class MoverManager : IMoverManager
	{
		private IList<Direction>[] directionList;
		private IList<Byte>[] moveFrameList;
		private Vector2[] unitVelocityList;

		public void Initialize()
		{
			// Initialize directions.
			directionList = new IList<Direction>[Constants.MAX_MOVES];
			directionList[(Byte)MoveType.None] = new List<Direction> { Direction.None };
			directionList[(Byte)MoveType.Horz] = GetHorzDirection();
			directionList[(Byte)MoveType.Vert] = GetVertDirection();
			directionList[(Byte)MoveType.Both] = GetBothDirection();

			// Initialize move frames.
			moveFrameList = new IList<Byte>[2];
			moveFrameList[(Byte) LevelType.Easy] = new List<Byte> { 1, 2, 3, 4};
			moveFrameList[(Byte) LevelType.Hard] = new List<Byte> {1, 2, 3, 4, 5};

			// Initialize velocities.
			unitVelocityList = new Vector2[Constants.MAX_MOVES + 1];
			unitVelocityList[(Byte)Direction.None] = Vector2.Zero;
			unitVelocityList[(Byte)Direction.Left] = new Vector2(-1, 0);
			unitVelocityList[(Byte)Direction.Right] = new Vector2(1, 0);
			unitVelocityList[(Byte)Direction.Up] = new Vector2(0, -1);
			unitVelocityList[(Byte)Direction.Down] = new Vector2(0, 1);
		}

		public void ResetEnemyMoves(IList<MoveType> enemyMoves, Byte percentage, Byte enemyTotal, MoveType moveType)
		{
			const Byte first = 1;
			Byte iterations = (Byte)(percentage / 100.0f * enemyTotal);
			for (Byte index = 0; index < iterations; index++)
			{
				while (true)
				{
					// Always want first [0th] enemy to be None so random starts >= first.
					Byte key = (Byte)MyGame.Manager.RandomManager.Next(first, enemyTotal);
					if (MoveType.None == enemyMoves[key])
					{
						enemyMoves[key] = moveType;
						break;
					}
				}
			}
		}

		public Boolean ShouldEnemyMove(Byte item, LevelType levelType)
		{
			IList<Byte> moveFrames = moveFrameList[(Byte) levelType];
			return moveFrames.Contains(item);
		}

		public Boolean UpdateVelocity(Byte frameIndex, MoveType moveType, LevelType levelType)
		{
			if (MoveType.Both == moveType)
			{
				return true;
			}

			IList<Byte> moveFrames = moveFrameList[(Byte)levelType];
			Byte moveFrame = moveFrames[0];
			if (frameIndex == moveFrame)
			{
				return true;
			}

			return false;
		}

		public Vector2 GetEnemyVelocity(MoveType moveType)
		{
			IList<Direction> directions = directionList[(Byte)moveType];
			Byte max = (Byte)directions.Count;
			Byte index = (Byte)MyGame.Manager.RandomManager.Next(max);
			Direction direction = directions[index];
			Vector2 unitVelocity = unitVelocityList[(Byte) direction];
			return unitVelocity;
		}

		private static IList<Direction> GetHorzDirection()
		{
			return new List<Direction>
			{
				Direction.Left,
				Direction.Right,
			};
		}
		private static IList<Direction> GetVertDirection()
		{
			return new List<Direction>
			{
				Direction.Up,
				Direction.Down,
			};
		}
		private static IList<Direction> GetBothDirection()
		{
			return new List<Direction>
			{
				Direction.Left,
				Direction.Right,
				Direction.Up,
				Direction.Down,
			};
		}

	}
}
