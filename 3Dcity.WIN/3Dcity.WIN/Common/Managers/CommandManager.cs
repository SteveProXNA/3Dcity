using System;
using System.Collections.Generic;
using System.Globalization;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface ICommandManager
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadCommandData(Byte commandId);
		void ParseCommandData(IList<String> lines, Byte commandId);

		IDictionary<Byte, IList<Single>> CommandTimeList { get; }
		IDictionary<Byte, IList<String>> CommandTypeList { get; }
		IDictionary<Byte, IList<String>> CommandArgsList { get; }
	}

	public class CommandManager : ICommandManager
	{
		private String commandRoot;
		private Single ratio;

		private const String EVENTS_DIRECTORY = "Events";

		public void Initialize()
		{
			Initialize(String.Empty);
		}
		public void Initialize(String root)
		{
			commandRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, EVENTS_DIRECTORY);

			CommandTimeList = new Dictionary<Byte, IList<Single>>();
			CommandTypeList = new Dictionary<Byte, IList<String>>();
			CommandArgsList = new Dictionary<Byte, IList<String>>();

			ratio = 1.0f;
		}

		public void LoadContent()
		{
			// Check if ratio configured externally for testing.
			ratio = MyGame.Manager.ConfigManager.GlobalConfigData.EventRatio;

			LoadCommandData(0);
			LoadCommandData(1);
			LoadCommandData(2);
		}

		public void LoadCommandData(Byte commandId)
		{
			if (CommandTimeList.ContainsKey(commandId))
			{
				return;
			}

			String file = String.Format("{0}/{1}.txt", commandRoot, commandId.ToString().PadLeft(2, '0'));
			var lines = MyGame.Manager.FileManager.LoadTxt(file);
			ParseCommandData(lines, commandId);
		}

		public void ParseCommandData(IList<String> lines, Byte commandId)
		{
			if (CommandTimeList.ContainsKey(commandId))
			{
				return;
			}

			IList<Single> eventTimeList = new List<Single>();
			IList<String> eventTypeList = new List<String>();
			IList<String> eventArgsList = new List<String>();

			UInt16 count = (UInt16)(lines.Count);
			for (UInt16 index = 0; index < count; ++index)
			{
				String line = lines[index];
				String[] items = line.Split(Constants.Delim0);

				Single time = Convert.ToSingle(items[0], CultureInfo.InvariantCulture) * ratio;
				eventTimeList.Add(time);
				eventTypeList.Add(items[1]);
				eventArgsList.Add(items[2]);
			}

			CommandTimeList.Add(commandId, eventTimeList);
			CommandTypeList.Add(commandId, eventTypeList);
			CommandArgsList.Add(commandId, eventArgsList);
		}


		public IDictionary<Byte, IList<Single>> CommandTimeList { get; private set; }
		public IDictionary<Byte, IList<String>> CommandTypeList { get; private set; }
		public IDictionary<Byte, IList<String>> CommandArgsList { get; private set; }

	}
}
