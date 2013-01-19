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
    class Carte
    {
        MoteurPhysique moteurPhysique;

        // Affichage de la carte
        Texture2D texture;
        Texture2D textureTileHover;

        List<Tuile> tuileArray = new List<Tuile>();

        public Vector2 tuileHover;

        Vector2 camera;

        int tileWidth;
        int tileHeight;

        int tileStepX;
        int tileStepY;

        public Carte(MoteurPhysique moteurPhysique, string[,] charArray, Vector2 camera, int tileWidth, int tileHeight, int tileStepX, int tileStepY)
        {
            this.moteurPhysique = moteurPhysique;

            this.camera = camera;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tileStepX = tileStepX;
            this.tileStepY = tileStepY;

            InitTileArray(charArray);

            moteurPhysique.SetCarte(tuileArray, charArray.GetLength(0), charArray.GetLength(1));
            
            tuileHover = new Vector2(10, 10);
        }

        private void InitTileArray(string[,] charArray)
        {

            for (int x = 0; x < charArray.GetLength(0); x++)
            {
                for (int y = 0; y < charArray.GetLength(1); y++)
			    {
                    Rectangle rectangleSource = Rectangle.Empty;

                    bool isCollision = false;

                    foreach (Char charType in charArray[y, x])
                    {
                        switch (charType)
                        {
                            case '.':
                                rectangleSource = new Rectangle(7 * tileWidth, 0 * tileHeight, tileWidth, tileHeight);
                                break;

                            case 'A':
                                rectangleSource = new Rectangle(4 * tileWidth, 5 * tileHeight, tileWidth, tileHeight);
                                isCollision = true;
                                break;

                            case 'b':
                                rectangleSource = new Rectangle(6 * tileWidth, 6 * tileHeight, tileWidth, tileHeight);
                                break;

                            case '/':
                                rectangleSource = new Rectangle(4 * tileWidth, 2 * tileHeight, tileWidth, tileHeight);
                                break;

                            default: //('a')
                                rectangleSource = new Rectangle(0 * tileWidth, 0 * tileHeight, tileWidth, tileHeight);
                                break;
                        }

                        tuileArray.Add(new Tuile (new Vector2(x, y), rectangleSource, isCollision));
                    }
			    }
            }
        }

        public void LoadTexture(Texture2D texture, Texture2D textureTileHover)
        {
            this.texture = texture;
            this.textureTileHover = textureTileHover;
        }

        public void Update(GameTime gameTime, Vector2 camera, MouseState mouseState)
        {
            this.camera = camera;

            SetTuileHover(new Vector2(mouseState.X, mouseState.Y));
        }

        private void SetTuileHover(Vector2 positionSouris)
        {
            Vector2 tuileHoverAux;

            tuileHoverAux.X = (((positionSouris.Y - camera.Y) / 32 + (positionSouris.X - 32 - camera.X) / 64) / 2) * 2;
            tuileHoverAux.Y = (((positionSouris.Y - camera.Y) / 32 - (positionSouris.X - 32 - camera.X) / 64) / 2) * 2;

            if (tuileHoverAux.X > 1 && tuileHoverAux.Y > 1)
                tuileHover = tuileHoverAux - Vector2.One;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tuile tuile in tuileArray)
            {
                Vector2 vIso = Constante.ConvertToIso(tuile.position);

                if ((vIso.X + camera.X > -tileWidth && vIso.X + camera.X < Constante.WindowWidth) && (vIso.Y + camera.Y > -tileHeight && vIso.Y + camera.Y < Constante.WindowHeight))
                    spriteBatch.Draw(texture, new Rectangle((int)(vIso.X + camera.X), (int)(vIso.Y + camera.Y), tileWidth, tileHeight),
                                    tuile.rectangleSource, Color.White);
            }

            Vector2 vIso2 = Constante.ConvertToIso(new Vector2((int)tuileHover.X + 1, (int)tuileHover.Y + 1));

            spriteBatch.Draw(textureTileHover, new Rectangle((int)(vIso2.X + camera.X), (int)(vIso2.Y + camera.Y), 64, 32), Color.White);
         
        }

    }
}
