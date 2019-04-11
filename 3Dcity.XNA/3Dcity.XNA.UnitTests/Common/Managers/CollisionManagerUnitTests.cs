using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class CollisionManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			CollisionManager = new CollisionManager();
			CollisionManager.Initialize(String.Empty);
			base.SetUp();
		}

		[Test]
		public void BoxesCollisionOnceTest()
		{
			// Arrange.
			const Byte radius = 64;
			Vector2 enemysPosition = new Vector2(354, 141);
			Vector2 targetPosition = new Vector2(368, 168);

			// Act.
			var collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);

			// Assert.
			Assert.True(collide);
		}

		[Test]
		public void BoxesCollisionManyTest()
		{
			const Byte radius = 64;
			Vector2 enemysPosition = new Vector2(100, 100);

			// Left.
			var targetPosition = new Vector2(30, 100);
			var collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(36, 100);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);

			// Top.
			targetPosition = new Vector2(120, 32);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(36, 36);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);

			// Right.
			targetPosition = new Vector2(230, 100);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(220, 100);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);

			// Down.
			targetPosition = new Vector2(100, 224);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.False(collide);
			targetPosition = new Vector2(120, 200);
			collide = CollisionManager.BoxesCollision(radius, enemysPosition, targetPosition);
			Assert.True(collide);
		}

		[Test]
		public void BulletCollideEnemyTest()
		{
			Vector2 enemysPosition = new Vector2(200, 100);
			Vector2 bulletPosition = new Vector2(228, 164);
			const LevelType levelType = LevelType.Easy;
			const Byte enemyFrame = 5;

			Boolean collide = CollisionManager.BulletCollideEnemy(enemysPosition, bulletPosition, levelType, enemyFrame);

			Assert.That(collide, Is.True);
		}

		[Test]
		public void DetermineEnemySlotTest()
		{
			var position = new Vector2(156, 275);
			var index = CollisionManager.DetermineEnemySlot(position);
			Assert.That(index, Is.EqualTo(Constants.INVALID_INDEX));

			position = new Vector2(157, 175);
			index = CollisionManager.DetermineEnemySlot(position);
			Assert.That(index, Is.EqualTo(1));
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
