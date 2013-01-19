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
    class Bouton
    {
        Texture2D texture;

        SpriteFont font;

        Rectangle rectangle;

        String titre;

        public bool isHover;
        public bool isSelected;

        Rectangle sourceRectNormal;
        Rectangle sourceRectHover;
        Rectangle sourceRectSelec;

        public Bouton(Rectangle rectangle, String titre)
        {
            this.rectangle = rectangle;
            this.titre = titre;

            isSelected = false;

            sourceRectNormal = new Rectangle(3, 31, 70, 14);
            sourceRectHover = new Rectangle(3, 50, 70, 14);
            sourceRectSelec = new Rectangle(3, 69, 70, 14);
        }

        public void ChangeTitle(string titre)
        {
            this.titre = titre;
        }

        public void LoadTexture(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
        }

        private bool IsHover(Vector2 positionSouris)
        {
            return ((positionSouris.X > rectangle.X && positionSouris.X < rectangle.X + rectangle.Width) &&
                    (positionSouris.Y > rectangle.Y && positionSouris.Y < rectangle.Y + rectangle.Height));
        }

        public void Update(Vector2 positionSouris)
        {
            if (IsHover(positionSouris))
                isHover = true;
            else
                isHover = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle;
            Color colorFont;

            if (isSelected)
            {
                sourceRectangle = sourceRectSelec;
                colorFont = Color.Gray;
            }
            else
                if (isHover)
                {
                    sourceRectangle = sourceRectHover;
                    colorFont = Color.Gray;
                }
                else
                {
                    sourceRectangle = sourceRectNormal;
                    colorFont = Color.White;
                }

            int centrerX = rectangle.X + rectangle.Width / 2 - 25 - (int)(titre.Length * 4.2f);

            spriteBatch.Draw(texture, rectangle, sourceRectangle, Color.White);

            spriteBatch.DrawString(font, titre, new Vector2(centrerX, rectangle.Y) + new Vector2(20, 5), colorFont);
        }


    }
}
