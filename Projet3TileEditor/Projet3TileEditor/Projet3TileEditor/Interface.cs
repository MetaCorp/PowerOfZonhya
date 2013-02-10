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
    class Interface
    {
        MoteurSysteme moteurSysteme;

        Texture2D texture;
        Texture2D tileSelect;
        Texture2D fond;
        Texture2D bouton;

        SpriteFont font;

        Carte carte;

        public char outilSelect;
        public Vector2 tuileSelect;

        Vector2 position;
        Vector2 positionPalette;

        Bouton boutonModeSelection;
        Bouton boutonModePoint;

        Bouton boutonAddLayer;
        Bouton boutonRemoveLayer;
        Bouton boutonUpLayer;
        Bouton boutonDownLayer;

        Bouton boutonAddDim;

        Bouton boutonSave;

        public Interface(Carte carte, MoteurSysteme moteurSysteme)
        {
            this.carte = carte;
            this.moteurSysteme = moteurSysteme; 

            position = new Vector2(10, 150);
            positionPalette = new Vector2(10, 250);

            outilSelect = 'S';

            boutonModeSelection = new Bouton(new Rectangle((int)position.X, (int)position.Y, 30, 25), "S", "selection tool (s)");
            boutonModePoint = new Bouton(new Rectangle((int)position.X + 30, (int)position.Y, 30, 25), "P", "point tool (p)");

            boutonAddLayer = new Bouton(new Rectangle((int)position.X, (int)position.Y + 50, 30, 25), "a", "add a layer (a)");
            boutonRemoveLayer = new Bouton(new Rectangle((int)position.X + 30, (int)position.Y + 50, 30, 25), "r", "remove a layer (r)");
            boutonUpLayer = new Bouton(new Rectangle((int)position.X + 30 * 2, (int)position.Y + 50, 30, 25), "+", "move up (q)");
            boutonDownLayer = new Bouton(new Rectangle((int)position.X + 30 * 3, (int)position.Y + 50, 30, 25), "-", "move down (s)");

            boutonAddDim = new Bouton(new Rectangle((int)position.X , (int)position.Y + 270, 70, 25), "Dim +", "add a dimension (n)");

            boutonSave = new Bouton(new Rectangle((int)position.X, (int)position.Y + 270 + 50, 150, 25), "Save", "save the current the map (ctrl + s)");

            boutonModePoint.isSelect = true;
        }

        public void LoadTexture(Texture2D texture, Texture2D tileSelect, Texture2D fond, Texture2D bouton, SpriteFont font)
        {
            this.texture = texture;
            this.tileSelect = tileSelect;
            this.font = font;
            this.fond = fond;

            boutonModeSelection.LoadTexture(bouton, font);
            boutonModePoint.LoadTexture(bouton, font);

            boutonAddLayer.LoadTexture(bouton, font);
            boutonRemoveLayer.LoadTexture(bouton, font);
            boutonUpLayer.LoadTexture(bouton, font);
            boutonDownLayer.LoadTexture(bouton, font);

            boutonAddDim.LoadTexture(bouton, font);

            boutonSave.LoadTexture(bouton, font);
        }

        public void Update(EvenementUtilisateur user)
        {
            if ((boutonModeSelection.IsHover(user.mouseState) && user.isLeftClick()) || user.IsKeyUsed(Keys.S))
            {
                outilSelect = 'S';
                boutonModeSelection.isSelect = true;
                boutonModePoint.isSelect = false;
            }

            if ((boutonModePoint.IsHover(user.mouseState) && user.isLeftClick()) || user.IsKeyUsed(Keys.P))
            {
                outilSelect = 'P';
                boutonModePoint.isSelect = true;
                boutonModeSelection.isSelect = false;
            }

            if ((boutonAddLayer.IsHover(user.mouseState) && user.isLeftClick()) || user.IsKeyUsed(Keys.A))
                carte.AddLayer(tuileSelect);            

            if ((boutonRemoveLayer.IsHover(user.mouseState) && user.isLeftClick()) || user.IsKeyUsed(Keys.R))
                carte.RemoveLayer();

            if ((boutonUpLayer.IsHover(user.mouseState) && user.isLeftClick()) || user.IsKeyUsed(Keys.Q))
                carte.UpRect(tuileSelect);

            if ((boutonDownLayer.IsHover(user.mouseState) && user.isLeftClick()) || user.IsKeyUsed(Keys.S))
                carte.DownRect();

            if ((boutonAddDim.IsHover(user.mouseState) && user.isLeftClick()) || user.IsKeyUsed(Keys.N))
                carte.AddDim(new Vector2(carte.tuileArray.GetLength(0) + 1, carte.tuileArray.GetLength(1) + 1));

            if ((boutonSave.IsHover(user.mouseState) && user.isLeftClick()) || (user.IsKeyUsed(Keys.S) && user.keyboardState.IsKeyDown(Keys.LeftControl)))
                moteurSysteme.SauvegarderCarte(carte.tuileArray, Environment.CurrentDirectory + @"\carte1.txt");

            if (user.isLeftClick())
            {
                if (user.mouseState.X > positionPalette.X && user.mouseState.X < positionPalette.X + 4 * 48 &&
                    user.mouseState.Y > positionPalette.Y + 15 && user.mouseState.Y < positionPalette.Y + 15 + 4 * 48)
                {
                    tuileSelect.X = (int)(user.mouseState.X - positionPalette.X) / 48;
                    tuileSelect.Y = (int)(user.mouseState.Y - positionPalette.Y - 15) / 33;

                    tuileSelect.X = tuileSelect.X + tuileSelect.Y * 4;
                    tuileSelect.Y = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fond, new Rectangle(0, 0, 210, Constante.WindowHeight), new Rectangle(0, 0, 2, 2), Color.Gray, 0, Vector2.Zero, SpriteEffects.None, 0.001f);
            
            boutonModeSelection.Draw(spriteBatch);
            boutonModePoint.Draw(spriteBatch);

            boutonAddLayer.Draw(spriteBatch);
            boutonRemoveLayer.Draw(spriteBatch);

            boutonUpLayer.Draw(spriteBatch);
            boutonDownLayer.Draw(spriteBatch);

            boutonAddDim.Draw(spriteBatch);

            boutonSave.Draw(spriteBatch);

            /*
            Color color;

            if (outilSelect == 'S')
                color = Color.Red;
            else
                color = Color.Black;

            spriteBatch.DrawString(font, "S", position + new Vector2(3, 0), color);


            if (outilSelect == 'P')
                color = Color.Red;
            else
                color = Color.Black;

            spriteBatch.DrawString(font, "P", position + new Vector2(3 + 15, 0), color);

            int offset = 15;
            spriteBatch.DrawString(font, "N", position + new Vector2(3, 20), Color.White);
            spriteBatch.DrawString(font, "D", position + new Vector2(3 + offset, 20), Color.White);
            spriteBatch.DrawString(font, "+", position + new Vector2(3 + offset * 2, 20), Color.White);
            spriteBatch.DrawString(font, "-", position + new Vector2(3 + offset * 3, 20), Color.White);
            */
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    spriteBatch.Draw(texture, new Rectangle(i * 48 + (int)positionPalette.X, (int)positionPalette.Y + j * 33, 48, 48), new Rectangle((i + j * 4) * 64, 0, 64, 64), Color.White);
            
            spriteBatch.Draw(tileSelect, new Rectangle((int)(positionPalette.X + (tuileSelect.X % 4) * 48), (int)(positionPalette.Y + (int)(tuileSelect.X / 4) * 33 + 15), 48, 33), Color.White);
        }
    }
}
