using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for ChefViewFood.xaml
    /// </summary> 

    public partial class ChefViewFood : Window
    {
        private Food food;
        private List<IngredientsModel> model;

        public ChefViewFood(Food food = null)
        {
            InitializeComponent();

            model = new List<IngredientsModel>();
            
            if (food != null)
            {
                this.food = food;
                
                LoadFood();
            }
            else
            {
                SaveChange.IsEnabled = false;
                SaveChange.Visibility = Visibility.Hidden;
            }

            Table.ItemsSource = model;

        }

        private void LoadFood()
        {
            using (MSSQLContext context = new MSSQLContext())
            { 

                foreach (var item in context.Structures.Where(s => s.FoodId == food.Id).ToList())
                {
                    model.Add(new IngredientsModel()
                    {
                        CookingStep = item.CookingStep,
                        Unit = item.Ingredient.Unit,
                        Weight = item.Quantity,
                        IngredientName = item.Ingredient.Name
                    });
                }

                Table.ItemsSource = model;
                AddFood.IsEnabled = false;
                AddFood.Visibility = Visibility.Hidden;
                Name.Text = food.Name;
                Category.Text = context.Categories.Find(food.CategoryId).Name;
                Image.Source = WorkWithImage.ConvertArrayByteToImage(food.Image);
            }
        }

        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            Image.Source = WorkWithImage.LoadImageFromPc();
        }

        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            if (Check()) UpdateDb();
        }

        private void UpdateDb()
        {
            using (var context = new MSSQLContext())
            {

                food = context.Foods.Find(food.Id);
                food.Name = Name.Text;
                food.Image = WorkWithImage.ConverImageToArrayByte(Image.Source);
                
                var category = context.Categories.First(c => c.Name.ToLower() == Category.Text.ToLower());

                if (category == null)
                {
                    var newCategory = new Category()
                    {
                        Name = Category.Text
                    };

                    context.Categories.Add(newCategory);
                    context.SaveChanges();

                    food.CategoryId = newCategory.Id;
                }
                else
                    food.CategoryId = category.Id;

                var structures = context.Structures.Where(s => s.FoodId == food.Id).ToList();

                var listName = new List<string>();
                var newModel = new List<IngredientsModel>();

                foreach (var item in model)
                {
                    if (!listName.Contains(item.IngredientName))
                    {
                        var usefullList = model.Where(m => m.IngredientName.ToLower() == item.IngredientName.ToLower()).ToList();

                        string CookingStep = "";
                        decimal Weight = 0;

                        foreach (var elem in usefullList)
                        {
                            CookingStep += elem.CookingStep + ";";
                            Weight += elem.Weight;
                        }


                        newModel.Add(new IngredientsModel()
                        {
                            IngredientName = item.IngredientName,
                            CookingStep = CookingStep,
                            Weight = Weight,
                            Unit = item.Unit
                        });
                    }
                }

                foreach (var item in newModel)
                {
                    var ingredient = context.Ingredients.FirstOrDefault(i => i.Name == item.IngredientName);

                    if( ingredient == null)
                    {
                        ingredient = new Ingredient() { Count = 0, Name = item.IngredientName, Price = 0, Unit = item.Unit };

                        context.Ingredients.Add(ingredient);
                        context.SaveChanges();

                        var structure = new Structure() { IngredientId = ingredient.Id, FoodId = food.Id, Quantity = item.Weight, CookingStep = item.CookingStep };

                        context.Structures.Add(structure);
                        context.SaveChanges();
                    }
                    else
                    {
                        var structure = context.Structures.FirstOrDefault(s => s.FoodId == food.Id && s.IngredientId == ingredient.Id);

                        if(structure == null)
                        {
                            structure = new Structure() { IngredientId = ingredient.Id, FoodId = food.Id, Quantity = item.Weight, CookingStep = item.CookingStep };

                            context.Structures.Add(structure);
                            context.SaveChanges();
                        }
                        else
                        {
                            structure.CookingStep = item.CookingStep;
                            structure.Quantity = item.Weight;
                            structures.Remove(structures.First(s => s.FoodId == food.Id && s.IngredientId == ingredient.Id));

                            context.SaveChanges();
                        }
                    }
                }

                context.Structures.RemoveRange(structures);
                context.SaveChanges();
            }
        }

        private bool Check()
        {
            if (!Validation.IsImgeNull(Image.Source)) return false;
            if (!Validation.NameCategoryValidation(Category.Text)) return false;
            
            foreach(var item in model)
            {
                if (!Validation.NameIngredientValidation(item.IngredientName)) return false;
                if (!Validation.CountValidation(item.Weight.ToString())) return false;
            }

            return true;
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            if (Check()) AddToDB();
        }

        private void AddToDB()
        {
            using (var context = new MSSQLContext())
            {
                var newFood = new Food
                {
                    Image = WorkWithImage.ConverImageToArrayByte(Image.Source),
                    Name = Name.Text,
                    InMenu = false,
                    Description = "",
                    CurrentPrice = 0,
                };

                var category = context.Categories.FirstOrDefault(c => c.Name.ToLower() == Category.Text.ToLower());

                if (category == null)
                {
                    var newCategory = new Category()
                    {
                        Name = Category.Text
                    };

                    context.Categories.Add(newCategory);
                    context.SaveChanges();

                    newFood.CategoryId = newCategory.Id;
                }
                else
                    newFood.CategoryId = category.Id;

                context.Foods.Add(newFood);
                context.SaveChanges();

                var listName = new List<string>();
                var newModel = new List<IngredientsModel>();

                foreach(var item in model)
                {
                    if (!listName.Contains(item.IngredientName))
                    {
                        var usefullList = model.Where(m => m.IngredientName == item.IngredientName).ToList();
                        string CookingStep = "";
                        decimal Weight = 0;
                        
                        foreach(var elem in usefullList)
                        {
                            CookingStep += elem.CookingStep + ";";
                            Weight += elem.Weight;  
                        }

                        newModel.Add(new IngredientsModel()
                        {
                            IngredientName = item.IngredientName,
                            CookingStep = CookingStep,
                            Weight = Weight,
                            Unit = item.Unit
                        }) ;
                    }
                }


                foreach (var item in newModel)
                {
                    var ingredient = context.Ingredients.FirstOrDefault(i => i.Name == item.IngredientName);

                    if (ingredient == null)
                    {
                        ingredient = new Ingredient() 
                        { 
                            Count = 0,
                            Name = item.IngredientName,
                            Price = 0, 
                            Unit = item.Unit
                        };

                        context.Ingredients.Add(ingredient);
                        context.SaveChanges();

                        var structure = new Structure()
                        { 
                            IngredientId = ingredient.Id,
                            FoodId = newFood.Id,
                            Quantity = item.Weight,
                            CookingStep = item.CookingStep };

                        context.Structures.Add(structure);
                        context.SaveChanges();
                    }
                    else
                    {
                        var structure = new Structure();

                        structure = new Structure() 
                        { 
                            IngredientId = ingredient.Id,
                            FoodId = newFood.Id,
                            Quantity = item.Weight,
                            CookingStep = item.CookingStep
                        };

                        context.Structures.Add(structure);
                        context.SaveChanges();
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
