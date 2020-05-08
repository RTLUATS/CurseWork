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
        private BindingList<RequestIngredientsModel> list;

        public Chef()
        {
            list = new BindingList<RequestIngredientsModel>();

            InitializeComponent();
            LoadAllFood();
        }

        private void LoadAllFood()
        {

            using (var context = new MSSQLContext())
            {

                foods = context.Foods
                    .Include(f => f.Category)
                    .Include(f => f.Structures.Select(s => s.Ingredients))
                    .ToList();
    
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

            Inquiry.IsEnabled = false;
            Table.IsEnabled = false;
            Table.Visibility = Visibility.Hidden;
            Menu.IsEnabled = true;
            Menu.Visibility = Visibility.Visible;
            
            LoadAllFood();
        }

        private void Inquiry_Click(object sender, RoutedEventArgs e)
        {
            InquiryExecute.IsEnabled = true;
            Table.IsEnabled = true;
            Table.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Hidden;
            Menu.IsEnabled = false;

            using(var context = new MSSQLContext())
            {
                var ingredients = context.Database.SqlQuery<Ingredient>("select Id,Name,Count,Unit from Ingredients").ToList();
                
                foreach(var ingredient in ingredients)
                {
                    list.Add(new RequestIngredientsModel() {Id = ingredient.Id, Count = ingredient.Count, Unit = ingredient.Unit, AdditionalAmount = 0, Name = ingredient.Name });
                }
            }

            Table.ItemsSource = list;
        }

        private void InquiryExecute_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MSSQLContext())
            {

                var listInquiry = new List<Inquiry>();

                foreach (var item in list)
                {
                    if (item.AdditionalAmount == 0) continue;

                    listInquiry.Add(new Inquiry() { IngredientId = item.Id, ExpectedQuantity = item.AdditionalAmount, Date = DateTime.Now, IsCompleted = false });
                }

                list.Clear();

                if (listInquiry.Count != 0)
                {
                    context.Inquiries.AddRange(listInquiry);
                    context.SaveChanges();
                }
            }
        }
    }
}
