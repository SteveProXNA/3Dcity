using System;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Devices
{
	public class DesktopDeviceManager : IDeviceManager
	{
		public void Initialize()
		{
			BuildVersion = "1.0.0.";
			MaxPlayers = 4;

#if ANDROID
			BuildVersion = "1.0.0";
#endif
#if IOS
			BuildVersion = "1.0.0";
#endif
		}

		public void DrawTitle()
		{
			DrawTitle(Globalize.DRAW_TITLE);
		}
		public void DrawTitle(String title)
		{
			Engine.Game.Window.Title = title;
		}

		public void Abort()
		{
			MyGame.Manager.ThreadManager.Abort();
		}

		public String BuildVersion { get; private set; }
		public Byte MaxPlayers { get; private set; }
	}
}