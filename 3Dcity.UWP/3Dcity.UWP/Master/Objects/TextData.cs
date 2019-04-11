using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WindowsGame.Master.Objects
{
	public struct TextData
	{
		public Vector2 Position { get; private set; }
		public String Text { get; private set; }
		public Color Color { get; private set; }
		private IList<String> List { get; set; }

		public TextData(Vector2 position, String text)
			: this(position, text, Color.White)
		{
		}
		public TextData(Vector2 position, IList<String> list)
			: this(position, list, Color.White)
		{
		}

		public TextData(Vector2 position, String text, Color color)
			: this()
		{
			Position = position;
			Color = color;
			Text = text;
		}

		private TextData(Vector2 position, IList<String> list, Color color)
			: this()
		{
			Position = position;
			Color = color;
			List = list;
		}

	}
}