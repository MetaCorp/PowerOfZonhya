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
    class Personnage
    {
        SpriteAnime spriteAnime;

        Vector2 camera;

        public String nom;

        String message;
        float timeToSay; 
        float timeElapsedMessage;


        // Variable pour l'affichage
        Texture2D texture;
        SpriteFont font;

        public Vector2 positionTile;
        public Vector2 positionTileCarte;
        public Vector2 positionTileCombat;

        public int hauteur;

        bool revert;// flip
        bool isMoving;
        bool isCombat;
        public bool isAggro;
        
        // Variable pour le déplacement
        Vector2 deplacement;
        float vitesse;
        List<Vector2> path = new List<Vector2>();

        // Variable pour les caracteristiques
        public int vie, vieMax;
        public int mana, manaMax;
        public int experience, experienceMax;

        int[,] carte;

        public Personnage(String nom, Vector2 position, int[,] carte)
        {
            this.nom = nom;
            positionTileCarte = position;
            positionTile = positionTileCarte;
            vitesse = 0.05f;
            isAggro = false;

            this.carte = carte;

            hauteur = 7;

            timeToSay = 3;

            message = "";

            vieMax = 200;
            vie = 200;
            manaMax = 100;
            mana = 100;
            experienceMax = 1000;
            experience = 300;
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

        public void Update(GameTime gameTime, Vector2 camera)
        {
            hauteur = carte[(int)positionTile.X, (int)positionTile.Y];

            if (isCombat)
                positionTile = positionTileCombat;
            else
                positionTile = positionTileCarte;

            this.camera = camera;

            // Gere les messages
            if (message != "")
                timeElapsedMessage += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsedMessage > timeToSay)
            {
                timeElapsedMessage = 0;
                message = "";
            }


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

            SetDirection(deplacement);

            if (deplacement != Vector2.Zero)
                deplacement.Normalize();
            
            positionTile += deplacement * vitesse;

            positionTile.X = (float)Math.Round(positionTile.X, 2, MidpointRounding.ToEven);
            positionTile.Y = (float)Math.Round(positionTile.Y, 2, MidpointRounding.ToEven);

            spriteAnime.IsAnimate(isMoving);
            spriteAnime.Update(gameTime, Constante.ConvertToIso(positionTile) + new Vector2(16, 20 - (hauteur - 1) * 12) + camera);

            if (isCombat)
                positionTileCombat = positionTile;
            else
                positionTileCarte = positionTile;
        }

        public void Combat(Vector2 position)
        {
            isCombat = true;
            positionTileCombat = position;
        }

        public void FinCombat(int exp)
        {
            isAggro = false;
            isCombat = false;
            path.Clear();
            experience += exp;
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

        public void SetDirection(Vector2 deplacement)
        {
            if (deplacement == new Vector2(1, -1))
            {
                spriteAnime.SetAnimation(2);
                revert = true;
            }
            else if (deplacement == new Vector2(1, 0))
            {
                spriteAnime.SetAnimation(3);
                revert = true;
            }
            else if (deplacement == new Vector2(1, 1))
            {
                spriteAnime.SetAnimation(0);
                revert = false;
            }
            else if (deplacement == new Vector2(0, 1))
            {
                spriteAnime.SetAnimation(3);
                revert = false;
            }
            else if (deplacement == new Vector2(-1, 0))
            {
                spriteAnime.SetAnimation(4);
                revert = false;
            }
            else if (deplacement == new Vector2(-1, 1))
            {
                spriteAnime.SetAnimation(2);
                revert = false;
            }
            else if (deplacement == new Vector2(-1, -1))
            {
                spriteAnime.SetAnimation(1);
                revert = false;
            }
            else if (deplacement == new Vector2(0, -1))
            {
                spriteAnime.SetAnimation(4);
                revert = true;
            }
        }

        public void Bouger(Vector2 tileHover, MoteurPhysique moteurPhysique)
        {
            if (path.Count == 0 && !isAggro)
                path = moteurPhysique.GetPath(positionTile, tileHover);
        }

        public void Aggro(Vector2 position)
        {
            path.Clear();
            isAggro = true;

            Vector2 orientation;

            if (position.X - positionTile.X > 1)
                orientation.X = 1;
            else if (position.X - positionTile.X < -1)
                orientation.X = -1;
            else
                orientation.X = 0;

            if (position.Y - positionTile.Y > 1)
                orientation.Y = 1;
            else if (position.Y - positionTile.Y < -1)
                orientation.Y = -1;
            else
                orientation.Y = 0;

            SetDirection(orientation);

        }

        public void Dire(String message)
        {
            this.message = message;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteAnime.Draw(spriteBatch, revert);
            spriteBatch.DrawString(font, nom, Constante.ConvertToIso(positionTile) + camera + new Vector2(14, - (hauteur - 1) * 12), Color.White);

            if (message != "")
            {
                spriteBatch.DrawString(font, message, Constante.ConvertToIso(positionTile) + camera + new Vector2(0, -15 - (hauteur - 1) * 12), Color.Black);
                //reste a afficher la bulle
            }
        }

    }
}
