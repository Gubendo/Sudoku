﻿using System;
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
    public partial class MainWindow : Window
    {
        Sudoku actSudoku = new Sudoku(9);
        SolidColorBrush white = new SolidColorBrush(Colors.WhiteSmoke);
        int tailleSudoku = 0;

        int i = 0;
        int j = 0;
        public MainWindow()
        {
            InitializeComponent();

            



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (sizeCombo.SelectedIndex)
            {
                case 0:
                    actSudoku = new Sudoku(9);
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

           
            dataShow.ItemsSource2D = actSudoku.mySudoku;
            dataShow.ColumnHeaderHeight = 0;
            dataShow.RowHeaderWidth = 0;
            dataShow.MinRowHeight = 600 / actSudoku.size;
            dataShow.MaxColumnWidth = 800 / actSudoku.size;
            dataShow.FontSize = 19*16 /actSudoku.size;

            if (tailleSudoku == 9)
            {
                Color9();
            }
            else if(tailleSudoku == 16)
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
        
        //LA VIE N'EST QUE MENSONGE
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            switch (sizeCombo.SelectedIndex)
            {
                case 0:
                    actSudoku = new Sudoku(9);
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
            dataShow.ItemsSource2D = actSudoku.mySudoku;
            dataShow.ColumnHeaderHeight = 0;
            dataShow.RowHeaderWidth = 0;
            dataShow.MinRowHeight = 600 / actSudoku.size;
            dataShow.MaxColumnWidth = 800 / actSudoku.size;
            dataShow.FontSize = 19 * 16 / actSudoku.size;

            

            if (actSudoku.size != 9) return;

            actSudoku.mySudoku[3][0].setValue(1);
            actSudoku.mySudoku[5][0].setValue(5);
            actSudoku.mySudoku[7][0].setValue(6);
            actSudoku.mySudoku[8][0].setValue(8);

            actSudoku.mySudoku[6][1].setValue(7);
            actSudoku.mySudoku[8][1].setValue(1);

            actSudoku.mySudoku[0][2].setValue(9);
            actSudoku.mySudoku[3][2].setValue(1);
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

            

            Console.WriteLine("let's go!");
            //actSudoku = Algorithm.backtracking(actSudoku);
            Console.WriteLine("finito");

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
            /*
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
            */
        }

        private void Color25()
        {

        }

        private void Color36()
        {

        }
    }
}