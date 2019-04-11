using System.Reflection;
using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.TheGame
{
	[TestFixture]
	public class GameManagerTests : BaseSystemTests
	{
		[Test]
		public void RegistrationTest()
		{
			var manager = MyGame.Manager;
			var properties = manager.GetType().GetProperties();
			foreach (PropertyInfo property in properties)
			{
				object obj = property.GetValue(manager, null);
				Assert.That(obj, Is.Not.Null, property.ToString());
			}
		}

	}
}