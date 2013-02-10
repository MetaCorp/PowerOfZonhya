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
    class Tuile
    {
        public Rectangle rectangleSource;

        public Vector2 position;

        public bool isCollision;

        public bool isSelected;

        public int hauteur;

        public char type;

        Vector2 positionSource;

        public Tuile(Vector2 position, Vector2 positionSource, bool isCollision, int hauteur)
        {
            type = (char)('a' + positionSource.X);
            this.position = position;
            this.positionSource = positionSource;
            rectangleSource = new Rectangle((int)positionSource.X * 64, (int)positionSource.Y * 64, 64, 64);
            this.isCollision = isCollision;
            this.hauteur = hauteur;
        }

        public void ChangerPositionSource(int i)
        {
            positionSource.X += i;

            if (positionSource.X < 0)
                positionSource.X = 15;
            else
                positionSource.X = (positionSource.X + i) % 16;
            rectangleSource = new Rectangle((int)positionSource.X * 64, (int)positionSource.Y * 64, 64, 64);
        }
    }
}
