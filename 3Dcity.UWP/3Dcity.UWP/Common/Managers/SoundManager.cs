using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using WindowsGame.Common.Static;
using WindowsGame.Master.Factorys;

namespace WindowsGame.Common.Managers
{
	public interface ISoundManager 
	{
		void Initialize();
		void Initialize(Boolean playAudio);

		void GamePause(Boolean gamePause);
		void GameQuiet(Boolean gameQuiet);
		SongType GetGameMusic(Byte levelIndex);

		void PlayGameMusic(SongType key);
		void PlayMusic(SongType key);
		void PlayMusic(SongType key, Boolean isRepeating);
		void PauseMusic();
		void ResumeMusic();
		void StopMusic();

		void PlayBulletSoundEffect();
		void PlaySoundEffect(SoundEffectType key);
		void StopSoundEffect(SoundEffectType key);

		void SetPlayAudio(Boolean playAudio);

		Boolean PlayAudio { get; }
	}

	public class SoundManager : ISoundManager
	{
		private readonly ISoundFactory soundFactory;

		private SongType[] gameMusicList;
		private SoundEffectType[] bulletSoundList;

		public SoundManager(ISoundFactory soundFactory)
		{
			this.soundFactory = soundFactory;
		}

		public void Initialize()
		{
			Initialize(MyGame.Manager.ConfigManager.GlobalConfigData.PlayAudio);
		}

		public void Initialize(Boolean playAudio)
		{
			PlayAudio = playAudio;

			gameMusicList = new[]
			{
				SongType.GameMusic1,
				SongType.GameMusic2,
				SongType.GameMusic3,
			};
			bulletSoundList = new[]
			{
				SoundEffectType.Fire1,
				SoundEffectType.Fire2,
				SoundEffectType.Fire3,
			};
		}

		public void GamePause(Boolean gamePause)
		{
			if (gamePause)
			{
				PauseAllAudio();
			}
			else
			{
				Boolean gameQuiet = MyGame.Manager.StateManager.GameQuiet;
				if (!gameQuiet)
				{
					ResumeAllAudio();
				}
			}
		}

		public void GameQuiet(Boolean gameQuiet)
		{
			if (gameQuiet)
			{
				PauseAllAudio();
			}
			else
			{
				ResumeAllAudio();
			}

			SetPlayAudio(!gameQuiet);
		}

		public SongType GetGameMusic(Byte levelIndex)
		{
			Byte index = (Byte)(levelIndex % Constants.GAME_MUSIC);
			return gameMusicList[index];
		}

		//SongType GetBossMusic(Byte levelIndex)
		//{
		//    Byte index = (Byte)(levelIndex % Constants.BOSS_MUSIC);
		//    return bossMusicList[index];
		//}

		public void PlayBulletSoundEffect()
		{
			Byte index = (Byte)MyGame.Manager.RandomManager.Next(Constants.FIRE_SOUND);
			SoundEffectType key = bulletSoundList[index];
			PlaySoundEffect(key);
		}

		public void PlaySoundEffect(SoundEffectType key)
		{
			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			if (!PlayAudio)
			{
				return;
			}

			SoundEffectInstance value = Assets.SoundEffectDictionary[key];
			soundFactory.PlaySoundEffect(value);
		}

		public void StopSoundEffect(SoundEffectType key)
		{
			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			if (!PlayAudio)
			{
				return;
			}

			SoundEffectInstance value = Assets.SoundEffectDictionary[key];
			soundFactory.StopSoundEffect(value);
		}

		public void PlayGameMusic(SongType key)
		{
			PlayMusic(key, true);
		}

		public void PlayMusic(SongType key)
		{
			PlayMusic(key, false);
		}

		public void PlayMusic(SongType key, Boolean isRepeating)
		{
			if (null == Assets.SongDictionary)
			{
				return;
			}

			Song song = Assets.SongDictionary[key];
			if (null == song)
			{
				return;
			}

			SetVolume();
			soundFactory.PlayMusic(song, isRepeating);
		}

		public void PauseMusic()
		{
			soundFactory.PauseMusic();
		}

		public void ResumeMusic()
		{
			SetVolume();
			soundFactory.ResumeMusic();
		}

		public void StopMusic()
		{
			soundFactory.StopMusic();
		}

		private void PauseAllAudio()
		{
			PauseMusic();

			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.PauseSoundEffect(soundEffect);
			}
		}

		private void ResumeAllAudio()
		{
			ResumeMusic();

			if (null == Assets.SoundEffectDictionary)
			{
				return;
			}

			foreach (SoundEffectInstance soundEffect in Assets.SoundEffectDictionary.Values)
			{
				soundFactory.ResumeSoundEffect(soundEffect);
			}
		}

		private void SetVolume()
		{
			if (PlayAudio)
			{
				soundFactory.SetMaxVolume();
			}
			else
			{
				soundFactory.SetMinVolume();
			}
		}

		public void SetPlayAudio(Boolean playAudio)
		{
			PlayAudio = playAudio;
			SetVolume();
		}

		public Boolean PlayAudio { get; private set; }
	}
}
