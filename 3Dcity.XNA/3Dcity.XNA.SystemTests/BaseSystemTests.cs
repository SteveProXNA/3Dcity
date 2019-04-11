using System;
using NUnit.Framework;
using WindowsGame.Common;
using WindowsGame.Common.Managers;
using WindowsGame.Common.Static;
using WindowsGame.Common.TheGame;
using WindowsGame.Master.Interfaces;
using WindowsGame.Master.IoC;
using WindowsGame.Master.Managers;
using WindowsGame.SystemTests.Master.Implementation;

namespace WindowsGame.SystemTests
{
	public abstract class BaseSystemTests
	{
		protected ICollisionManager CollisionManager;
		protected ICommandManager CommandManager;
		protected IConfigManager ConfigManager;
		protected IDelayManager DelayManager;
		protected IEnemyManager EnemyManager;
		protected ILevelManager LevelManager;
		protected IStateManager StateManager;
		protected IStopwatchManager StopwatchManager;

		protected ITextManager TextManager;

		// mklink /D C:\3DCity.Content D:\SVN\3Dcity\3Dcity.XNA\3Dcity.XNA\3Dcity.XNA\bin\x86\Debug\
		// mklink /D C:\3DCity.Content E:\GitHub\StevePro7\3Dcity\3Dcity.XNA\3Dcity.XNA\3Dcity.XNA\bin\x86\Debug\
		protected const String CONTENT_ROOT = @"C:\3DCity.Content\";

#pragma warning disable 618
		[TestFixtureSetUp]
#pragma warning restore 618
		public void TestFixtureSetUp()
		{
			Registration.Initialize();
			IoCContainer.Initialize<IFileProxy, TestFileProxy>();

			IGameManager manager = GameFactory.Resolve();
			MyGame.Construct(manager);
		}

#pragma warning disable 618
		[TestFixtureTearDown]
#pragma warning restore 618
		public void TestFixtureTearDown()
		{
			GameFactory.Release();
		}

	}
}