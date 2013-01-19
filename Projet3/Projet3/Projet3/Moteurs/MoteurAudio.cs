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

        public static int songVolume;
        public static int soundVolume;

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
            songVolume = 100;
            soundVolume = 100;
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
            MediaPlayer.Volume = (float)songVolume/100;

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
                sonBoutonInstance.Volume = (float)soundVolume / 100;
                sonBoutonInstance.Play();
            }
            else if (sound == "Aggro")
            {
                sonAggroInstance.Volume = (float)soundVolume / 100;
                sonAggroInstance.Play();
            }
        }
    }
}
