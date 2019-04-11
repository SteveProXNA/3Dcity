using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IEventManager
	{
		void Initialize();
		void ClearEvents();
		//void ProcessEvents(GameTime gameTime);
		void ProcessEvent(EventType eventType, ValueType eventArgs);

		//void AddLargeTargetMoveEvent(Vector2 position);
		//void AddSmallTargetMoveEvent(Vector2 position);

		String SerializeTypeData(IList<EventType> theEventTypeData);
		String SerializeArgsData(IList<ValueType> theEventArgsData);
		IList<EventType> DeserializeTypeText(String theEventTypeText);
		IList<ValueType> DeserializeArgsText(String theEventArgsText);
	}

	public class EventManager : IEventManager
	{
		//private IList<Single> eventTimeList;
		//private IList<String> eventTypeList;
		//private IList<String> eventArgsList;

		private IList<EventType> eventTypeData;
		private IList<ValueType> eventArgsData;

		private StringBuilder eventTypeBuilder;
		private StringBuilder eventArgsBuilder;

		//private Single delta;
		//private Single timer;

		public void Initialize()
		{
			//eventTimeList = new List<Single>();
			//eventTypeList = new List<String>();
			//eventArgsList = new List<String>();

			eventTypeData = new List<EventType>();
			eventArgsData = new List<ValueType>();

			eventTypeBuilder = new StringBuilder();
			eventArgsBuilder = new StringBuilder();

			//delta = 0.0f;
			//timer = 0.0f;
		}

		public void ClearEvents()
		{
			eventTypeData.Clear();
			eventArgsData.Clear();
		}

		//public void ProcessEvents(GameTime gameTime)
		//{
		//    delta += (Single) gameTime.ElapsedGameTime.TotalSeconds;
		//    if (0 == eventTypeData.Count)
		//    {
		//        return;
		//    }

		//    Byte count = (Byte)(eventTypeData.Count);
		//    for (Byte index = 0; index < count; ++index)
		//    {
		//        EventType eventType = eventTypeData[index];
		//        ValueType eventArgs = eventArgsData[index];

		//        ProcessEvent(eventType, eventArgs);
		//    }

		//    // Save events for later.
		//    String eventTypeText = SerializeTypeData(eventTypeData);
		//    String eventArgsText = SerializeArgsData(eventArgsData);

		//    timer = (Single)Math.Round(delta, 2);
		//    eventTimeList.Add(timer);
		//    eventTypeList.Add(eventTypeText);
		//    eventArgsList.Add(eventArgsText);

		//    delta = 0.0f;
		//    timer = 0.0f;
		//}

		public void ProcessEvent(EventType eventType, ValueType eventArgs)
		{
			switch (eventType)
			{
				case EventType.LargeTargetMove:
					LargeTargetMove(eventArgs);
					break;

				case EventType.SmallTargetMove:
					SmallTargetMove(eventArgs);
					break;
			}
		}

		//public void SerializeAllEvents()
		//{
		//    UInt16 count = (UInt16)(eventTimeList.Count);
		//    for (UInt16 index = 0; index < count; ++index)
		//    {
		//        String time = eventTimeList[index].ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
		//        String type = eventTypeList[index];
		//        String args = eventArgsList[index];
		//        String value = String.Format("{0},{1},{2}", time, type, args);
		//        System.Diagnostics.Debug.WriteLine(value);
		//    }

		//    System.Diagnostics.Debug.WriteLine(String.Empty);
		//}

		public String SerializeTypeData(IList<EventType> theEventTypeData)
		{
			eventTypeBuilder.Length = 0;
			if (0 == theEventTypeData.Count)
			{
				return String.Empty;
			}

			foreach (EventType eventType in theEventTypeData)
			{
				String value = ((Byte)eventType).ToString().PadLeft(2, '0');
				eventTypeBuilder.Append(value);
				eventTypeBuilder.Append(Constants.Delim1);
			}

			String data = eventTypeBuilder.ToString();
			data = data.Substring(0, data.Length - 1);
			return data;
		}

		public String SerializeArgsData(IList<ValueType> theEventArgsData)
		{
			eventArgsBuilder.Length = 0;
			if (0 == theEventArgsData.Count)
			{
				return String.Empty;
			}

			foreach (ValueType valueType in theEventArgsData)
			{
				if (valueType is Vector2)
				{
					Vector2 position = (Vector2)valueType;
					eventArgsBuilder.Append(Math.Round(position.X, 2).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
					eventArgsBuilder.Append(Constants.Delim2);
					eventArgsBuilder.Append(Math.Round(position.Y, 2).ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
					eventArgsBuilder.Append(Constants.Delim1);
				}
			}

			String data = eventArgsBuilder.ToString();
			data = data.Substring(0, data.Length - 1);
			return data;
		}

		public IList<EventType> DeserializeTypeText(String theEventTypeText)
		{
			eventTypeData.Clear();

			String[] theList = theEventTypeText.Split(Constants.Delim1);
			for (Byte index = 0; index < theList.Length; ++index)
			{
				String theText = theList[index];
				if (0 == theText.Length)
				{
					continue;
				}

				EventType eventType = (EventType)Enum.Parse(typeof(EventType), theText, true);
				eventTypeData.Add(eventType);
			}

			return eventTypeData;
		}

		public IList<ValueType> DeserializeArgsText(String theEventArgsText)
		{
			eventArgsData.Clear();

			String[] theList = theEventArgsText.Split(Constants.Delim1);
			for (Byte index = 0; index < theList.Length; ++index)
			{
				String theText = theList[index];
				if (0 == theText.Length)
				{
					continue;
				}

				// Position [Vector2]
				if (theText.Contains(Constants.Delim2[0].ToString()))
				{
					Vector2 position = Vector2.Zero;
					String[] data = theText.Split(Constants.Delim2);
					position.X = Convert.ToSingle(data[0]);
					position.Y = Convert.ToSingle(data[1]);
					eventArgsData.Add(position);
				}
			}

			return eventArgsData;
		}

		//public void AddLargeTargetMoveEvent(Vector2 position)
		//{
		//    AddEvent(EventType.LargeTargetMove, position);
		//}
		//public void AddSmallTargetMoveEvent(Vector2 position)
		//{
		//    AddEvent(EventType.SmallTargetMove, position);
		//}

		//private void AddEvent(EventType type, ValueType args)
		//{
		//    eventTypeData.Add(type);
		//    eventArgsData.Add(args);
		//}

		private static void LargeTargetMove(ValueType eventArgs)
		{
			Vector2 position = (Vector2)eventArgs;
			MyGame.Manager.SpriteManager.SetPosition(SpriteType.LargeTarget, position);
		}

		private static void SmallTargetMove(ValueType eventArgs)
		{
			Vector2 position = (Vector2)eventArgs;
			MyGame.Manager.SpriteManager.SetPosition(SpriteType.SmallTarget, position);
		}

	}
}
