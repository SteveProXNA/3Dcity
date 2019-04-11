using System;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IConfigManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadGlobalConfigData();
		void LoadPlaformConfigData(PlatformType platformType);

		GlobalConfigData GlobalConfigData { get; }
		PlatformConfigData PlatformConfigData { get; }
	}

	public class ConfigManager : IConfigManager 
	{
		private String configRoot;

		private const String CONFIG_DIRECTORY = "Config";
		private const String GLOBAL_CONFIG_FILENAME = "GlobalConfig.xml";
		public const String PLATFORM_CONFIG_FILENAME = "PlatformConfig{0}.xml";

		public void Initialize()
		{
			Initialize(String.Empty);
		}
		public void Initialize(String root)
		{
			configRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, CONFIG_DIRECTORY);
		}

		public void LoadContent()
		{
			LoadGlobalConfigData();
			LoadPlaformConfigData(Constants.PlatformType);
		}

		public void LoadGlobalConfigData()
		{
			String file = String.Format("{0}/{1}", configRoot, GLOBAL_CONFIG_FILENAME);
			GlobalConfigData = MyGame.Manager.FileManager.LoadXml<GlobalConfigData>(file);
		}

		public void LoadPlaformConfigData(PlatformType platformType)
		{
			String name = PLATFORM_CONFIG_FILENAME.Replace("{0}", platformType.ToString());
			String file = String.Format("{0}/{1}", configRoot, name);
			PlatformConfigData = MyGame.Manager.FileManager.LoadXml<PlatformConfigData>(file);
		}

		public GlobalConfigData GlobalConfigData { get; private set; }
		public PlatformConfigData PlatformConfigData { get; private set; }
	}
}
