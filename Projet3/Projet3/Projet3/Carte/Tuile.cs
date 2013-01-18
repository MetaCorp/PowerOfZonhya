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

        public Tuile(Vector2 position, Rectangle rectangleSource, bool isCollision)
        {
            this.position = position;
            this.rectangleSource = rectangleSource;
            this.isCollision = isCollision;
        }

    }
}
