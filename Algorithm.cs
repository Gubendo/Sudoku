using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IA_TP2
{
    class Algorithm
    {
        public static void ReadCSV()
        {
            Sudoku sukodu = new Sudoku(9);
            int i = 0;

            //Remplacer par adrese du fichier
            using (var reader = new StreamReader(@"/Users/yassirchekour/Desktop/test.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    for (int j = 1; j < 9; j++)
                    {
                        //Console.Write(Int16.Parse(values[j]));
                        sukodu.mySudoku[i][j].setValue(Int16.Parse(values[j]));
                    }
                    i = i + 1;
                }
            }

        }

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
                        queue.Enqueue(new Arc(ref actCase, ref actArc.xi));
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
        public static List<(int, int)> selectMRV(Sudoku sudoku)
        {
            int i = 0;
            int j = 0;
            List<(int, int)> ret = new List<(int, int)>();

            for (int indexI = 0; i < sudoku.size; i++)
            {
                for (int indexJ = 0; j < sudoku.size; j++)
                {
                    if (sudoku.mySudoku[i][j].domain.Count >= sudoku.mySudoku[indexI][indexJ].domain.Count)
                    {
                        i = indexI;
                        j = indexJ;
                        ret.Add((i, j));
                    }
                }
            }
            return ret;
        }

        /**
         * On selectionne la variable qui contraint le plus de variables et on renvoie sa position i,j
         * On appelle selectDH après avoir appelé selectMRV
         */
        public static (int, int) selectDH(Sudoku sudoku, List<(int, int)> resultMRV)
        {
            int i = 0;
            int j = 0;
            (int, int) current;
            int length = resultMRV.Count;
            for (int k = 0; k < length; k++)
            {
                current = resultMRV.ElementAt(k);
                if (sudoku.mySudoku[i][j].getRelatives().Count < sudoku.mySudoku[current.Item1][current.Item2].getRelatives().Count)
                {
                    i = current.Item1;
                    j = current.Item2;
                }
            }
            return (i, j);
        }

        /**
         * On selectionne la VALEUR pour la variable DEJA CHOISIE qui réduit le - réduit le moins les valeurs possibles des prochaines variables (sudoku.getRelatives(var))
         */
        public static int selectLCV(Sudoku sudoku,(int,int) var)
        {
            /*Queue<Arc> queue = new Queue<Arc>();

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
            }*/
            return 0;
        }

        //On test si une solution est encore possible
        public static bool isLost(Sudoku sudoku)
        {
             for (int i = 0; i < sudoku.size; i++)
            {
                for (int j = 0; j < sudoku.size; j++)
                {
                    if (sudoku.mySudoku[i][j].isLost()) {return true;}
                }
            }
             return false;
        }

        public static Sudoku backtracking(Sudoku sudoku)
        {
            Sudoku result = sudoku;
            Sudoku tmp = sudoku;
            bool flag;

            // 1 - Selectionner var non assigné : utiliser MRV puis DH en cas d'égalité
            (int, int) var;
            var = selectDH(sudoku, selectMRV(sudoku));

            // 2 - Selectionner val pour var choisie : utiliser LCV
            int l = sudoku.mySudoku[var.Item1][var.Item2].domain.Count;
            for (int i=0; i<l; i++)
            {
                //LCV pas correct pour le moment
                //int val = selectLCV(sudoku, var);
                int val = sudoku.mySudoku[var.Item1][var.Item2].domain[i];
                sudoku.mySudoku[var.Item1][var.Item2].setValue(val);

                // 3 - AC3 => refresh du domaine de chaque case
                AC3(sudoku);;

                //detection de l'échec
                if (isLost(sudoku))
                {
                     // le resultat trouvé n'était pas viable => on revient en arrière
                     sudoku = tmp;
                }
                else
                {
                    // 4 - Recursivité
                    try
                    {
                        flag = true;
                        result = backtracking(sudoku);
                    }
                    catch (Failure ex)
                    {
                        // le resultat trouvé n'était pas viable => on revient en arrière
                        sudoku = tmp;
                        flag = false;

                    }
                    if (flag)
                    {
                        //RETURN SOLUTION ICI SI PAS DE PROBLEME
                        return result;
                    }
                }
            }
            //Failure
            throw new Failure("Pas de solution il faut remonter");
            return result;
        }

        public class Failure : Exception
        {
            public Failure(string message)
                :base(message)
            {

            }
        }
    }


}