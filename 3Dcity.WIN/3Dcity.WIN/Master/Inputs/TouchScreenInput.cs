using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsGame.Master.Inputs
{
	public interface ITouchScreenInput
	{
		void Initialize(GestureType gestureType);
		void Update(GameTime gameTime);

		TouchCollection TouchCollection { get; }
		Boolean IsGestureAvailable { get; }
		GestureSample? GestureSample { get; }
	}

	public class TouchScreenInput : ITouchScreenInput
	{
		public void Initialize(GestureType gestureType)
		{
			TouchPanel.EnabledGestures = gestureType;
		}

		public void Update(GameTime gameTime)
		{
			TouchCollection = TouchPanel.GetState();
			GestureSample = null;

			IsGestureAvailable = TouchPanel.IsGestureAvailable;
			if (!IsGestureAvailable)
			{
				return;
			}

			GestureSample = TouchPanel.ReadGesture();
		}

		public TouchCollection TouchCollection { get; private set; }
		public Boolean IsGestureAvailable { get; private set; }
		public GestureSample? GestureSample { get; private set; }
	}
}
