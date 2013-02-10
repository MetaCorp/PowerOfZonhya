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
    class Carte
    {
        // Affichage de la carte
        Texture2D texture;
        Texture2D textureTileHover;

        public List<Tuile>[,] tuileArray;

        public Vector2 tuileHover;

        Vector2 camera;

        int tileWidth;
        int tileHeight;

        int tileStepX;
        int tileStepY;

        Rectangle rectSelect;

        bool inSelect;
        Vector2 initialPos;

        public Carte(List<string>[,] charArray, Vector2 camera, int tileWidth, int tileHeight, int tileStepX, int tileStepY)
        {
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

            inSelect = false;
            initialPos = Vector2.Zero;

            tuileHover = new Vector2(10, 10);

            rectSelect = new Rectangle(7, 7, 3, 3);
        }

        public void UpdateZoom()
        {
        }

        private void InitTileArray(List<string>[,] charArray)
        {

            for (int x = 0; x < charArray.GetLength(0); x++)
            {
                for (int y = 0; y < charArray.GetLength(1); y++)
                {
                    Vector2 positionSource = Vector2.Zero;

                    bool isCollision = false;

                    foreach(string tuileStr in charArray[y, x])
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
        
        public void LoadTexture(Texture2D texture, Texture2D textureTileHover)
        {
            this.texture = texture;
            this.textureTileHover = textureTileHover;
        }

        public void Update(GameTime gameTime, Vector2 camera, EvenementUtilisateur user, Vector2 typeSelect, char outilSelect)
        {
            this.camera = camera;

            SetTuileHover(new Vector2(user.mouseState.X, user.mouseState.Y));

            if (outilSelect == 'P')
            {
                if (tuileHover.X < tuileArray.GetLength(0) && tuileHover.Y < tuileArray.GetLength(1))
                    rectSelect = new Rectangle((int)tuileHover.X, (int)tuileHover.Y, 1, 1);

                if (user.isLeftClick())
                    AddLayer(typeSelect);
                else if (user.isRightClick())
                    RemoveLayer();

                /*if (user.mouseState.LeftButton == ButtonState.Pressed)
                    if (tuileHover.X < tuileArray.GetLength(0) && tuileHover.Y < tuileArray.GetLength(1))
                        columnSelect = tuileHover;*/

                /*if (user.IsKeyUsed(Keys.Enter) && user.keyboardState.IsKeyDown(Keys.LeftShift) && tuileArray[(int)tuileHover.X, (int)tuileHover.Y].Count > 0)
                    tuileArray[(int)tuileHover.X, (int)tuileHover.Y].RemoveAt(tuileArray[(int)tuileHover.X, (int)tuileHover.Y].Count - 1);
                else if (user.IsKeyUsed(Keys.Enter))
                    tuileArray[(int)tuileHover.X, (int)tuileHover.Y].Add(new Tuile(tuileHover, typeSelect, false, 0));*/


                /*if ((user.IsKeyUsed(Keys.Up) || user.isLeftClick() || user.isMouseWheelUp()) && tuileArray[(int)columnSelect.X, (int)columnSelect.Y].Count < 20)
                {
                    tuileArray[(int)columnSelect.X, (int)columnSelect.Y].Add
                        (new Tuile(columnSelect, typeSelect, false, tuileArray[(int)columnSelect.X, (int)columnSelect.Y].Count));
                }*/

                /*if ((user.IsKeyUsed(Keys.Down) || user.isRightClick() || user.isMouseWheelDown()) && tuileArray[(int)columnSelect.X, (int)columnSelect.Y].Count > 0)
                    tuileArray[(int)columnSelect.X, (int)columnSelect.Y].RemoveAt(tuileArray[(int)columnSelect.X, (int)columnSelect.Y].Count - 1);
                */

                /*if (user.IsKeyUsed(Keys.Right))
                    tuileSelect.ChangerPositionSource(1);

                if (user.IsKeyUsed(Keys.Left))
                    tuileSelect.ChangerPositionSource(-1);

                if (user.IsKeyUsed(Keys.Tab) && user.keyboardState.IsKeyDown(Keys.LeftShift))
                    tuileSelect = tuileArray[(int)tuileSelect.position.X, (int)tuileSelect.position.Y][0];
                else if (user.IsKeyUsed(Keys.Tab))
                    tuileSelect = tuileArray[(int)tuileSelect.position.X, (int)tuileSelect.position.Y][1];*/

                /*if (user.isMouseWheelUp())
                {
                    foreach (Tuile tuile in tuileArray[(int)tuileHover.X, (int)tuileHover.Y])
                        if (tuile.hauteur < 9)
                            tuile.hauteur++;
                }
                else if (user.isMouseWheelDown())
                    foreach (Tuile tuile in tuileArray[(int)tuileHover.X, (int)tuileHover.Y])
                        if (tuile.hauteur > 0)
                            tuile.hauteur--;*/
            }
            else if (outilSelect == 'S')
            {
                if (user.mouseState.LeftButton == ButtonState.Pressed && !inSelect)
                {
                    initialPos = tuileHover;
                    inSelect = true;
                }

                if (inSelect)
                {
                    if (tuileHover.X < initialPos.X)// || tuileHover.Y < initialPos.Y)
                        initialPos.X = tuileHover.X;

                    rectSelect = new Rectangle((int)initialPos.X, (int)initialPos.Y,
                                               (int)Math.Abs(tuileHover.X - initialPos.X) + 1, (int)Math.Abs(tuileHover.Y - initialPos.Y) + 1);
                }

                if (user.mouseState.LeftButton == ButtonState.Released && inSelect)
                    inSelect = false;

                if (rectSelect.X + rectSelect.Width > tuileArray.GetLength(0))// pour ne pas sortir de la map
                    rectSelect.Width = tuileArray.GetLength(0) - rectSelect.X;

                if (rectSelect.Y + rectSelect.Height > tuileArray.GetLength(1))
                    rectSelect.Height = tuileArray.GetLength(1) - rectSelect.Y;
            }

        }

        public void AddDim(Vector2 dim)
        {
            List<Tuile>[,] carteAux = new List<Tuile>[(int)dim.X, (int)dim.Y];

            for (int x = 0; x < tuileArray.GetLength(0); x++)
                for (int y = 0; y < tuileArray.GetLength(1); y++)
                    carteAux[x, y] = tuileArray[x, y];

            for (int x = 0; x < carteAux.GetLength(0); x++)
            {
                carteAux[x, carteAux.GetLength(1) - 1] = new List<Tuile>();
                carteAux[x, carteAux.GetLength(1) - 1].Add(new Tuile(new Vector2(x, carteAux.GetLength(1) - 1), new Vector2(7, 0), false, 0));
            }

            for (int y = 0; y < carteAux.GetLength(1); y++)
            {
                carteAux[carteAux.GetLength(0) - 1, y] = new List<Tuile>();
                carteAux[carteAux.GetLength(0) - 1, y].Add(new Tuile(new Vector2(carteAux.GetLength(0) - 1, y), new Vector2(7, 0), false, 0));
            }

            tuileArray = carteAux;

            Console.WriteLine("new dim : " + tuileArray.GetLength(0));
        }

        public void AddLayer(Vector2 typeSelect)
        {
            for (int x = rectSelect.X; x < rectSelect.X + rectSelect.Width; x++)
                for (int y = rectSelect.Y; y < rectSelect.Y + rectSelect.Height; y++)
                    if (tuileArray[x, y].Count == 0 || tuileArray[x, y][tuileArray[x, y].Count - 1].hauteur < 20)
                        tuileArray[x, y].Add((new Tuile(new Vector2(x, y), typeSelect, false, tuileArray[x, y].Count)));
        }

        public void RemoveLayer()
        {
            for (int x = rectSelect.X; x < rectSelect.X + rectSelect.Width; x++)
                for (int y = rectSelect.Y; y < rectSelect.Y + rectSelect.Height; y++)
                    if (tuileArray[x, y].Count > 0)
                        tuileArray[x, y].RemoveAt(tuileArray[x, y].Count - 1);
        }

        public void UpRect(Vector2 typeSelect)
        {
            for (int x = rectSelect.X; x < rectSelect.X + rectSelect.Width; x++)
                for (int y = rectSelect.Y; y < rectSelect.Y + rectSelect.Height; y++)
                {
                    foreach (Tuile tuile in tuileArray[x, y])
                        tuile.hauteur++;

                    tuileArray[x, y].Add((new Tuile(new Vector2(x, y), typeSelect, false, 0)));
                }
        }

        public void DownRect()
        {
            for (int x = rectSelect.X; x < rectSelect.X + rectSelect.Width; x++)
                for (int y = rectSelect.Y; y < rectSelect.Y + rectSelect.Height; y++)
                    if (tuileArray[x, y].Count > 0)
                    {
                        tuileArray[x, y].RemoveAt(tuileArray[x, y].Count - 1);

                        foreach (Tuile tuile in tuileArray[x, y])
                        {
                            tuile.hauteur--;
                            Console.WriteLine(tuile.hauteur);
                        }
                    }
        }


        private void SetTuileHover(Vector2 positionSouris)
        {
            Vector2 tuileHoverAux;

            /*tuileHoverAux.X = ((((positionSouris.Y - camera.Y) / 32 + (positionSouris.X - 32 - camera.X) / 64) / 2) * 2);
            tuileHoverAux.Y = ((((positionSouris.Y - camera.Y) / 32 - (positionSouris.X - 32 - camera.X) / 64) / 2) * 2);
            */

            tuileHoverAux.X = (((positionSouris.Y - camera.Y) / (Constante.StepY * 2 * Constante.zoom) + (positionSouris.X - 32 * Constante.zoom - camera.X) / (Constante.StepX * 2 * Constante.zoom)) / 2) * 2;
            tuileHoverAux.Y = (((positionSouris.Y - camera.Y) / (Constante.StepY * 2 * Constante.zoom) - (positionSouris.X - 32 * Constante.zoom - camera.X) / (Constante.StepX * 2 * Constante.zoom)) / 2) * 2;

            if (tuileHoverAux.X > 1 && tuileHoverAux.Y > 1)// && tuileHover.X < tuileArray.GetLength(0) && tuileHover.Y < tuileArray.GetLength(1))
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

                        if (tuile.position.X >= rectSelect.X && tuile.position.X < rectSelect.X + rectSelect.Width &&
                            tuile.position.Y >= rectSelect.Y && tuile.position.Y < rectSelect.Y + rectSelect.Height)
                            color = Color.Gray;
                        else
                            color = Color.White;

                        if (tuile.hauteur >= 0)
                            layer = (float)Math.Exp(-vIso.Y / 100 - tuile.hauteur / 1.2);
                        else
                            layer = (float)Math.Exp(-vIso.Y / 100);

                        //if ((vIso.X + camera.X > -tileWidth && vIso.X + camera.X < Constante.WindowWidth) && (vIso.Y + camera.Y > -tileHeight && vIso.Y + camera.Y < Constante.WindowHeight))
                            spriteBatch.Draw(texture, new Rectangle((int)((vIso.X + camera.X) * Constante.zoom), (int)((vIso.Y + camera.Y) * Constante.zoom) - (int)((tuile.hauteur - 1) * 12 * Constante.zoom), (int)(tileWidth * Constante.zoom), (int)(tileHeight * Constante.zoom)),
                                            tuile.rectangleSource, color,
                                            0, Vector2.Zero, SpriteEffects.None, layer);
                    }

            Vector2 vIso2 = Constante.ConvertToIso(new Vector2((int)tuileHover.X + 1, (int)tuileHover.Y + 1));

            //spriteBatch.Draw(textureTileHover, new Rectangle((int)(vIso2.X + camera.X), (int)(vIso2.Y + camera.Y), 64, 32), new Rectangle(0, 0, 64, 32), Color.White,
            //    0, Vector2.Zero, SpriteEffects.None, 0);

        }

    }
}
