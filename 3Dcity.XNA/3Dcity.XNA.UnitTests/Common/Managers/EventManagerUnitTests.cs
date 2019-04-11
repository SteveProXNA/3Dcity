using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;

namespace WindowsGame.UnitTests.Common.Managers
{
	[TestFixture]
	public class EventManagerUnitTests : BaseUnitTests
	{
		[SetUp]
		public new void SetUp()
		{
			// System under test.
			EventManager = new EventManager();
			EventManager.Initialize();
			base.SetUp();
		}

		[Test]
		public void SerializeTypeDataNoneTest()
		{
			// Arrange.
			IList<EventType> eventTypeData = new List<EventType>();

			// Act.
			String result = EventManager.SerializeTypeData(eventTypeData);

			// Assert.
			Assert.That(String.Empty, Is.EqualTo(result));
		}
		[Test]
		public void SerializeTypeDataOnceTest()
		{
			// Arrange.
			IList<EventType> eventTypeData = new List<EventType>();
			eventTypeData.Add(EventType.LargeTargetMove);

			// Act.
			String result = EventManager.SerializeTypeData(eventTypeData);

			// Assert.
			Assert.That("00", Is.EqualTo(result));
		}
		[Test]
		public void SerializeTypeDataTwiceTest()
		{
			// Arrange.
			IList<EventType> eventTypeData = new List<EventType>();
			eventTypeData.Add(EventType.LargeTargetMove);
			eventTypeData.Add(EventType.SmallTargetMove);

			// Act.
			String result = EventManager.SerializeTypeData(eventTypeData);

			// Assert.
			Assert.That("00|01", Is.EqualTo(result));
		}

		[Test]
		public void SerializeTypeArgsNoneTest()
		{
			// Arrange.
			IList<ValueType> eventTypeArgs = new List<ValueType>();

			// Act.
			String result = EventManager.SerializeArgsData(eventTypeArgs);

			// Assert.
			Assert.That(String.Empty, Is.EqualTo(result));
		}
		[Test]
		public void SerializeTypeArgsOnceTest()
		{
			// Arrange.
			IList<ValueType> eventTypeArgs = new List<ValueType>();
			eventTypeArgs.Add(new Vector2(372.2f, 250));

			// Act.
			String result = EventManager.SerializeArgsData(eventTypeArgs);

			// Assert.
			Assert.That("372.2:250", Is.EqualTo(result));
		}
		[Test]
		public void SerializeTypeArgsTwiceTest()
		{
			// Arrange.
			IList<ValueType> eventTypeArgs = new List<ValueType>();
			eventTypeArgs.Add(new Vector2(372.2f, 250));
			eventTypeArgs.Add(new Vector2(87.5f, 360));

			// Act.
			String result = EventManager.SerializeArgsData(eventTypeArgs);

			// Assert.
			Assert.That("372.2:250|87.5:360", Is.EqualTo(result));
		}

		[Test]
		public void DeserializeTypeTextNoneTest()
		{
			// Arrange.
			String theEventTypeText = String.Empty;

			// Act.
			IList<EventType> result = EventManager.DeserializeTypeText(theEventTypeText);

			// Assert.
			Assert.That(0, Is.EqualTo(result.Count));
		}
		[Test]
		public void DeserializeTypeTextOnceTest()
		{
			// Arrange.
			const String theEventTypeText = "00";

			// Act.
			IList<EventType> result = EventManager.DeserializeTypeText(theEventTypeText);

			// Assert.
			Assert.That(1, Is.EqualTo(result.Count));
			Assert.That(EventType.LargeTargetMove, Is.EqualTo(result[0]));
		}
		[Test]
		public void DeserializeTypeTextTwiceTest()
		{
			// Arrange.
			const String theEventTypeText = "00|01";

			// Act.
			IList<EventType> result = EventManager.DeserializeTypeText(theEventTypeText);

			// Assert.
			Assert.That(2, Is.EqualTo(result.Count));
			Assert.That(EventType.LargeTargetMove, Is.EqualTo(result[0]));
			Assert.That(EventType.SmallTargetMove, Is.EqualTo(result[1]));
		}

		[Test]
		public void DeserializeArgsTextNoneTest()
		{
			// Arrange.
			String theEventArgsText = String.Empty;

			// Act.
			IList<ValueType> result = EventManager.DeserializeArgsText(theEventArgsText);

			// Assert.
			Assert.That(0, Is.EqualTo(result.Count));
		}
		[Test]
		public void DeserializeArgsTextOnceTest()
		{
			// Arrange.
			const String theEventArgsText = "80:360";

			// Act.
			IList<ValueType> result = EventManager.DeserializeArgsText(theEventArgsText);

			// Assert.
			Assert.That(1, Is.EqualTo(result.Count));
			Vector2 position = (Vector2)result[0];
			Assert.That(80, Is.EqualTo(position.X));
			Assert.That(360, Is.EqualTo(position.Y));
		}
		[Test]
		public void DeserializeArgsTextTwiceTest()
		{
			// Arrange.
			const String theEventArgsText = "348.99:241.88|87.5:360";

			// Act.
			IList<ValueType> result = EventManager.DeserializeArgsText(theEventArgsText);

			// Assert.
			Assert.That(2, Is.EqualTo(result.Count));
			Vector2 position = (Vector2)result[1];
			Assert.That(87.5f, Is.EqualTo(position.X));
			Assert.That(360, Is.EqualTo(position.Y));
		}

		[TearDown]
		public void TearDown()
		{
			CollisionManager = null;
		}

	}
}
