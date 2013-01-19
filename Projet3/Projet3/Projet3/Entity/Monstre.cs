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
    enum MonstreType
    {
        brasegali,
        rondoudou
    }

    class Monstre
    {
        public MonstreType type;

        Texture2D texture;
        SpriteFont font;

        public Vector2 positionTile;
        public Vector2 positionTileCombat;
        public Vector2 positionTileCarte;

        Vector2 deplacement;
        int direction;
        float vitesse;

        Vector2 camera;

        SpriteAnime spriteAnime;

        public bool isAggro;
        bool revert;
        bool isMoving;
        bool isCombat;

        String message;
        float timeToSay;
        float timeElapsedMessage;

        List<Vector2> path = new List<Vector2>();

        float timeElapsedMove;
        float timeToMove;

        float timeToWait;

        public Monstre(MonstreType type, Vector2 position)
        {
            this.type = type;

            positionTileCarte = position;
            positionTile = positionTileCarte;

            vitesse = 0.05f;

            timeElapsedMove = 0;
            timeToMove = 5;// bouge toutes les 5 secondes
            direction = 2;

            timeToSay = 3;

            isAggro = false;
        }

        public void LoadTexture(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;

            message = "";

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
            if (isCombat)
                positionTile = positionTileCombat;
            else
                positionTile = positionTileCarte;

            timeElapsedMove += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeToWait > 0)
                timeToWait -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (message != "")
                timeElapsedMessage += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsedMessage > timeToSay)
            {
                timeElapsedMessage = 0;
                message = "";
            }



            if (timeElapsedMove > timeToMove)
            {
                timeElapsedMove -= timeToMove + Constante.Random.Next(-3, 3);

                if (!isAggro)
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

            if (isCombat)
                positionTileCombat = positionTile;
            else
                positionTileCarte = positionTile;
        }

        public void Combat(Vector2 position)
        {
            isCombat = true;
            positionTileCombat = position;
            path.Clear();
        }

        public void BougerRnd(MoteurPhysique moteurPhysique)
        {
            if (path.Count == 0)
            {
                int radius = 3;

                Vector2 nextPosition;

                do
                {
                nextPosition.X = Constante.Random.Next(-radius, radius);
                nextPosition.Y = Constante.Random.Next(-radius, radius);
                } while ((nextPosition.X <= 0 && nextPosition.X >= Constante.TotalWidth) && ((nextPosition.Y <= 0 && nextPosition.Y >= Constante.TotalHeight)));

                path = moteurPhysique.GetPath(positionTile, positionTile + nextPosition);
            }
        }

        public bool IsAggro(Vector2 positionJoueur)
        {
            if (direction == 1 && positionJoueur.Y == positionTile.Y && (positionJoueur.X - positionTile.X <= 3 && positionJoueur.X - positionTile.X >= 0))
                return true;
            else if (direction == 3 && positionJoueur.X == positionTile.X && (positionJoueur.Y - positionTile.Y <= 3 && positionJoueur.Y - positionTile.Y >= 0))
                return true;
            else if (direction == 5 && positionJoueur.Y == positionTile.Y && (positionJoueur.X - positionTile.X >= -3 && positionJoueur.X - positionTile.X <= 0))
                return true;
            else if (direction == 7 && positionJoueur.X == positionTile.X && (positionJoueur.Y - positionTile.Y >= -3 && positionJoueur.Y - positionTile.Y <= 0))
                return true;

            else if (direction == 2 && (positionJoueur.X - positionTile.X <= 3 && positionJoueur.X - positionTile.X >= 0) &&
                                       (positionJoueur.Y - positionTile.Y <= 3 && positionJoueur.Y - positionTile.Y >= 0) &&
                                       (positionTile.X - positionTile.Y == positionJoueur.X - positionJoueur.Y))
                return true;

            return false;
        }

        public void Aggro(Vector2 positionJoueur, MoteurPhysique moteurPhysique)
        {
            if (!isAggro)
            {
                timeToWait = 1;
                MoteurAudio.PlaySound("Aggro");
                isAggro = true;
            }

            if (timeToWait <= 0)
                Bouger(positionJoueur, moteurPhysique);
        }

        public void Dire(String message)
        {
            this.message = message;
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

            if (message != "")
            {
                spriteBatch.DrawString(font, message, Constante.ConvertToIso(positionTile) + camera + new Vector2(23, 10), Color.Black);
                //reste a afficher la bulle
            }
        }
    }
}
