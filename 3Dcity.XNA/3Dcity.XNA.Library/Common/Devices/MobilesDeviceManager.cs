using System;
using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Devices
{
	public class MobilesDeviceManager : IDeviceManager
	{
		public void Initialize()
		{
			BuildVersion = "1.0.0.";
			MaxPlayers = 1;

#if ANDROID
			BuildVersion = "1.0.0";
#endif
#if IOS
			BuildVersion = "1.0.0";
#endif
		}

		public void DrawTitle()
		{
		}

		public void Abort()
		{
		}

		public String BuildVersion { get; private set; }
		public Byte MaxPlayers { get; private set; }
	}
}