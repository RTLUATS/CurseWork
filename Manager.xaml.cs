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
        private List<Inquiry> listInquiry;
        private List<Ingredient> ingredients;
        private Dictionary<int, TextBox> inquiryDictionary;
        private Dictionary<int, Action> foodDictionary;
        private Dictionary<int, Action> ingredientsDictionary;

        public Manager()
        {
            InitializeComponent();

            foodDictionary = new Dictionary<int, Action>()
            {
                {0, LoadAllFood},
                {1, LoadWoodWithOutPrice},
                {2, LoadFoodWithOutDescription},
                {3, LoadFoodNotInMenu}

            };

            ingredientsDictionary = new Dictionary<int, Action>()
            {
                {0, LoadAllIngredients },
                {1, LoadInquiryListBox},
                {2, LoadIngredientsWithCountNull},
            };
        }

        private void LoadFoodNotInMenu()
        {
            using (var context = new MSSQLContext())
            {
                foods = context.Foods.Where(f => f.InMenu == false).ToList();
            }
        }

        private void LoadWoodWithOutPrice()
        {
            using (var context = new MSSQLContext())
            {
                foods = context.Foods.Where(f => f.CurrentPrice == 0).ToList();
            }
        }

        private void LoadFoodWithOutDescription()
        {
            using (var context = new MSSQLContext())
            {
                foods = context.Foods.Where(f => f.Description == "").ToList();
            }
        }

        private void LoadAllFood()
        {
            using(var context = new MSSQLContext())
            {
                foods = context.Foods.ToList();
            }
        }

        private void LoadFood()
        {
            FoodContolVisiability();

            Table.Items.Clear();

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

                Table.Items.Add(button);
            }
        }

        private void FoodContolVisiability()
        {
            Table.Visibility = Visibility.Visible;
            LName.Visibility = Visibility.Hidden;
            LCount.Visibility = Visibility.Hidden;
            LDate.Visibility = Visibility.Hidden;
            InquiryListBox.Visibility = Visibility.Hidden;

            Table.IsEnabled = true;
            InquiryListBox.IsEnabled = false;
        }

        private void FoodClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var id = Convert.ToInt32(button.Name.Replace("Name", ""));
            var window = new ManagerViewFood(foods.First(f=>f.Id == id));
            
            window.Show();
        }

        private void IngredientControlVisibility(int a )
        {
            if (a == 1)
            {
                Table.Visibility = Visibility.Visible;
                InquiryListBox.Visibility = Visibility.Hidden;
                LName.Visibility = Visibility.Hidden;
                LCount.Visibility = Visibility.Hidden;
                LDate.Visibility = Visibility.Hidden;

                Table.IsEnabled = true;
                InquiryListBox.IsEnabled = false;
            }
            else
            {
                Table.Visibility = Visibility.Hidden;
                InquiryListBox.Visibility = Visibility.Visible;
                LName.Visibility = Visibility.Visible;
                LCount.Visibility = Visibility.Visible;
                LDate.Visibility = Visibility.Visible;

                Table.IsEnabled = false;
                InquiryListBox.IsEnabled = true;
            }
        }

        private void LoadIngredients()
        {
            IngredientControlVisibility(1);

            Table.Items.Clear();

            foreach (var ingredient in ingredients)
            {
                var button = new Button()
                {
                    Name = "Name" + ingredient.Id.ToString(),
                    IsEnabled = true,
                    Content = ingredient.Name,
                    Foreground = Brushes.White
                };

                button.Margin = new Thickness(10);
                button.Click += EventForIngredients;

                Table.Items.Add(button);
            }
        }

        private void EventForIngredients(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender; 
            var id = Convert.ToInt32(button.Name.Replace("Name",""));
            var window = new ManagerViewIngredients(ingredients.First(i => i.Id == id));
            
            window.Show();
        }

        private void LoadAllIngredients()
        {
            using(var context = new MSSQLContext())
            {
                ingredients = context.Ingredients.ToList();
            }

            LoadIngredients();
        }

        private void LoadIngredientsWithCountNull()
        {
            using (var context = new MSSQLContext())
            {
                ingredients = context.Ingredients.Where(i => i.Count == 0).ToList();
            }
            LoadIngredients();
        }

        private void LoadInquiryListBox()
        {
            IngredientControlVisibility(2);

            inquiryDictionary = new Dictionary<int, TextBox>();

            var context = new MSSQLContext();

                listInquiry = context.Inquiries
                     .Include(i => i.Ingredient)
                     .ToList();
 
            foreach(var item in listInquiry)
            {
                var panel = new StackPanel();

                var label = new Label()
                {
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Content = item.Ingredient.Name + "\t\t"
                                + item.ExpectedQuantity + "\t\t" + item.Date.ToString("d", CultureInfo.CreateSpecificCulture("de-DE"))
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

            context.Database.Connection.Close();
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

            if (!Validation.PriceValidation(inquiryDictionary[id].Text)) return;
            if (!Validation.NegativeValidation(inquiryDictionary[id].Text)) return;
            if (!Validation.ManagerBuyValidation()) return;

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
                ingredient.Price = Convert.ToDecimal(inquiryDictionary[id].Text);

                context.Inquiries.Remove(context.Inquiries.Find(id));
                context.SaveChanges();
            }

            InquiryListBox.Items.RemoveAt(listInquiry.IndexOf(listInquiry.First(i => i.Id == id)));
            InquiryListBox.Items.Refresh();
            inquiryDictionary.Remove(id);
        }

        private void DishReport_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();

            report.CommonPart(1);
        }

        private void IngredientReport_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report();

            report.CommonPart(2);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foodDictionary[BoxWithFoods.SelectedIndex].Invoke();
            LoadFood();
        }

        private void BoxWithIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ingredientsDictionary[BoxWithIngredients.SelectedIndex].Invoke();
        }
    }
}