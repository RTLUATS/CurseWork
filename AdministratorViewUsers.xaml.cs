using System;
using System.Linq;
using System.Windows;


namespace CurseWork
{
   
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