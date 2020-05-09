using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Media;
using MSharp.Framework;
using System.Data.Entity;

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

        }

        private void LoadFood()
        {
            foreach(var item in food.Structures)
            {
                model.Add(new IngredientsModel() 
                { 
                    CookingStep = item.CookingStep,
                    Unit = item.Ingredients.First(i => i.Id == item.IngredientId).Unit,
                    Weight = item.Quntity,
                    IngredientName = item.Ingredients.First(i => i.Id == item.IngredientId).Name
                });
            }

            Table.ItemsSource = model;
            AddFood.IsEnabled = false;
            AddFood.Visibility = Visibility.Hidden;
            Name.Text = food.Name;
            Category.Text = food.Category.Name;

            LoadImage();
        }

        private void LoadImage()
        {
            using (var ms = new MemoryStream(food.Image))
            {
                var image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                Image.Source = image;
            }
        }


        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Image File |*.jpg;*.jpeg;*.png";
            dialog.CheckFileExists = true;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == true)
            {
                if (dialog.OpenFile() != null)
                {
                    Image.Source = new BitmapImage(new Uri(dialog.FileName));
                }
            }
        }

        private void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            var bmp = Image.Source as BitmapImage;
            byte[] bits = null;

            if (bmp != null)
            {
                ConverImageToBytes(bmp, ref bits);
            }

            if (Check()) UpdateDb(bits);

        }

        private void UpdateDb(byte[] bits)
        {
            using (var context = new MSSQLContext())
            {

                food = context.Foods.First(f => f.Id == food.Id);
                food.Name = Name.Text;
                food.Image = bits;
                
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

                foreach (var item in model)
                {
                    var ingredient = context.Ingredients.FirstOrDefault(i => i.Name == item.IngredientName);

                    if( ingredient == null)
                    {
                        ingredient = new Ingredient() { Count = 0, Name = item.IngredientName, Price = 0, Unit = item.Unit };

                        context.Ingredients.Add(ingredient);
                        context.SaveChanges();

                        var structure = new Structure() { IngredientId = ingredient.Id, FoodId = food.Id, Quntity = item.Weight, CookingStep = item.CookingStep };

                        context.Structures.Add(structure);
                        context.SaveChanges();
                    }
                    else
                    {
                        var structure = context.Structures.FirstOrDefault(s => s.FoodId == food.Id && s.IngredientId == ingredient.Id);

                        if(structure == null)
                        {
                            structure = new Structure() { IngredientId = ingredient.Id, FoodId = food.Id, Quntity = item.Weight, CookingStep = item.CookingStep };

                            context.Structures.Add(structure);
                            context.SaveChanges();
                        }
                        else
                        {
                            structure.CookingStep = item.CookingStep;
                            structure.Quntity = item.Weight;
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
            /*
            if (Image.Source == null || Name.Text.IsEmpty() || Category.Text.IsEmpty() ||
                Description.Text.IsEmpty() || Structure.Text.IsEmpty() || Recept.Text.IsEmpty())
            {
                MessageBox.Show("Для добавления еды, надо заполнить все поля и загрузить картинку");
                return false;
            }
            */

            /*
            var checkN = checkStringField(Name.Text.ToLower(), "[а-я]+ [а-я]*");

            if (!checkN)
            {
                MessageBox.Show("В названии продукции должны быть только буквы кириллицы");
                return false;
            }

            var checkC = checkStringField(Category.Text, "[А-Я]{1}[а-я]+");

            if (!checkC)
            {
                MessageBox.Show("В названии продукции должны быть только буквы кириллицы");
                return false;
            }
            */

            /*
            var checkP = checkStringField(Price.Text, "^[0-9]+[.]?[0-9]{0,3}$");

            if (!checkP)
            {
                MessageBox.Show("В цене продукции должны быть только цифры. Пример: 123.333");
                return;
            }
            */
            //Описание не проверяемо?!

            /*var checkS = checkStringField(Structure.Text, "\\^([а-я]+)([,][а-я]+)*\\$");

            if (!checkS)
            {
                MessageBox.Show("В составе продукции должны быть только буквы и запятые без пробелов," +
                                " в конце недолжно быть неикаких символов. Пример: булка,кетчуп");
                return false;
            }
            */
            return true;

        }

        private void ConverImageToBytes(BitmapImage bmp, ref byte[] bits)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(memStream);
            bits = memStream.ToArray();
        }

        private void AddFood_Click(object sender, RoutedEventArgs e)
        {
            var bmp = Image.Source as BitmapImage;
            byte[] bits = null;

            if (bmp != null)
            {
                ConverImageToBytes(bmp, ref bits);
            }

            if (Check()) AddToDB(bits);
        }

        private void AddToDB(byte[] bits)
        {
            using (var context = new MSSQLContext())
            {
                var newFood = new Food
                {
                    Image = bits,
                    Name = Name.Text,
                    InMenu = false,
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

                foreach (var item in model)
                {
                    var ingredient = context.Ingredients.FirstOrDefault(i => i.Name == item.IngredientName);

                    if (ingredient == null)
                    {
                        ingredient = new Ingredient() { Count = 0, Name = item.IngredientName, Price = 0, Unit = item.Unit };

                        context.Ingredients.Add(ingredient);
                        context.SaveChanges();

                        var structure = new Structure() { IngredientId = ingredient.Id, FoodId = food.Id, Quntity = item.Weight, CookingStep = item.CookingStep };

                        context.Structures.Add(structure);
                        context.SaveChanges();
                    }
                    else
                    {
                        var structure = new Structure();

                        structure = new Structure() { IngredientId = ingredient.Id, FoodId = food.Id, Quntity = item.Weight, CookingStep = item.CookingStep };

                        context.Structures.Add(structure);
                        context.SaveChanges();
                    }
                }

                context.SaveChanges();
            }
        }

        private bool checkStringField(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }

    }
}
