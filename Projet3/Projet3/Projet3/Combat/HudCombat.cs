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
    class HudCombat
    {
        Combat combat;

        Menu boutonLancerCombat;

        Bouton boutonLoot;

        public HudCombat(Combat combat)
        {
            this.combat = combat;

            boutonLancerCombat = new Menu(new Vector2(Constante.WindowWidth / 2 - 50, Constante.WindowHeight - 100), new string[1] {"Lancer"},
                                          100, 40, 40, true);
            boutonLancerCombat.Activer();

            boutonLoot = new Bouton(new Rectangle(100, 100, Constante.WindowWidth - 100 * 2, Constante.WindowHeight - 100 * 2), "Cliquer pour fermer");
        }

        public void LoadTexture(Texture2D textureBouton, Texture2D textureFin, SpriteFont font)
        {
            boutonLancerCombat.LoadTexture(textureBouton, font);

            boutonLoot.LoadTexture(textureFin, font);
        }

        public void Update(MouseState mouseState)
        {
            if (boutonLancerCombat.isActive)
            {
                int action = boutonLancerCombat.Update(mouseState);

                if (action == 0)
                {
                    combat.phase = PhaseCombat.Combat;
                    boutonLancerCombat.Desactiver();
                }
            }

            if (combat.phase == PhaseCombat.Fin)
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    combat.joueur.FinCombat(300);
                    combat.isActive = false;
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (boutonLancerCombat.isActive)
                boutonLancerCombat.Draw(spriteBatch);

            if (combat.phase == PhaseCombat.Fin)
                boutonLoot.Draw(spriteBatch);
            
        }
    }
}
