using System;
using Microsoft.Xna.Framework;
using WindowsGame.Master;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class ExitScreen : BaseScreen, IScreen 
	{
		public override Int32 Update(GameTime gameTime)
		{
			MyGame.Manager.SoundManager.StopMusic();
			MyGame.Manager.StorageManager.SaveContent();

#if ANDROID
	// Android
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
			System.Environment.Exit(0);
			return (Int32)CurrScreen;
#endif
#if IOS
	// iOS
			throw new System.DivideByZeroException();
#endif
#if !IOS && !ANDROID
			// Default.
			Engine.Game.Exit();
			return (Int32) CurrScreen;
#endif
		}

	}
}
