using MSharp.Framework;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
namespace CurseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User currentUser;
        private List<Food> listFood;
        private List<Category> listCategories;
        private List<Ingredient> listIngredients;
        private List<BasketModel> foodInBasket;
        private Action<User> successAuth;
        private int checker = 0;
        private bool show = false;
        
        public MainWindow()
        {
            InitializeComponent();
            Load();

            foodInBasket =  new List<BasketModel>();

        }

        private void Load()
        {

            using (var context = new MSSQLContext())
            {
                
                listCategories = context.Database.SqlQuery<Category>("select * from Categories").ToList();
                listFood = context.Database.SqlQuery<Food>("select * from Foods where InMenu = 1 and CurrentPrice > 0").ToList();
                listIngredients = context.Database.SqlQuery<Ingredient>("select * from Ingredients where Count > 0").ToList();
            }

            LoadCategories();
            LoadFood(0);

        }

        private void LogOut_Click(object sender, RoutedEventArgs e) 
        {
            LogOut.Visibility = Visibility.Hidden;
            LogOut.IsEnabled = false;

            if (currentUser.LvlAccess > 0) 
            {
                WorkRoom.Visibility = Visibility.Hidden;
                WorkRoom.IsEnabled = false;
            }
            
            Autorization.Visibility = Visibility.Visible;
            Autorization.IsEnabled = true;
            currentUser = null;
        }

        private void LoadFood(int category = 0, string name = "")
        {
            bool checkIngredients;
           
            Menu.Children.RemoveRange(0, Menu.Children.Count);

            using (var context = new MSSQLContext())
            {
                foreach (var food in listFood)
                {
                    checkIngredients = true;

                    var structures = context.Structures.Where(s => s.FoodId == food.Id).ToList();

                    foreach (var currentStructure in structures)
                    {
                        bool exist = false;

                        foreach (var ingredient in listIngredients)
                        {
                            exist = ingredient.Id == currentStructure.IngredientId;

                            if (exist) break;
                        }

                        if (!exist)
                        {
                            checkIngredients = false;
                            break;
                        }

                    }

                    if (!checkIngredients) continue;
                    if (food.CategoryId != category && category != 0) continue;
                    if (!food.Name.StartsWith(name) && name != "") continue;

                    var button = new Button()
                    {
                        Name = "Name" + food.Id.ToString(),
                        IsEnabled = true
                    };

                    button.Foreground = Brushes.White;
                    button.Content = $"{food.Name}";
                    button.Click += EventForFood;
                    Menu.Children.Add(button);

                }
            }
        }

        private void LoadCategories()
        {
            var allCategories = new Button()
            {
                Name = "AllCategories",
                Margin = new Thickness(10),
                Content = "Все категории",
                FontSize = 16,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.White
            };
           
            allCategories.Click += EventForCategories;
            Categories.Children.Add(allCategories);

            foreach (var category in listCategories)
            {
                var button = new Button()
                {
                    Name = "Name" + category.Id,
                    Content = $"{category.Name}",
                    FontWeight = FontWeights.DemiBold,
                    FontSize = 16,
                    Foreground = Brushes.White, 
                };

                button.Click += EventForCategories;
                Categories.Children.Add(button);

            }
        }

        private void EventForFood(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = Convert.ToInt32(button.Name.Replace("Name", ""));
            var window = new UserViewFood(listFood.First(f=>f.Id == id), foodInBasket, Basket);

            window.Show();
        }

        private void EventForCategories(object sender, RoutedEventArgs e)
        {
            Menu.Children.RemoveRange(0,Menu.Children.Count);

            var button = (Button) sender;
            var id = button.Name == "AllCategories" ? 0 : Convert.ToInt32(button.Name.Replace("Name", ""));

            LoadFood(id);
        }

        private void Autarization_Click(object sender, RoutedEventArgs e)
        {
            successAuth += GetUserData;
            
            var autorization = new Authorization(currentUser, LogOut, WorkRoom, Autorization, successAuth);

            autorization.Show();

        }

        private void GetUserData(User user)
        {
            currentUser = user;
            successAuth -= GetUserData;
        }

        private void WorkRoom_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser.Id == 1)
            {
                var manager = new Manager();
                manager.Show();
            }
            else if(currentUser.Id == 2)
            {
                var chef = new Chef();
                chef.Show();
            }
            else if(currentUser.Id == 3)
            {
                var economist = new Economist();
                economist.Show();
            }

        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            var basket = new Basket(foodInBasket, currentUser, Basket);
            basket.Show();
        }

        //???
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (SearchField.Text != "")
            {
                checker = 1;
                LoadFood(0 ,SearchField.Text);

            }
            else if(checker == 1)
            {
                checker = 0;
                LoadFood(0);
            }
        }

        private void OpenCategoriesClick(object sender, RoutedEventArgs e)
        {
            if (show == false )
            {
                Categories.Visibility = Visibility.Visible;
                Categories.IsEnabled = true;
                show = true;
            }
            else
            {
                Categories.Visibility = Visibility.Hidden;
                Categories.IsEnabled = false;
                show = false;
            }
        }
    }
}
