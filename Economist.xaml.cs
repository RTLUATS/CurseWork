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
using System.Windows.Shapes;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for Economist.xaml
    /// </summary>
    public partial class Economist : Window
    {
        private DiagramIncome pageIncome;
        private ExpenseChart pageExpense;
        private Dictionary<int, int> dictionary;
        private bool isIncome;

        public Economist()
        {
            dictionary = new Dictionary<int, int>()
            {
                {0, 0},
                {1, 365},
                {2, 182},
                {3, 91},
                {4, 30},
                {5, 7},
                {6, 3},
                {7, 1}
            };

            InitializeComponent();
        }

        private void Foods_Click(object sender, RoutedEventArgs e)
        {
            isIncome = true;

            LoadIncome(Date.SelectedIndex);
        }

        private void LoadIncome(int index = 0)
        {
            pageIncome = new DiagramIncome(dictionary[index]);

            Frame.Navigate(pageIncome);
        }

        private void Ingredients_Click(object sender, RoutedEventArgs e)
        {
            isIncome = false;

            LoadExpense(Date.SelectedIndex);   
        }

        private void LoadExpense(int index = 0)
        {
            pageExpense = new ExpenseChart(dictionary[index]);
            
            Frame.Navigate(pageExpense);
        }

        private void Date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isIncome)
                LoadIncome(Date.SelectedIndex);
            else
                LoadExpense(Date.SelectedIndex);
        }

        private void Report_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reports report = new Reports();

            if(Report.SelectedIndex == 0)
                report.CommonPart(3, dictionary[Date.SelectedIndex]);
            else
                report.CommonPart(4, dictionary[Date.SelectedIndex]);
        }
    }
}
