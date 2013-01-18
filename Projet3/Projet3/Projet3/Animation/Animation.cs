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
        Texture2D texture;
        SpriteFont font;

        Vector2 position;
        Vector2 deplacement;
        float vitesse;

        int width;
        int height;

        public bool isFinished;
        public bool isActive;

        public Animation()
        {
            isFinished = true;

            isActive = false;

            deplacement = new Vector2(-1, 0);

            width = Constante.WindowWidth;
            height = 200;
            vitesse = 3;
        }

        public void LoadTexture(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;

        }

        public void Update()
        {
            vitesse = (float)Math.Pow(position.X, 2) / 1500 + 0.4f;
            position += deplacement * vitesse;

            if (isFinished)
                isActive = false;

            if (position.X + Constante.WindowWidth < 0)
                isFinished = true;
        }

        public void Lancer()
        {
            isActive = true;
            isFinished = false;
            position = new Vector2(Constante.WindowWidth, Constante.WindowHeight / 2 - height/2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);
            spriteBatch.DrawString(font, "VS", position + new Vector2(Constante.WindowWidth/2 - 5, height/2 - 5), Color.White);
        }
    }
}
