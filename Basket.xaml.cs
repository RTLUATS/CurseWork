using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Data.Entity;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Basket : Window
    { 
    
        private List<FoodInBasket> basket;
        private User user;
        private Button button;

        public Basket(List<FoodInBasket> basket, User currentUser, Button button)
        {
            InitializeComponent();

            if (basket.Count != 0)
            {
                this.basket = basket;
                BasketTable.ItemsSource = this.basket;
                this.button = button;
                user = currentUser;
                AllSum.Text = this.basket.Sum(f => f.Price).ToString();
                DeleteFromBasket.IsEnabled = false;
                Buy.IsEnabled = true;
            }

            if (user != null)
            {
                Telephone.Visibility = Visibility.Hidden;
                FirstName.Visibility = Visibility.Hidden;
                MiddleName.Visibility = Visibility.Hidden;
                LastName.Visibility = Visibility.Hidden;

                Telephone.IsEnabled = false;
                FirstName.IsEnabled = false;
                MiddleName.IsEnabled = false;
                LastName.IsEnabled = false;

            }
        }

        private void DeleteFromBasket_Click(object sender, RoutedEventArgs e)
        {
            var str = Delete.Text;
            var numbers = str.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var num in numbers)
            {
                if (!Validation.IsNumValidation(num)) continue;
             
                var index = Convert.ToInt32(num);

                if (basket.FirstOrDefault(f => f.Num == index) == null) continue;
                
                basket.Remove(basket.First(f=>f.Num == index));
            }

            AllSum.Text = basket.Sum(f => f.Price).ToString();
            button.Content = $"Корзина ({basket.Count()})";
            Delete.Text = "";

            if (basket.Count == 0) Buy.IsEnabled = false;
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (user == null)
            {
                if (!Validation.TelephoneValidation(Telephone.Text) || !Validation.NameValidation(FirstName.Text) 
                    || !Validation.NameValidation(MiddleName.Text) || !Validation.NameValidation(LastName.Text))
                {
                    return;
                }

                SaveOrder();
            }
            else
            {
                SaveOrder(user.Id);
            }

            button.Content = "Корзина (0)";
            basket.Clear();
            Buy.IsEnabled = false;
        }

        private void SaveOrder(int id = 0)
        {
            using (var context =  new MSSQLContext())
            {
                var listId = new List<int>();

                var currentOrder = new OrderList()
                {
                    AmountOrder = Convert.ToDecimal(AllSum.Text),
                    DateOrder = DateTime.Now,
                    FirstName = default,
                    MiddleName = default,
                    LastName = default,
                    Telephone = default,
                };

                if (id == 0)
                {
                    currentOrder.FirstName = FirstName.Text;
                    currentOrder.LastName = LastName.Text;
                    currentOrder.MiddleName = MiddleName.Text;
                    currentOrder.Telephone = Telephone.Text;
                }
                else
                {
                    currentOrder.UserId = id;
                    currentOrder.FirstName = user.FirstName;
                    currentOrder.LastName = user.LastName;
                    currentOrder.MiddleName = user.MiddleName;
                    currentOrder.Telephone = user.Telephone;
                }

                context.OrderLists.Add(currentOrder);
                context.SaveChanges();

                var orders =  new List<Order>();

                foreach (var food in basket)
                {
                    if (!listId.Contains(food.Id))
                    {
                        orders.Add(new Order()
                        {
                            FoodId = food.Id,
                            OrderListId = currentOrder.Id,
                            PriceBoughtFor = food.Price,
                            Count = basket.Where(f => f.Id == food.Id).ToList().Count,
                        });

                        var list = context.Structures.Where(s => s.FoodId == food.Id)
                                                       .Include(s => s.Ingredient)
                                                       .ToList();

                        foreach(var structure in list)
                        {
                            var ingr = context.Ingredients.Find(structure.IngredientId);
                            
                            ingr.Count -= structure.Quantity * orders[orders.Count-1].Count;

                            context.SaveChanges();
                        }

                        listId.Add(food.Id);
                    }

                }

                context.OrderLists.Add(currentOrder);
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
