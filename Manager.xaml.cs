using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private List<Inquiry> listInquiry;
        private List<Ingredient> ingredients;
        private Dictionary<int, TextBox> inquiryDictionary;

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

        private void ShowFoodWithOutDescription_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InquiryBuy_Click(object sender, RoutedEventArgs e)
        {
            InquiryListBox.Visibility = Visibility.Visible;
            InquiryListBox.IsEnabled = true;


            LoadInquiryListBox();
        }

        private void LoadInquiryListBox()
        {
            inquiryDictionary = new Dictionary<int, TextBox>();

            using (var context = new MSSQLContext())
            {
                listInquiry = context.Inquiries
                     .Include(i => i.Ingredients)
                     .ToList();
            }   
            
            foreach(var item in listInquiry)
            {
                var panel = new StackPanel();

                var label = new Label()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Content = "Наименование: " + item.Ingredients.First(i => i.Id == item.IngredientId).Name + " Количество: "
                                + item.ExpectedQuantity + " Дата подачи заказа: " + item.Date.ToString("d", CultureInfo.CreateSpecificCulture("de-DE"))
                };

                var textBox = new TextBox()
                {
                    Text = "Введите стоймость 1 единицы ингредиента",
                    Foreground = Brushes.PaleGoldenrod,
                    HorizontalAlignment = HorizontalAlignment.Center    
                };

                textBox.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(GotKeyboardFocus);
                textBox.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(LostKeyboardFocus);
                    
                var button = new Button()
                {
                    Name  = "Name" + item.Id,
                    Content = "Купить",
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                button.Click += EventForButton;

                panel.Children.Add(label);
                panel.Children.Add(textBox);
                panel.Children.Add(button);

                InquiryListBox.Items.Add(panel);
                inquiryDictionary.Add(item.Id, textBox);
            }

            
        }

        private void GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                //If nothing has been entered yet.
                if (((TextBox)sender).Foreground == Brushes.PaleGoldenrod)
                {
                    ((TextBox)sender).Text = "";
                    ((TextBox)sender).Foreground = Brushes.PaleGreen;
                }
            }
        }

        private void LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Make sure sender is the correct Control.
            if (sender is TextBox)
            {
                //If nothing was entered, reset default text.
                if (((TextBox)sender).Text.Trim().Equals(""))
                {
                    ((TextBox)sender).Foreground = Brushes.PaleGoldenrod;
                    ((TextBox)sender).Text = "Введите стоймость 1 единицы ингредиента";
                }
            }
        }

        private void EventForButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = Convert.ToInt32(button.Name.Replace("Name", ""));

            if (!Regex.IsMatch(inquiryDictionary[id].Text, "^([0-9]+([.][0-9]{1,2}){0,1})$"))
            {
                MessageBox.Show("Проверьте праильность ввёднных данных");
                return;
            }

            using(var context = new MSSQLContext())
            {
                context.PurchaseIngredients.Add(new PurchaseIngredient()
                {
                    Count = listInquiry.First(i => i.Id == id).ExpectedQuantity,
                    IngredientId = listInquiry.First(i => i.Id == id).IngredientId,
                    Price = Convert.ToDecimal(inquiryDictionary[id].Text),
                    DateOfPurchase = DateTime.Now
                });

                var ingredient = context.Ingredients.Find(listInquiry.First(i => i.Id == id).IngredientId);

                ingredient.Count += listInquiry.First(i => i.Id == id).ExpectedQuantity;

                context.Inquiries.Remove(listInquiry.First(i => i.Id == id));
                context.SaveChanges();
            }

            InquiryListBox.Items.Remove(listInquiry.IndexOf(listInquiry.First(i => i.Id == id)));
            dictionary.Remove(id);
        }
    }
}