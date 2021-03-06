﻿using System;
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
    class Menu
    {
        public Menu menuParent;

        public List<Menu> menuFils = new List<Menu>();

        Texture2D texture;

        Vector2 position;

        List<Bouton> boutons = new List<Bouton>();

        public bool isActive;

        int itemWidth;
        int itemHeight;

        int itemSelected;

        int spacing;

        bool vertical;

        public Menu(Menu menuParent, Vector2 position, String[] items, int itemWidth, int itemHeight, int spacing, bool vertical)
        {
            isActive = false;

            this.menuParent = menuParent;

            this.position = position;
            this.itemHeight = itemHeight;
            this.itemWidth = itemWidth;
            this.spacing = spacing;
            this.vertical = vertical;

            InitBoutons(items);
        }

        public Menu(Vector2 position, String[] items, int itemWidth, int itemHeight, int spacing, bool vertical)
        {
            isActive = false;

            this.position = position;
            this.itemHeight = itemHeight;
            this.itemWidth = itemWidth;
            this.spacing = spacing;
            this.vertical = vertical;

            InitBoutons(items);
        }

        public void LoadTexture(Texture2D texture, SpriteFont textureFont)
        {
            this.texture = texture;
            foreach (Bouton bouton in boutons)
                bouton.LoadTexture(texture, textureFont);
        }

        public void AddFils(Menu menuFils)
        {
            this.menuFils.Add(menuFils);
        }

        public void ChangeTitle(int bouton, string titre)
        {
            boutons[bouton].ChangeTitle(titre);
        }

        private void InitBoutons(String[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (vertical)
                    boutons.Add(new Bouton(new Rectangle((int)position.X, (int)position.Y + i * spacing, itemWidth, itemHeight), items[i]));
                else
                    boutons.Add(new Bouton(new Rectangle((int)position.X  + i * spacing, (int)position.Y, itemWidth, itemHeight), items[i]));
            }
        }

        public void SetSelectedItem(int item)
        {
            boutons[itemSelected].isSelected = false;
            itemSelected = item;
            boutons[item].isSelected = true;
        }

        public Menu Activer()
        {
            if (menuParent != null)
                menuParent.isActive = false;

            isActive = true;

            return this;
        }

        public Menu Desactiver()
        {
            isActive = false;

            if (menuParent != null)
                menuParent.Activer();

            return menuParent;
        }

        public int Update(EvenementUtilisateur user)
        {
            foreach (Bouton bouton in boutons)
                bouton.Update(new Vector2(user.mouseState.X, user.mouseState.Y));

            if (user.isLeftClicked())
            {
                int i = 0;
                while (i < boutons.Count && !boutons[i].isHover)
                    i++;

                if (i == boutons.Count)
                    return -1;
                else
                {

                    MoteurAudio.PlaySound("Click");
                    return i;
                }

            }
            else
                return -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bouton bouton in boutons)
                bouton.Draw(spriteBatch);
        }
    }
}
