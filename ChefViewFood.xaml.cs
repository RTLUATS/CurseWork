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

        public ChefViewFood(Food food = null)
        {
            InitializeComponent();
            if (food != null)
            {
                this.food = food;
                AddFood.IsEnabled = false;
                AddFood.Visibility = Visibility.Hidden;
                /*
                using (var ms = new MemoryStream(food.Image))
                {
                    var image = new BitmapImage();

                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    Image.Source = image;
                }
                */

                List<Structure> structuries;

                using (var context = new MSSQLContext())
                {

                    Name.Text = food.Name;
                    Category.Text = context.Categories.First(c => c.Id == food.CategoryId).Name;
                    Recept.Text = food.Recept;
                    Description.Text = food.Description;
                    structuries = context.Database.SqlQuery<Structure>($"select *  from Structures where FoodId={food.Id}").ToList();


                    foreach (var structure in structuries)
                    {
                        Structure.Text += context.Ingredients.First(i => i.Id == structure.IngredientId).Name + ",";
                    }
                }

                Structure.Text = Structure.Text.TrimEnd(new char[] { ',' });
            }
            else
            {
                SaveChange.IsEnabled = false;
                SaveChange.Visibility = Visibility.Hidden;
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

                food = context.Foods.FirstOrDefault(f => f.Id == food.Id);

                food.Name = Name.Text;
                food.Image = bits;
                food.Recept = Recept.Text;
                food.Description = Description.Text;

                var category = context.Categories.FirstOrDefault(c => c.Name == Category.Text);

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
                {
                    food.CategoryId = category.Id;
                }

                var str = Structure.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var ingredients = new List<Ingredient>();

                foreach (var ingredient in str)
                {
                    var curIngredient = context.Ingredients.FirstOrDefault(i => i.Name == ingredient);

                    if (curIngredient == null)
                    {
                        var newIngredient = new Ingredient()
                        {
                            Name = ingredient,
                            Count = 0,
                            Price = 0,
                        };

                        context.Ingredients.Add(newIngredient);
                        context.SaveChanges();

                        ingredients.Add(newIngredient);
                    }
                    else
                    {
                        ingredients.Add(curIngredient);
                    }
                }



                var structures = context.Structures.Where(s => s.FoodId == food.Id);

                context.Structures.RemoveRange(structures);

                var list = ingredients.Select(ingredient => new Structure() { FoodId = food.Id, IngredientId = ingredient.Id }).ToList();

                context.Structures.AddRange(list);
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
                    Description = Description.Text,
                    Recept = Recept.Text,
                    InMenu = false,
                    CurrentPrice = 0,
                };

                var category = context.Categories.FirstOrDefault(c => c.Name == Category.Text);

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
                {
                    newFood.CategoryId = category.Id;
                }

                context.Foods.Add(newFood);
                context.SaveChanges();

                var str = Structure.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var ingredients = new List<Ingredient>();

                foreach (var ingredient in str)
                {
                    var curIngredient = context.Ingredients.FirstOrDefault(i => i.Name == ingredient);

                    if (curIngredient == null)
                    {
                        var newIngredient = new Ingredient()
                        {
                            Name = ingredient,
                            Count = 0,
                            Price = 0,
                        };

                        ingredients.Add(newIngredient);
                        context.SaveChanges();

                    }
                    else
                    {
                        ingredients.Add(curIngredient);
                    }
                }


                var list = ingredients.Select(ingredient => new Structure() { FoodId = newFood.Id, IngredientId = ingredient.Id }).ToList();

                context.Structures.AddRange(list);
                context.SaveChanges();
            }
        }

        private bool checkStringField(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }

    }
}
