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
    class MoteurSysteme
    {
        public List<string>[,] carteArray;

        System.IO.StreamReader lireCarte;

        System.IO.StreamWriter ecrireCarte;

        public MoteurSysteme()
        {
            InitCarte(Environment.CurrentDirectory + @"\carte1.txt");

            LireCarte(Environment.CurrentDirectory + @"\carte1.txt");

        }

        public void InitCarte(String asset)
        {
            lireCarte = new System.IO.StreamReader(asset);

            carteArray = new List<string>[Convert.ToInt32(lireCarte.ReadLine()), Convert.ToInt32(lireCarte.ReadLine())];

            for (int x = 0; x < carteArray.GetLength(0); x++)
                for (int y = 0; y < carteArray.GetLength(1); y++)
                    carteArray[y, x] = new List<string>();

            lireCarte.Close();
        }

        public void LireCarte(String asset)
        {
            lireCarte = new System.IO.StreamReader(asset);

            int width = Convert.ToInt32(lireCarte.ReadLine());
            int height = Convert.ToInt32(lireCarte.ReadLine());

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    string str = lireCarte.ReadLine();

                    for (int i = 0; i < str.Length; i += 3)
                        carteArray[y, x].Add(str.Substring(i, 3));
                }

            }

            lireCarte.Close();
        }
    }
}
