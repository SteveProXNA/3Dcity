using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master;
using WindowsGame.Master.Objects;

namespace WindowsGame.Common.Managers
{
	public interface IScoreManager 
	{
		void Initialize();
		void LoadContent();
		void ResetAll();
		void ResetLevel();
		void ResetMisses();
		void ResetStats();
		void ResetTimer();
		void Update(GameTime gameTime);
		void Draw();
		void DrawBlink();

		void UpdateGameScore(Byte index);
		void SetHighScore(UInt32 score);
		void IncrementMisses();

		// Properties.
		UInt32 HighScore { get; }
		Byte MissesTotal { get; }
		Byte ScoreKills { get; }
	}

	public class ScoreManager : IScoreManager
	{
		private const Byte MAX_LENGTH = 6;
		private Vector2 gameScorePosition;
		private Vector2 highScorePosition;
		private TextData[] missTextData;
		private TextData socreTextData;
		private TextData highTextData;
		private String gameScoreText;
		private String highScoreText;
		private Int32 missScore;
		private UInt32 gameScore;
		private UInt16 scoreDelay;
		private UInt16 scoreTimer;
		private Boolean scoreFlag;
		private Boolean scoreBlink;

		public void Initialize()
		{
			scoreDelay = MyGame.Manager.ConfigManager.GlobalConfigData.ScoreDelay;
			SetHighScore(Constants.DEF_HIGH_SCORE);
			ResetAll();
		}

		public void LoadContent()
		{
			gameScorePosition = MyGame.Manager.TextManager.GetTextPosition(8, 1);
			highScorePosition = MyGame.Manager.TextManager.GetTextPosition(30, 1);
			missTextData = GetMissTextDataList();
			Vector2 scorePosition = MyGame.Manager.TextManager.GetTextPosition(4, 1);
			socreTextData = new TextData(scorePosition, Globalize.PLAYER_FLASH, Color.Yellow);
			Vector2 highPosition = MyGame.Manager.TextManager.GetTextPosition(27, 1);
			highTextData = new TextData(highPosition, Globalize.HISCORE_TEXT, Color.Yellow);
			scoreBlink = MyGame.Manager.ConfigManager.GlobalConfigData.ScoreBlink;
		}

		public void ResetAll()
		{
			ResetMisses();
			ResetStats();
			ResetTimer();

			missScore = 0;
			gameScore = 0;
			gameScoreText = GetGameScoreText();
		}
		public void ResetLevel()
		{
			ResetMisses();
			ResetStats();
			ResetTimer();
		}
		public void ResetMisses()
		{
			MissesTotal = 0;
		}
		public void ResetStats()
		{
			ScoreAvoid = 0;
			ScoreKills = 0;
		}
		public void ResetTimer()
		{
			scoreTimer = 0;
			scoreFlag = true;
		}

		public void Update(GameTime gameTime)
		{
			if (!scoreBlink)
			{
				return;
			}

			scoreTimer += (UInt16)gameTime.ElapsedGameTime.Milliseconds;
			if (scoreTimer >= scoreDelay)
			{
				scoreTimer -= scoreDelay;
				scoreFlag = !scoreFlag;
			}
		}

		public void Draw()
		{
			DrawCommon();
			MyGame.Manager.TextManager.Draw(socreTextData);
		}

		public void DrawBlink()
		{
			DrawCommon();
			if (scoreFlag)
			{
				MyGame.Manager.TextManager.Draw(socreTextData);
			}
		}

		private void DrawCommon()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, gameScoreText, gameScorePosition, Color.White);
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, highScoreText, highScorePosition, Color.White);
			MyGame.Manager.TextManager.Draw(highTextData);
			for (Byte index = 0; index < MissesTotal; index++)
			{
				MyGame.Manager.TextManager.Draw(missTextData[index]);
			}
		}

		public void UpdateGameScore(Byte index)
		{
			ScoreKills++;

			// Update miss score.
			missScore += Constants.ENEMYS_SCORE[index];
			if (missScore >= Constants.DEF_MISS_SCORE)
			{
				missScore -= Constants.DEF_MISS_SCORE;
				if (MissesTotal > 0)
				{
					ResetMisses();
					MyGame.Manager.SoundManager.PlaySoundEffect(SoundEffectType.Extra);
				}
			}

			// Update game score.
			gameScore += Constants.ENEMYS_SCORE[index];
			if (gameScore >= Constants.MAX_HIGH_SCORE)
			{
				gameScore = Constants.MAX_HIGH_SCORE;
			}
			gameScoreText = GetGameScoreText();

			if (gameScore > HighScore)
			{
				HighScore = gameScore;
				highScoreText = GetHighScoreText();
			}
		}
		public void SetHighScore(UInt32 score)
		{
			HighScore = score;
			highScoreText = GetHighScoreText();
		}
		public void IncrementMisses()
		{
			MissesTotal++;
			ScoreAvoid++;
		}

		private static TextData[] GetMissTextDataList()
		{
			TextData[] data = new TextData[Constants.MAX_MISSES];
			for (Byte index = 0; index < Constants.MAX_MISSES; index++)
			{
				data[index] = GetMissTextData((Byte)(11 - index));
			}

			return data;
		}

		private static TextData GetMissTextData(Byte y)
		{
			Vector2 position = MyGame.Manager.TextManager.GetTextPosition(39, (SByte)y);
			return new TextData(position, Globalize.MISS_TEXT);
		}

		private String GetGameScoreText()
		{
			return gameScore.ToString().PadLeft(MAX_LENGTH, '0');
		}
		private String GetHighScoreText()
		{
			return HighScore.ToString().PadLeft(MAX_LENGTH, '0');
		}

		public UInt32 HighScore { get; private set; }
		public Byte MissesTotal { get; private set; }
		public Byte ScoreAvoid { get; private set; }
		public Byte ScoreKills { get; private set; }
	}
}
