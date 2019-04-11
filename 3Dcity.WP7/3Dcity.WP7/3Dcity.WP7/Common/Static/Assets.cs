using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame.Common.Static
{
	public static class Assets
	{
		// Fonts.
		public static SpriteFont EmulogicFont;

		// Sound.
		public static IDictionary<SoundEffectType, SoundEffectInstance> SoundEffectDictionary;
		public static IDictionary<SongType, Song> SongDictionary;

		// Textures.
		public static Texture2D SplashTexture;

		public static Texture2D SpriteSheet01Texture;
		public static Texture2D SpriteSheet02Texture;
	}
}
