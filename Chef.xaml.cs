using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Chef.xaml
    /// </summary>
    public partial class Chef : Window
    {
        private List<Food> foods;

        public Chef()
        {
            InitializeComponent();
            LoadAllFood();
        }

        private void LoadAllFood()
        {

            using (var context = new MSSQLContext())
            {
                foods = context.Database.SqlQuery<Food>("select * from Foods").ToList();
            }   

            foreach (var food in foods)
            {
                var button = new Button()
                {
                     Name = "Name" + food.Id.ToString(),
                     IsEnabled = true,
                     Content = food.Name
                };

                 button.Click += EventForFood;
                 Menu.Items.Add(button);

            }

        }

        private void EventForFood(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var id = Convert.ToInt32(button.Name.Replace("Name", ""));

            var currentFood = new ChefViewFood(foods.First(f=>f.Id == id));

            currentFood.Show();

        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        { 
            var newCurrentFood = new ChefViewFood();

            newCurrentFood.Show();
        }

        private void AllFood_Click(object sender, RoutedEventArgs e)
        {
            Menu.Items.Clear();
            LoadAllFood();
        }

        private void Querty_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
