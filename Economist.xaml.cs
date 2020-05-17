using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        private void LoadIncome(int index )
        {
            Income.Visibility = Visibility.Visible;
            LabelIncome.Visibility = Visibility.Visible;
            Expenses.Visibility = Visibility.Hidden;
            LabelExpenses.Visibility = Visibility.Hidden;

            pageIncome = new DiagramIncome(dictionary[index]);

            Frame.Navigate(pageIncome);

            using (var context = new MSSQLContext())
            {
                var list = context.Orders
                                .Include(o => o.OrderList)
                                .ToList();

                if(index != 0)
                    Income.Text = list.Where(l => DateTime.Now
                                        .Subtract(l.OrderList.DateOrder.Date).TotalDays <= dictionary[index])
                                        .Sum(l => l.PriceBoughtFor * l.Count).ToString();
                else
                    Income.Text = list.Sum(l => l.PriceBoughtFor * l.Count).ToString();
            }
        }

        private void Ingredients_Click(object sender, RoutedEventArgs e)
        {
            isIncome = false;

            LoadExpense(Date.SelectedIndex);   
        }

        private void LoadExpense(int index)
        {
            Income.Visibility = Visibility.Hidden;
            LabelIncome.Visibility = Visibility.Hidden;
            Expenses.Visibility = Visibility.Visible;
            LabelExpenses.Visibility = Visibility.Visible;

            pageExpense = new ExpenseChart(dictionary[index]);
            
            Frame.Navigate(pageExpense);

            using (var context = new MSSQLContext())
            {
                var list = context.PurchaseIngredients.ToList();

                if (index != 0)
                    Expenses.Text = list.Where(l => DateTime.Now
                                        .Subtract(l.DateOfPurchase.Date).TotalDays <= dictionary[index])
                                        .Sum(l => l.Price * l.Count).ToString();
                else
                    Expenses.Text = list.Sum(l => l.Price * l.Count).ToString();
            }

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
            Report report = new Report();

            if(Report.SelectedIndex == 0)
                report.CommonPart(3, dictionary[Date.SelectedIndex]);
            else if(Report.SelectedIndex == 1)
                report.CommonPart(4, dictionary[Date.SelectedIndex]);
        }
    }
}
