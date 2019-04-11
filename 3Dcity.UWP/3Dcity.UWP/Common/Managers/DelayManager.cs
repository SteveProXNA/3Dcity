using System;
using System.Collections.Generic;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IDelayManager
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();

		UInt16 GetStartDelay(Byte index, UInt16 enemyStartDelay, UInt16 enemyStartDelta);
		UInt16 GetTotalDelay(UInt16[] frameDelays);

		void ResetEnemyDelays(IDictionary<Byte, UInt16> enemyDelays, LevelConfigData levelConfigData);
		void CalcdEnemyDelays(IDictionary<Byte, UInt16> enemyDelays, LevelConfigData levelConfigData);

		IList<Single> DelayWaves { get; }
	}

	public class DelayManager : IDelayManager
	{
		private String delayRoot;

		private const String MATHS_DIRECTORY = "Maths";
		private const UInt16 DEGREES_PER_CIRCLE = 360;

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			delayRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, MATHS_DIRECTORY);
		}

		public void LoadContent()
		{
			String file = String.Format("{0}/DelayWaves.txt", delayRoot);
			DelayWaves = MyGame.Manager.FileManager.LoadTxt<Single>(file);
		}

		public UInt16 GetStartDelay(Byte index, UInt16 enemyStartDelay, UInt16 enemyStartDelta)
		{
			UInt16 delay = (UInt16)((index + 1) * enemyStartDelay);
			UInt16 delta = (UInt16)MyGame.Manager.RandomManager.Next(enemyStartDelta);

			return (UInt16)(delay - delta);
		}

		public UInt16 GetTotalDelay(UInt16[] frameDelays)
		{
			UInt16 total = 0;
			for (Byte index = 1; index < frameDelays.Length; index++)
			{
				total += frameDelays[index];
			}

			return total;
		}

		public void ResetEnemyDelays(IDictionary<Byte, UInt16> enemyDelays, LevelConfigData levelConfigData)
		{
			 Byte enemyTotal = levelConfigData.EnemyTotal;

			enemyDelays.Clear();
			for (Byte index = 0; index < enemyTotal; index++)
			{
				enemyDelays.Add(index, (Byte)SpeedType.None);
			}

			Byte enemySpeedWave = (Byte)(levelConfigData.EnemySpeedWave / 100.0f * levelConfigData.EnemyTotal);
			ResetEnemySpeedTypes(enemyDelays, enemyTotal, enemySpeedWave, SpeedType.Wave);

			Byte enemySpeedFast = (Byte)(levelConfigData.EnemySpeedFast / 100.0f * levelConfigData.EnemyTotal);
			ResetEnemySpeedTypes(enemyDelays, enemyTotal, enemySpeedFast, SpeedType.Fast);
		}
		private static void ResetEnemySpeedTypes(IDictionary<Byte, UInt16> enemyDelays, Byte enemyTotal, Byte count, SpeedType speedType)
		{
			const Byte first = 1;
			for (Byte index = 0; index < count; index++)
			{
				while (true)
				{
					// Always want first [0th] enemy to be None so random starts >= first.
					Byte key = (Byte)MyGame.Manager.RandomManager.Next(first, enemyTotal);
					if ((Byte)SpeedType.None == enemyDelays[key])
					{
						enemyDelays[key] = (Byte)speedType;
						break;
					}
				}
			}
		}

		public void CalcdEnemyDelays(IDictionary<Byte, UInt16> enemyDelays, LevelConfigData levelConfigData)
		{
			Byte enemyTotal = levelConfigData.EnemyTotal;

			UInt16 enemyFrameDelay = levelConfigData.EnemyFrameDelay;
			UInt16 enemyFrameDelta = levelConfigData.EnemyFrameDelta;

			UInt16 noneFrameDelay = GetNoneFrameDelay(enemyFrameDelay, enemyFrameDelta);
			for (Byte key = 0; key < enemyTotal; key++)
			{
				SpeedType speedType = (SpeedType)enemyDelays[key];
				switch (speedType)
				{
					case SpeedType.None:
						enemyDelays[key] = GetNoneFrameDelay(enemyFrameDelay, enemyFrameDelta);
						break;
					case SpeedType.Wave:
						enemyDelays[key] = GetWaveFrameDelay(enemyFrameDelay, levelConfigData.EnemyFrameRange, levelConfigData.EnemyFrameMinim);
						break;
					case SpeedType.Fast:
						enemyDelays[key] = GetFastFrameDelay(key, enemyTotal, enemyFrameDelay, levelConfigData.EnemyFrameRange, levelConfigData.EnemyFrameMinim);
						break;
					default:
						enemyDelays[key] = noneFrameDelay;
						break;
				}
			}
		}
		private static UInt16 GetNoneFrameDelay(UInt16 enemyFrameDelay, UInt16 enemyFrameDelta)
		{
			UInt16 delta = (UInt16)MyGame.Manager.RandomManager.Next(enemyFrameDelta);
			return (UInt16)(enemyFrameDelay - delta);
		}
		private UInt16 GetWaveFrameDelay(UInt16 enemyFrameDelay, UInt16 enemyFrameRange, UInt16 enemyFrameMinim)
		{
			return GetFrameDelayCommon(SpeedType.Wave, 0, enemyFrameDelay, enemyFrameRange, enemyFrameMinim);
		}
		private UInt16 GetFastFrameDelay(Byte key, Byte enemyTotal, UInt16 enemyFrameDelay, UInt16 enemyFrameRange, UInt16 enemyFrameMinim)
		{
			Single percentage = 0;
			if (0 != enemyTotal)
			{
				percentage = (Single)(key + 1) / enemyTotal;
			}

			return GetFrameDelayCommon(SpeedType.Fast, percentage, enemyFrameDelay, enemyFrameRange, enemyFrameMinim);
		}

		private UInt16 GetFrameDelayCommon(SpeedType speedType, Single percentage, UInt16 enemyFrameDelay, UInt16 enemyFrameRange, UInt16 enemyFrameMinim)
		{
			// 360 degrees in sine wave.
			UInt16 index = (Byte)MyGame.Manager.RandomManager.Next(DEGREES_PER_CIRCLE);
			Single value = DelayWaves[index];

			if (SpeedType.Fast == speedType)
			{
				value = Math.Abs(value);
				value += percentage;
			}

			Int16 multi = (Int16)(value * enemyFrameRange);
			Int16 delay = (Int16)(enemyFrameDelay - multi);

			// Prevent from too fast...!
			if (delay < enemyFrameMinim)
			{
				delay = (Int16)enemyFrameMinim;
			}

			return (UInt16)delay;
		}

		public IList<Single> DelayWaves { get; private set; }
	}
}
