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
    class Animation
    {
        public List<Rectangle> rectangles = new List<Rectangle>();

        public Rectangle currentRectangle;

        public int currentImage;

        public Animation()
        {
        }

        public void AddRectangle(Rectangle rectangleSource)
        {
            rectangles.Add(rectangleSource);
            currentRectangle = rectangles[0];
        }

        public void Update(GameTime gameTime)
        {
            currentImage = (currentImage + 1) % rectangles.Count;
            currentRectangle = rectangles[currentImage];
        }
    }
}
