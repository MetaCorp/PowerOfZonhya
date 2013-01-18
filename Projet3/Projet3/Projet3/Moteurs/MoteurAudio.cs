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
    class MoteurAudio
    {
        MoteurJeu moteurJeu;

        SoundEffect sonBouton;
        static SoundEffectInstance sonBoutonInstance;

        SoundEffect sonAggro;
        static SoundEffectInstance sonAggroInstance;

        Song songPlaying;

        Song mainSong;
        Song menuSong;

        public MoteurAudio(MoteurJeu moteurJeu)
        {
            this.moteurJeu = moteurJeu;

            MediaPlayer.IsRepeating = true;
        }

        public void LoadContent(ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("Audio/Pioche");
            sonBoutonInstance = sonBouton.CreateInstance();

            sonAggro = content.Load<SoundEffect>("Audio/WHOOSH");
            sonAggroInstance = sonAggro.CreateInstance();

            mainSong = content.Load<Song>("Audio/18-Theme of Sorrow");
            menuSong = content.Load<Song>("Audio/06-Troia");
        }

        public void Update()
        {
            if (moteurJeu.statusJeu == Status.EnJeu && songPlaying != mainSong)
            {
                MediaPlayer.Play(mainSong);
                songPlaying = mainSong;
            }
            else if (moteurJeu.statusJeu == Status.MenuAccueil && songPlaying != menuSong)
            {
                MediaPlayer.Play(menuSong);
                songPlaying = menuSong;
            }
        }

        public static void PlaySound(String sound)
        {
            if (sound == "Click")
            {
                sonBoutonInstance.Volume = 0.3f;
                sonBoutonInstance.Play();
            }
            else if (sound == "Aggro")
            {
                sonAggroInstance.Volume = 0.2f;
                sonAggroInstance.Play();
            }
        }

        public static void StopSong(String song)
        {
            if (song == "Main")
                MediaPlayer.Stop();
        }
    }
}
