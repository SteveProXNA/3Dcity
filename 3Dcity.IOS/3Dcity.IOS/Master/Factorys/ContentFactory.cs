using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame.Master.Factorys
{
	public interface IContentFactory
	{
		SpriteFont LoadFont(String assetName);
		Song LoadSong(String assetName);
		SoundEffectInstance LoadSoundEffectInstance(String assetName);
		Texture2D LoadTexture(String assetName);
	}

	public class ContentFactory : IContentFactory
	{
		public SpriteFont LoadFont(String assetName)
		{
			return ContentLoad<SpriteFont>(assetName);
		}

		public Song LoadSong(String assetName)
		{
			return ContentLoad<Song>(assetName);
		}

		public SoundEffectInstance LoadSoundEffectInstance(String assetName)
		{
			SoundEffect effect = ContentLoad<SoundEffect>(assetName);
			return effect.CreateInstance();
		}

		public Texture2D LoadTexture(string assetName)
		{
			return ContentLoad<Texture2D>(assetName);
		}

		private static T ContentLoad<T>(String assetName)
		{
			return Engine.Content.Load<T>(assetName);
		}

	}
}
