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
        SoundEffect sonBouton;
        static SoundEffectInstance sonBoutonInstance;

        static Song mainSong;
        static Song menuSong;

        static String songPlaying;

        public MoteurAudio()
        {
            songPlaying = "";
        }

        public void LoadContent(ContentManager content)
        {
            sonBouton = content.Load<SoundEffect>("Pioche");
            sonBoutonInstance = sonBouton.CreateInstance();

            mainSong = content.Load<Song>("18-Theme of Sorrow");
            menuSong = content.Load<Song>("06-Troia");
        }

        public static void PlaySound(String sound)
        {
            if (sound == "Click")
                sonBoutonInstance.Volume = 0.3f;
            sonBoutonInstance.Play();
        }

        public static void PlaySong(String song)
        {
            MediaPlayer.Volume = 0.5f;
            if (songPlaying != song)
            {
                if (song == "Main")
                    MediaPlayer.Play(mainSong);
                else if (song == "Menu")
                    MediaPlayer.Play(menuSong);
                songPlaying = song;
            }
        }

        public static void StopSong(String song)
        {
            if (song == "Main")
                MediaPlayer.Stop();
        }
    }
}
