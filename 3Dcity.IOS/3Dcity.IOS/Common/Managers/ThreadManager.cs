using System;
using System.Threading;

namespace WindowsGame.Common.Managers
{
	public interface IThreadManager
	{
		void Initialize();
		void LoadContentAsync();
		Boolean Join(Int32 millisecondsTimeout);
		void Abort();
	}
	public class ThreadManager : IThreadManager
	{
		private Thread backgroundThread;

		public void Initialize()
		{
			backgroundThread = new Thread(BackgroundLoadContent);
		}

		public void LoadContentAsync()
		{
			backgroundThread.Start();
		}

		public Boolean Join(Int32 millisecondsTimeout)
		{
			return backgroundThread.Join(millisecondsTimeout);
		}

		public void Abort()
		{
			if (null != backgroundThread)
			{
				backgroundThread.Abort();
				backgroundThread = null;
			}
		}
		private static void BackgroundLoadContent()
		{
			MyGame.LoadContentAsync();
		}

	}
}