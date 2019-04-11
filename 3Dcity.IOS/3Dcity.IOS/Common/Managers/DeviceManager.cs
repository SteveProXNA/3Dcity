using System;
using WindowsGame.Common.Interfaces;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Managers
{
	public interface IDeviceManager 
	{
		void Initialize();
	}

	public class DeviceManager : IDeviceManager 
	{
		private readonly IDeviceFactory deviceFactory;

		public DeviceManager(IDeviceFactory deviceFactory)
		{
			this.deviceFactory = deviceFactory;
		}

		public void Initialize()
		{
			deviceFactory.Initialize();
		}

		public void Update(GameTime gameTime)
		{
		}

	}
}
