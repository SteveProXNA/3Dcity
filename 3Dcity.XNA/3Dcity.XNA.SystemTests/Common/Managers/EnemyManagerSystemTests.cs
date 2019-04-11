using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class EnemyManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			EnemyManager = MyGame.Manager.EnemyManager;
		}

		[Test]
		public void Test()
		{
			Assert.That(1, Is.EqualTo(1));
		}

		[TearDown]
		public void TearDown()
		{
			EnemyManager = null;
		}

	}
}
