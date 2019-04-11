using System;
using System.Collections.Generic;
using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class CommandManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			CommandManager = MyGame.Manager.CommandManager;
			CommandManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadCommandDataTest()
		{
			const Byte commandId = 0;
			CommandManager.LoadCommandData(commandId);
			Assert.That(CommandManager.CommandTimeList, Is.Not.Null);
			Assert.That(CommandManager.CommandTypeList, Is.Not.Null);
			Assert.That(CommandManager.CommandArgsList, Is.Not.Null);
			ShowCommandData(commandId);
		}

		[Test]
		public void ParseCommandDataTest()
		{
			IList<String> lines = new List<string>();
			lines.Add("0.02,01,80:360");

			CommandManager.ParseCommandData(lines, 0);

			Assert.That(1, Is.EqualTo(CommandManager.CommandTypeList.Count));
		}

		private void ShowCommandData(Byte commandId)
		{
			IList<Single> eventTimeList = CommandManager.CommandTimeList[commandId];
			IList<String> eventTypeList = CommandManager.CommandTypeList[commandId];
			IList<String> eventArgsList = CommandManager.CommandArgsList[commandId];

			Assert.AreEqual(eventTimeList.Count, eventTypeList.Count);
			Assert.AreEqual(eventTypeList.Count, eventArgsList.Count);

			Byte count = (Byte)(eventTimeList.Count);
			for (Byte index = 0; index < count; ++index)
			{
				String message = String.Format("Index:{0} Time:{1} Type:{2} Args:{3}", index, eventTimeList[index], eventTypeList[index], eventArgsList[index]);
				Console.WriteLine(message);
			}

			Console.WriteLine();
		}

		[TearDown]
		public void TearDown()
		{
			CommandManager = null;
		}

	}
}