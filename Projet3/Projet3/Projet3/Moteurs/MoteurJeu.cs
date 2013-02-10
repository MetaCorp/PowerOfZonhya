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
    enum Status
    {
        MenuAccueil,
        EnPause,
        EnJeu,
        EnCombat,
        Quitter,
        Test
    }

    enum Meteo
    {
        Soleil,
        Pluie,
        Neige
    }

    enum Saison
    {
        Printemps,
        Ete,
        Automne,
        Hiver
    }

    class MoteurJeu
    {
        #region Déclaration des variables
        public Carte carte;
        public Personnage personnage;
        public List<Monstre> monstres = new List<Monstre>();
        public Vector2 camera;

        public Combat combat;

        public MenuManager menuManager;

        public Sprite menuAccueuilFond;

        public Status statusJeu;

        public Meteo meteo;
        public Saison saison;

        public HUD hud;

        public List<Animation> animations = new List<Animation>();

        MoteurSysteme moteurSysteme;
        MoteurPhysique moteurPhysique;

        public EvenementUtilisateur evenementUtilisateur;

        #endregion

        public MoteurJeu(MoteurSysteme moteurSysteme, MoteurPhysique moteurPhysique)
        {
            statusJeu = Status.MenuAccueil;

            meteo = Meteo.Neige;

            menuManager = new MenuManager(this);

            this.moteurPhysique = moteurPhysique;
            this.moteurSysteme = moteurSysteme;

            evenementUtilisateur = new EvenementUtilisateur();
            
            menuAccueuilFond = new Sprite(new Rectangle(0, 0, Constante.WindowWidth, Constante.WindowHeight));

            carte = new Carte(moteurPhysique, moteurSysteme.carteArray, Vector2.Zero, 64, 64, 32, 16);
            
            carte.SetCarte();

            personnage = new Personnage("Meta", new Vector2(3, 3), moteurPhysique.collisionCarte);
            monstres.Add(new Monstre(MonstreType.rondoudou, new Vector2(10, 8), moteurPhysique.collisionCarte));
            //monstres.Add(new Monstre(MonstreType.brasegali, new Vector2(3, 5), moteurPhysique.collisionCarte));

            hud = new HUD(personnage);

            animations.Add(new Animation());

            combat = new Combat(moteurPhysique, evenementUtilisateur);
        }

        public void Update(GameTime gameTime)
        {
            evenementUtilisateur.Update();

            if (statusJeu == Status.EnJeu)
                UpdateJeu(gameTime);
            else if (statusJeu == Status.EnCombat)
                UpdateCombat(gameTime);
            else if (statusJeu == Status.MenuAccueil)
                menuManager.UpdateMenuAccueil(evenementUtilisateur);
            else
                menuManager.UpdateMenuJeu(evenementUtilisateur);
        }

        public void UpdateCombat(GameTime gameTime)
        {
            combat.Update(gameTime);

            if (!combat.isActive)
            {
                for (int i = 0; i < monstres.Count; i++)
                    if (monstres[i].isAggro)
                    {
                        monstres.RemoveAt(i);
                        i--;
                    }

                carte.SetCarte();

                statusJeu = Status.EnJeu;
            }
        }

        public void UpdateJeu(GameTime gameTime)
        {
            if (evenementUtilisateur.IsKeyUsed(Keys.Escape))
            {
                menuManager.isMenuJeuActif = !menuManager.isMenuJeuActif;
            }

            if (menuManager.IsMenuJeuActif())
                menuManager.UpdateMenuJeu(evenementUtilisateur);
            else
            {
                /*foreach (Monstre monstre in monstres) // aggro des monstres
                    if (monstre.IsAggro(personnage.positionTile))
                    {
                        //if (!personnage.isAggro)
                        {
                            monstre.Aggro(personnage.positionTile, moteurPhysique);
                            monstre.Dire("!!!");
                            personnage.Aggro(monstre.positionTile);
                            personnage.Dire("OH MY GOSH !");
                        }

                        if (monstre.positionTile == personnage.positionTile)
                        {
                            if (!animations[0].isActive)
                                animations[0].Lancer();

                            if (animations[0].isFinished)
                            {
                                personnage.isAggro = false;
                                animations[0].isActive = false;
                                combat.LancerCombat(personnage, monstre);
                                statusJeu = Status.EnCombat;
                            }
                        }
                    }*/



                if (evenementUtilisateur.mouseState.RightButton == ButtonState.Pressed)
                    personnage.Bouger(carte.tuileHover, moteurPhysique);

                carte.Update(gameTime, camera, evenementUtilisateur.mouseState);
                personnage.Update(gameTime, camera);

                foreach (Monstre monstre2 in monstres)
                    monstre2.Update(gameTime, camera, moteurPhysique);

                hud.Update();

                UpdateCamera();

                foreach (Animation animation in animations)
                    if (!animation.isFinished)
                        animation.Update();
            }
                
        }

        public void UpdateCamera()
        {
            Vector2 position = Constante.ConvertToIso(personnage.positionTile) + camera;
            float vitesse = 300;

            if (position.X < 300) // calcul pour déplacer la camera vite fait compliqué
                camera.X += vitesse / (position.X + 100);// / (_position.X-213);
            else if (position.X > 500)
                camera.X -= vitesse / ((-position.X + 800) + 100);

            if (position.Y < 180)
                camera.Y += vitesse / (position.Y + 100);
            else if (position.Y > 300)
                camera.Y -= vitesse / ((-position.Y + 480) + 100);

        }
    }
}
