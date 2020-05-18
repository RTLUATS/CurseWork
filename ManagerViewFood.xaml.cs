using System;
using System.Linq;
using System.Windows;

namespace CurseWork
{
  
    public partial class ManagerViewFood : Window
    {
        private Food food;

        public ManagerViewFood(Food food)
        {
            InitializeComponent();

            var context = new MSSQLContext();

            FoodImage.Source = WorkWithImage.ConvertArrayByteToImage(food.Image);
            NameFood.Text = food.Name;
            Category.Text = context.Categories.Find(food.CategoryId).Name;
            Price.Text = food.CurrentPrice.ToString();
            InMenu.IsChecked = food.InMenu;
            Description.Text = food.Description;
            this.food = food;
 
            var structures = context.Structures.Where(s => s.FoodId == food.Id).ToList();

            foreach (var currentStructure in structures)
            {
                if (structures.IndexOf(currentStructure) == structures.Count)
                    Ingredients.Text += context.Ingredients.First(i => i.Id == currentStructure.IngredientId).Name + ".";
                else
                    Ingredients.Text += context.Ingredients.First(i => i.Id == currentStructure.IngredientId).Name + " ,";     
            }

            context.Database.Connection.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!Validation.PriceValidation(Price.Text)) return;
            if (!Validation.NegativeValidation(Price.Text)) return;

            using (var context = new MSSQLContext())
            {
                food = context.Foods.First(f => f.Id == food.Id);
                food.CurrentPrice = Convert.ToDecimal(Price.Text);
                food.Description = Description.Text;
                food.InMenu = InMenu.IsChecked == true;
                context.SaveChanges();
            }

            Close();
        }
    }
}
