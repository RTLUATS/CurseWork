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

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for Basket.xaml
    /// </summary>
    public partial class Basket : Window
    { 
    
        private List<BasketModel> foodInBasket;
        private User user;
        private Button button;

        public Basket(List<BasketModel> foods, User currentUser, Button button)
        {
            InitializeComponent();

            if (foods.Count != 0)
            {
                foodInBasket = foods;
                BasketTable.ItemsSource = foods;
                
                this.button = button;
                user = currentUser;
                AllSum.Text = foods.Sum(f => f.Price).ToString();
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
            var str = Delete.Text.TrimEnd();
            var numbers = str.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var num in numbers)
            {
                if (!Regex.IsMatch(num, "[0-9]+")) continue;
             
                var id = Convert.ToInt32(num);

                if (foodInBasket.FirstOrDefault(f => f.Num == id) == null) continue;
                
                foodInBasket.Remove(foodInBasket.First(f=>f.Num==id));
                foodInBasket.Remove(foodInBasket.First(f => f.Num == id));
                
            }

            AllSum.Text = foodInBasket.Sum(f => f.Price).ToString();
            button.Content = $"Корзина ({foodInBasket.Count()})";

            if (foodInBasket.Count == 0) Buy.IsEnabled = false;

        }

        private bool checkStringField(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }


        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (user == null)
            {
                if (Telephone.Text.IsEmpty() || FirstName.Text.IsEmpty() || LastName.Text.IsEmpty() ||
                    MiddleName.Text.IsEmpty())
                {
                    MessageBox.Show("Контактные данные должны быть заполнены");
                    return;
                }

                var checkFN = checkStringField(FirstName.Text, "[А-Я]{1}[а-я]+");
                var checkMN = checkStringField(MiddleName.Text, "[А-Я]{1}[а-я]+");
                var checkLN = checkStringField(LastName.Text, "[А-Я]{1}[а-я]+");

                if (!checkFN || !checkMN || !checkLN)
                {
                    MessageBox.Show("В ФИО должны быть только символы кириллицы Пример:Иванов Иван Иванович");
                    return;
                }

                var checkT = checkStringField(Telephone.Text, "\\+375[0-9]{9}");
                
                if (!checkT)
                {
                    MessageBox.Show("В Телефоне должны быть только цифры Пример: +375447856909");
                    return;
                }

                SaveOrder();
            }
            else
            {
                SaveOrder(user.Id);
            }

            button.Content = "Корзина (0)";
            foodInBasket = null;

        }

        private void SaveOrder(int id = 0)
        {
            using (var context =  new MSSQLContext())
            {
                var currentOrder = new OrderList()
                {
                    AmountOrder = Convert.ToDecimal(AllSum.Text),
                    DateOrder = DateTime.Now,

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
                    currentOrder.IdUser = id;
                }

                var orders =  new List<Order>();

                foreach (var food in foodInBasket)
                {
                    orders.Add(new Order()
                    {
                        FoodId = food.Num,
                        IdOrderList = currentOrder.Id
                    });
                }

                context.OrderLists.Add(currentOrder);
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
