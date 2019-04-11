using NUnit.Framework;
using Rhino.Mocks;
using WindowsGame.Common;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.Managers;

namespace WindowsGame.UnitTests
{
	public abstract class BaseUnitTests
	{
		protected IBossManager BossManager;
		protected IBulletManager BulletManager;
		protected ICollisionManager CollisionManager;
		protected ICommandManager CommandManager;
		protected IConfigManager ConfigManager;
		protected IContentManager ContentManager;
		protected IControlManager ControlManager;
		protected IDebugManager DebugManager;
		protected IDelayManager DelayManager;
		protected IDeviceManager DeviceManager;
		protected IEnemyManager EnemyManager;
		protected IEventManager EventManager;
		protected IExplosionManager ExplosionManager;
		protected IIconManager IconManager;
		protected IImageManager ImageManager;
		protected IInputManager InputManager;
		protected ILevelManager LevelManager;
		protected IMoverManager MoverManager;
		protected IRandomManager RandomManager;
		protected IRenderManager RenderManager;
		protected IResolutionManager ResolutionManager;
		protected IScoreManager ScoreManager;
		protected IScreenManager ScreenManager;
		protected ISoundManager SoundManager;
		protected ISpriteManager SpriteManager;
		protected IStateManager StateManager;
		protected IStopwatchManager StopwatchManager;
		protected IStorageManager StorageManager;
		protected ITextManager TextManager;
		protected IThreadManager ThreadManager;
		protected IFileManager FileManager;
		protected ILogger Logger;

#pragma warning disable 618
		[TestFixtureSetUp]
#pragma warning restore 618
		public void TestFixtureSetUp()
		{
			BossManager = MockRepository.GenerateStub<IBossManager>();
			BulletManager = MockRepository.GenerateStub<IBulletManager>();
			CollisionManager = MockRepository.GenerateStub<ICollisionManager>();
			CommandManager = MockRepository.GenerateStub<ICommandManager>();
			ConfigManager = MockRepository.GenerateStub<IConfigManager>();
			ContentManager = MockRepository.GenerateStub<IContentManager>();
			ControlManager = MockRepository.GenerateStub<IControlManager>();
			DebugManager = MockRepository.GenerateStub<IDebugManager>();
			DelayManager = MockRepository.GenerateStub<IDelayManager>();
			DeviceManager = MockRepository.GenerateStub<IDeviceManager>();
			EnemyManager = MockRepository.GenerateStub<IEnemyManager>();
			EventManager = MockRepository.GenerateStub<IEventManager>();
			ExplosionManager = MockRepository.GenerateStub<IExplosionManager>();
			IconManager = MockRepository.GenerateStub<IIconManager>();
			ImageManager = MockRepository.GenerateStub<IImageManager>();
			InputManager = MockRepository.GenerateStub<IInputManager>();
			LevelManager = MockRepository.GenerateStub<ILevelManager>();
			MoverManager = MockRepository.GenerateStub<IMoverManager>();
			RandomManager = MockRepository.GenerateStub<IRandomManager>();
			RenderManager = MockRepository.GenerateStub<IRenderManager>();
			ResolutionManager = MockRepository.GenerateStub<IResolutionManager>();
			ScoreManager = MockRepository.GenerateStub<IScoreManager>();
			ScreenManager = MockRepository.GenerateStub<IScreenManager>();
			SoundManager = MockRepository.GenerateStub<ISoundManager>();
			SpriteManager = MockRepository.GenerateStub<ISpriteManager>();
			StateManager = MockRepository.GenerateStub<IStateManager>();
			StopwatchManager = MockRepository.GenerateStub<IStopwatchManager>();
			StorageManager = MockRepository.GenerateStub<IStorageManager>();
			TextManager = MockRepository.GenerateStub<ITextManager>();
			ThreadManager = MockRepository.GenerateStub<IThreadManager>();
			FileManager = MockRepository.GenerateStub<IFileManager>();
			Logger = MockRepository.GenerateStub<ILogger>();
		}

		protected void SetUp()
		{
			IGameManager manager = new GameManager
			(
				BossManager,
				BulletManager,
				CollisionManager,
				CommandManager,
				ConfigManager,
				ContentManager,
				ControlManager,
				DebugManager,
				DelayManager,
				DeviceManager,
				EnemyManager,
				EventManager,
				ExplosionManager,
				IconManager,
				ImageManager,
				InputManager,
				LevelManager,
				MoverManager,
				RandomManager,
				RenderManager,
				ResolutionManager,
				ScoreManager,
				ScreenManager,
				SoundManager,
				SpriteManager,
				StateManager,
				StopwatchManager,
				StorageManager,
				TextManager,
				ThreadManager,
				FileManager,
				Logger
			);

			MyGame.Construct(manager);
		}

#pragma warning disable 618
		[TestFixtureTearDown]
#pragma warning restore 618
		public void TestFixtureTearDown()
		{
			BossManager = null;
			BulletManager = null;
			CollisionManager = null;
			CommandManager = null;
			ConfigManager = null;
			ContentManager = null;
			ControlManager = null;
			DebugManager = null;
			DelayManager = null;
			DeviceManager = null;
			EnemyManager = null;
			EventManager = null;
			ExplosionManager = null;
			IconManager = null;
			ImageManager = null;
			InputManager = null;
			LevelManager = null;
			MoverManager = null;
			RandomManager = null;
			RenderManager = null;
			ResolutionManager = null;
			ScoreManager = null;
			ScreenManager = null;
			SoundManager = null;
			SpriteManager = null;
			StateManager = null;
			StopwatchManager = null;
			StorageManager = null;
			TextManager = null;
			ThreadManager = null;
			FileManager = null;
			Logger = null;
		}

	}
}
