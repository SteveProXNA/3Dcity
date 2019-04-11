using System;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Data
{
	public struct GlobalConfigData
	{
		public ScreenType ScreenType;
		public Byte LevelNo;
		public LevelType LevelType;
		public Byte FramesPerSecond;
		public Byte MaximLevel;
		public Byte MaxBullets;
		public Boolean LoadAudio;
		public Boolean PlayAudio;
		public UInt16 StarDelay;
		public UInt16 SplashDelay;
		public UInt16 SelectDelay;
		public UInt16 IntroDelay;
		public UInt16 FlashDelay;
		public UInt16 LoadDelay;
		public UInt16 ReadyDelay;
		public UInt16 FinishDelay;
		public UInt16 DeadDelay;
		public UInt16 ResumeDelay;
		public UInt16 OverDelay;
		public UInt16 BeatDelay;
		public UInt16 ScoreDelay;
		public Single EventRatio;
		public Boolean EnemyBlink;
		public Boolean ScoreBlink;
		public Boolean UpdateStar;
		public Boolean UpdateGrid;
		public Boolean RenderBack;
		public Boolean RenderIcon;
		public Boolean CoolMusic;
		public Boolean BossBig;
		public Boolean IsGodMode;
		public Boolean UnlimitedCont;
		public Boolean BackBorders;
		public Boolean DebugTester;
		public Boolean DonotSave;
		public Boolean QuitsToExit;
	}
}
