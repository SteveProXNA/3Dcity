using WindowsGame.Master.IoC;

namespace WindowsGame.Common.TheGame
{
	public static class GameFactory
	{
		public static IGameManager Resolve()
		{
			return IoCContainer.Resolve<IGameManager>();
		}

		public static void Release()
		{
			IoCContainer.Release();
		}
	}
}