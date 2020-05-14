using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using System.Data.Entity;
using System.Web.Routing;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for Director.xaml
    /// </summary>
    public partial class Director : Window
    {
        private Dictionary<int, int> dictionary;
        private DataTable dataTablePurchase;
        private DataTable commonDataTable;
        private DataTable dataTableOrders;

        public Director()
        {
            InitializeComponent();

            dataTablePurchase = new DataTable();
            dataTableOrders = new DataTable();
            commonDataTable = new DataTable();

            dataTablePurchase.Columns.Add(new DataColumn("Название", typeof(string)));
            dataTablePurchase.Columns.Add(new DataColumn("Сумма затрат", typeof(decimal)));
            dataTableOrders.Columns.Add(new DataColumn("Название", typeof(string)));
            dataTableOrders.Columns.Add(new DataColumn("Сумма дохода", typeof(decimal)));

            dictionary = new Dictionary<int, int>() 
            {
                {0, 0 },
                {1, 365},
                {2, 182},
                {3, 91},
                {4, 30},
                {5, 7},
                {6, 3},
                {7, 1}
            };

        }

        private void AllFood_Click(object sender, RoutedEventArgs e)
        {
            FoodVisibility();
            LoadFood(1);   
        }

        private void NeedForIngredients_Click(object sender, RoutedEventArgs e)
        {
            TableOrders.Visibility = Visibility.Hidden;
            TableIngredients.Visibility = Visibility.Hidden;
            Income.Visibility = Visibility.Hidden;
            Expenses.Visibility = Visibility.Hidden;
            CommonIncome.Visibility = Visibility.Hidden;
            CommonExpenses.Visibility = Visibility.Hidden;

            CommonExpenses.IsEnabled = false;
            CommonIncome.IsEnabled = false;
            TableOrders.IsEnabled = false;
            TableIngredients.IsEnabled = false;

            LoadIngredients();
        }

        private void LoadIngredients()
        {
            commonDataTable = new DataTable();

            commonDataTable.Columns.Add(new DataColumn("Название", typeof(string)));
            commonDataTable.Columns.Add(new DataColumn("Количество", typeof(string)));
            commonDataTable.Columns.Add(new DataColumn("Дата подачи заявки", typeof(DateTime)));

            using(var context = new MSSQLContext())
            {
                var list = context.Inquiries
                                .Include(i => i.Ingredient);

                foreach(var item in list)
                {
                    commonDataTable.Rows.Add(item.Ingredient.Name, item.ExpectedQuantity, item.Date);
                }
            }

            CommonTable.ItemsSource = commonDataTable.DefaultView;
        }

        private void LoadFood(int a)
        {
            commonDataTable = new DataTable();

            commonDataTable.Columns.Add(new DataColumn("Название", typeof(string)));
            commonDataTable.Columns.Add(new DataColumn("Состав", typeof(string)));
            commonDataTable.Columns.Add(new DataColumn("Текущая цена", typeof(decimal)));

            if (a == 1)
                LoadAllFood();
            else
                LoadFoodInMenu();
        }   

        private void FoodInMenu_Click(object sender,  RoutedEventArgs e)
        {
            FoodVisibility();
            LoadFood(0);
        }

        private void LoadFoodInMenu()
        {
            using (var context = new MSSQLContext())
            {
                var list = context.Foods
                             .Where(f => f.InMenu == true)
                            .Include(f => f.Structures).ToList();

                foreach (var item in list)
                {
                    string str = "";

                    foreach (var structure in item.Structures)
                        str += $"{structure.Ingredient.Name} {structure.Quantity} " +
                                         $"{structure.Ingredient.Unit} ,";
                    commonDataTable.Rows.Add(item.Name, str, item.CurrentPrice);
                }
            }

            CommonTable.ItemsSource = commonDataTable.DefaultView;
        }

        private void LoadAllFood()
        {
            using(var context = new MSSQLContext())
            {
                var list = context.Foods
                            .Include(f => f.Structures).ToList();
                
                foreach(var item in list)
                {
                    string str = "";

                    foreach (var structure in item.Structures)
                        str += $"{structure.Ingredient.Name} {structure.Quantity} " +
                                        $"{structure.Ingredient.Unit} ,";
                    commonDataTable.Rows.Add(item.Name, str, item.CurrentPrice);
                }
            }
            CommonTable.ItemsSource = commonDataTable.DefaultView;
        }

        private void FoodVisibility()
        {
            TableOrders.Visibility = Visibility.Hidden;
            TableIngredients.Visibility = Visibility.Hidden;
            Date.Visibility = Visibility.Hidden;
            Income.Visibility = Visibility.Hidden;
            Expenses.Visibility = Visibility.Hidden;
            CommonIncome.Visibility = Visibility.Hidden;
            CommonExpenses.Visibility = Visibility.Hidden;
            CommonTable.Visibility = Visibility.Visible;

            Date.IsEnabled = false;
            CommonTable.IsEnabled = true;
            CommonExpenses.IsEnabled = false;
            CommonIncome.IsEnabled = false;
            TableOrders.IsEnabled = false;
            TableIngredients.IsEnabled = false;
        }

        private void Statistic_Click(object sender, RoutedEventArgs e)
        {
            TableOrders.Visibility = Visibility.Visible;
            TableIngredients.Visibility = Visibility.Visible;
            Income.Visibility = Visibility.Visible;
            Expenses.Visibility = Visibility.Visible;
            CommonIncome.Visibility = Visibility.Visible;
            CommonExpenses.Visibility = Visibility.Visible;

            CommonExpenses.IsEnabled = true;
            CommonIncome.IsEnabled = true;
            TableOrders.IsEnabled = true;
            TableIngredients.IsEnabled = true;

            LoadPurchase(dataTablePurchase);
            LoadOrders(dataTableOrders);

            Date.IsEnabled = true;
            Date.Visibility = Visibility.Visible;
        }


        private void LoadPurchase(DataTable dataTable, int date = 0)
        {
            dataTable.Rows.Clear();

            using(var context = new MSSQLContext())
            {
                var list = context.PurchaseIngredients
                    .Include(p => p.Ingredient).ToList();
                
                var listId = new List<int>();
                
                foreach(var item in list)
                {
                    if (!listId.Contains(item.IngredientId))
                    {
                        listId.Add(item.IngredientId);

                        if(date == 0)
                            dataTable.Rows.Add(item.Ingredient.Name, 
                                list.Where(i=>i.IngredientId == item.IngredientId).Sum(i=>i.Price * i.Count));
                        else
                            dataTable.Rows.Add(item.Ingredient.Name,
                               list.Where(i => i.IngredientId == item.IngredientId &&
                                    i.DateOfPurchase.Subtract(DateTime.Now).TotalDays <= dictionary[date])
                                        .Sum(i => i.Price * i.Count));
                    }
                }
                  
                TableIngredients.ItemsSource = dataTable.DefaultView;

                if (date == 0)
                    CommonExpenses.Text = list.Sum(l => l.Price * l.Count).ToString();
                else
                    CommonExpenses.Text = list.Where(l => l.DateOfPurchase.Subtract(DateTime.Now).TotalDays <= dictionary[date])
                        .Sum(l => l.Price).ToString();
            }
        }

        private void LoadOrders(DataTable dataTable, int date = 0)
        {
            dataTable.Rows.Clear();

            using (var context = new MSSQLContext())
            {
                var list = context.Orders
                            .Include(o => o.OrderList)
                            .Include(o => o.Food).ToList();

                var listId = new List<int>();

                foreach (var item in list)
                {
                   if (!listId.Contains(item.FoodId))
                   {
                        listId.Add(item.FoodId);

                        if(date == 0)
                            dataTable.Rows.Add(item.Food.Name, list.Where(i => i.FoodId == item.FoodId)
                                .Sum(i => i.PriceBoughtFor));
                         else
                            dataTable.Rows.Add(item.Food.Name,
                                list.Where(i => i.FoodId == item.FoodId &&
                                        i.OrderList.DateOrder.Subtract(DateTime.Now).TotalDays <= dictionary[date])
                                            .Sum(i => i.PriceBoughtFor));
                   }
                }

                if (date == 0)
                    CommonIncome.Text = list.Sum(l => l.PriceBoughtFor).ToString();
                else
                    CommonIncome.Text = list.Where(l => l.OrderList.DateOrder.Subtract(DateTime.Now).TotalDays <= dictionary[date]).Sum(l => l.PriceBoughtFor).ToString();
            }       

            TableOrders.ItemsSource = dataTable.DefaultView;
        }

        private void Date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadPurchase(dataTablePurchase, Date.SelectedIndex);
            LoadOrders(dataTableOrders, Date.SelectedIndex);
        }
    }
}

