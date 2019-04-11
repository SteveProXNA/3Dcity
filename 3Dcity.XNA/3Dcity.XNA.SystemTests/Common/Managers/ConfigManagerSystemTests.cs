using System;
using System.Reflection;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class ConfigManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			ConfigManager = MyGame.Manager.ConfigManager;
			ConfigManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadGlobalConfigDataTest()
		{
			ConfigManager.LoadGlobalConfigData();
			GlobalConfigData data = ConfigManager.GlobalConfigData;

			Assert.IsNotNull(data);
			ShowConfigData(data);
		}

		[Test]
		public void LoadPlaformConfigDataTest()
		{
			ConfigManager.LoadPlaformConfigData(PlatformType.Mobiles);
			PlatformConfigData data = ConfigManager.PlatformConfigData;

			Assert.IsNotNull(data);
			ShowConfigData(data);
		}

		private static void ShowConfigData(object data)
		{
			var fields = data.GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
				object obj = field.GetValue(data);
				Assert.That(obj, Is.Not.Null, field.ToString());

				String message = String.Format("{0}={1}", field.Name, obj);
				Console.WriteLine(message);
			}
		}

		[TearDown]
		public void TearDown()
		{
			ConfigManager = null;
		}

	}
}