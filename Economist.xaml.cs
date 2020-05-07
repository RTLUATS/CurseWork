using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Economist.xaml
    /// </summary>
    public partial class Economist : Window
    {
        public Economist()
        {
            InitializeComponent();

        }

        private void Foods_Checked(object sender, RoutedEventArgs e)
        {
            var page = new DiagramIncome();

        }

        private void Ingredients_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
