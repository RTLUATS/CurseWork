using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;


namespace CurseWork
{
    /// <summary>
    /// Interaction logic for ManagerViewIngredients.xaml
    /// </summary>
    public partial class ManagerViewIngredients : Window
    {
        private Ingredient ingredient;

        public ManagerViewIngredients(Ingredient ingredient)
        {
            InitializeComponent();

            IngredientName.Text = ingredient.Name;
            IngredientCount.Text = ingredient.Count.ToString();
            IngredientPrice.Text = ingredient.Price.ToString();
            this.ingredient = ingredient;

        }

        private void ClickBuy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Buy.Text))
            {
                MessageBox.Show("Перед покупкой вы должны заполнить сколько штук покупать будем");
                return;
            }
            
            if (!Regex.IsMatch(Buy.Text, "[0-9]+"))
            {
                MessageBox.Show("Введите целое число, сколько штук купим. Пример: 40");
                return;
            }

            if (!Regex.IsMatch(IngredientPrice.Text, "^([0-9]+)([.][0-9]{1,3})$"))
            {
                MessageBox.Show("Введите дробное число, по какой цене купим 1 штуку товара. Пример: 10.22");
                return;
            }

            IngredientCount.Text = (Convert.ToInt32(IngredientCount.Text) + Convert.ToInt32(Buy.Text)).ToString();

            using(var context= new MSSQLContext())
            {
                var buy = new PurchaseIngredient()
                {
                    IngredientId = ingredient.Id,
                    DateOfPurchase = DateTime.Now,
                    Price = Convert.ToDecimal(IngredientPrice.Text),
                    Count = Convert.ToInt32(IngredientCount.Text),
                };

                context.PurchaseIngredients.Add(buy);

                context.SaveChanges();
            }
            //диалоговое окно с тончно вы хотите докупить столько?
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using(var context = new MSSQLContext())
            {
                ingredient = context.Ingredients.First(i => i.Id == ingredient.Id);
                ingredient.Count = Convert.ToInt32(IngredientCount.Text);
                ingredient.Price = Convert.ToDecimal(IngredientPrice.Text);

                context.SaveChanges();
            }
        }
    }
}
