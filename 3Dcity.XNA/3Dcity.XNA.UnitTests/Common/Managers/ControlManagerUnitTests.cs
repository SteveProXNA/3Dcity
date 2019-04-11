using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using WindowsGame.Common.Managers;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class ControlManagerUnitTests : BaseUnitTests
	{
		private Int32 posX, posY;
		private Int32 collX, collY, collW, collH;
		private Int32 sizeX, sizeY, sizeW, sizeH;

		[SetUp]
		public new void SetUp()
		{
			// System under test.
			ControlManager = new ControlManager();
			base.SetUp();
		}

		[Test]
		public void CheckPosInRectTest01()
		{
			posX = 100; posY = 300;
			collX = 0; collY = 280; collW = 200; collH = 200;

			Vector2 position = new Vector2(posX, posY);
			Rectangle collision = new Rectangle(collX, collY, collW, collH);

			Boolean posInRect = ControlManager.CheckPosInRect(position, collision);

			Assert.That(true, Is.EqualTo(posInRect));
		}

		[Test]
		public void CheckPosInRectTest02()
		{
			posX = 300; posY = 300;
			collX = 0; collY = 280; collW = 200; collH = 200;

			Vector2 position = new Vector2(posX, posY);
			Rectangle collision = new Rectangle(collX, collY, collW, collH);

			Boolean posInRect = ControlManager.CheckPosInRect(position, collision);

			Assert.That(false, Is.EqualTo(posInRect));
		}

		[Test]
		public void ClampPosInRectTest01()
		{
			posX = 250; posY = 200;
			sizeX = 0; sizeY = 280; sizeW = 200; sizeH = 200;

			Vector2 position = new Vector2(posX, posY);
			Rectangle bounds = new Rectangle(sizeX, sizeY, sizeW, sizeH);

			position = ControlManager.ClampPosInRect(position, bounds);

			Assert.That(200, Is.EqualTo(position.X));
			Assert.That(280, Is.EqualTo(position.Y));
		}

		[Test]
		public void ClampPosInRectTest02()
		{
			posX = 50; posY = 580;
			sizeX = 0; sizeY = 280; sizeW = 200; sizeH = 200;

			Vector2 position = new Vector2(posX, posY);
			Rectangle bounds = new Rectangle(sizeX, sizeY, sizeW, sizeH);

			position = ControlManager.ClampPosInRect(position, bounds);

			Assert.That(50, Is.EqualTo(position.X));
			Assert.That(480, Is.EqualTo(position.Y));
		}

		[Test]
		public void CalcJoyPadPosnTest01()
		{
			const Single space = 200.0f;
			const Single coord = 51.0f;
			const Single bound = 0.0f;

			Single value = ControlManager.CalcJoyPadPosn(space, coord, bound);

			Assert.That(-0.49f, Is.EqualTo(value));
		}

		[Test]
		public void CalcJoyPadPosnTest02()
		{
			const Single space = 200.0f;
			const Single coord = 426.0f;
			const Single bound = 280.0f;

			Single value = ControlManager.CalcJoyPadPosn(space, coord, bound);

			Assert.That(0.46f, Is.EqualTo(value));
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
