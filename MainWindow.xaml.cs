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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sudoku actSudoku = new Sudoku(9);
            switch (sizeCombo.SelectedIndex)
            {
                case 0:
                    actSudoku = new Sudoku(9);
                    break;
                case 1:
                    actSudoku = new Sudoku(16);
                    break;
                case 2:
                    actSudoku = new Sudoku(25);
                    break;
                case 3:
                    actSudoku = new Sudoku(36);
                    break;
                default:
                    actSudoku = new Sudoku(9);
                    break;

            }

           
            dataShow.ItemsSource2D = actSudoku.mySudoku;
            dataShow.ColumnHeaderHeight = 0;
            dataShow.RowHeaderWidth = 0;
            dataShow.MinRowHeight = 600 / actSudoku.size;
            dataShow.MaxColumnWidth = 800 / actSudoku.size;
            dataShow.FontSize = 19*16 /actSudoku.size;
        }
    }
}
/*
List<Case> actCases = new List<Case>();
                for(int i = 0; i<actSudoku.size;i++)
                {
                    dataGrid.Row
                    actCases.Add(actSudoku.mySudoku[i, j]);
                }*/