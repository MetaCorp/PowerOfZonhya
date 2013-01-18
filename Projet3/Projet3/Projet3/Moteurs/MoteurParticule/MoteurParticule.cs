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
    public class MoteurParticule
    {
        public List<Texture2D> Textures { get; set; }

        public Rectangle rectanglePosition;

        public Vector2 Vent { get; set; }
        public Vector2 Gravite { get; set; }

        public int GMax, GMin, RMax, RMin, BMax, BMin;

        public float VitesseMin { get; set; }
        public float VitesseMax { get; set; }
        public float VariationVitesse { get; set; }

        public float AngleMin { get; set; }
        public float AngleMax { get; set; }

        public Vector2 VariationAngle { get; set; }

        public float SizeMin { get; set; }
        public float SizeMax { get; set; }
        public float VariationSize { get; set; }

        List<Particule> Particules { get; set; }

        public int TTLMin { get; set; }
        public int TTLMax { get; set; }
        public int VariationTTL { get; set; }

        public int Totale { get; set; }

        public Vector2 variationVent;

        Random random = new Random();

        public MoteurParticule(List<Texture2D> textures, Vector2 position, Vector2 vent, Vector2 gravite)
        {
            Textures = textures;

            rectanglePosition = new Rectangle((int)position.X, (int)position.Y, 1, 1);

            Vent = vent;

            Particules = new List<Particule>();

            Gravite = gravite;
            variationVent = new Vector2(0, 0);
            Totale = 10;

            GMin = 0;
            BMin = 0;
            RMin = 0;

            GMax = 255;
            BMax = 255;
            RMax = 255;
        }

        public MoteurParticule(List<Texture2D> textures, Rectangle rectanglePosition, Vector2 vent, Vector2 gravite)
        {
            Textures = textures;

            this.rectanglePosition = rectanglePosition;

            Vent = vent;

            Particules = new List<Particule>();

            Gravite = gravite;
            variationVent = new Vector2(0, 0);
            Totale = 10;
        }



        public void setAngle(float angleMin, float angleMax)
        {
            AngleMin = angleMin;
            AngleMax = angleMax;
            //VariationAngle = variationAngle;
        }

        public void setSize(float sizeMin, float sizeMax, float variationSize)
        {
            SizeMin = sizeMin;
            SizeMax = sizeMax;
            VariationSize = variationSize;
        }

        public void setVitesse(float vitesseMin, float vitesseMax, float variationVitesse)
        {
            VitesseMin = vitesseMin;
            VitesseMax = vitesseMax;
            VariationVitesse = variationVitesse;
        }

        public void setTTL(int TTLMin, int TTLMax, int variationTTL)
        {
            this.TTLMin = TTLMin;
            this.TTLMax = TTLMax;
            VariationTTL = variationTTL;
        }

        public void SetColor(int RMin, int RMax, int GMin, int GMax, int BMin, int BMax)
        {
            this.RMax = RMax;
            this.RMin = RMin;
            this.GMax = GMax;
            this.GMin = GMin;
            this.BMax = BMax;
            this.BMin = BMin;
        }

        private Particule GenererNouvelleParticule()
        {
            Vector2 Position = new Vector2(random.Next(rectanglePosition.X, rectanglePosition.X + rectanglePosition.Width), random.Next(rectanglePosition.Y, rectanglePosition.Y + rectanglePosition.Height));

            Texture2D texture = Textures[random.Next(Textures.Count)];

            float angle = (float)(AngleMin + random.NextDouble() * AngleMax);

            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));//new Vector2(0 + (float)(random.NextDouble() - 0.5) * 5f, 0 + (float)(random.NextDouble() - 0.5) * 5f);
            direction.Normalize();

            float vitesse = (float)(VitesseMin + random.NextDouble() * VitesseMax);

            int TTL = random.Next(TTLMin, TTLMax);

            float size = random.Next((int)SizeMin, (int)SizeMax);

            Color Color = new Color(random.Next(RMin, RMax), random.Next(GMin, GMax), random.Next(BMin, BMax));

            return new Particule(texture, Position, direction, vitesse, TTL, size, Color);
        }

        public void Update()
        {
            Update(new Vector2(rectanglePosition.X, rectanglePosition.Y));
        }

        public void Update(Vector2 Position)
        {
            rectanglePosition.X = (int)Position.X;
            rectanglePosition.Y = (int)Position.Y;

            Vent += variationVent;

            for (int i = 0; i < Totale; i++)
                Particules.Add(GenererNouvelleParticule());

            for (int particule = 0; particule < Particules.Count; particule++)
            {
                Particules[particule].Update(Gravite, Vent, VariationVitesse, VariationSize);

                if (Particules[particule].TTL == 0)
                    Particules.RemoveAt(particule);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particule particule in Particules)
                particule.Draw(spriteBatch);
        }

    }
}
