using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame.Common.Inputs.Types
{
	public interface IJoystickInput
	{
		void Initialize();
		void Update(GameTime gameTime);

		// TODO take this out because this is the engine!
		Single Horizontal();
		Single Vertical();
		// TODO take this out because this is the engine!

		Boolean JoyHold(Buttons button);
		Boolean JoyMove(Buttons button);

		//void SetMotors(Single leftMotor, Single rightMotor);
		//void ResetMotors();
	}

	public class JoystickInput : IJoystickInput
	{
		private GamePadState currGamePadState;
		private GamePadState prevGamePadState;

		private const Single TOLERANCE = 0.4f;

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			// http://xona.com/2010/05/03.html.
			prevGamePadState = currGamePadState;
			currGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
		}

		public Single Horizontal()
		{
			if (currGamePadState.ThumbSticks.Left.X < -TOLERANCE || currGamePadState.ThumbSticks.Left.X > TOLERANCE)
			{
				return currGamePadState.ThumbSticks.Left.X;
			}

			if (currGamePadState.IsButtonDown(Buttons.DPadLeft))
			{
				return -1;
			}
			if (currGamePadState.IsButtonDown(Buttons.DPadRight))
			{
				return 1;
			}

			return 0.0f;
		}

		public Single Vertical()
		{
			if (currGamePadState.ThumbSticks.Left.Y < -TOLERANCE || currGamePadState.ThumbSticks.Left.Y > TOLERANCE)
			{
				return -currGamePadState.ThumbSticks.Left.Y;
			}

			if (currGamePadState.IsButtonDown(Buttons.DPadUp))
			{
				return -1;
			}
			if (currGamePadState.IsButtonDown(Buttons.DPadDown))
			{
				return 1;
			}

			return 0.0f;
			//return currGamePadState.ThumbSticks.Left.Y;
		}

		public Boolean JoyHold(Buttons button)
		{
			return currGamePadState.IsButtonDown(button) && prevGamePadState.IsButtonUp(button);

		}
		public Boolean JoyMove(Buttons button)
		{
			return currGamePadState.IsButtonDown(button);
		}
	}
}
