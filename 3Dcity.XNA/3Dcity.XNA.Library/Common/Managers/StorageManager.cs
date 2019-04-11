using System;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master.Factorys;

namespace WindowsGame.Common.Managers
{
	public interface IStorageManager
	{
		void Initialize();
		void Initialize(String fileName);

		void LoadContent();
		void SaveContent();
	}

	public class StorageManager : IStorageManager
	{
		private readonly IStorageFactory storageFactory;
		private StoragePersistData storagePersistData;

		public StorageManager(IStorageFactory storageFactory)
		{
			this.storageFactory = storageFactory;
		}

		public void Initialize()
		{
			Initialize("GameData.xml");
		}

		public void Initialize(String fileName)
		{
			storageFactory.Initialize(fileName);
		}

		public void LoadContent()
		{
			if (MyGame.Manager.ConfigManager.GlobalConfigData.DonotSave)
			{
				return;
			}

			storagePersistData = storageFactory.LoadContent<StoragePersistData>();
			if (null == storagePersistData)
			{
				return;
			}

			MyGame.Manager.SoundManager.SetPlayAudio(storagePersistData.PlayAudio);
			MyGame.Manager.StateManager.UpdateGameSound();

			MyGame.Manager.StateManager.SetCoolMusic(storagePersistData.CoolMusic);
			MyGame.Manager.LevelManager.SetLevelType(storagePersistData.LevelType);
			MyGame.Manager.LevelManager.SetLevelIndex(storagePersistData.LevelIndex);
			MyGame.Manager.ScoreManager.SetHighScore(storagePersistData.HighScore);
		}

		public void SaveContent()
		{
			if (MyGame.Manager.ConfigManager.GlobalConfigData.DonotSave)
			{
				return;
			}

			if (null == storagePersistData)
			{
				storagePersistData = new StoragePersistData
				{
					HighScore = Constants.DEF_HIGH_SCORE,
					PlayAudio = MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio,
					CoolMusic = MyGame.Manager.ConfigManager.GlobalConfigData.CoolMusic,
					LevelType = MyGame.Manager.ConfigManager.GlobalConfigData.LevelType,
					LevelIndex = (Byte)(MyGame.Manager.ConfigManager.GlobalConfigData.LevelNo - 1),
				};
			}
			else
			{
				storagePersistData.HighScore = MyGame.Manager.ScoreManager.HighScore;
				storagePersistData.PlayAudio = MyGame.Manager.SoundManager.PlayAudio;
				storagePersistData.CoolMusic = MyGame.Manager.StateManager.CoolMusic;
				storagePersistData.LevelType = MyGame.Manager.LevelManager.LevelType;
				storagePersistData.LevelIndex = MyGame.Manager.LevelManager.LevelIndex;
			}

			storageFactory.SaveContent(storagePersistData);
		}

	}
}
