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
    class MoteurGraphique
    {
        #region Déclaration des variables
        MoteurJeu moteurJeu;

        Texture2D textureCarte;
        Texture2D textureBrasegali;
        Texture2D textureBrasegaliVignette;
        Texture2D textureRondoudou;
        Texture2D textureTileHover;

        Texture2D textureAnimationLancerCombat;
        Texture2D textureFinCombat;

        Texture2D textureMenu;
        Texture2D textureMenuFond;

        SpriteFont fontPersonnage;
        SpriteFont fontMonstre;
        SpriteFont fontMenu;
        SpriteFont fontHUD;

        MoteurParticule moteurParticuleMenu;
        MoteurParticule moteurParticuleMenuSouris;
        MoteurParticule moteurParticulePluie;
        MoteurParticule moteurParticuleNeige;

        List<Texture2D> textureParticuleMenu = new List<Texture2D>();
        List<Texture2D> textureParticuleMenuSouris = new List<Texture2D>();
        List<Texture2D> textureParticulePluie = new List<Texture2D>();
        List<Texture2D> textureParticuleNeige = new List<Texture2D>();
        #endregion

        public MoteurGraphique(MoteurJeu moteurJeu)
        {
            this.moteurJeu = moteurJeu;
        }

        public void LoadContent(ContentManager content)
        {
            textureCarte = content.Load<Texture2D>("Images/Carte/isometric_tile");
            textureTileHover = content.Load<Texture2D>("Images/Carte/hilight");

            textureBrasegali = content.Load<Texture2D>("Images/CharSet/brasegali");
            textureBrasegaliVignette = content.Load<Texture2D>("Images/CharSet/brasegaliVignette");
            textureRondoudou = content.Load<Texture2D>("Images/CharSet/rondoudou");

            textureMenu = content.Load<Texture2D>("Images/GUI/Menu/rpg_gui_v1");
            textureMenuFond = content.Load<Texture2D>("Images/997973hdhsfuc");

            textureAnimationLancerCombat = content.Load<Texture2D>("menubouton");

            fontPersonnage = content.Load<SpriteFont>("font/fontPersonnage");
            fontMonstre = content.Load<SpriteFont>("font/fontMonstre");
            fontMenu = content.Load<SpriteFont>("font/fontMenu");
            fontHUD = content.Load<SpriteFont>("font/fontHUD");

            textureFinCombat = content.Load<Texture2D>("Bouton1");

            moteurJeu.carte.LoadTexture(textureCarte, textureTileHover);
            moteurJeu.personnage.LoadTexture(textureBrasegali, fontPersonnage);

            moteurJeu.animations[0].LoadTexture(textureAnimationLancerCombat, fontMenu);

            foreach (Monstre monstre in moteurJeu.monstres)
            {
                if (monstre.type == MonstreType.rondoudou)
                    monstre.LoadTexture(textureRondoudou, fontMonstre);
                else if (monstre.type == MonstreType.brasegali)
                    monstre.LoadTexture(textureBrasegali, fontMonstre);
            }

            moteurJeu.menuManager.LoadTexture(textureMenu, fontMenu);

            moteurJeu.menuAccueuilFond.LoadTexture(textureMenuFond);

            moteurJeu.hud.LoadTexture(textureMenu, textureBrasegaliVignette, fontHUD);

            moteurJeu.combat.LoadTexture(textureCarte, textureTileHover, textureFinCombat, textureMenu, fontMenu);

            LoadParticule(content);
        }

        public void LoadParticule(ContentManager content)
        {
            textureParticuleMenu.Add(content.Load<Texture2D>("Images/Particules/blue"));
            textureParticuleMenu.Add(content.Load<Texture2D>("Images/Particules/green"));
            textureParticuleMenu.Add(content.Load<Texture2D>("Images/Particules/red"));

            moteurParticuleMenu = new MoteurParticule(textureParticuleMenu, new Vector2(716, 282), Vector2.Zero, Vector2.Zero);
            moteurParticuleMenu.setAngle((float)(-0 * Math.PI / 2.5f), (float)(-6.00 * Math.PI / 3));
            moteurParticuleMenu.setVitesse(1.2f, 2.2f, -0.01f);
            moteurParticuleMenu.setSize(1, 15, 0.0f);
            moteurParticuleMenu.setTTL(55, 60, 0);
            moteurParticuleMenu.Totale = 80;

            textureParticuleMenuSouris.Add(content.Load<Texture2D>("Images/Particules/6"));

            moteurParticuleMenuSouris = new MoteurParticule(textureParticuleMenuSouris, Vector2.Zero, Vector2.Zero, new Vector2(0, 0.08f));
            moteurParticuleMenuSouris.setAngle((float)(-0 * Math.PI / 2.5f), (float)(-6.00 * Math.PI / 3));

            textureParticulePluie.Add(content.Load<Texture2D>("Images/Particules/blue"));

            moteurParticulePluie = new MoteurParticule(textureParticulePluie, new Rectangle(-100, -50, Constante.WindowWidth + 100, 1), Vector2.Zero, new Vector2(0, 0.1f));
            moteurParticulePluie.setAngle((float)(1 * Math.PI / 3f), (float)(0 * Math.PI / 3));
            moteurParticulePluie.setVitesse(3.0f, 3.3f, 0.01f);
            moteurParticulePluie.setSize(1, 10, 0.0f);
            moteurParticulePluie.setTTL(100, 110, 0);
            moteurParticulePluie.Totale = 20;
            moteurParticulePluie.SetColor(255, 255, 255, 255, 255, 255);

            textureParticuleNeige.Add(content.Load<Texture2D>("Images/Particules/6"));

            moteurParticuleNeige = new MoteurParticule(textureParticuleNeige, new Rectangle(-20, -20, Constante.WindowWidth + 40, 1), Vector2.Zero, new Vector2(0, 0.01f));
            moteurParticuleNeige.setAngle((float)(1 * Math.PI / 2f), (float)(0 * Math.PI / 3));
            moteurParticuleNeige.setVitesse(2.0f, 2.3f, 0.0f);
            moteurParticuleNeige.setSize(3, 5, 0.0f);
            moteurParticuleNeige.setTTL(50, 200, 0);
            moteurParticuleNeige.Totale = 5;
            moteurParticuleNeige.SetColor(255, 255, 255, 255, 255, 255);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (moteurJeu.statusJeu == Status.EnJeu)
            {
                moteurJeu.carte.Draw(spriteBatch);

                foreach (Monstre monstre in moteurJeu.monstres)
                    monstre.Draw(spriteBatch);

                moteurJeu.personnage.Draw(spriteBatch);

                moteurJeu.hud.Draw(spriteBatch);

                foreach (Animation animation in moteurJeu.animations)
                    if (!animation.isFinished)
                        animation.Draw(spriteBatch);

                if (moteurJeu.menuManager.IsMenuJeuActif())
                    moteurJeu.menuManager.Draw(spriteBatch);
            }
            else if (moteurJeu.statusJeu == Status.MenuAccueil)
            {
                moteurJeu.menuAccueuilFond.Draw(spriteBatch);

                moteurJeu.menuManager.Draw(spriteBatch);
            }
            else if (moteurJeu.statusJeu == Status.EnCombat)
            {
                moteurJeu.combat.Draw(spriteBatch);
            }
            else
                moteurJeu.menuManager.Draw(spriteBatch);
        }

        public void UpdateParticule()
        {
            if (moteurJeu.statusJeu == Status.MenuAccueil)
            {
                moteurParticuleMenu.Update();
                moteurParticuleMenuSouris.Update(new Vector2(moteurJeu.evenementUtilisateur.mouseState.X, moteurJeu.evenementUtilisateur.mouseState.Y));

                if (moteurJeu.evenementUtilisateur.mouseState.LeftButton == ButtonState.Pressed)
                {
                    moteurParticuleMenuSouris.setVitesse(1.5f, 1.7f, -0.03f);
                    moteurParticuleMenuSouris.setTTL(50, 60, 0);
                    moteurParticuleMenuSouris.Totale = 10;
                }
                else
                {
                    moteurParticuleMenuSouris.setVitesse(0.1f, 0.3f, 0.0f);
                    moteurParticuleMenuSouris.setSize(5, 10, -0.01f);
                    moteurParticuleMenuSouris.setTTL(47, 53, 0);
                    moteurParticuleMenuSouris.Totale = 1;
                }
            }
            else if (moteurJeu.statusJeu == Status.EnJeu && !moteurJeu.menuManager.IsMenuJeuActif())
            {
                if (moteurJeu.meteo == Meteo.Pluie)
                    moteurParticulePluie.Update();
                else if (moteurJeu.meteo == Meteo.Neige)
                    moteurParticuleNeige.Update();
            }
        }

        public void DrawParticule(SpriteBatch spriteBatch)
        {
            if (moteurJeu.statusJeu == Status.MenuAccueil)
            {
                moteurParticuleMenu.Draw(spriteBatch);
                moteurParticuleMenuSouris.Draw(spriteBatch);
            }
            else if (moteurJeu.statusJeu == Status.EnJeu)
            {
                if (moteurJeu.meteo == Meteo.Pluie)
                    moteurParticulePluie.Draw(spriteBatch);
                else if (moteurJeu.meteo == Meteo.Neige)
                    moteurParticuleNeige.Draw(spriteBatch);
            }
        }
    }
}
