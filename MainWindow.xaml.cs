using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IA_TP2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {
        Sudoku actSudoku = new Sudoku(9);
        SolidColorBrush white = new SolidColorBrush(Colors.WhiteSmoke);
        int tailleSudoku = 0;

        int i = 0;
        int j = 0;

        public static string diff = " ";
        public static int num = 0;


        List<(int,int)> fix = new List<(int,int)>();


        public MainWindow()
        {
            InitializeComponent();

            



        }


        private void Button_Load(object sender, RoutedEventArgs e)
        {

            fix.Clear();

            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == true)
            {
                Console.WriteLine(openFile.FileName);
                actSudoku = new Sudoku(Algorithm.ReadCSVFromFile(openFile.FileName));
                tailleSudoku = 9;

                numero.Text = "Custom Sudoku";
                difficulty.Text = "Custom Difficulty";


                dataShow.ItemsSource2D = actSudoku.mySudoku;
                dataShow.ColumnHeaderHeight = 0;
                dataShow.RowHeaderWidth = 0;
                dataShow.MinRowHeight = 600 / actSudoku.size;
                dataShow.MaxColumnWidth = 800 / actSudoku.size;
                dataShow.FontSize = 19 * 16 / actSudoku.size;

                dataShow.UpdateLayout();
                for (i = 0; i < 9; i++)
                {
                    for (j = 0; j < 9; j++)
                    {
                        DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                        DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;

                        if (cell.ToString() != "System.Windows.Controls.DataGridCell: 0")
                        {
                            cell.FontWeight = FontWeights.Bold;

                            fix.Add((i, j));

                        }
                    }
                }

                    Color9();



                }
        }

        private void Button_Solve(object sender, RoutedEventArgs e)
        {

            for(int i = 0; i < 9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    Console.Write(actSudoku.mySudoku[i][j] +" ; ");
                }
                Console.WriteLine();
            }
            //Console.WriteLine("ICI ?" + actSudoku.mySudoku[0][0]);
            actSudoku = Algorithm.backtracking(actSudoku);
            
            
            dataShow.ItemsSource2D = actSudoku.mySudoku;
            /*dataShow.ColumnHeaderHeight = 0;
            dataShow.RowHeaderWidth = 0;
            dataShow.MinRowHeight = 600 / actSudoku.size;
            dataShow.MaxColumnWidth = 800 / actSudoku.size;
            dataShow.FontSize = 19 * 16 / actSudoku.size;
            */

            dataShow.UpdateLayout();
            foreach ((int,int) doubleV in fix)
            {
               // Console.WriteLine(doubleV);
                DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(doubleV.Item1) as DataGridRow;
                DataGridCell cell = dataShow.Columns[doubleV.Item2].GetCellContent(dgRow).Parent as DataGridCell;
                

                cell.FontWeight = FontWeights.Bold;

            }

            fix.Clear();

            if (tailleSudoku == 9)
            {
                Color9();
            }
            else if (tailleSudoku == 16)
            {
                Color16();
            }
            else if (tailleSudoku == 25)
            {
                Color25();
            }
            else if (tailleSudoku == 36)
            {
                Color36();
            }
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            fix.Clear();

            switch (sizeCombo.SelectedIndex)
            {
                case 0:
                    actSudoku = new Sudoku(Algorithm.ReadCSV());
                    //actSudoku = new Sudoku(9);
                    tailleSudoku = 9;
                    break;
                case 1:
                    actSudoku = new Sudoku(16);
                    tailleSudoku = 16;
                    break;
                case 2:
                    actSudoku = new Sudoku(25);
                    tailleSudoku = 25;
                    break;
                case 3:
                    actSudoku = new Sudoku(36);
                    tailleSudoku = 36;
                    break;
                default:
                    actSudoku = new Sudoku(9);
                    tailleSudoku = 9;
                    break;

            }

            numero.Text = "Sudoku n° " + num.ToString();
            difficulty.Text = "Difficulty : " + diff;
            

            dataShow.ItemsSource2D = actSudoku.mySudoku;
            dataShow.ColumnHeaderHeight = 0;
            dataShow.RowHeaderWidth = 0;
            dataShow.MinRowHeight = 600 / actSudoku.size;
            dataShow.MaxColumnWidth = 800 / actSudoku.size;
            dataShow.FontSize = 19 * 16 / actSudoku.size;

            

            if (actSudoku.size != 9) return;


            /*
            actSudoku.mySudoku[3][0].setValue(1);
            actSudoku.mySudoku[5][0].setValue(5);
            actSudoku.mySudoku[7][0].setValue(6);
            actSudoku.mySudoku[8][0].setValue(8);

            actSudoku.mySudoku[6][1].setValue(7);
            actSudoku.mySudoku[8][1].setValue(1);

            actSudoku.mySudoku[0][2].setValue(9);
 
            actSudoku.mySudoku[7][2].setValue(3);

            actSudoku.mySudoku[2][3].setValue(7);
            actSudoku.mySudoku[4][3].setValue(2);
            actSudoku.mySudoku[5][3].setValue(6);

            actSudoku.mySudoku[0][4].setValue(5);
            actSudoku.mySudoku[8][4].setValue(3);

            actSudoku.mySudoku[3][5].setValue(8);
            actSudoku.mySudoku[4][5].setValue(7);
            actSudoku.mySudoku[6][5].setValue(4);

            actSudoku.mySudoku[1][6].setValue(3);
            actSudoku.mySudoku[6][6].setValue(8);
            actSudoku.mySudoku[8][6].setValue(5);

            actSudoku.mySudoku[0][7].setValue(1);
            actSudoku.mySudoku[2][7].setValue(5);


            actSudoku.mySudoku[0][8].setValue(7);
            actSudoku.mySudoku[1][8].setValue(9);
            actSudoku.mySudoku[3][8].setValue(4);
            actSudoku.mySudoku[5][8].setValue(1);
            */


            /* DIABOLIQUE
            actSudoku.mySudoku[0][0].setValue(1);
            actSudoku.mySudoku[1][0].setValue(7);

            actSudoku.mySudoku[4][0].setValue(6);

            actSudoku.mySudoku[7][0].setValue(5);
            actSudoku.mySudoku[8][0].setValue(9);

            actSudoku.mySudoku[7][1].setValue(8);

            actSudoku.mySudoku[1][2].setValue(5);
            actSudoku.mySudoku[2][2].setValue(6);

            actSudoku.mySudoku[5][2].setValue(8);

            actSudoku.mySudoku[0][3].setValue(3);

            actSudoku.mySudoku[6][3].setValue(7);
            actSudoku.mySudoku[7][3].setValue(2);
            actSudoku.mySudoku[8][3].setValue(8);

            actSudoku.mySudoku[6][4].setValue(3);

            actSudoku.mySudoku[3][5].setValue(1);
            actSudoku.mySudoku[4][5].setValue(9);

            actSudoku.mySudoku[6][5].setValue(4);

            actSudoku.mySudoku[1][6].setValue(1);

            actSudoku.mySudoku[3][6].setValue(8);

            actSudoku.mySudoku[7][6].setValue(4);

            actSudoku.mySudoku[0][7].setValue(6);

            actSudoku.mySudoku[5][7].setValue(5);

            actSudoku.mySudoku[7][7].setValue(7);
            actSudoku.mySudoku[8][7].setValue(3);

            actSudoku.mySudoku[0][8].setValue(4);
            */

            /* FACILE 2
            actSudoku.mySudoku[0][0].setValue(8);
            actSudoku.mySudoku[1][0].setValue(2);
            actSudoku.mySudoku[2][0].setValue(3);

            actSudoku.mySudoku[7][0].setValue(5);
            actSudoku.mySudoku[8][0].setValue(6);

            actSudoku.mySudoku[1][1].setValue(9);
            actSudoku.mySudoku[2][1].setValue(1);

            actSudoku.mySudoku[5][1].setValue(4);

            actSudoku.mySudoku[0][2].setValue(7);

            actSudoku.mySudoku[6][2].setValue(1);

            actSudoku.mySudoku[0][3].setValue(5);

            actSudoku.mySudoku[4][3].setValue(1);

            actSudoku.mySudoku[7][3].setValue(4);

            actSudoku.mySudoku[0][4].setValue(3);

            actSudoku.mySudoku[6][4].setValue(9);
            actSudoku.mySudoku[7][4].setValue(7);

            actSudoku.mySudoku[3][5].setValue(7);

            actSudoku.mySudoku[6][5].setValue(8);
            actSudoku.mySudoku[7][5].setValue(6);
            actSudoku.mySudoku[8][5].setValue(5);

            actSudoku.mySudoku[1][6].setValue(3);

            actSudoku.mySudoku[4][6].setValue(8);

            actSudoku.mySudoku[6][6].setValue(5);
            actSudoku.mySudoku[7][6].setValue(2);

            actSudoku.mySudoku[1][7].setValue(6);
            actSudoku.mySudoku[2][7].setValue(8);
            actSudoku.mySudoku[3][7].setValue(5);

            actSudoku.mySudoku[6][7].setValue(7);

            actSudoku.mySudoku[8][7].setValue(4);

            actSudoku.mySudoku[0][8].setValue(2);

            actSudoku.mySudoku[2][8].setValue(4);

            actSudoku.mySudoku[5][8].setValue(7);

            actSudoku.mySudoku[7][8].setValue(8);

            */


            //actSudoku = Algorithm.backtracking(actSudoku);
            dataShow.UpdateLayout();
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;

                    if (cell.ToString() != "System.Windows.Controls.DataGridCell: 0")
                    {
                        cell.FontWeight = FontWeights.Bold;

                        fix.Add((i, j));

                    }
                }
            }

            if (tailleSudoku == 9)
            {
                Color9();
            }
            else if (tailleSudoku == 16)
            {
                Color16();
            }
            else if (tailleSudoku == 25)
            {
                Color25();
            }
            else if (tailleSudoku == 36)
            {
                Color36();
            }



        }

        private void Color9()
        {
            
            
            dataShow.UpdateLayout();
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 3; i < 6; i++)
            {
                for (j = 3; j < 6; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 0; i < 3; i++)
            {
                for (j = 6; j < 9; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 6; i < 9; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 6; i < 9; i++)
            {
                for (j = 6; j < 9; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
        }

        private void Color16()
        {
            dataShow.UpdateLayout();
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 0; i < 4; i++)
            {
                for (j = 8; j < 12; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 4; i < 8; i++)
            {
                for (j = 4; j < 8; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 4; i < 8; i++)
            {
                for (j = 12; j < 16; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 8; i < 12; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 8; i < 12; i++)
            {
                for (j = 8; j < 12; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 12; i < 16; i++)
            {
                for (j = 4; j < 8; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

            for (i = 12; i < 16; i++)
            {
                for (j = 12; j < 16; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            
        }

        private void Color25()
        {
            dataShow.UpdateLayout();
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 0; i < 5; i++)
            {
                for (j = 10; j < 15; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 0; i < 5; i++)
            {
                for (j = 20; j < 25; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 5; i < 10; i++)
            {
                for (j = 5; j < 10; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 5; i < 10; i++)
            {
                for (j = 15; j < 20; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 10; i < 15; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 10; i < 15; i++)
            {
                for (j = 10; j < 15; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 10; i < 15; i++)
            {
                for (j = 20; j < 25; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 15; i < 20; i++)
            {
                for (j = 5; j < 10; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 15; i < 20; i++)
            {
                for (j = 15; j < 20; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 20; i < 25; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 20; i < 25; i++)
            {
                for (j = 10; j < 15; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 20; i < 25; i++)
            {
                for (j = 20; j < 25; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }

        }

        private void Color36()
        {
            dataShow.UpdateLayout();
            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 0; i < 6; i++)
            {
                for (j = 12; j < 18; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 0; i < 6; i++)
            {
                for (j = 24; j < 30; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 6; i < 12; i++)
            {
                for (j = 6; j < 12; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 6; i < 12; i++)
            {
                for (j = 18; j < 24; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 6; i < 12; i++)
            {
                for (j = 30; j < 36; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 12; i < 18; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 12; i < 18; i++)
            {
                for (j = 12; j < 18; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 12; i < 18; i++)
            {
                for (j = 24; j < 30; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 18; i < 24; i++)
            {
                for (j = 6; j < 12; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 18; i < 24; i++)
            {
                for (j = 18; j < 24; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 18; i < 24; i++)
            {
                for (j = 30; j < 36; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 24; i < 30; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 24; i < 30; i++)
            {
                for (j = 12; j < 18; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 24; i < 30; i++)
            {
                for (j = 24; j < 30; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 30; i < 36; i++)
            {
                for (j = 6; j < 12; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 30; i < 36; i++)
            {
                for (j = 18; j < 24; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
            for (i = 30; i < 36; i++)
            {
                for (j = 30; j < 36; j++)
                {
                    DataGridRow dgRow = dataShow.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    DataGridCell cell = dataShow.Columns[j].GetCellContent(dgRow).Parent as DataGridCell;
                    cell.Background = white;
                }
            }
        }
    }
}