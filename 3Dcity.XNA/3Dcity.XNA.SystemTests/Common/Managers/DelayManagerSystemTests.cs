using System;
using System.Collections.Generic;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class DelayManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			DelayManager = MyGame.Manager.DelayManager;
			DelayManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadContentTest()
		{
			DelayManager.LoadContent();
			Assert.That(DelayManager.DelayWaves, Is.Not.Null);
			Assert.That(DelayManager.DelayWaves.Count, Is.EqualTo(360));
		}

		[Test]
		public void CalcdEnemyDelaysTest()
		{
			IDictionary<Byte, UInt16> enemyDelays = new Dictionary<Byte, UInt16>(Constants.MAX_ENEMYS_TOTAL);

			const LevelType levelType = LevelType.Hard;
			const Byte levelNo = 13;
			const Byte levelIndex = levelNo - 1;

			MyGame.Manager.LevelManager.Initialize(CONTENT_ROOT);
			MyGame.Manager.LevelManager.LoadLevelConfigData(levelType, levelIndex);
			LevelConfigData levelConfigData = MyGame.Manager.LevelManager.LevelConfigData;

			MyGame.Manager.RandomManager.Initialize();

			DelayManager.LoadContent();
			Console.WriteLine("Level:{0} Num:{1}", levelType, levelNo);

			DelayManager.ResetEnemyDelays(enemyDelays, levelConfigData);

			IDictionary<SpeedType, Byte> totals = new Dictionary<SpeedType, Byte>(Constants.MAX_ENEMYS_TOTAL);
			for (Byte index = 0; index < enemyDelays.Count; index++)
			{
				UInt16 value = enemyDelays[index];
				SpeedType speedType = (SpeedType)value;
				if (!totals.ContainsKey(speedType))
				{
					totals.Add(speedType, 1);
				}
				else
				{
					totals[speedType]++;
				}
			}

			Console.WriteLine("Speed[{0}]={1}", SpeedType.None, totals[SpeedType.None]);
			Console.WriteLine("Speed[{0}]={1}", SpeedType.Wave, totals[SpeedType.Wave]);
			Console.WriteLine("Speed[{0}]={1}", SpeedType.Fast, totals[SpeedType.Fast]);


			DelayManager.CalcdEnemyDelays(enemyDelays, levelConfigData);

			
			UInt16 minValue = UInt16.MaxValue;
			UInt16 maxValue = UInt16.MinValue;
			for (Byte index = 0; index < enemyDelays.Count; index++)
			{
				UInt16 value = enemyDelays[index];
				if (value < minValue)
				{
					minValue = value;
				}
				if (value > maxValue)
				{
					maxValue = value;
				}
			}

			Console.WriteLine("Min:{0} Max:{1}", minValue, maxValue);
		}

		[TearDown]
		public void TearDown()
		{
			DelayManager = null;
		}

	}
}
