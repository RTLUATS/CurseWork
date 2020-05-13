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
        private List<Food> FoodsInBasket;
        private Food currentFood;
        private Button basketButton;

        public UserViewFood(Food currentFood, List<Food> FoodsInBasket, Button basketButton)
        {
            InitializeComponent();

            Price.Text = currentFood.CurrentPrice.ToString();
            FoodDescription.Text = currentFood.Description;
            AddToBasket.IsEnabled = Validation.CanUserBuy(FoodsInBasket, currentFood);
            foodImage.Source = WorkWithImage.ConvertArrayByteToImage(currentFood.Image);

            using (var context = new MSSQLContext())
            {
                var structures = context.Structures.Where(s => s.FoodId == currentFood.Id).ToList();

                foreach (var currentStructure in structures)
                {
                    FoodStruct.Text += context.Ingredients.First(i => i.Id == currentStructure.IngredientId).Name + ", ";
                }
            }

            FoodStruct.Text = FoodStruct.Text.TrimEnd(new char[] { ',' });
            
            this.FoodsInBasket = FoodsInBasket;
            this.currentFood = currentFood;
            this.basketButton = basketButton;
        }

        private void AddToBasket_Click(object sender, RoutedEventArgs e)
        {
            FoodsInBasket.Add(currentFood);
            basketButton.Content = $"Корзина ({FoodsInBasket.Count})";

        }
    }
}
