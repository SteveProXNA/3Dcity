using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Managers;
using WindowsGame.Master.Inputs;

namespace WindowsGame.Common.Inputs
{
	public class MobilesInputManager : IInputManager
	{
		private readonly IJoystickInput joystickInput;
		private readonly ITouchScreenInput touchScreenInput;
		private readonly IControlManager controlManager;

		private IList<Vector2> pressPositions;
		private IList<Vector2> movePositions;
		private Vector2 viewPortVector2;
		private Matrix invertTransformationMatrix;
		private Byte maxInputs;

		public MobilesInputManager(IJoystickInput joystickInput, ITouchScreenInput touchScreenInput, IControlManager controlManager)
		{
			this.joystickInput = joystickInput;
			this.touchScreenInput = touchScreenInput;
			this.controlManager = controlManager;
		}

		public void Initialize()
		{
			const GestureType gestureType = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag;
			touchScreenInput.Initialize(gestureType);
			joystickInput.Initialize();
			maxInputs = 0;
		}

		public void LoadContent()
		{
			viewPortVector2 = MyGame.Manager.ResolutionManager.ViewPortVector2;
			invertTransformationMatrix = MyGame.Manager.ResolutionManager.InvertTransformationMatrix;

			maxInputs = MyGame.Manager.ConfigManager.PlatformConfigData.MaxInputs;
			pressPositions = new List<Vector2>(maxInputs);
			movePositions = new List<Vector2>(maxInputs);
		}

		public void Update(GameTime gameTime)
		{
			// Update joystick input.
			joystickInput.Update(gameTime);

			// Update touch input
			touchScreenInput.Update(gameTime);

			// Process touch input.
			pressPositions.Clear();
			movePositions.Clear();

			TouchCollection touchCollection = touchScreenInput.TouchCollection;
			Int32 count = touchCollection.Count;
			if (0 == count)
			{
				return;
			}

			Int32 loops = Math.Min(maxInputs, count);
			for (Byte index = 0; index < loops; index++)
			{
				TouchLocation touchLocation = touchCollection[index];

				TouchLocationState state = touchLocation.State;
				Vector2 position = GetTouchPosition(touchLocation.Position);

				if (TouchLocationState.Pressed == state)
				{
					pressPositions.Add(position);
				}
				if (TouchLocationState.Pressed == state || TouchLocationState.Moved == state)
				{
					movePositions.Add(position);
				}
			}
		}

		public Boolean BackAll()
		{
			return Back();
		}
		public Boolean Back()
		{
			return Escape() || StatusBar();
		}

		public Boolean Escape()
		{
			return joystickInput.JoyHold(Buttons.Back);
		}

		public Boolean Accelerate()
		{
			return MyMove2Func(controlManager.CheckAcclerate);
		}

		public Single LittleHorz()
		{
			return MyMoveFunc(controlManager.CheckJoyPadTiny);
		}

		public Single Horizontal()
		{
			return MyMoveFunc(controlManager.CheckJoyPadHorz);
		}

		public Single Vertical()
		{
			return MyMoveFunc(controlManager.CheckJoyPadVert);
		}

		public Boolean Fire()
		{
			return MyMove2Func(controlManager.CheckJoyPadFire);
		}

		public Boolean SelectAll()
		{
			return Select();
		}
		public Boolean Select()
		{
			return MyPressFunc(controlManager.CheckJoyPadFire);
		}

		public Boolean SelectJoystick()
		{
			return false;
		}

		public Boolean SelectWithout()
		{
			return Select();
		}

		public Boolean GameState()
		{
			return MyPressFunc(controlManager.CheckGameState);
		}

		public Boolean GameSound()
		{
			return MyPressFunc(controlManager.CheckGameSound);
		}

		public Boolean CenterPos()
		{
			return MyPressFunc(controlManager.CheckCenterPos);
		}

		public Boolean TitleModeAll()
		{
			return TitleMode();
		}
		public Boolean TitleMode()
		{
			return MyPressFunc(controlManager.CheckTitleMode);
		}

		public Boolean StatusBar()
		{
			return MyPressFunc(controlManager.CheckStatusBar);
		}

		public Boolean LeftsSide()
		{
			return MyPressFunc(controlManager.CheckLeftsSide);
		}

		public Boolean RightSide()
		{
			return MyPressFunc(controlManager.CheckRightSide);
		}

		public void SetMotors(Single leftMotor, Single rightMotor)
		{
		}

		public void ResetMotors()
		{
		}


		private Single MyMoveFunc(Func<Vector2, Single> func)
		{
			Single data = 0.0f;

			Byte count = (Byte)(movePositions.Count);
			if (0 == count)
			{
				return data;
			}

			for (Byte index = 0; index < count; index++)
			{
				Vector2 position = movePositions[index];
				Single temp = func(position);

				if (Math.Abs(temp) > Single.Epsilon)
				{
					data = temp;
				}
			}

			return data;
		}
		private Boolean MyMove2Func(Func<Vector2, Boolean> func)
		{
			Boolean data = false;

			Byte count = (Byte)(movePositions.Count);
			if (0 == count)
			{
				return false;
			}

			for (Byte index = 0; index < count; index++)
			{
				Vector2 position = movePositions[index];
				Boolean temp = func(position);

				if (!temp)
				{
					continue;
				}

				data = true;
				break;
			}

			return data;
		}
		private Boolean MyPressFunc(Func<Vector2, Boolean> func)
		{
			Boolean data = false;

			Byte count = (Byte)(pressPositions.Count);
			if (0 == count)
			{
				return false;
			}

			for (Byte index = 0; index < count; index++)
			{
				Vector2 position = pressPositions[index];
				Boolean temp = func(position);

				if (!temp)
				{
					continue;
				}

				data = true;
				break;
			}

			return data;
		}

		private Vector2 GetTouchPosition(Vector2 touchPosition)
		{
			// http://www.david-amador.com/2010/03/xna-2d-independent-resolution-rendering.
			Vector2 deltaPosition = touchPosition - viewPortVector2;
			return Vector2.Transform(deltaPosition, invertTransformationMatrix);
		}
 
	}
}