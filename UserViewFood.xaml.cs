using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.ComponentModel;
namespace CurseWork
{
    /// <summary>
    /// Interaction logic for UserViewFood.xaml
    /// </summary>
    public partial class UserViewFood : Window
    {
        private List<BasketModel> FoodInBasket;
        private Food currentFood;
        private Button basketButton;

        public UserViewFood(Food currentFood, List<BasketModel> FoodInBasket, Button basketButton)
        {
            InitializeComponent();
            Price.Text = currentFood.CurrentPrice.ToString();
            foodDescription.Text = currentFood.Description;

            using (var context = new MSSQLContext())
            {

                var structures = context.Structures.Where(s => s.FoodId == currentFood.Id).ToList();

                foreach (var currentStructure in structures)
                {
                    foodStruct.Text += context.Ingredients.First(i => i.Id == currentStructure.IngredientId).Name + ", ";
                }
            }

            foodStruct.Text = foodStruct.Text.TrimEnd(new char[] { ',' });

            using (var ms = new MemoryStream(currentFood.Image))
            {
                var image = new BitmapImage();

                image.BeginInit();
                image.StreamSource = ms;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                foodImage.Source = image;
            }

            this.FoodInBasket = FoodInBasket;
            this.currentFood = currentFood;
            this.basketButton = basketButton;
        }

        private void AddToBasket_Click(object sender, RoutedEventArgs e)
        {
            FoodInBasket.Add(new BasketModel() { Num = currentFood.Id, Name = currentFood.Name, Price = currentFood.CurrentPrice  });
            basketButton.Content = $"Корзина ({FoodInBasket.Count})";
        }
    }
}
