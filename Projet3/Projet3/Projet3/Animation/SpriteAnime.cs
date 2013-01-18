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
    class SpriteAnime
    {
        List<Animation> animations = new List<Animation>();

        int currentAnimation;

        bool isAnimate;

        Vector2 position;

        Texture2D texture;

        float timeElapsed;
        float timeToUpdate;

        public SpriteAnime(Texture2D texture, float timeToUpdate)
        {
            timeElapsed = 0;
            this.timeToUpdate = timeToUpdate;
            isAnimate = false;
            this.texture = texture;
        }

        public void AddAnimation(List<Rectangle> rectangles)
        {
            animations.Add(new Animation());

            foreach (Rectangle rectangle in rectangles)
                animations[animations.Count - 1].AddRectangle(rectangle);
        }

        public void IsAnimate(bool animate)
        {
            isAnimate = animate;
        }

        public void SetAnimation(int animation)
        {
            currentAnimation = animation;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            if (isAnimate)
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                animations[currentAnimation].currentImage = animations[currentAnimation].rectangles.Count - 1;
                animations[currentAnimation].Update(gameTime);
            }

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                animations[currentAnimation].Update(gameTime);
            }

            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch, bool revert)
        {
            SpriteEffects effect;

            if (revert)
                effect = SpriteEffects.FlipHorizontally;
            else
                effect = SpriteEffects.None;

            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, animations[currentAnimation].currentRectangle.Width, animations[currentAnimation].currentRectangle.Height),
                animations[currentAnimation].currentRectangle, Color.White,
                0, Vector2.Zero, effect, 0);
        }   

    }
}
