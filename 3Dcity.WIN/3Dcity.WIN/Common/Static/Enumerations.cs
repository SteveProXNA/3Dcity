namespace WindowsGame.Common.Static
{
	public enum PlatformType
	{
		Desktop = 0,
		Mobiles = 1
	}

	public enum LevelType
	{
		Easy = 0,
		Hard = 1,
		Test
	}

	public enum EnemyType
	{
		Idle,
		Move,
		Test,
		Dead,
		Kill,
		None
	}

	public enum SpeedType
	{
		None = 0,
		Wave = 1,
		Fast = 2
	}

	public enum RotateType
	{
		None = 0,
		Rght = 1,
		Down = 2,
		Left = 3,
	}

	public enum Direction
	{
		None = 0,
		Left = 1,
		Right = 2,
		Up = 3,
		Down = 4
	}

	public enum MoveType
	{
		None = 0,
		Horz = 1,
		Vert = 2,
		Both = 3,
	}

	public enum ShipType
	{
		Ship = 0,
		Boss = 1
	}

	public enum ExplodeType
	{
		Small = 0,
		Big = 1
	}

	public enum StatusType
	{
		Black = 0,
		Yellow = 1,
		Red = 2,
	}

	public enum SongType
	{
		BossMusic1,
		BossMusic2,
		ContMusic,
		CoolMusic,
		GameMusic1,
		GameMusic2,
		GameMusic3,
		GameOver,
		GameTitle
	}

	public enum SoundEffectType
	{
		Aaargh,
		Cheat,
		Extra,
		Finish,
		Fire1,
		Fire2,
		Fire3,
		Over,
		Ready,
		Right,
		Ship,
		Wrong,
	}

	public enum EventType
	{
		LargeTargetMove,
		SmallTargetMove,
	}

	public enum SpriteType
	{
		LargeTarget,
		SmallTarget,
		Bullet,
		Enemy,
	}

	public enum ScreenType
	{
		Splash,
		Init,
		Title,
		Intro,
		Begin,
		Diff,
		Level,
		Load,
		Ready,
		Play,
		Quit,
		Finish,
		Boss,
		Dead,
		Cont,
		Over,
		Resume,
		Beat,
		Demo,
		Exit,
		Test,
	}

}