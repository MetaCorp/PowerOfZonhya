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
    class MoteurGraphique
    {

        MoteurJeu moteurJeu;

        SpriteFont font;

        Texture2D tileSet;
        Texture2D tileHover;
        Texture2D fond;
        Texture2D bouton;

        Texture2D tileSelect;

        public MoteurGraphique(MoteurJeu moteurJeu)
        {
            this.moteurJeu = moteurJeu;
        }

        public void LoadContent(ContentManager content)
        {
            tileSet = content.Load<Texture2D>("isometric_tile");
            tileHover = content.Load<Texture2D>("hilight");
            tileSelect = content.Load<Texture2D>("select");
            bouton = content.Load<Texture2D>("bouton");

            fond = content.Load<Texture2D>("color");

            font = content.Load<SpriteFont>("SpriteFont1");

            moteurJeu.carte.LoadTexture(tileSet, tileHover);
            moteurJeu.gui.LoadTexture(tileSet, tileSelect, fond, bouton, font);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            moteurJeu.carte.Draw(spriteBatch);
            moteurJeu.gui.Draw(spriteBatch);
        }
    }
}
