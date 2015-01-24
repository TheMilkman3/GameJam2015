using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GameJam2015
{
    public interface ControllerListener
    {
        void ButtonDown(Buttons button);
        void ButtonUp(Buttons button);
        void ButtonPress(Buttons button);
        void LeftThumbstickMove(Vector2 pos, ButtonState state);
        void RightThumbstickMove(Vector2 pos, ButtonState state);
        void LeftTrigger(float pos);
        void RightTrigger(float pos);
    }

    public class ControllerInput : GameComponent
    {
        /// <summary>
        /// Creates a ControllerInput for player 1.
        /// </summary>
        /// <param name="g">Game class for the ControllerInput</param>
        public ControllerInput(Game g)
            : base(g)
        {
            mPlayer = PlayerIndex.One;
            mState = GamePad.GetState(PlayerIndex.One);
            Enabled = true;
            this.UpdateOrder = 0;
        }

        /// <summary>
        /// Creates a ControllerInput for the specified player index.
        /// </summary>
        /// <param name="g">Game class for the controller input.</param>
        /// <param name="i">Player index that this controller listener is for.</param>
        public ControllerInput(Game g, PlayerIndex i)
            : base(g)
        {
            mPlayer = i;
            mState = GamePad.GetState(i);
            Enabled = true;
            this.UpdateOrder = 0;
        }


        public ControllerListener Listener
        {
            get { return mListener; }
            set { mListener = value; }
        }

        /// <summary>
        /// Property for the player this controller is used by.
        /// </summary>
        public PlayerIndex Player
        {
            get { return mPlayer; }
            set { mPlayer = value; }
        }

        public override void Update(GameTime gameTime)
        {
            GamePadState newstate = GamePad.GetState(mPlayer);

            // don't do anything if the controller is not connected
            if (!newstate.IsConnected)
                return;

            // only check the state if the packet number has changed
            if (mState.PacketNumber == newstate.PacketNumber)
                return;

            if (newstate.ThumbSticks.Left != mState.ThumbSticks.Left)
                NotifyLeftThumbstickMoved(newstate.ThumbSticks.Left, newstate.Buttons.LeftStick);

            if (newstate.ThumbSticks.Right != mState.ThumbSticks.Right)
                NotifyRightThumbstickMoved(newstate.ThumbSticks.Right, newstate.Buttons.RightStick);

            if (newstate.Triggers.Left != mState.Triggers.Left)
                NotifyLeftTrigger(newstate.Triggers.Left);

            if (newstate.Triggers.Right != mState.Triggers.Right)
                NotifyRightTrigger(newstate.Triggers.Right);


            if (newstate.DPad.Down != mState.DPad.Down)
            {
                if (newstate.DPad.Down == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.DPadDown);
                else
                {
                    NotifyButtonUp(Buttons.DPadDown);
                    NotifyButtonPressed(Buttons.DPadDown);
                }
            }

            if (newstate.DPad.Up != mState.DPad.Up)
            {
                if (newstate.DPad.Up == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.DPadUp);
                else
                {
                    NotifyButtonUp(Buttons.DPadUp);
                    NotifyButtonPressed(Buttons.DPadUp);
                }
            }

            if (newstate.DPad.Left != mState.DPad.Left)
            {
                if (newstate.DPad.Left == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.DPadLeft);
                else
                {
                    NotifyButtonUp(Buttons.DPadLeft);
                    NotifyButtonPressed(Buttons.DPadLeft);
                }
            }
            if (newstate.DPad.Right != mState.DPad.Right)
            {
                if (newstate.DPad.Right == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.DPadRight);
                else
                {
                    NotifyButtonUp(Buttons.DPadRight);
                    NotifyButtonPressed(Buttons.DPadRight);
                }
            }

            if (newstate.Buttons.A != mState.Buttons.A)
            {
                if (newstate.Buttons.A == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.A);
                else
                {
                    NotifyButtonUp(Buttons.A);
                    NotifyButtonPressed(Buttons.A);
                }
            }

            if (newstate.Buttons.B != mState.Buttons.B)
            {
                if (newstate.Buttons.B == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.B);
                else
                {
                    NotifyButtonUp(Buttons.B);
                    NotifyButtonPressed(Buttons.B);
                }
            }

            if (newstate.Buttons.X != mState.Buttons.X)
            {
                if (newstate.Buttons.X == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.X);
                else
                {
                    NotifyButtonUp(Buttons.X);
                    NotifyButtonPressed(Buttons.X);
                }
            }

            if (newstate.Buttons.Y != mState.Buttons.Y)
            {
                if (newstate.Buttons.Y == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.Y);
                else
                {
                    NotifyButtonUp(Buttons.Y);
                    NotifyButtonPressed(Buttons.Y);
                }
            }

            if (newstate.Buttons.LeftShoulder != mState.Buttons.LeftShoulder)
            {
                if (newstate.Buttons.LeftShoulder == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.LeftShoulder);
                else
                {
                    NotifyButtonUp(Buttons.LeftShoulder);
                    NotifyButtonPressed(Buttons.LeftShoulder);
                }
            }

            if (newstate.Buttons.RightShoulder != mState.Buttons.RightShoulder)
            {
                if (newstate.Buttons.RightShoulder == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.RightShoulder);
                else
                {
                    NotifyButtonUp(Buttons.RightShoulder);
                    NotifyButtonPressed(Buttons.RightShoulder);
                }
            }

            if (newstate.Buttons.LeftStick != mState.Buttons.LeftStick)
            {
                if (newstate.Buttons.LeftStick == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.LeftStick);
                else
                {
                    NotifyButtonUp(Buttons.LeftStick);
                    NotifyButtonPressed(Buttons.LeftStick);
                }
            }

            if (newstate.Buttons.RightStick != mState.Buttons.RightStick)
            {
                if (newstate.Buttons.RightStick == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.RightStick);
                else
                {
                    NotifyButtonUp(Buttons.RightStick);
                    NotifyButtonPressed(Buttons.RightStick);
                }
            }

            if (newstate.Buttons.Start != mState.Buttons.Start)
            {
                if (newstate.Buttons.Start == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.Start);
                else
                {
                    NotifyButtonUp(Buttons.Start);
                    NotifyButtonPressed(Buttons.Start);
                }
            }
            if (newstate.Buttons.Back != mState.Buttons.Back)
            {
                if (newstate.Buttons.Back == ButtonState.Pressed)
                    NotifyButtonDown(Buttons.Back);
                else
                {
                    NotifyButtonUp(Buttons.Back);
                    NotifyButtonPressed(Buttons.Back);
                }
            }

            // update the state
            mState = newstate;
        }

        protected void NotifyButtonDown(Buttons button)
        {
            mListener.ButtonDown(button);
        }

        protected void NotifyButtonUp(Buttons button)
        {
            mListener.ButtonUp(button);
        }

        protected void NotifyButtonPressed(Buttons button)
        {
            mListener.ButtonPress(button);
        }

        protected void NotifyLeftThumbstickMoved(Vector2 pos, ButtonState bs)
        {
            mListener.LeftThumbstickMove(pos, bs);
        }

        protected void NotifyRightThumbstickMoved(Vector2 pos, ButtonState bs)
        {
            mListener.RightThumbstickMove(pos, bs);
        }

        protected void NotifyLeftTrigger(float pos)
        {
            mListener.LeftTrigger(pos);
        }

        protected void NotifyRightTrigger(float pos)
        {
            mListener.RightTrigger(pos);
        }

        private PlayerIndex mPlayer;
        private ControllerListener mListener;
        private GamePadState mState;
    }

    public interface KeyListener
    {
        void KeyDown(Keys key);  // fired whenever a key is pressed down (but not released)
        void KeyUp(Keys key);    // fired whenever a key is released (after having been pressed)
        void KeyPress(Keys key); // fired whenever a key has been pressed and then released
    }

#if !XBOX360
    public interface MouseListener
    {
        void MouseMove(MouseState state);
        void MouseDown(MouseButtons state);
        void MouseUp(MouseButtons state);
        void MouseClick(MouseButtons state);
    }

#endif

#if !XBOX360
    // enumeration of possible mouse buttons so we can switch off a MouseDown, MouseUp, or MouseClick 
    public enum MouseButtons
    {
        LeftButton,
        RightButton,
        MiddleButton
    }

    public class MouseInput : GameComponent
    {
        protected Vector2 mPosition;
        public MouseInput(Game g)
            : base(g)
        {
            // initialize to the current state of the mouse
            mState = Mouse.GetState();
            mListeners = new List<MouseListener>(5);
            Enabled = true;
            UpdateOrder = 0;
        }


        public void AddMouseListener(MouseListener listener)
        {
            // add the listener to our list of listeners
            mListeners.Add(listener);
        }
        public void RemoveMouseListener(MouseListener listener)
        {
            // remove the listener from our list of listeners
            mListeners.Remove(listener);
        }

        public override void Update(GameTime gts)
        {
            MouseState newstate = Mouse.GetState();
            if (mState.X != newstate.X || mState.Y != newstate.Y)
            {
                NotifyMouseMove(newstate);
            }

            if (mState.LeftButton != newstate.LeftButton)
            {
                if (newstate.LeftButton == ButtonState.Pressed)
                    NotifyMouseDown(MouseButtons.LeftButton);
                else
                {
                    NotifyMouseUp(MouseButtons.LeftButton);
                    NotifyMouseClick(MouseButtons.LeftButton);
                }
            }

            if (mState.RightButton != newstate.RightButton)
            {
                if (newstate.RightButton == ButtonState.Pressed)
                    NotifyMouseDown(MouseButtons.RightButton);
                else
                {
                    NotifyMouseUp(MouseButtons.RightButton);
                    NotifyMouseClick(MouseButtons.RightButton);
                }
            }

            if (mState.MiddleButton != newstate.MiddleButton)
            {
                if (newstate.MiddleButton == ButtonState.Pressed)
                {
                    NotifyMouseDown(MouseButtons.MiddleButton);
                }
                else
                {
                    NotifyMouseUp(MouseButtons.MiddleButton);
                    NotifyMouseClick(MouseButtons.MiddleButton);
                }
            }
            mState = newstate;
        }

        protected void NotifyMouseDown(MouseButtons mb)
        {
            for (int i = 0; i < mListeners.Count; i++)
                mListeners[i].MouseDown(mb);
        }

        protected void NotifyMouseUp(MouseButtons mb)
        {
            for (int i = 0; i < mListeners.Count; i++)
                mListeners[i].MouseUp(mb);
        }

        protected void NotifyMouseClick(MouseButtons mb)
        {
            for (int i = 0; i < mListeners.Count; i++)
                mListeners[i].MouseClick(mb);
        }

        protected void NotifyMouseMove(MouseState ms)
        {
            foreach (MouseListener ml in mListeners)
                ml.MouseMove(ms);

        }

        private List<MouseListener> mListeners;
        private MouseState mState;
    }
#endif


    public class KeyboardInput : GameComponent
    {
        /// <summary>
        /// KeyboardInput processes input from a keyboard or chatpad and sends events
        /// to KeyListeners, which are added via AddKeyListener. By default, KeyboardInput has its
        /// UpdateOrder set to 0 and is Enabled.
        /// 
        /// Trying to make a useful KeyboardInput class is a bit more difficult given what MS has given us:
        /// ie, we have to scan all possible keys to generate events generically. My compromise is to
        /// force you to register what keys you're interested in. This way, the time to run update
        /// is proportional to the keys you're interested in, which would be the cost of the old fashioned
        /// way. In particular, each listener has their own set of keys they listen to so that the cost
        /// is not anymore than if you manually checked all the keys for all components you wanted to hear.
        /// 
        /// One final note, I need to look into how things are removed from List because if you call RemoveListener
        /// in the middle of an update, it's bound to throw off foreach.
        /// </summary>
        /// <param name="g">Microsoft.Xna.Framework.Game class that this component will belong to.</param>
        /// <param name="p">PlayerIndex of the player that is using this keyboard.</param>
        public KeyboardInput(Game g)
            : base(g)
        {
            mListeners = new List<KeyboardListener>(4);
            mState = Keyboard.GetState();
            Enabled = true;
            UpdateOrder = 1;
            standard_keyboard = true;
        }

        public KeyboardInput(Game g, PlayerIndex p)
            : base(g)
        {
            mListeners = new List<KeyboardListener>(12);
            mState = Keyboard.GetState(p);
            Enabled = true;
            UpdateOrder = 0;
            standard_keyboard = false;
            mPlayer = p;
        }


        /// <summary>
        /// Adds a KeyListener to the list of listeners to be notified by events.
        /// </summary>
        /// <param name="kl">The listener that wants to be added.</param>
        public void AddKeyListener(KeyListener kl, Keys[] keys)
        {
            mListeners.Add(new KeyboardListener(kl, keys));
        }



        /// <summary>
        /// Removes a KeyListener from the list of listeners that are notified of events.
        /// </summary>
        /// <param name="kl">The listener that wants to be removed.</param>
        public void RemoveKeyListener(KeyListener kl)
        {
            // Hmmm, can this cause a problem? Removing somethign while iterating over the list?
            // certainly seems unsafe threadwise, so watch out...need to look into foreach and
            // collections more
            foreach (KeyboardListener kbl in mListeners)
                if (kbl.Listener == kl)
                    mListeners.Remove(kbl);
        }

        public PlayerIndex Player
        {
            get { return mPlayer; }
            set { mPlayer = value; }
        }

        /// <summary>
        /// Called by the Game class whenever an update is going to occur.
        /// </summary>
        /// <param name="gameTime">Passed in by the Game class.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState newstate;
            // check whether it's a keyboard or a chat pad
            if (standard_keyboard)
                newstate = Keyboard.GetState();
            else
                newstate = Keyboard.GetState(mPlayer);

            // check all the keys we're interested in
            for (int i = 0; i < mListeners.Count; i++)
            {
                // walk through the key array
                foreach (Keys key in mListeners[i].Keys)
                {
                    // this means the key was just pressed down (and holding)
                    if (mState.IsKeyUp(key) && newstate.IsKeyDown(key))
                    {
                        NotifyKeyDown(key);
                    }
                    // this means the key was released, and also indicates a key press
                    else if (mState.IsKeyDown(key) && newstate.IsKeyUp(key))
                    {
                        NotifyKeyUp(key);
                        NotifyKeyPress(key);
                    }
                }
            }

            mState = newstate;
            base.Update(gameTime);
        }



        /// <summary>
        ///  Notifies all the listeners that one or more keys have been pushed down.
        /// </summary>
        /// <param name="keys">Indicates which keys have been pressed down.</param>
        protected void NotifyKeyDown(Keys keys)
        {
            for (int i = 0; i < mListeners.Count; i++)
                mListeners[i].Listener.KeyDown(keys);
        }


        /// <summary>
        ///  Notifies all the listeners that one or more keys have been released.
        /// </summary>
        /// <param name="keys">Indicates which keys have been released.</param>
        protected void NotifyKeyUp(Keys keys)
        {
            for (int i = 0; i < mListeners.Count; i++)
                mListeners[i].Listener.KeyUp(keys);
        }
        /// <summary>
        /// Notifies all the listeners that one or more keys have been pressed.
        /// </summary>
        /// <param name="keys">Indicates which keys have been pressed.</param>
        protected void NotifyKeyPress(Keys keys)
        {
            for (int i = 0; i < mListeners.Count; i++)
                mListeners[i].Listener.KeyPress(keys);
        }

        protected class KeyboardListener
        {
            public KeyboardListener(KeyListener l, Keys[] k)
            {
                mListener = l;
                mKeylist = k;
            }
            public KeyListener Listener
            {
                get { return mListener; }
                set { mListener = value; }
            }

            public Keys[] Keys
            {
                get { return mKeylist; }
                set { mKeylist = value; }
            }
            private Keys[] mKeylist;
            private KeyListener mListener;
        }

        private List<KeyboardListener> mListeners;
        private Microsoft.Xna.Framework.Input.KeyboardState mState;
        private PlayerIndex mPlayer;
        private bool standard_keyboard;
    }
}
