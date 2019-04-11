using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Objects;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IIconManager
	{
		// Methods.
		void Initialize();
		void LoadContent();
		void ToggleIcon(BaseObject icon);
		void UpdateFireIcon(Byte index);
		void Draw();
		void DrawControls();

		// Properties.
		GameSound GameSound { get; }
		GameState GameState { get; }
		JoypadMove JoypadMove { get; }
		JoyButton JoyButton { get; }
	}

	public class IconManager : IIconManager
	{
		// Methods.
		public void Initialize()
		{
			const Byte gameOffset = 100;
			GameState = new GameState();
			Vector2 statePosn = new Vector2(5, 4 + Constants.GameOffsetY);
			Rectangle stateColl = new Rectangle(0, + Constants.GameOffsetY, gameOffset, gameOffset);
			GameState.Initialize(statePosn, stateColl);

			GameSound = new GameSound();
			Vector2 soundPosn = new Vector2(725, 4 + Constants.GameOffsetY);
			Rectangle soundColl = new Rectangle(Constants.ScreenWide - gameOffset, 0 + Constants.GameOffsetY, gameOffset, gameOffset);
			GameSound.Initialize(soundPosn, soundColl);


			// Joystick controller.
			JoypadMove = new JoypadMove();
			Vector2 jpPos = new Vector2(20, 300 + Constants.GameOffsetY);
			Rectangle jpColl = new Rectangle(-100, 180 + Constants.GameOffsetY, 400, 400);
			Rectangle jpBndl = new Rectangle(0, 280 + Constants.GameOffsetY, 200, 200);
			JoypadMove.Initialize(jpPos, jpColl, jpBndl);

			// Joystick fire button.
			const Byte fireOffsetX = Constants.FIRE_OFFSET_X;
			const Byte fireOffsetY = Constants.FIRE_OFFSET_Y;

			JoyButton = new JoyButton();
			const Byte textSize = Constants.TextsSize;
			const Byte baseSize = Constants.BaseSize;
			Vector2 firePosn = new Vector2(Constants.ScreenWide - baseSize - (2 * textSize), Constants.ScreenHigh - Constants.GameOffsetY - baseSize - (1 * textSize));
			Rectangle fireColl = new Rectangle(Constants.ScreenWide - fireOffsetX, Constants.ScreenHigh - Constants.GameOffsetY - fireOffsetY, fireOffsetX, fireOffsetY);
			JoyButton.Initialize(firePosn, fireColl);
		}

		public void LoadContent()
		{
			JoypadMove.LoadContent(MyGame.Manager.ImageManager.JoypadRectangle);
			JoyButton.LoadContent(MyGame.Manager.ImageManager.JoyButtonRectangles);
			GameState.LoadContent(MyGame.Manager.ImageManager.GameStateRectangles);
			GameSound.LoadContent(MyGame.Manager.ImageManager.GameSoundRectangles);

			Byte index = Convert.ToByte(MyGame.Manager.StateManager.GameQuiet);
			UpdateIcon(GameSound, index);
		}

		public void ToggleIcon(BaseObject icon)
		{
			icon.ToggleIcon();
		}
		public void UpdateIcon(BaseObject icon, Byte index)
		{
			icon.UpdateIcon(index);
		}
		public void UpdateFireIcon(Byte index)
		{
			JoyButton.UpdateIcon(index);
		}

		public void Draw()
		{
			GameState.Draw();
			GameSound.Draw();
		}

		public void DrawControls()
		{
			JoypadMove.Draw();
			JoyButton.Draw();
		}

		// Properties.
		public GameSound GameSound { get; private set; }
		public GameState GameState { get; private set; }
		public JoypadMove JoypadMove { get; private set; }
		public JoyButton JoyButton { get; private set; }

	}
}
