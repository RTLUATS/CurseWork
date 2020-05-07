﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for ManagerViewFood.xaml
    /// </summary>
    public partial class ManagerViewFood : Window
    {
        private Food food;

        public ManagerViewFood(Food food)
        {
            InitializeComponent();

            using (var ms = new MemoryStream(food.Image))
            {
                BitmapImage biImg = new BitmapImage();
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.CacheOption = BitmapCacheOption.OnLoad;
                biImg.EndInit();

                FoodImage.Source = biImg as ImageSource;
            }

            using(var context = new MSSQLContext())
            {
                NameFood.Text = food.Name;
                Category.Text = context.Categories.First(c=>c.Id== food.CategoryId).Name ;
                Price.Text = ((food.CurrentPrice == null) ? 0 : food.CurrentPrice).ToString();
                InMenu.IsChecked = food.InMenu;
                Description.Text = food.Description;
                Recept.Text = food.Recept;
                this.food = food;

                var structures = context.Structures.Where(s => s.FoodId == food.Id).ToList();

                foreach (var currentStructure in structures)
                {
                    Ingredients.Text += context.Ingredients.First(i => i.Id == currentStructure.IngredientId).Name + ",";
                }
            }

            Ingredients.Text.TrimEnd(new char[]{','});
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (Regex.IsMatch(Price.Text, "^(0-9)+([.][0-9]+)*$"))
            {
                MessageBox.Show("Цена должна быть числом. Пример: 12");
                return;
            }

            using (var context = new MSSQLContext())
            {
                food = context.Foods.First(f => f.Id == food.Id);
                food.CurrentPrice = Convert.ToDecimal(Price.Text);
                food.InMenu = InMenu.IsChecked;
                context.SaveChanges();
            }
        }
    }
}