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
        public string[,] carteArray;

        System.IO.StreamReader lireCarte;

        public MoteurSysteme()
        {
            InitCarteArray(Environment.CurrentDirectory + @"\carte1.txt");

            LireCarte(Environment.CurrentDirectory + @"\carte1.txt");
            LireCarte(Environment.CurrentDirectory + @"\carte2.txt");
        }

        private void InitCarteArray(String asset)
        {
            lireCarte = new System.IO.StreamReader(asset);

            carteArray = new string[Convert.ToInt32(lireCarte.ReadLine()), Convert.ToInt32(lireCarte.ReadLine())];

            for (int x = 0; x < carteArray.GetLength(0); x++)
                for (int y = 0; y < carteArray.GetLength(1); y++)
                    carteArray[y, x] = "";

            lireCarte.Close();
        }

        private void LireCarte(String asset)
        {
            lireCarte = new System.IO.StreamReader(asset);

            int width = Convert.ToInt32(lireCarte.ReadLine());
            int height = Convert.ToInt32(lireCarte.ReadLine());

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char c = (Char)lireCarte.Read();
                    
                    if (c != ' ')
                        carteArray[y, x] += c;

                    if (x == (width - 1) && y != (height - 1)) // passe le char de retour à la ligne
                    {
                        lireCarte.Read();
                        lireCarte.Read();
                    }
                }

            }

            lireCarte.Close();
        }

    }
}
