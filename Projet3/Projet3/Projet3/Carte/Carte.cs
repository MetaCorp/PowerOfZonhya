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

        List<Tuile>[,] tuileArray;

        public Vector2 tuileHover;

        Vector2 camera;

        int tileWidth;
        int tileHeight;

        int tileStepX;
        int tileStepY;

        public Carte(MoteurPhysique moteurPhysique, List<string>[,] charArray, Vector2 camera, int tileWidth, int tileHeight, int tileStepX, int tileStepY)
        {
            this.moteurPhysique = moteurPhysique;
            this.camera = camera;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tileStepX = tileStepX;
            this.tileStepY = tileStepY;

            tuileArray = new List<Tuile>[charArray.GetLength(0), charArray.GetLength(1)];

            for (int x = 0; x < charArray.GetLength(0); x++)
                for (int y = 0; y < charArray.GetLength(1); y++)
                    tuileArray[y, x] = new List<Tuile>();

            InitTileArray(charArray);

            tuileHover = new Vector2(10, 10);
        }

        private void InitTileArray(List<string>[,] charArray)
        {

            for (int x = 0; x < charArray.GetLength(0); x++)
            {
                for (int y = 0; y < charArray.GetLength(1); y++)
                {
                    Vector2 positionSource = Vector2.Zero;

                    bool isCollision = false;

                    foreach (string tuileStr in charArray[y, x])
                    {

                        switch (tuileStr[0])
                        {
                            case 'a':
                                positionSource = new Vector2(0, 0);
                                break;

                            case 'b':
                                positionSource = new Vector2(1, 0);
                                isCollision = true;
                                break;

                            case 'c':
                                positionSource = new Vector2(2, 0);
                                break;

                            case 'd':
                                positionSource = new Vector2(3, 0);
                                break;

                            case 'e':
                                positionSource = new Vector2(4, 0);
                                break;

                            case 'f':
                                positionSource = new Vector2(5, 0);
                                break;

                            case 'g':
                                positionSource = new Vector2(6, 0);
                                break;

                            case 'h':
                                positionSource = new Vector2(7, 0);
                                break;

                            case 'i':
                                positionSource = new Vector2(8, 0);
                                break;

                            case 'j':
                                positionSource = new Vector2(9, 0);
                                break;

                            case 'k':
                                positionSource = new Vector2(10, 0);
                                break;

                            case 'l':
                                positionSource = new Vector2(11, 0);
                                break;

                            case 'm':
                                positionSource = new Vector2(12, 0);
                                break;

                            case 'n':
                                positionSource = new Vector2(13, 0);
                                break;

                            case 'o':
                                positionSource = new Vector2(14, 0);
                                break;

                            case 'p':
                                positionSource = new Vector2(15, 0);
                                break;

                            default: //('a')
                                positionSource = new Vector2(7, 0);
                                break;
                        }

                        tuileArray[x, y].Add(new Tuile(new Vector2(x, y), positionSource, isCollision, int.Parse(tuileStr.Substring(1, 2))));
                    }
                }
            }
        }

        public void SetCarte()
        {
            moteurPhysique.SetCarte(tuileArray);
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

            tuileHover = new Vector2((int)tuileHover.X, (int)tuileHover.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < tuileArray.GetLength(0); x++)
                for (int y = 0; y < tuileArray.GetLength(1); y++)
                    foreach (Tuile tuile in tuileArray[y, x])
                    {
                        Vector2 vIso = Constante.ConvertToIso(tuile.position);

                        float layer;

                        Color color;

                        if (tuile.position == tuileHover)
                            color = Color.Gray;
                        else
                            color = Color.White;

                        if (tuile.hauteur >= 0)
                            layer = (float)Math.Exp(-vIso.Y / 100 - tuile.hauteur / 1.2);
                        else
                            layer = (float)Math.Exp(-vIso.Y / 100);

                        //if ((vIso.X + camera.X > -tileWidth && vIso.X + camera.X < Constante.WindowWidth) && (vIso.Y + camera.Y > -tileHeight && vIso.Y + camera.Y < Constante.WindowHeight))
                        spriteBatch.Draw(texture, new Rectangle((int)((vIso.X + camera.X)), (int)((vIso.Y + camera.Y)) - (int)((tuile.hauteur - 1) * 12), (int)(tileWidth), (int)(tileHeight)),
                                        tuile.rectangleSource, color,
                                        0, Vector2.Zero, SpriteEffects.None, layer);
                    }

            Vector2 vIso2 = Constante.ConvertToIso(new Vector2((int)tuileHover.X + 1, (int)tuileHover.Y + 1));

            //spriteBatch.Draw(textureTileHover, new Rectangle((int)(vIso2.X + camera.X), (int)(vIso2.Y + camera.Y), 64, 32), new Rectangle(0, 0, 64, 32), Color.White,
            //    0, Vector2.Zero, SpriteEffects.None, 0);

        }

    }
}
