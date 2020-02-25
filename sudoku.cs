﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_TP2
{
    class Case
    {
        public int i;
        public int j;
        public int value;
        private List<Case> relatives;
        public List<int> domain;
        public Case(int i, int j)
        {
            this.i = i;
            this.j = j;
            this.value = 0;
            relatives = new List<Case>();
            domain = new List<int>(Enumerable.Range(1,9).ToArray());
        }
        public List<Case> getRelatives()
        {
            relatives.RemoveAll(c => c.isFixed());
            return relatives;
        }
        public void addRelative(ref Case relative)
        {
            if (!relatives.Contains(relative) && relative != this)
            {
                this.relatives.Add(relative);
            }
        }
        public bool isFixed()
        {
            if(domain.Count==1 ||value!=0)
            {
                return true;
            }
            return false;
        }

    }
    class Arc
    {
        public Case xi;
        public Case xj;

        public Arc(ref Case xi, ref Case xj)
        {
            this.xi = xi;
            this.xj = xj;
        }
    }
    class Sudoku
    {

        public Case[,] mySudoku;
        public int size;
        int subSize = 3;

        // SUBSIZE EST TOUJOURS EGAL A RACINE DE SIZE
        public Sudoku(int size)
        {
            mySudoku = new Case[size, size];
            this.size = size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    mySudoku[i, j] = new Case(i, j);
                }
            }
        }

        public void generateRelatives()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    generateRelatives(ref mySudoku[i, j]);
                }
            }
        }

        public void generateRelatives(ref Case actCase)
        {
            int actI = actCase.i;
            int actJ = actCase.j;
            for (int i = 0; i < size; i++)
            {
                    actCase.addRelative(ref mySudoku[actI, i]);
                    actCase.addRelative(ref mySudoku[i, actJ]);
                 
            }
            int startingSubI =actI -  actI % subSize;
            int startingSubJ = actJ- actJ % subSize;
            for(int i =startingSubI; i<startingSubI + subSize;i++)
            {
                for(int j = startingSubJ; j <startingSubJ +subSize; j++)
                {
                        actCase.addRelative(ref mySudoku[i, j]);
                    
                }
            }
        }

        public List<Case> getRelatives(int i, int j)
        {
            return mySudoku[i, j].getRelatives();
        }
    }
}
