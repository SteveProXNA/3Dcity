using NUnit.Framework;
using WindowsGame.Common;

namespace WindowsGame.SystemTests.Common.Managers
{
	[TestFixture]
	public class CollisionManagerSystemTests : BaseSystemTests
	{
		[SetUp]
		public void SetUp()
		{
			// System under test.
			CollisionManager = MyGame.Manager.CollisionManager;
			CollisionManager.Initialize(CONTENT_ROOT);
		}

		[Test]
		public void LoadContentEnemysTest()
		{
			CollisionManager.LoadContentEnemys();
			Assert.That(CollisionManager.EnemysList, Is.Not.Null);
		}

		[Test]
		public void LoadContentTargetTest()
		{
			CollisionManager.LoadContentTarget();
			Assert.That(CollisionManager.TargetList, Is.Not.Null);
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
