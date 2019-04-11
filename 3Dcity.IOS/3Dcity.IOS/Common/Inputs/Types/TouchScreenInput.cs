//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input.Touch;

//namespace WindowsGame.Common.Inputs.Types
//{
//    public interface ITouchScreenInput
//    {
//        void Initialize(GestureType gestureType);
//        void LoadContent();
//        void Update(GameTime gameTime);

//        void ProcessTouches(TouchCollection touchCollection);
//        //void ProcessGesture(Boolean isGestureAvailable, GestureSample gestureSample);

//        IList<Vector2> TouchPositions { get; }
//        IList<Boolean> TouchStates { get; }

//        Vector2[] TouchPositionsX { get; }
//        TouchLocationState[] TouchStatesX { get; }
//        Boolean Tap { get; }
//        Boolean Hold { get; }
//        Boolean DoubleTap { get; }
//        Boolean HorizontalDrag { get; }
//        Boolean VerticalDrag { get; }
//    }

//    public class TouchScreenInput : ITouchScreenInput
//    {
//        private Vector2 viewPortVector2;
//        private Matrix invertTransformationMatrix;
//        //private IList<TouchLocation> touchLocationList;
//        //private const Byte MAX_TOUCHES = 10;
//        private Byte maxInputs;

//        public TouchScreenInput()
//        {
//            // Construct touch information.
//            //touchLocationList = new List<TouchLocation>(MAX_TOUCHES);

//            //TouchPositionsX = new Vector2[MAX_TOUCHES];
//            //TouchStatesX = new TouchLocationState[MAX_TOUCHES];
//        }

//        public void Initialize(GestureType gestureType)
//        {
//            TouchPanel.EnabledGestures = gestureType;
//            //TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag;
//            maxInputs = 0;
//            //Initialize(Vector2.Zero, Matrix.Identity);
//        }

//        //public void LoadContent()
//        //{
//        //    TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap | GestureType.Hold | GestureType.HorizontalDrag | GestureType.VerticalDrag;

//        //    //Initialize(Vector2.Zero, Matrix.Identity);
//        //}
//        public void LoadContent()
//        {
//            viewPortVector2 = MyGame.Manager.ResolutionManager.ViewPortVector2;
//            invertTransformationMatrix = MyGame.Manager.ResolutionManager.InvertTransformationMatrix;

//            maxInputs = MyGame.Manager.ConfigManager.PlatformConfigData.MaxInputs;

//            //touchLocationList = new List<TouchLocation>(maxInputs);

//            TouchPositions = new List<Vector2>(maxInputs);
//            TouchStates = new List<Boolean>(maxInputs);
//            //InitializeTouchData();
//            //ResetAllTouchData();
//        }

//        public void Update(GameTime gameTime)
//        {
//            // Populate touch information accordingly.
//            TouchCollection touchCollection = TouchPanel.GetState();
//            ProcessTouches(touchCollection);

//            // Process gestures if any are available.
//            Tap = Hold = DoubleTap = HorizontalDrag = VerticalDrag = false;
//            if (!TouchPanel.IsGestureAvailable)
//            {
//                return;
//            }

//            GestureSample gesture = TouchPanel.ReadGesture();
//            Tap = gesture.GestureType == GestureType.Tap;
//            Hold = gesture.GestureType == GestureType.Hold;
//            DoubleTap = gesture.GestureType == GestureType.DoubleTap;
//            HorizontalDrag = gesture.GestureType == GestureType.HorizontalDrag;
//            VerticalDrag = gesture.GestureType == GestureType.VerticalDrag;


//            //Boolean isGestureAvailable = TouchPanel.IsGestureAvailable;
//            //GestureSample gestureSample = TouchPanel.ReadGesture();
//            //ProcessGesture(isGestureAvailable, gestureSample);

//            // Reset all touch information first.
//            //ResetAllTouchData();
//            //OLD code
//            //var location = GetTouchLocation();
//            //if (null != location)
//            //{
//            //    TouchLocation touchLocation = (TouchLocation)location;
//            //    //TouchPosition = touchLocation.Position;
//            //    TouchPosition = GetTouchPosition(touchLocation.Position);
//            //    TouchState = touchLocation.State;
//            //}

//            //touchLocationList = GetTouchLocationList();
//        }

//        public void ProcessTouches(TouchCollection touchCollection)
//        {
//            TouchPositions.Clear();
//            TouchStates.Clear();

//            if (0 == touchCollection.Count)
//            {
//                return;
//            }

//            int count = Math.Max(maxInputs, touchCollection.Count);
//            for (Byte index = 0; index < touchCollection.Count; index++)
//            {
//                TouchLocation touchLocation = touchCollection[index];

//                Vector2 position = GetTouchPosition(touchLocation.Position);
//                Boolean isValid = TouchLocationState.Pressed == touchLocation.State || TouchLocationState.Moved == touchLocation.State;

//                TouchPositions.Add(position);
//                TouchStates.Add(isValid);
//            }
//        }

//        //public void ProcessGesture(Boolean isGestureAvailable, GestureSample gestureSample)
//        //{
//        //    Tap = Hold = DoubleTap = HorizontalDrag = VerticalDrag = false;

//        //    if (!isGestureAvailable)
//        //    {
//        //        return;
//        //    }

//        //    Tap = gestureSample.GestureType == GestureType.Tap;
//        //    Hold = gestureSample.GestureType == GestureType.Hold;
//        //    DoubleTap = gestureSample.GestureType == GestureType.DoubleTap;
//        //    HorizontalDrag = gestureSample.GestureType == GestureType.HorizontalDrag;
//        //    VerticalDrag = gestureSample.GestureType == GestureType.VerticalDrag;
//        //}

//        private Vector2 GetTouchPosition(Vector2 touchPosition)
//        {
//            // http://www.david-amador.com/2010/03/xna-2d-independent-resolution-rendering.
//            Vector2 deltaPosition = touchPosition - viewPortVector2;
//            return Vector2.Transform(deltaPosition, invertTransformationMatrix);
//        }

//        /*
//        private void ResetAllTouchData()
//        {
//            // Reset all touch information.
//            touchLocationList.Clear();

//            TouchPositions.Clear();
//            TouchStates.Clear();

//            //for (Byte index = 0; index < maxInputs; index++)
//            //{
//            //    TouchPositions[index].Ad = Vector2.Zero;
//            //    TouchStates[index] = false;

//            //    //TouchPositionsX[index] = Vector2.Zero;
//            //    //TouchStatesX[index] = TouchLocationState.Invalid;
//            //}
//        }
//        */

//        //private IList<TouchLocation> GetTouchLocationList()
//        //{
//        //    TouchCollection touchCollection = TouchPanel.GetState();
//        //    if (0 == touchCollection.Count)
//        //    {
//        //        return touchLocationList;
//        //    }

//        //    int count = Math.Max(maxInputs, touchCollection.Count);
//        //    for (Byte index = 0; index < touchCollection.Count; index++)
//        //    {
//        //        TouchLocation touchLocation = touchCollection[index];

//        //        TouchPositionsX[index] = GetTouchPosition(touchLocation.Position);
//        //        TouchStatesX[index] = touchLocation.State;
//        //    }

//        //    return touchLocationList;
//        //}

		

//        //private static TouchLocation? GetTouchLocation()
//        //{
//        //    TouchCollection touchCollection = TouchPanel.GetState();
//        //    if (touchCollection.Count > 0)
//        //    {
//        //        return touchCollection[0];
//        //    }

//        //    return null;
//        //}

//            // TODO delete!
//        //private void InitializeTouchData()
//        //{
			
//        //}

//        public IList<Vector2> TouchPositions { get; private set; }
//        public IList<Boolean> TouchStates { get; private set; }

//        public Vector2[] TouchPositionsX { get; private set; }
//        public TouchLocationState[] TouchStatesX { get; private set; }
//        public Boolean Tap { get; private set; }
//        public Boolean Hold { get; private set; }
//        public Boolean DoubleTap { get; private set; }
//        public Boolean HorizontalDrag { get; private set; }
//        public Boolean VerticalDrag { get; private set; }
//    }
//}