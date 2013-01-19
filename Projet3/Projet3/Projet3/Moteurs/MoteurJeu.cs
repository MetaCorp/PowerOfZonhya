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
        EnJeu,
        EnCombat,
        Quitter
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

        public Menu menuAccueilPrincipal;
        public Menu menuAccueilReglage;
        public Menu menuAccueilAudio;
        public Sprite menuAccueuilFond;

        public Menu menuPausePrincipal;
        public Menu menuPauseMeteo;
        public Menu menuPauseSaison;

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

            this.moteurPhysique = moteurPhysique;
            this.moteurSysteme = moteurSysteme;

            evenementUtilisateur = new EvenementUtilisateur();

            #region MenuAccueil
            menuAccueilPrincipal = new Menu(new Vector2(Constante.WindowWidth / 2 - 100 - 200 - 50, Constante.WindowHeight - 100), 
                                   new string[3] { "Reglages", "Nouvelle Partie", "Quitter" }, 200, 40, 250, false);

            menuAccueilPrincipal.Activer();

            menuAccueilReglage = new Menu(new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 120),
                                 new string[4] { "Video", "Audio", "Raccourcis", "Retour" }, 200, 40, 60, true);

            menuAccueilAudio = new Menu(new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 60 - 30),
                                 new string[3] { "Musique : ", "Son : ", "Retour" }, 200, 40, 60, true);

            menuAccueuilFond = new Sprite(new Rectangle(0, 0, Constante.WindowWidth, Constante.WindowHeight));
            #endregion

            menuPausePrincipal = new Menu(new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 120),
                                 new string[4] { "Reprendre", "Reglage", "Meteo - Saison", "Quitter" }, 200, 40, 60, true);

            menuPauseMeteo = new Menu(new Vector2(Constante.WindowWidth / 2 - 100 + 100, Constante.WindowHeight / 2 - 40 * 2),
                                 new string[4] { "Soleil", "Pluie", "Neige", "retour"}, 200, 40, 40, true);

            menuPauseSaison = new Menu(new Vector2(Constante.WindowWidth / 2 - 100 - 100, Constante.WindowHeight / 2 - 40 * 2),
                                 new string[4] { "Printemps", "Ete", "Automne", "Hiver" }, 200, 40, 40, true);


            carte = new Carte(moteurPhysique, moteurSysteme.carteArray, Vector2.Zero, 64, 64, 32, 16);
            personnage = new Personnage("Meta", new Vector2(10, 5));
            monstres.Add(new Monstre(MonstreType.rondoudou, new Vector2(14, 8)));
            monstres.Add(new Monstre(MonstreType.brasegali, new Vector2(20, 20)));

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
                UpdateMenu();
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
                if (menuPausePrincipal.isActive)
                {
                    menuPausePrincipal.Desactiver();
                    menuPauseMeteo.Desactiver();
                }
                else
                    menuPausePrincipal.Activer();
            }

            if (menuPauseMeteo.isActive && menuPauseSaison.isActive) // Menu Saison et météo
            {
                int action1 = menuPauseMeteo.Update(evenementUtilisateur.mouseState);

                if (action1 == 0)
                    meteo = Meteo.Soleil;
                else if (action1 == 1)
                    meteo = Meteo.Pluie;
                else if (action1 == 2)
                    meteo = Meteo.Neige;
                else if (action1 == 3)
                {
                    menuPauseMeteo.Desactiver();
                    menuPauseSaison.Desactiver();
                }

                menuPauseMeteo.SetSelectedItem((int)meteo);

                int action2 = menuPauseSaison.Update(evenementUtilisateur.mouseState);

                if (action2 == 0)
                    saison = Saison.Printemps;
                else if (action2 == 1)
                    saison = Saison.Ete;
                else if (action2 == 2)
                    saison = Saison.Automne;
                else if (action2 == 3)
                    saison = Saison.Hiver;

                menuPauseSaison.SetSelectedItem((int)saison);

            }
            else if (menuPausePrincipal.isActive) // Menu Pause
            {
                int action = menuPausePrincipal.Update(evenementUtilisateur.mouseState);

                if (action == 0)
                    menuPausePrincipal.Desactiver();
                else if (action == 2)
                {
                    menuPauseMeteo.Activer();
                    menuPauseSaison.Activer();
                }
                else if (action == 3)
                {
                    menuPausePrincipal.Desactiver();
                    statusJeu = Status.MenuAccueil;
                }
            }
            else // en jeu
            {

                foreach (Monstre monstre in monstres) // aggro des monstres
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
                    }


                if (evenementUtilisateur.mouseState.RightButton == ButtonState.Pressed)
                    personnage.Bouger(carte.tuileHover, moteurPhysique);

                carte.Update(gameTime, camera, evenementUtilisateur.mouseState);
                personnage.Update(gameTime, camera);

                foreach (Monstre monstre in monstres)
                    monstre.Update(gameTime, camera, moteurPhysique);

                hud.Update();

                UpdateCamera();

                foreach (Animation animation in animations)
                    if (!animation.isFinished)
                        animation.Update();
            }
        }

        public void UpdateMenu()
        {
            if (menuAccueilPrincipal.isActive)
            {
                int action = menuAccueilPrincipal.Update(evenementUtilisateur.mouseState);

                if (action == 0)
                {
                    menuAccueilPrincipal.Desactiver();
                    menuAccueilReglage.Activer();
                }
                else if (action == 1)
                    statusJeu = Status.EnJeu;
                else if (action == 2)
                    statusJeu = Status.Quitter;
            }
            else if (menuAccueilReglage.isActive)
            {
                int action = menuAccueilReglage.Update(evenementUtilisateur.mouseState);

                if (action == 3)
                {
                    menuAccueilReglage.Desactiver();
                    menuAccueilPrincipal.Activer();
                }
                else if (action == 1)
                {
                    menuAccueilReglage.Desactiver();
                    menuAccueilAudio.Activer();
                }
            }
            else if (menuAccueilAudio.isActive)
            {
                int action = menuAccueilAudio.Update(evenementUtilisateur.mouseState);

                menuAccueilAudio.ChangeTitle(0, "Musique : " + MoteurAudio.songVolume);
                menuAccueilAudio.ChangeTitle(1, "Son : " + MoteurAudio.soundVolume);

                if (action == 0)
                {
                    MoteurAudio.songVolume = (MoteurAudio.songVolume + 1) % 101;
                }
                else if (action == 1)
                {
                    MoteurAudio.soundVolume = (MoteurAudio.soundVolume + 1) % 101;
                }
                else if (action == 2)
                {
                    menuAccueilAudio.Desactiver();
                    menuAccueilReglage.Activer();
                }
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
