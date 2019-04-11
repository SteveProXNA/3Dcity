using System;
using Microsoft.Xna.Framework;

namespace WindowsGame.Common.Interfaces
{
	public interface IInputManager
	{
		void Initialize();
		void LoadContent();
		void Update(GameTime gameTime);

		Boolean BackAll();
		Boolean Back();
		Boolean Escape();

		Boolean Accelerate();
		Single LittleHorz();
		Single Horizontal();
		Single Vertical();
		Boolean Fire();
		Boolean SelectAll();
		Boolean Select();
		Boolean SelectJoystick();
		Boolean SelectWithout();
		Boolean GameState();
		Boolean GameSound();
		Boolean CenterPos();
		Boolean TitleModeAll();
		Boolean TitleMode();
		Boolean StatusBar();
		Boolean LeftsSide();
		Boolean RightSide();

		void SetMotors(Single leftMotor, Single rightMotor);
		void ResetMotors();
	}
}
