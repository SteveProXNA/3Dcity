using System;

namespace WindowsGame.Common.Static
{
	public static class Globalize
	{
		public const String YEAR_TITLE = "(C) 1988";

		public const String DRAW_TITLE = "3D City";
		public const String GAME_TITLE = "--3D CITY--";
		public const String GAME_CHEAT = "==3D CITY==";
		public const String MOVE_TITLE = "MOVE";
		public const String FIRE_TITLE = "FIRE";

		public const String PLAYER_FLASH = "1UP";
		public const String HISCORE_TEXT = "HI";
		public const String CURSOR_LEFTS = "<<";
		public const String CURSOR_RIGHT = ">>";
		public const String MISS_TEXT = "X";
		public const String SEPARATOR = "/";
		public const String PERCENTAGE = "%";

		public const String SHIP_TYPE = "SHIP";
		public const String BOSS_TYPE = "BOSS";

		public const String INSTRUCTION1 = "MOVE TARGET TO CHOOSE";
		public const String INSTRUCTION2 = "FIRE BUTTON TO SELECT";

		public const String INSERT_COINS = "INSERT COIN(S)";

		public const String DEAD_OPTION1 = "4X MISSES";
		public const String DEAD_OPTION2 = "YOU DIED";

		public const String FINISH_TEXT1 = "LEVEL COMPLETE";

		public static readonly String[] INTRO_TEXT1 = new String[Constants.INFO_LINES]
		{
			"CLASSIC SHOOT 'EM UP!",
			"HEAVILY INSPIRED FROM",
			"ORIGINAL GAME WRITTEN",
		};
		public static readonly String[] INTRO_TEXT2 = new String[Constants.INFO_LINES]
		{
			"FROM STEVEPRO STUDIOS",
			"SPACE HARRIER IN 1985",
			"FROM THE SEGA SC-3000",
		};
	}
}