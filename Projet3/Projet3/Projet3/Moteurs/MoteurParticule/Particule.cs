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
    public class Particule
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Vitesse { get; set; }
        public Color Color { get; set; }
        public int TTL { get; set; }
        public float Size { get; set; }

        public Particule(Texture2D texture, Vector2 position, Vector2 direction, float vitesse, int TTL, float size, Color color)
        {
            Texture = texture;
            Position = new Vector2(position.X - size / 2, position.Y - size / 2);
            Direction = direction;
            Vitesse = vitesse;
            this.TTL = TTL;
            Color = color;
            Size = size;
        }

        public void Update(Vector2 gravite, Vector2 vent, float variationVitesse, float variationSize)
        {
            TTL--;

            Vector2 variationDirection = gravite * Size / 20 + vent;

            Direction += variationDirection;
            Direction.Normalize();

            Vitesse += variationVitesse;

            Size += variationSize;

            Position += Direction * Vitesse;// *(float)Math.Pow(TTL - 30, 2f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size, (int)Size), Color);
        }

    }
}
