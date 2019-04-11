using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IStateManager
	{
		void Initialize();
		void ToggleGameState();
		void ToggleGameSound();
		void UpdateGameSound();

		void SetCoolMusic(Boolean coolMusic);
		void SetIsGodMode(Boolean isGodMode);
		void SetCheatGame(Boolean cheatGame);
		void SetKillSpace(Vector2 position);
		Vector2[] SetBackedPositions(UInt16 lft, UInt16 top, UInt16 rgt, UInt16 bot);

		Boolean GamePause { get; }
		Boolean GameQuiet { get; }
		Boolean CoolMusic { get; }
		Boolean IsGodMode { get; }
		Boolean CheatGame { get; }
		Vector2 KillSpace { get; }
	}

	public class StateManager : IStateManager
	{
		public void Initialize()
		{
			GamePause = false;
			UpdateGameSound();
		}

		public void ToggleGameState()
		{
			GamePause = !GamePause;
		}

		public void ToggleGameSound()
		{
			GameQuiet = !GameQuiet;
		}

		public void UpdateGameSound()
		{
			GameQuiet = !MyGame.Manager.SoundManager.PlayAudio;
		}

		public void SetCoolMusic(Boolean coolMusic)
		{
			CoolMusic = coolMusic;
		}

		public void SetIsGodMode(Boolean isGodMode)
		{
			IsGodMode = isGodMode;
		}

		public void SetCheatGame(Boolean cheatGame)
		{
			CheatGame = cheatGame;
		}

		public void SetKillSpace(Vector2 position)
		{
			KillSpace = position;
		}

		public Vector2[] SetBackedPositions(UInt16 lft, UInt16 top, UInt16 rgt, UInt16 bot)
		{
			Vector2[] backedPositions = new Vector2[Constants.MAX_BORDER];
			backedPositions[0] = new Vector2(lft, top + Constants.GameOffsetY);
			backedPositions[1] = new Vector2(rgt, top + Constants.GameOffsetY);
			backedPositions[2] = new Vector2(lft, bot + Constants.GameOffsetY);
			backedPositions[3] = new Vector2(rgt, bot + Constants.GameOffsetY);
			return backedPositions;
		}

		public Boolean GamePause { get; private set; }
		public Boolean GameQuiet { get; private set; }
		public Boolean CoolMusic { get; private set; }
		public Boolean IsGodMode { get; private set; }
		public Boolean CheatGame { get; private set; }
		public Vector2 KillSpace { get; private set; }
	}
}
