//using System;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;

//namespace WindowsGame.Common.Inputs.Types
//{
//    public interface IKeyboardInput
//    {
//        void Update(GameTime gameTime);
//        Boolean KeyHold(Keys key);
//        Boolean KeyPress(Keys key);
//    }

//    public class KeyboardInput : IKeyboardInput
//    {
//        private KeyboardState currKeyboardState;
//        private KeyboardState prevKeyboardState;

//        public void Update(GameTime gameTime)
//        {
//            prevKeyboardState = currKeyboardState;
//            currKeyboardState = Keyboard.GetState();
//        }

//        public Boolean KeyHold(Keys key)
//        {
//            return currKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key);
			
//        }
//        public Boolean KeyPress(Keys key)
//        {
//            return currKeyboardState.IsKeyDown(key);
//        }
//    }
//}