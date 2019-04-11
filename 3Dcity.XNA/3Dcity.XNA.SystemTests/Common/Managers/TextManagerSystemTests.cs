using System;
using System.Collections.Generic;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Screens;
using WindowsGame.Master.Objects;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class TextManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			TextManager = MyGame.Manager.TextManager;
			TextManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadTextDataTest()
		{
			String screen = typeof (TestScreen).Name;
			IList<TextData> list = TextManager.LoadTextData(screen);

			Assert.IsNotNull(list);
			ShowTextData(list);
		}

		private static void ShowTextData(IEnumerable<TextData> list)
		{
			foreach (TextData data in list)
			{
				Console.WriteLine(data.Text);
			}
		}

		[TearDown]
		public void TearDown()
		{
			ConfigManager = null;
		}

	}
}
