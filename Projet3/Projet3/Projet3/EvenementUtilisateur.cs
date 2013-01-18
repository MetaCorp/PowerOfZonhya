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

namespace Projet3
{
    class EvenementUtilisateur
    {
        public MouseState mouseState;

        public KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        public EvenementUtilisateur()
        {

        }

        public void Update()
        {
            mouseState = Mouse.GetState();

            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        public bool IsKeyUsed(Keys key)
        {
            return oldKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

    }
}
