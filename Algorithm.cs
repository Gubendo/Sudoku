using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_TP2
{
    class Algorithm
    {
        public static void AC3(ref Sudoku sudoku)
        {

            Queue<Arc> queue = new Queue<Arc>();

            //ADD ALL ARC TO QUEUE
            for (int i = 0; i < sudoku.size; i++)
            {
                for (int j = 0; j < sudoku.size; j++)
                {
                    for (int index = 0; index < sudoku.getRelatives(i, j).Count; index++)
                    {
                        Case actCase = sudoku.mySudoku[i][j].getRelatives()[index];
                        queue.Enqueue(new Arc(ref sudoku.mySudoku[i][j], ref actCase));
                    }
                }
            }
            while (queue.Count > 0)
            {
                Arc actArc = queue.Dequeue();
                if (removeInconsistentValues(actArc))
                {
                    for (int index = 0; index < actArc.xi.getRelatives().Count; index++)
                    {
                        Case actCase = actArc.xi.getRelatives()[index];
                        queue.Enqueue(new Arc(ref actArc.xi, ref actCase));
                    }
                }
            }


        }


        /**
         * Si une valeur de xi n'est possible avec aucune des valeurs du domaines de xj alors on la supprime du domaine de xi
         */
        public static bool removeInconsistentValues(Arc arc)
        {
            bool removed = false;
            foreach (int valueI in arc.xi.domain)
            {
                bool allow = false;
                foreach (int valueJ in arc.xj.domain)
                {
                    if (valueJ != valueI)
                    {
                        allow = true;
                    }
                }
                if (!allow)
                {
                    arc.xi.domain.Remove(valueI);
                    removed = true;
                }
            }
            return removed;
        }

        /**
         * On selectionne la variable qui a le moins de valeurs légales et on renvoie sa position i,j
         */
        public static (int, int) selectMRV(Sudoku sudoku)
        {
            int i = 0;
            int j = 0;

            for (int indexI = 0; i < sudoku.size; i++)
            {
                for (int indexJ = 0; j < sudoku.size; j++)
                {
                    if (sudoku.mySudoku[i][j].domain.Count > sudoku.mySudoku[indexI][indexJ].domain.Count)
                    {
                        i = indexI;
                        j = indexJ;
                    }
                }
            }
            return (i, j);
        }

        /**
         * On selectionne la variable qui contraint le plus de variables et on renvoie sa position i,j
         */
        public static (int, int) selectDH(Sudoku sudoku)
        {
            int i = 0;
            int j = 0;

            for (int indexI = 0; i < sudoku.size; i++)
            {
                for (int indexJ = 0; j < sudoku.size; j++)
                {
                    if (sudoku.mySudoku[i][j].getRelatives().Count < sudoku.mySudoku[indexI][indexJ].getRelatives().Count)
                    {
                        i = indexI;
                        j = indexJ;
                    }
                }
            }
            return (i, j);
        }

        /**
         * On selectionne la variable qui réduit le - les autres variables et on renvoie sa position
         */
        public static (int, int) selectLCV(Sudoku sudoku, Arc arc)
        {
            Queue<Arc> queue = new Queue<Arc>();

            //ADD ALL ARC TO QUEUE
            for (int i = 0; i < sudoku.size; i++)
            {
                for (int j = 0; j < sudoku.size; j++)
                {
                    for (int index = 0; index < sudoku.getRelatives(i, j).Count; index++)
                    {
                        Case actCase = sudoku.mySudoku[i][j].getRelatives()[index];
                        queue.Enqueue(new Arc(ref sudoku.mySudoku[i][j], ref actCase));
                    }
                }
            }
            
            //ON RECUPERE LA PREMIERE CASE DANS LES CONTRAINTES
            Arc actArc = queue.Dequeue();
            Arc choisie = actArc;
            
            
            while (queue.Count > 0)
            {
                actArc = queue.Dequeue();
                //SI Xi MOINS CONTRAIGNANTE QUE CHOISIE
                if (choisie.xi.domain.Count() < actArc.xi.domain.Count())
                {
                    choisie = actArc;
                }
            }
            return (choisie.xj.i, choisie.xj.j);
        }

    }


}
