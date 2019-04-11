using WindowsGame.Common.Devices;
using WindowsGame.Common.Inputs;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Implementation;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.IoC;
using TheRegistration = WindowsGame.Master.Static.Registration;

namespace WindowsGame.Common.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			// Initialize engine first.
			TheRegistration.Initialize();

			IoCContainer.Initialize<IGameManager, GameManager>();

			IoCContainer.Initialize<IBossManager, BossManager>();
			IoCContainer.Initialize<IBulletManager, BulletManager>();
			IoCContainer.Initialize<ICollisionManager, CollisionManager>();
			IoCContainer.Initialize<ICommandManager, CommandManager>();
			IoCContainer.Initialize<IConfigManager, ConfigManager>();
			IoCContainer.Initialize<IContentManager, ContentManager>();
			IoCContainer.Initialize<IControlManager, ControlManager>();
			IoCContainer.Initialize<IDebugManager, DebugManager>();
			IoCContainer.Initialize<IDelayManager, DelayManager>();
			IoCContainer.Initialize<IEnemyManager, EnemyManager>();
			IoCContainer.Initialize<IEventManager, EventManager>();
			IoCContainer.Initialize<IExplosionManager, ExplosionManager>();
			IoCContainer.Initialize<IIconManager, IconManager>();
			IoCContainer.Initialize<IImageManager, ImageManager>();
			IoCContainer.Initialize<ILevelManager, LevelManager>();
			IoCContainer.Initialize<IMoverManager, MoverManager>(); 
			IoCContainer.Initialize<IRenderManager, RenderManager>();
			IoCContainer.Initialize<IScoreManager, ScoreManager>();
			IoCContainer.Initialize<IScreenManager, ScreenManager>();
			IoCContainer.Initialize<ISoundManager, SoundManager>();
			IoCContainer.Initialize<ISpriteManager, SpriteManager>();
			IoCContainer.Initialize<IStateManager, StateManager>();
			IoCContainer.Initialize<IStorageManager, StorageManager>();
			IoCContainer.Initialize<ITextManager, TextManager>();
			IoCContainer.Initialize<IThreadManager, ThreadManager>();


#if WINDOWS
			IoCContainer.Initialize<IDeviceManager, DesktopDeviceManager>();
			IoCContainer.Initialize<IInputManager, DesktopInputManager>();
			IoCContainer.Initialize<ILogger, ProdLogger>();
#endif

#if WINDOWS_UAP
			IoCContainer.Initialize<IDeviceManager, DesktopDeviceManager>();
			IoCContainer.Initialize<IInputManager, DesktopInputManager>();
			IoCContainer.Initialize<ILogger, TestLogger>();
#endif

#if !WINDOWS && !WINDOWS_UAP
			IoCContainer.Initialize<IDeviceManager, MobilesDeviceManager>();
			IoCContainer.Initialize<IInputManager, MobilesInputManager>();
			IoCContainer.Initialize<ILogger, TestLogger>();
#endif

		}
	}
}