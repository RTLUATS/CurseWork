using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User currentUser;
        private List<Food> listFood;
        private List<Window> windows;
        private List<Category> listCategories;
        private List<Ingredient> listIngredients;
        private List<FoodInBasket> basket;
        private Action<User> success;
        private int checker = 0;
        private bool show = false;
        
        public MainWindow()
        {
            InitializeComponent();
            Load();

            basket =  new List<FoodInBasket>();
            windows = new List<Window>();
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

        private void EditInfo_Click(object sender, RoutedEventArgs e)
        {
            success += GetUserData;

            var window = new EditInfo(currentUser, success);
            
            window.Show();

            windows.Add(window);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e) 
        {
            if (currentUser.LvlAccess > 0) 
            {
                WorkRoom.Visibility = Visibility.Hidden;
                WorkRoom.IsEnabled = false;
            }
            LogOut.Visibility = Visibility.Hidden;
            LogOut.IsEnabled = false;
            EditInfo.Visibility = Visibility.Hidden;
            EditInfo.IsEnabled = false;
            Autorization.Visibility = Visibility.Visible;
            Autorization.IsEnabled = true;
            currentUser = null;

            basket.Clear();

            foreach(var item in windows)
            {
                if (item.IsVisible) item.Close();
            }
        }

        private void LoadFood(int category = 0, string name = "")
        {
            bool checkIngredients;

            Menu.Items.Clear();

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

                    var stackpanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal
                    };

                    Label label = new Label()
                    {
                        Content = food.Name,
                        Foreground = Brushes.White,
                        FontWeight = FontWeights.DemiBold,
                        FontSize = 16,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    Image img = new Image()
                    {
                        Source = WorkWithImage.ConvertArrayByteToImage(food.Image),
                    };

                    var button = new Button()
                    {
                        Name = "Name" + food.Id.ToString(),
                        IsEnabled = true,
                        Foreground = Brushes.White,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Margin = new Thickness(5),
                        Content = $"{food.Name}",
                    };

                    stackpanel.Children.Add(img);
                    stackpanel.Children.Add(label);
                    stackpanel.Children.Add(button);
     
                    button.Click += EventForFood;

                    Menu.Items.Add(stackpanel);

                }
            }
        }

        private void LoadCategories()
        {
            var allCategories = new Button()
            {
                Name = "AllCategories",
                Margin = new Thickness(5),
                Content = "Все категории",
                FontSize = 16,
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.White
            };
           
            allCategories.Click += EventForCategories;
            Categories.Items.Add(allCategories);

            foreach (var category in listCategories)
            {
                var button = new Button()
                {
                    Name = "Name" + category.Id,
                    Margin = new Thickness(5),
                    Content = $"{category.Name}",
                    FontWeight = FontWeights.DemiBold,
                    FontSize = 16,
                    Foreground = Brushes.White, 
                };

                button.Click += EventForCategories;
                Categories.Items.Add(button);

            }
        }

        private void EventForFood(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = Convert.ToInt32(button.Name.Replace("Name", ""));
            var window = new UserViewFood(listFood.First(f=>f.Id == id), basket, Basket);

            window.Show();

            windows.Add(window);
        }

        private void EventForCategories(object sender, RoutedEventArgs e)
        {

            var button = (Button) sender;
            var id = button.Name == "AllCategories" ? 0 : Convert.ToInt32(button.Name.Replace("Name", ""));

            LoadFood(id);
        }

        private void Autarization_Click(object sender, RoutedEventArgs e)
        {
            success += GetUserData;
            
            var autorization = new Authorization(EditInfo, LogOut, WorkRoom, Autorization, success);

            autorization.Show();

        }

        private void GetUserData(User user)
        {
            currentUser = user;
            success -= GetUserData;
        }

        private void WorkRoom_Click(object sender, RoutedEventArgs e)
        {
            switch (currentUser.LvlAccess) 
            {
                case 1: var managerWindow = new Manager();
                        managerWindow.Show();
                        windows.Add(managerWindow);
                    break;
                case 2: var chefWindow = new Chef();
                        chefWindow.Show();
                        windows.Add(chefWindow);
                    break;
                case 3: var economistWindow = new Economist();
                        economistWindow.Show();
                        windows.Add(economistWindow);
                    break;
                case 4: var adminWindow = new Administrator();
                        adminWindow.Show();
                        windows.Add(adminWindow);
                    break;
                case 5: var directorWindow = new Director();
                        directorWindow.Show();
                        windows.Add(directorWindow);
                    break;
            }


        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            var windowBasket = new Basket(basket, currentUser, Basket);
            windowBasket.Show();
            windows.Add(windowBasket);
        }

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
