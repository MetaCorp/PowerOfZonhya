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
    class Bouton
    {
        Rectangle position;

        Texture2D texture;
        SpriteFont font;

        String titre;
        String description;

        bool isHover;

        public bool isSelect;

        int timeHover;

        public Bouton(Rectangle position, String titre, String description)
        {
            this.position = position;
            this.titre = titre;
            this.description = description;

            isHover = false;
            timeHover = 0;
        }

        public void LoadTexture(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
        }

        public bool IsHover(MouseState mouseState)
        {
            isHover = position.Intersects(new Rectangle(mouseState.X, mouseState.Y, 1, 1));

            if (isHover)
                timeHover++;
            else
                timeHover = 0;

            //Console.WriteLine(timeHover);

            return isHover;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, 20, 20), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0001f);

            Color color = Color.White;

            if (isSelect)
                color = Color.Red;

            spriteBatch.DrawString(font, titre, new Vector2(position.X + 10, position.Y), color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            if (timeHover > 60)
                spriteBatch.DrawString(font, description, new Vector2(position.X + 10, position.Y + 20), Color.Black);

        }


    }
}
