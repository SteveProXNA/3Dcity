using NUnit.Framework;
using WindowsGame.Common.Managers;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class SpriteManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			SpriteManager = new SpriteManager();
			base.SetUp();
		}

		[Test]
		public void MyTest()
		{
			SpriteManager.Initialize();
			SpriteManager.SetMovement(false, 0, 0);
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}
	}
}
