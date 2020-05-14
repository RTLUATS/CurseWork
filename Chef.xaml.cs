
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
        private List<RequestIngredientsModel> list;

        public Chef()
        {
            list = new List<RequestIngredientsModel>();

            InitializeComponent();
            LoadAllFood();
        }

        private void LoadAllFood()
        {
            using (var context = new MSSQLContext())
            {

                foods = context.Foods.ToList();
            }

            CreateButtons();
        }

        private void CreateButtons()
        {

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

            var window = new ChefViewFood(foods.First(f=>f.Id == id));

            window.Show();

        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        { 
            var newCurrentFood = new ChefViewFood();

            newCurrentFood.Show();
        }

        private void AllFood_Click(object sender, RoutedEventArgs e)
        {
            Menu.Items.Clear();

            Inquiry.IsEnabled = false;
            Table.IsEnabled = false;
            Table.Visibility = Visibility.Hidden;
            Menu.IsEnabled = true;
            Menu.Visibility = Visibility.Visible;
            AllFood.IsEnabled = false;
            InquiryExecute.IsEnabled = false;

            LoadAllFood();
        }

        private void Inquiry_Click(object sender, RoutedEventArgs e)
        {
            InquiryExecute.IsEnabled = true;
            Table.IsEnabled = true;
            Inquiry.IsEnabled = false;
            Menu.IsEnabled = false;
            AllFood.IsEnabled = true;
            Table.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Hidden;
            

            using(var context = new MSSQLContext())
            {
                var ingredients = context.Ingredients.ToList();
              
                LoadModel(ingredients);
            }
        }

        private void InquiryExecute_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MSSQLContext())
            {
                var listInquiry = new List<Inquiry>();

                foreach (var item in list)
                {
                    if (item.AdditionalAmount == 0) continue;

                    listInquiry.Add(new Inquiry() { IngredientId = item.Id, ExpectedQuantity = item.AdditionalAmount, Date = DateTime.Now});
                }

                list.Clear();

                if (listInquiry.Count != 0)
                {
                    context.Inquiries.AddRange(listInquiry);
                    context.SaveChanges();
                }
            }
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            if (Table.IsEnabled)
            {
                LoadIngredients(SearchField.Text);
            }
            else
            {
                LoadFood(SearchField.Text);
            }
        }

        private void LoadIngredients(string str)
        {
            using(var context = new MSSQLContext())
            {
                var list = context.Ingredients.Where(i => i.Name.StartsWith(str) == true).ToList();

                LoadModel(list);
            }

        }

        private void LoadModel(List<Ingredient> ingredients)
        {
            list.Clear();

            foreach (var ingredient in ingredients)
            {
                list.Add(new RequestIngredientsModel() 
                {
                    Id = ingredient.Id,
                    Count = ingredient.Count,
                    Unit = ingredient.Unit,
                    AdditionalAmount = 0,
                    Name = ingredient.Name
                });
            }


            Table.ItemsSource = list;
        }

        private void LoadFood(string str)
        {
            using (var context = new MSSQLContext())
            {
                foods = context.Foods.Where(f => f.Name.StartsWith(str) == true).ToList();
            }

            CreateButtons();
        }

        private void RequestReport_Click(object sender, RoutedEventArgs e)
        {
            var flag = true;

            using(var context = new MSSQLContext())
            {
                flag = context.Inquiries.ToList().Count != 0;    
            }

            if (flag)
            {
                Reports report = new Reports();

                report.CommonPart(0);
            }
        }

        private void Chef_Closing(object sender, CancelEventArgs e)
        {
           
        }
    }
}
