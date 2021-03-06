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
    class MoteurPhysique
    {

        public int[,] collisionCarte;

        public MoteurPhysique()
        {

        }

        public void SetCarte(List<Tuile>[,] carte)
        {
            collisionCarte = new int[carte.GetLength(0), carte.GetLength(1)];

            for (int x = 0; x < collisionCarte.GetLength(0); x++)
                for (int y = 0; y < collisionCarte.GetLength(1); y++)
                    collisionCarte[y, x] = carte[y, x].Count;
        }

        public bool isCollision(Vector2 position, Vector2 posInit)
        {
            if (Math.Abs(collisionCarte[(int)position.X, (int)position.Y] - collisionCarte[(int)posInit.X, (int)posInit.Y]) > 1)
                return true;
            else
                return false;
        }

        public List<Vector2> GetPath(Vector2 positionDepart, Vector2 positionFinale)
        {
            positionFinale.X = (int)positionFinale.X;
            positionFinale.Y = (int)positionFinale.Y;

            bool cheminImpossible = false;

            Vector2 currentPosition = positionDepart;

            List<Vector2> openList = new List<Vector2>();
            List<Vector2> closedList = new List<Vector2>();

            closedList.Add(positionDepart);

            //if (collisionCarte[(int)positionFinale.X, (int)positionFinale.Y] == 0)
                while (closedList[closedList.Count - 1] != positionFinale && !cheminImpossible)
                {
                    openList = RemplirOpenList(currentPosition, closedList);

                    if (openList.Count == 0)// si aucune pos n'était possible alors
                        cheminImpossible = true;
                    else
                    {
                        int i = 0;

                        while (openList.Count > 1)// Garde le meilleur noeud
                        {
                            if (GetF(positionFinale, openList[i]) < GetF(positionFinale, openList[i + 1]))
                                openList.RemoveAt(i + 1);
                            else
                                openList.RemoveAt(i);
                        }

                        currentPosition = openList[0];

                        closedList.Add(openList[0]);

                        openList.Clear();
                    }
                }

            return closedList;
        }

        private List<Vector2> RemplirOpenList(Vector2 position, List<Vector2> closedList)// Rempli la liste avec les positions n'étant pas des obstacles ni une pos déja parcourue;
        {
            List<Vector2> openList = new List<Vector2>();

            Vector2 posTest;

            for (int i = 0; i < 8; i++)
            {
                if (i == 0 && position.X - 1 < collisionCarte.GetLength(0))
                    posTest = new Vector2(position.X + 1, position.Y);
                else if (i == 1 && position.X + 1 < collisionCarte.GetLength(0) && position.Y + 1 < collisionCarte.GetLength(1))
                    posTest = new Vector2(position.X + 1, position.Y + 1);
                else if (i == 2 && position.Y + 1 < collisionCarte.GetLength(1))
                    posTest = new Vector2(position.X, position.Y + 1);
                else if (i == 3 && position.X - 1 > 0 && position.Y + 1 < collisionCarte.GetLength(1))
                    posTest = new Vector2(position.X - 1, position.Y + 1);
                else if (i == 4 && position.X - 1 > 0)
                    posTest = new Vector2(position.X - 1, position.Y);
                else if (i == 5 && position.X - 1 > 0 && position.Y - 1 > 0)
                    posTest = new Vector2(position.X - 1, position.Y - 1);
                else if (i == 6 && position.Y - 1 > 0)
                    posTest = new Vector2(position.X, position.Y - 1);
                else if (i == 7 && position.Y - 1 > 0 && position.X + 1 < collisionCarte.GetLength(0))
                    posTest = new Vector2(position.X + 1, position.Y - 1);
                else
                    posTest = new Vector2(position.X, position.Y);


                if (posTest.X > 0 && posTest.Y > 0 && !isCollision(posTest, position) && !isVectorInList(posTest, closedList))
                    openList.Add(posTest);
            }

            return openList;
        }
        
        private bool isVectorInList(Vector2 v, List<Vector2> list)
        {
            foreach (Vector2 listV in list)
                if (listV == v)
                    return true;

            return false;
        }

        private float GetF(Vector2 _endPos, Vector2 _currentPos)
        {
            return //(float)Math.Sqrt((Math.Pow(_currentPos.X - _startPos.X, 2) + Math.Pow(_currentPos.Y - _startPos.Y, 2)))
                 +(float)Math.Sqrt((Math.Pow(_endPos.X - _currentPos.X, 2) + Math.Pow(_endPos.Y - _currentPos.Y, 2)));
        }
    }
}
