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
    class Monstre
    {
        public MonstreType type;

        Texture2D texture;
        SpriteFont font;

        public Vector2 positionTile;
        Vector2 deplacement;
        int direction;
        float vitesse;

        Vector2 camera;

        SpriteAnime spriteAnime;

        bool isAggro;
        bool revert;
        bool isMoving;

        List<Vector2> path = new List<Vector2>();

        float timeElapsed;
        float timeToMove;

        public Monstre(MonstreType type, Vector2 positionTile)
        {
            this.type = type;

            this.positionTile = positionTile;

            vitesse = 0.05f;

            timeElapsed = 0;
            timeToMove = 5;// bouge toutes les 5 secondes
            direction = 2;

            isAggro = false;
        }

        public void LoadTexture(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;

            spriteAnime = new SpriteAnime(texture, 0.1f);

            List<Rectangle> down = new List<Rectangle>();// vers le bas [0]
            down.Add(new Rectangle(32, 0, 32, 32));
            down.Add(new Rectangle(32 * 2, 0, 32, 32));
            down.Add(new Rectangle(32 * 0, 0, 32, 32));
            spriteAnime.AddAnimation(down);

            List<Rectangle> up = new List<Rectangle>();// vers le haut [1]
            up.Add(new Rectangle(32, 32, 32, 32));
            up.Add(new Rectangle(32 * 2, 32, 32, 32));
            up.Add(new Rectangle(32 * 0, 32, 32, 32));
            spriteAnime.AddAnimation(up);

            List<Rectangle> left = new List<Rectangle>();// vers la gauche [2]
            left.Add(new Rectangle(32, 32 * 2, 32, 32));
            left.Add(new Rectangle(32 * 2, 32 * 2, 32, 32));
            left.Add(new Rectangle(32 * 0, 32 * 2, 32, 32));
            spriteAnime.AddAnimation(left);

            List<Rectangle> leftDown = new List<Rectangle>();// vers le bas gauche [3]
            leftDown.Add(new Rectangle(32, 32 * 3, 32, 32));
            leftDown.Add(new Rectangle(32 * 2, 32 * 3, 32, 32));
            leftDown.Add(new Rectangle(32 * 0, 32 * 3, 32, 32));
            spriteAnime.AddAnimation(leftDown);

            List<Rectangle> leftUp = new List<Rectangle>();// vers le haut gauche [4]
            leftUp.Add(new Rectangle(32, 32 * 4, 32, 32));
            leftUp.Add(new Rectangle(32 * 2, 32 * 4, 32, 32));
            leftUp.Add(new Rectangle(32 * 0, 32 * 4, 32, 32));
            spriteAnime.AddAnimation(leftUp);
        }

        public void Update(GameTime gameTime, Vector2 camera, MoteurPhysique moteurPhysique)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToMove)
            {
                timeElapsed -= timeToMove + Constante.Random.Next(-3, 3);

                BougerRnd(moteurPhysique);
            }

            this.camera = camera;

            if (path.Count > 0)
            {
                isMoving = true;

                if (positionTile != path[0])
                    SetDeplacement();
                else
                {
                    deplacement = Vector2.Zero;
                    path.RemoveAt(0);
                }
            }
            else
            {
                isMoving = false;
                deplacement = Vector2.Zero;
            }

            SetDirection();

            if (deplacement != Vector2.Zero)
                deplacement.Normalize();
            
            positionTile += deplacement * vitesse;

            positionTile.X = (float)Math.Round(positionTile.X, 2, MidpointRounding.ToEven);
            positionTile.Y = (float)Math.Round(positionTile.Y, 2, MidpointRounding.ToEven);

            spriteAnime.IsAnimate(isMoving);
            spriteAnime.Update(gameTime, Constante.ConvertToIso(positionTile) + new Vector2(16, 20) + camera);

        }

        public void BougerRnd(MoteurPhysique moteurPhysique)
        {
            if (path.Count == 0)
            {
                int radius = 3;

                Vector2 nextPosition;

                nextPosition.X = Constante.Random.Next(-radius, radius);
                nextPosition.Y = Constante.Random.Next(-radius, radius);

                path = moteurPhysique.GetPath(positionTile, positionTile + nextPosition);
            }
        }

        public void SetDeplacement()
        {
            if (positionTile.X < path[0].X)
                deplacement.X = 1;
            else if (positionTile.X > path[0].X)
                deplacement.X = -1;
            else
                deplacement.X = 0;

            if (positionTile.Y < path[0].Y)
                deplacement.Y = 1;
            else if (positionTile.Y > path[0].Y)
                deplacement.Y = -1;
            else
                deplacement.Y = 0;
        }

        public void SetDirection()
        {
            if (deplacement == new Vector2(1, -1))
            {
                direction = 0;
                spriteAnime.SetAnimation(2);
                revert = true;
            }
            else if (deplacement == new Vector2(1, 0))
            {
                direction = 1;
                spriteAnime.SetAnimation(3);
                revert = true;
            }
            else if (deplacement == new Vector2(1, 1))
            {
                direction = 2;
                spriteAnime.SetAnimation(0);
                revert = false;
            }
            else if (deplacement == new Vector2(0, 1))
            {
                direction = 3;
                spriteAnime.SetAnimation(3);
                revert = false;
            }
            else if (deplacement == new Vector2(-1, 0))
            {
                direction = 5;
                spriteAnime.SetAnimation(4);
                revert = false;
            }
            else if (deplacement == new Vector2(-1, 1))
            {
                direction = 4;
                spriteAnime.SetAnimation(2);
                revert = false;
            }
            else if (deplacement == new Vector2(-1, -1))
            {
                direction = 6;
                spriteAnime.SetAnimation(1);
                revert = false;
            }
            else if (deplacement == new Vector2(0, -1))
            {
                direction = 7;
                spriteAnime.SetAnimation(4);
                revert = true;
            }
        }

        public void Bouger(Vector2 tileHover, MoteurPhysique moteurPhysique)
        {
            if (path.Count == 0)
                path = moteurPhysique.GetPath(positionTile, tileHover);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteAnime.Draw(spriteBatch, revert);


            if (isAggro)
                spriteBatch.DrawString(font, "!!", Constante.ConvertToIso(positionTile) + camera + new Vector2(23, 10), Color.White);
        }
    }
}
