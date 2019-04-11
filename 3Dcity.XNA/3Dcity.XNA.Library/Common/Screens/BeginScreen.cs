using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Screens
{
	public class BeginScreen : BaseScreen, IScreen
	{
		private Boolean playSound;

		public override void Initialize()
		{
			MyGame.Manager.DebugManager.Reset(CurrScreen);
			base.Initialize();

			playSound = true;

			MyGame.Manager.DebugManager.Reset(CurrScreen);
		}

		public override Int32 Update(GameTime gameTime)
		{
			Boolean gameState = MyGame.Manager.InputManager.GameState();
			if (gameState)
			{
				if (playSound)
				{
					PlaySound(SoundEffectType.Fire1);
				}
				else
				{
					//PlayMusic(SongType.BossMusic);
					PlayMusic(SongType.CoolMusic);
				}
			}
			else
			{
				Boolean gameSound = MyGame.Manager.InputManager.GameSound();
				if (gameSound)
				{
					if (playSound)
					{
						PlaySound(SoundEffectType.Fire2);
					}
					else
					{
						PlayMusic(SongType.ContMusic);
					}

				}
				else
				{
					Boolean fire = MyGame.Manager.InputManager.Select();
					if (fire)
					{
						PlaySound(playSound ? SoundEffectType.Extra : SoundEffectType.Fire3);
					}
					else
					{
						Single horz = MyGame.Manager.InputManager.Horizontal();
						Single vert = MyGame.Manager.InputManager.Vertical();
						if (0 == horz && 0 == vert)
						{
							return (Int32)CurrScreen;
						}
						else
						{
							if (playSound)
							{
								PlaySound(SoundEffectType.Fire3);
							}
							else
							{
								PlaySound(SoundEffectType.Fire3);
							}
						}
					}
				}
			}

			return (Int32)CurrScreen;
		}

		private void PlayMusic(SongType songType)
		{
			MyGame.Manager.SoundManager.StopMusic();
			MyGame.Manager.SoundManager.PlayMusic(songType, false);
		}

		private void PlaySound(SoundEffectType soundEffectType)
		{
			MyGame.Manager.SoundManager.PlaySoundEffect(soundEffectType);
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();

			// Sprite sheet #02.
			MyGame.Manager.LevelManager.Draw();

			// Text data last!
			MyGame.Manager.TextManager.DrawTitle();
			MyGame.Manager.TextManager.DrawControls();
			MyGame.Manager.LevelManager.DrawTextData();
			MyGame.Manager.ScoreManager.Draw();
		}

	}
}
