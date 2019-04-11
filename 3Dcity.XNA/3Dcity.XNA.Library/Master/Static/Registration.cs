using WindowsGame.Master.Factorys;
using WindowsGame.Master.Implementation;
using WindowsGame.Master.Inputs;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.IoC;
using WindowsGame.Master.Managers;

namespace WindowsGame.Master.Static
{
	public static class Registration
	{
		public static void Initialize()
		{
			// Factorys.
			IoCContainer.Initialize<IContentFactory, ContentFactory>();
			IoCContainer.Initialize<ISoundFactory, SoundFactory>();
			IoCContainer.Initialize<IStorageFactory, StorageFactory>();

			// Inputs.
			IoCContainer.Initialize<IJoystickInput, JoystickInput>();
			IoCContainer.Initialize<IKeyboardInput, KeyboardInput>();
			IoCContainer.Initialize<IMouseScreenInput, MouseScreenInput>();
			IoCContainer.Initialize<ITouchScreenInput, TouchScreenInput>();

			// Managers.
			IoCContainer.Initialize<IRandomManager, RandomManager>();
			IoCContainer.Initialize<IResolutionManager, ResolutionManager>();
			IoCContainer.Initialize<IStopwatchManager, StopwatchManager>();

			IoCContainer.Initialize<IFileProxy, ProdFileProxy>();
			IoCContainer.Initialize<IFileManager, FileManager>();
		}

	}
}
