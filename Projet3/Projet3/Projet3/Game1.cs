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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // ==== Moteurs ====
        MoteurJeu moteurJeu;
        MoteurGraphique moteurGraphique;
        MoteurPhysique moteurPhysique;
        MoteurSysteme moteurSysteme;
        MoteurAudio moteurAudio;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;

            this.graphics.PreferredBackBufferHeight = Constante.WindowHeight;
            this.graphics.PreferredBackBufferWidth = Constante.WindowWidth;
        }

        protected override void Initialize()
        {
            moteurSysteme = new MoteurSysteme();
            moteurPhysique = new MoteurPhysique();
            moteurAudio = new MoteurAudio();
            moteurJeu = new MoteurJeu(moteurSysteme, moteurPhysique);
            moteurGraphique = new MoteurGraphique(moteurJeu);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            moteurGraphique.LoadContent(Content);
            //moteurAudio.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (moteurJeu.statusJeu == Status.Quitter)
                this.Exit();

            moteurJeu.Update(gameTime);
            moteurGraphique.UpdateParticule();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            moteurGraphique.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            moteurGraphique.DrawParticule(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
