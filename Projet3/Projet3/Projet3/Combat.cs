﻿using System;
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
    class Combat
    {
        MoteurPhysique moteurPhysique;

        EvenementUtilisateur evenementUtilisateur;

        public bool isFinished;
        public bool isActive;

        Texture2D textureCarte;
        Texture2D textureTileHover;
        SpriteFont font;

        Personnage joueur;
        Monstre ennemi;

        Carte carte;

        Vector2 camera;

        Vector2 positionPersonnageMap;

        public Combat(MoteurPhysique moteurPhysique, EvenementUtilisateur evenementUtilisateur)
        {
            this.moteurPhysique = moteurPhysique;
            this.evenementUtilisateur = evenementUtilisateur;
            camera = new Vector2(14*32 - 16, 0);
        }

        public void LoadTexture(Texture2D textureCarte, Texture2D textureTileHover, SpriteFont font)
        {
            this.textureCarte = textureCarte;
            this.textureTileHover = textureTileHover;
            this.font = font;
        }

        public void LancerCombat(Personnage joueur, Monstre ennemi)
        {
            this.joueur = joueur;
            this.ennemi = ennemi;

            positionPersonnageMap = joueur.positionTile;

            joueur.positionTile = new Vector2(8, 8);

            ennemi.positionTile = new Vector2(12, 4);

            isActive = true;

            string[,] stringCarte = GenererCarte();

            carte = new Carte(moteurPhysique, stringCarte, camera, 64, 64, 32, 16);
            carte.LoadTexture(textureCarte, textureTileHover);
        }

        private string[,] GenererCarte()
        {
            string[,] stringCarte = new string[15, 15];

            for (int x = 0; x < 15; x++)
                for (int y = 0; y < 15; y++)
                    stringCarte[y, x] = "c/";

            return stringCarte;
        }

        public void Update(GameTime gameTime)
        {
            if (evenementUtilisateur.mouseState.RightButton == ButtonState.Pressed)
                joueur.positionTile = new Vector2((int)carte.tuileHover.X, (int)carte.tuileHover.Y);

            Console.WriteLine("positionJoueur : " + joueur.positionTile);
            Console.WriteLine("tileHover : " + carte.tuileHover);

            carte.Update(gameTime, camera, evenementUtilisateur.mouseState);
            joueur.Update(gameTime, camera);
            ennemi.Update(gameTime, camera, moteurPhysique);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            carte.Draw(spriteBatch);
            ennemi.Draw(spriteBatch);
            joueur.Draw(spriteBatch);
        }
    }
}
