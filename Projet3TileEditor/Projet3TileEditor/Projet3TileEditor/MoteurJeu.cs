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
    class MoteurJeu
    {
        MoteurSysteme moteurSysteme;

        public Carte carte;
        public Interface gui;

        EvenementUtilisateur user;

        Vector2 camera;

        public MoteurJeu(MoteurSysteme moteurSysteme)
        {
            this.moteurSysteme = moteurSysteme;

            camera = new Vector2(550, 100);

            user = new EvenementUtilisateur();

            carte = new Carte(moteurSysteme.carteArray, camera, 64, 64, 32, 16);

            gui = new Interface(carte, moteurSysteme);
        }

        public void Update(GameTime gameTime)
        {
            user.Update();

            gui.Update(user);

            if (user.keyboardState.IsKeyDown(Keys.Down) || Constante.WindowHeight - user.mouseState.Y < 50)
                camera.Y -= 3;
            else if (user.keyboardState.IsKeyDown(Keys.Up) || user.mouseState.Y < 50)
                camera.Y += 3;

            if (user.keyboardState.IsKeyDown(Keys.Right) || Constante.WindowWidth - user.mouseState.X < 50)
                camera.X -= 3;
            else if (user.keyboardState.IsKeyDown(Keys.Left) || (user.mouseState.X < 250 && user.mouseState.X > 200))
                camera.X += 3;

            if (user.isMouseWheelUp())
                Constante.zoom += 0.05f;
            else if (user.isMouseWheelDown())
                Constante.zoom -= 0.05f;

            if (user.IsKeyUsed(Keys.Space))
                Constante.zoom = 1;

            if(user.mouseState.X > 200)
                carte.Update(gameTime, camera, user, gui.tuileSelect, gui.outilSelect);

        }
    }
}
