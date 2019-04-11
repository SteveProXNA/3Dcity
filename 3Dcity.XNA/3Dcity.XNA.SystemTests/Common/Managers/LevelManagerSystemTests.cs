using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class LevelManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			LevelManager = MyGame.Manager.LevelManager;
			LevelManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadContentTest()
		{
			LevelManager.LoadContent();

			Assert.IsNotNull(LevelManager.LevelNames);

			Print(LevelManager.LevelNames);
		}
		private static void Print(IEnumerable<string> lines)
		{
			foreach (var line in lines)
			{
				Console.WriteLine(line);
			}
		}

		[Test]
		public void LoadLevelConfigDataTest()
		{
			const LevelType levelType = LevelType.Test;
			const Byte levelIndex = 98;

			LevelManager.LoadLevelConfigData(levelType, levelIndex);

			Assert.That(LevelManager.LevelConfigData, Is.Not.Null);

			// Ensure that enemy frame delay proportions work.
			LevelConfigData data = LevelManager.LevelConfigData;
			Byte sum1 = (Byte) (data.EnemySpeedNone + data.EnemySpeedWave + data.EnemySpeedFast);
			Assert.That(100, Is.EqualTo(sum1));

			Byte sum2 = (Byte)(data.EnemyMoverNone + data.EnemyMoverHorz + data.EnemyMoverVert + data.EnemyMoverBoth);
			Assert.That(100, Is.EqualTo(sum2));

			PrintData(LevelManager.LevelConfigData);
		}
		private static void PrintData(LevelConfigData data)
		{
			var fields = data.GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
				object obj = field.GetValue(data);
				Console.WriteLine(field.Name + "\t\t" + obj);
			}
		}

		[TearDown]
		public void TearDown()
		{
			LevelManager = null;
		}
	}
}
