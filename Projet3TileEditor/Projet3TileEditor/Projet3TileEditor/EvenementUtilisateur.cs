using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Projet3TileEditor
{
    class EvenementUtilisateur
    {
        public MouseState mouseState;
        MouseState oldMouseState;

        public KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        int mouseScroll;
        int oldMouseScroll;

        public EvenementUtilisateur()
        {
        }

        public void Update()
        {
            oldMouseScroll = mouseScroll;
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseScroll = mouseState.ScrollWheelValue;

            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        public bool isLeftClick()
        {
            return (oldMouseState.LeftButton == ButtonState.Released) && (mouseState.LeftButton == ButtonState.Pressed);
        }

        public bool isRightClick()
        {
            return (oldMouseState.RightButton == ButtonState.Released) && (mouseState.RightButton == ButtonState.Pressed);
        }

        public bool IsKeyUsed(Keys key)
        {
            return oldKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

        public bool isMouseWheelDown()
        {
            return oldMouseScroll - mouseScroll > 0;
        }

        public bool isMouseWheelUp()
        {
            return oldMouseScroll - mouseScroll < 0;
        }
    }
}
