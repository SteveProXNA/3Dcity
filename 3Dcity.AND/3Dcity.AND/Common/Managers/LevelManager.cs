using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Managers
{
	public interface ILevelManager 
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadLevelConfigData(LevelType levelType, Byte levelIndex);

		void SetLevelType(LevelType levelType);
		void SetLevelIndex(Byte levelIndex);
		void SetLevelNo(Byte levelNo);
		void CheckLevelOrbs();

		void Draw();
		void DrawTextData();

		// Properties.
		IList<String> LevelNames { get; }
		LevelConfigData LevelConfigData { get; }
		Byte MaximLevel { get; }
		LevelType LevelType { get; }
		Byte LevelIndex { get; }
		Byte LevelNo { get; }
		String LevelValu { get; }
		String LevelName { get; }
	}

	public class LevelManager : ILevelManager 
	{
		private String levelRoot;

		private String levelData;
		private Vector2 levelNumPosition;
		private Vector2 levelOrbPosition;
		private Rectangle levelOrbPbRectangle;

		private const String LEVELS_DIRECTORY = "Levels";
		private const String LEVELS_NAMESFILE = "LevelNames";

		public void Initialize()
		{
			Initialize(String.Empty);
		}

		public void Initialize(String root)
		{
			levelRoot = String.Format("{0}{1}/{2}/{3}", root, Constants.CONTENT_DIRECTORY, Constants.DATA_DIRECTORY, LEVELS_DIRECTORY);

			const Byte offset = 48;
			levelOrbPosition = new Vector2(Constants.ScreenWide - offset - Constants.GameOffsetX - 4, Constants.ScreenHigh - offset - Constants.GameOffsetY);
			levelNumPosition = MyGame.Manager.TextManager.GetTextPosition(0, 23);
		}

		public void LoadContent()
		{
			String namesFile = String.Format("{0}/{1}.txt", levelRoot, LEVELS_NAMESFILE);
			LevelNames = MyGame.Manager.FileManager.LoadTxt(namesFile);

			MaximLevel = MyGame.Manager.ConfigManager.GlobalConfigData.MaximLevel;
			if (MaximLevel > LevelNames.Count)
			{
				MaximLevel = (Byte)LevelNames.Count;
			}
		}

		public void LoadLevelConfigData(LevelType levelType, Byte levelIndex)
		{
			String levelValue = (levelIndex + 1).ToString().PadLeft(2, '0');
			String file = String.Format("{0}/{1}/{2}-{1}.xml", levelRoot, levelType, levelValue);
			LevelConfigData = MyGame.Manager.FileManager.LoadXml<LevelConfigData>(file);
		}

		public void SetLevelType(LevelType levelType)
		{
			LevelType = levelType;
			CheckLevelOrbs();
		}
		
		public void SetLevelIndex(Byte levelIndex)
		{
			LevelIndex = levelIndex;

			levelData = (levelIndex + 1).ToString().PadLeft(2, '0');
			LevelValu = String.Format("[{0}]", levelData);

			if (null == LevelNames)
			{
				return;
			}
			LevelName = LevelNames[levelIndex];
		}

		public void SetLevelNo(Byte levelNo)
		{
			LevelNo = levelNo;
			Byte levelIndex = (Byte) (levelNo - 1);
			SetLevelIndex(levelIndex);
		}

		public void CheckLevelOrbs()
		{
			if (null == MyGame.Manager.ImageManager.OrbDiffRectangles)
			{
				return;
			}

			levelOrbPbRectangle = MyGame.Manager.ImageManager.OrbDiffRectangles[(Byte)LevelType];
		}

		public void Draw()
		{
			Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, levelOrbPosition, levelOrbPbRectangle, Color.White);
		}

		public void DrawTextData()
		{
			Engine.SpriteBatch.DrawString(Assets.EmulogicFont, levelData, levelNumPosition, Color.White);
		}

		public IList<String> LevelNames { get; private set; }
		public LevelConfigData LevelConfigData { get; private set; }
		public Byte MaximLevel { get; private set; }
		public LevelType LevelType { get; private set; }
		public Byte LevelIndex { get; private set; }
		public Byte LevelNo { get; private set; }
		public String LevelValu { get; private set; }
		public String LevelName { get; private set; }
	}
}
