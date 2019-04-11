using System;

namespace WindowsGame.Common.Data
{
	public struct LevelConfigData
	{
		public String LevelNo;
		public String LevelType;
		public Boolean BonusLevel;
		public UInt16 GridDelay;
		public Byte BulletMaxim;
		public UInt16 BulletFrame;
		public UInt16 BulletShoot;
		public Byte EnemySpawn;
		public Byte EnemyTotal;
		public UInt16 EnemyStartDelay;
		public UInt16 EnemyStartDelta;
		public UInt16 EnemyFrameDelay;
		public UInt16 EnemyFrameDelta;
		public UInt16 EnemyFrameMinim;
		public UInt16 EnemyFrameRange;
		public Byte EnemySpeedNone;
		public Byte EnemySpeedWave;
		public Byte EnemySpeedFast;
		public Byte EnemyMoverNone;
		public Byte EnemyMoverHorz;
		public Byte EnemyMoverVert;
		public Byte EnemyMoverBoth;
		public Byte EnemyVelocity;
		public Byte EnemyRotates;
		public UInt16 ExplodeDelay;
	}
}
