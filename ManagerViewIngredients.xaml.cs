using System;
using System.Windows;


namespace CurseWork
{
   
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
            if (!Validation.CountValidation(Buy.Text) || !Validation.PriceValidation(IngredientPrice.Text)) return;
            if (!Validation.NegativeValidation(Buy.Text) || !Validation.NegativeValidation(IngredientPrice.Text)) return;

            if (Validation.ManagerBuyValidation())
            {
                IngredientCount.Text = (Convert.ToDecimal(IngredientCount.Text) + Convert.ToDecimal(Buy.Text)).ToString();
                Buy.Text = "";

                using (var context = new MSSQLContext())
                {
                    var buy = new PurchaseIngredient()
                    {
                        IngredientId = ingredient.Id,
                        DateOfPurchase = DateTime.Now,
                        Price = Convert.ToDecimal(IngredientPrice.Text),
                        Count = Convert.ToInt32(IngredientCount.Text),
                    };

                    var ingr = context.Ingredients.Find(ingredient.Id);

                    ingr.Price = buy.Price;
                    ingr.Count += buy.Count; 

                    context.PurchaseIngredients.Add(buy);
                    context.SaveChanges();
                }
            }
        }
    }
}
