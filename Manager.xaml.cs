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
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        private List<Food> foods;
        private List<User> users;
        private List<Ingredient> ingredients;

        private Dictionary<int, string> dictionary = new Dictionary<int, string>()
        {
            {0, "(Пользователь)"},
            {1, "(Менеджер)"},
            {2, "(Шеф-повар)"},
            {3, "(Экономист)" },
            
        };

        public Manager()
        {
            InitializeComponent();
        }

        private void AllFoods_Click(object sender, RoutedEventArgs e)
        {
            LoadFood("select * from Foods");
        }

        private void FoodClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;

            var id = Convert.ToInt32(button.Name.Replace("Name", ""));
            
            var window = new ManagerViewFood(foods.First(f=>f.Id == id));
            
            window.Show();
        }

        private void FoodWithOutPrice_Click(object sender, RoutedEventArgs e)
        {
           LoadFood("select * from Foods where Price=0");
        }

        private void LoadFood(string sqlQuery)
        {
            Table.Children.RemoveRange(0, Table.Children.Count);

            using (var context = new MSSQLContext())
            {
                foods = context.Database.SqlQuery<Food>(sqlQuery).ToList();
            }

            foreach (var food in foods)
            {
                var button = new Button()
                {
                    Name = "Name" + food.Id.ToString(),
                    IsEnabled = true,
                    Content = food.Name
                };

                button.Margin = new Thickness(10);
                button.Click += FoodClick;

                Table.Children.Add(button);
            }
        }

        private void FoodNotInMenu_Click(object sender, RoutedEventArgs e)
        {
            LoadFood("select * from Foods where InMenu=0");
        }

        private void AllUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users");
        }

        private void LoadUsers(string sqlQuery)
        {
            Table.Children.RemoveRange(0, Table.Children.Count);

            using (var context = new MSSQLContext())
            {
                users = context.Database.SqlQuery<User>(sqlQuery).ToList();
            }

            foreach (var user in users)
            {
                var button = new Button()
                {
                    Name = "Name" + user.Id.ToString(),
                    IsEnabled = true,
                    Content = $"{user.FirstName} {user.MiddleName} {user.LastName}" + $"{dictionary[user.LvlAccess]}"
                };

                button.Margin = new Thickness(10);
                button.Click += EventForUsers;
               
                Table.Children.Add(button);
            }
        }

        private void LoadIngredients(string sqlQuery)
        {
            Table.Children.RemoveRange(0, Table.Children.Count);

            using (var context = new MSSQLContext())
            {
                ingredients = context.Database.SqlQuery<Ingredient>(sqlQuery).ToList();
            }

            foreach (var ingredient in ingredients)
            {
                var button = new Button()
                {
                    Name = "Name" + ingredient.Id.ToString(),
                    IsEnabled = true,
                    Content = ingredient.Name
                };

                button.Margin = new Thickness(10);
                button.Click += EventForIngredients;

                Table.Children.Add(button);
            }
        }

        private void EventForIngredients(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            
            var id = Convert.ToInt32(button.Name.Replace("Name",""));

            var window = new ManagerViewIngredients(ingredients.First(i => i.Id == id));
            
            window.Show();
        }

        private void EventForUsers(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = Convert.ToInt32(button.Name.Replace("Name", ""));
            var window = new ManagerViewUsers(users.First(u => u.Id == id ));

            window.Show();
        }

        private void Economists_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=2");
        }

        private void Managers_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=1");
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=0");
        }

        private void Chefs_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=3");
        }

        private void AllIngredients_Click(object sender, RoutedEventArgs e)
        {
            LoadIngredients("select * from Ingredients");
        }

        private void IngredientsCountNull_Click(object sender, RoutedEventArgs e)
        {
            LoadIngredients("select * from Ingredients where Count=0");
        }

        private void IngredientsWithOutPrice_Click(object sender, RoutedEventArgs e)
        {
            LoadIngredients("select * from Ingredients where Price=0");
        }
    }
}
