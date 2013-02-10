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
    class MenuManager
    {
        MoteurJeu moteurJeu;

        Texture2D texture;
        SpriteFont font;

        // ==== Menu Accueil ====
        Menu menuAccueilPrincipal;
        Menu menuAccueilReglage;
        Menu menuAccueilVideo;
        Menu menuAccueilAudio;
        Menu menuAccueilRaccourcis;

        // ====   Menu Jeu   ====
        Menu menuPausePrincipal;
        Menu menuPauseReglage;
        Menu menuPauseMeteo, menuPauseSaison;


        Menu menuActif;

        public bool isMenuJeuActif;

        public MenuManager(MoteurJeu moteurJeu)
        {

            this.moteurJeu = moteurJeu;

            #region Menu Accueil
            menuAccueilPrincipal = new Menu(null, new Vector2(Constante.WindowWidth / 2 - 100 - 200 - 50, Constante.WindowHeight - 100),
                                   new string[3] { "Reglages", "Nouvelle Partie", "Quitter" }, 200, 40, 250, false);

            menuAccueilReglage = new Menu(menuAccueilPrincipal ,new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 120),
                                 new string[4] { "Video", "Audio", "Raccourcis", "Retour" }, 200, 40, 60, true);

            menuAccueilVideo = new Menu(menuAccueilReglage, new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 20),
                                 new string[1] { "Retour" }, 200, 40, 60, true);

            menuAccueilAudio = new Menu(menuAccueilReglage, new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 60 - 30),
                                 new string[3] { "Musique : 100", "Son : 100", "Retour" }, 200, 40, 60, true);

            menuAccueilRaccourcis = new Menu(menuAccueilReglage, new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 20),
                                 new string[1] { "Retour" }, 200, 40, 60, true);

            menuActif = menuAccueilPrincipal.Activer();

            menuAccueilPrincipal.AddFils(menuAccueilReglage);

            menuAccueilReglage.AddFils(menuAccueilVideo);
            menuAccueilReglage.AddFils(menuAccueilAudio);
            menuAccueilReglage.AddFils(menuAccueilRaccourcis);
            #endregion

            #region Menu Jeu
            menuPausePrincipal = new Menu(null, new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 120),
                                 new string[5] { "Reprendre", "Reglage", "Meteo",  "Saison", "Quitter" }, 200, 40, 60, true);

            menuPauseReglage = new Menu(menuAccueilReglage, new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 20),
                                 new string[1] { "Retour" }, 200, 40, 60, true);

            menuPauseMeteo = new Menu(menuPausePrincipal, new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 40 * 2),
                                 new string[4] { "Soleil", "Pluie", "Neige", "retour" }, 200, 40, 40, true);

            menuPauseSaison = new Menu(menuPausePrincipal, new Vector2(Constante.WindowWidth / 2 - 100, Constante.WindowHeight / 2 - 40 * 2),
                                 new string[5] { "Printemps", "Ete", "Automne", "Hiver", "retour" }, 200, 40, 40, true);

            menuPausePrincipal.AddFils(menuPauseReglage);
            menuPausePrincipal.AddFils(menuPauseMeteo);
            menuPausePrincipal.AddFils(menuPauseSaison);
            #endregion
        }

        public void LoadTexture(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;

            menuAccueilPrincipal.LoadTexture(texture, font);
            menuAccueilReglage.LoadTexture(texture, font);
            menuAccueilVideo.LoadTexture(texture, font);
            menuAccueilAudio.LoadTexture(texture, font);
            menuAccueilRaccourcis.LoadTexture(texture, font);

            menuPausePrincipal.LoadTexture(texture, font);
            menuPauseReglage.LoadTexture(texture, font);
            menuPauseSaison.LoadTexture(texture, font);
            menuPauseMeteo.LoadTexture(texture, font);
        }


        public void UpdateMenuAccueil(EvenementUtilisateur user)
        {
            int action = menuActif.Update(user);

            if (action != -1)
            {
                if (menuAccueilPrincipal.isActive)
                {
                    if (action == 1)
                    {
                        moteurJeu.statusJeu = Status.EnJeu;
                        menuActif = menuPausePrincipal.Activer();
                    }
                    else if (action == 2)
                        moteurJeu.statusJeu = Status.Quitter;
                    else
                        menuActif = menuAccueilPrincipal.menuFils[0].Activer();
                }
                else if (menuAccueilReglage.isActive)
                {
                    if (action == 3)
                        menuActif = menuAccueilReglage.Desactiver();
                    else
                        menuActif = menuAccueilReglage.menuFils[action].Activer();
                }
                else if (menuAccueilVideo.isActive)
                {
                    if (action == 0)
                        menuActif = menuAccueilVideo.Desactiver();
                }
                else if (menuAccueilAudio.isActive)
                {
                    if (action == 0)
                        MoteurAudio.songVolume = (MoteurAudio.songVolume + 1) % 101;
                    else if (action == 1)
                        MoteurAudio.soundVolume = (MoteurAudio.soundVolume + 1) % 101;
                    else if (action == 2)
                        menuActif = menuAccueilAudio.Desactiver();

                    menuAccueilAudio.ChangeTitle(0, "Musique : " + MoteurAudio.songVolume);
                    menuAccueilAudio.ChangeTitle(1, "Son : " + MoteurAudio.soundVolume);
                }
                else if (menuAccueilRaccourcis.isActive)
                {
                    if (action == 0)
                        menuActif = menuAccueilRaccourcis.Desactiver();
                }
            }
        }

        public void UpdateMenuJeu(EvenementUtilisateur user)
        {
            int action = menuActif.Update(user);

            if (action != -1)
            {
                if (menuPausePrincipal.isActive)
                {
                    if (action == 0)
                        isMenuJeuActif = false;
                    else if (action == 4)
                    {
                        moteurJeu.statusJeu = Status.MenuAccueil;
                        menuActif = menuAccueilPrincipal.Activer();
                    }
                    else
                        menuActif = menuPausePrincipal.menuFils[action - 1].Activer();
                }
                else if (menuPauseReglage.isActive)
                {
                    if (action == 0)
                        menuActif = menuPauseReglage.Desactiver();
                }
                else if (menuPauseMeteo.isActive)
                {
                    if (action == 3)
                        menuActif = menuPauseMeteo.Desactiver();
                }
                else if (menuPauseSaison.isActive)
                {
                    if (action == 4)
                        menuActif = menuPauseMeteo.Desactiver();
                }
            }
        }

        public bool IsMenuJeuActif()
        {
            return isMenuJeuActif;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menuActif.Draw(spriteBatch);
        }
    }
}
