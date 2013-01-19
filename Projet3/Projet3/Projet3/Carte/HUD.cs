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
    class HUD
    {
        Texture2D texture;
        Texture2D textureVignette;
        SpriteFont font;

        Personnage joueur;

        public HUD(Personnage joueur)
        {
            this.joueur = joueur;
        }

        public void LoadTexture(Texture2D texture, Texture2D textureVignette, SpriteFont font)
        {
            this.texture = texture;
            this.textureVignette = textureVignette;
            this.font = font;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, joueur.nom, new Vector2(75, 10), Color.White);// pseudo

            spriteBatch.Draw(texture, new Rectangle(66, 29, (int)((float)joueur.vie / (float)joueur.vieMax * 100), 3), new Rectangle(0, 21, 2, 2), Color.White);// vie
            spriteBatch.DrawString(font, joueur.vie.ToString() + "/" + joueur.vieMax.ToString(), new Vector2(100, 24), Color.White);
            
            spriteBatch.Draw(texture, new Rectangle(66, 29 + 13, (int)((float)joueur.mana / (float)joueur.manaMax * 100), 3), new Rectangle(2, 21, 2, 2), Color.White);// mana
            spriteBatch.DrawString(font, joueur.mana.ToString() + "/" + joueur.manaMax.ToString(), new Vector2(100, 24 + 13), Color.White);
            
            spriteBatch.Draw(texture, new Rectangle(66, 29 + 13 * 2, (int)((float)joueur.experience / (float)joueur.experienceMax * 100), 3), new Rectangle(4, 21, 2, 2), Color.White);// exp
            spriteBatch.DrawString(font, joueur.experience.ToString() + "/" + joueur.experienceMax.ToString(), new Vector2(100, 24 + 13 * 2), Color.White);

            spriteBatch.Draw(texture, new Rectangle(5, 5, 65, 65), new Rectangle(29, 111, 20, 20), Color.White);// vignette

            spriteBatch.Draw(textureVignette, new Rectangle(9, 9, 54, 50), new Rectangle(50, 0, 220, 200), Color.White);

            for (int i = 0; i < 3; i++)
                spriteBatch.Draw(texture, new Rectangle(164, 24 + i * 13, 13, 13), new Rectangle(131, 62, 10, 10), Color.White);// truc triangle

        }
    }
}
