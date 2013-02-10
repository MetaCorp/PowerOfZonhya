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

namespace Projet3TileEditor
{
    static class Constante
    {
        public static int WindowWidth = 950;
        public static int WindowHeight = 600;

        public static float zoom = 1f;

        public static int TotalWidth = 40;
        public static int TotalHeight = 40;

        public static int TileWidth = 64;
        public static int TileHeight = 64;

        public static int StepX = 32;
        public static int StepY = 16;

        public static Random Random = new Random();

        public static Vector2 ConvertToIso(Vector2 pos)
        {
            return new Vector2(StepX * (pos.X - pos.Y), StepY * (pos.X + pos.Y));
        }

        public static Rectangle ConvertToIso(Vector2 pos, int width, int height)
        {
            return new Rectangle((int)(StepX * (pos.X - pos.Y)), (int)(StepY * (pos.X + pos.Y)), width, height);
        }
    }
}
