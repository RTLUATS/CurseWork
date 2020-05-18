using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Data.Entity;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for UserViewFood.xaml
    /// </summary>
    public partial class UserViewFood : Window
    {
        private List<FoodInBasket> basket;
        private Food currentFood;
        private Button basketButton;

        public UserViewFood(Food currentFood, List<FoodInBasket> basket, Button basketButton)
        {
            InitializeComponent();

            Price.Text = currentFood.CurrentPrice.ToString();
            FoodDescription.Text = currentFood.Description;
            AddToBasket.IsEnabled = Validation.CanUserBuy(basket, currentFood);
            foodImage.Source = WorkWithImage.ConvertArrayByteToImage(currentFood.Image);

            using (var context = new MSSQLContext())
            {
                var structures = context.Structures.Where(s => s.FoodId == currentFood.Id)
                    .Include(s => s.Ingredient)
                    .ToList();

                foreach (var currentStructure in structures)
                {
                    if (structures.IndexOf(currentStructure) == structures.Count)
                        FoodStruct.Text += context.Ingredients.First(i => i.Id == currentStructure.IngredientId).Name +
                            $" {currentStructure.Quantity} {currentStructure.Ingredient.Unit}.";
                    else
                        FoodStruct.Text += context.Ingredients.First(i => i.Id == currentStructure.IngredientId).Name + 
                            $" {currentStructure.Quantity} {currentStructure.Ingredient.Unit},";
                }
            }
            
            this.basket = basket;
            this.currentFood = currentFood;
            this.basketButton = basketButton;
        }

        private void AddToBasket_Click(object sender, RoutedEventArgs e)
        {
            AddToBasket.IsEnabled = Validation.CanUserBuy(basket, currentFood);

            if (AddToBasket.IsEnabled)
            {

                basket.Add(new FoodInBasket()
                {
                    Num = (basket.Count == 0 ? 0 : basket[basket.Count-1].Num++),
                    Id = currentFood.Id,
                    Name = currentFood.Name,
                    Price = currentFood.CurrentPrice
                });

                basketButton.Content = $"Корзина ({basket.Count})";
            }
        }
    }
}
