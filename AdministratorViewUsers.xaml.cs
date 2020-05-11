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
    /// Interaction logic for AdministratorViewUsers.xaml
    /// </summary>
    public partial class AdministratorViewUsers : Window
    {
        public AdministratorViewUsers(User user)
        {
            InitializeComponent();
            FirstName.Text = user.FirstName;
            MiddleName.Text = user.MiddleName;
            LastName.Text = user.LastName;
            Telephone.Text = user.Telephone;
            Id.Text = user.Id.ToString();
            Role.SelectedIndex = user.LvlAccess;

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MSSQLContext())
            {
                int id = Convert.ToInt32(Id.Text);
                var user = context.Users.FirstOrDefault(u => u.Id == id);
                user.LvlAccess = Role.SelectedIndex;
                user.IsBlock = IsBlock.IsChecked == true;
                context.SaveChanges();
            }
        }

    }
}