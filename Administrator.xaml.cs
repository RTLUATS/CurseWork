using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        private List<User> users;

        public Administrator()
        {
            InitializeComponent();
        }

        private void AllUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users");
        }

        private void LoadUsers(string sqlQuery)
        {
            ListUsers.Items.Clear();

            using (var context = new MSSQLContext())
            {
                users = context.Database.SqlQuery<User>(sqlQuery).ToList();
            }

            foreach (var user in users)
            {

                var panel = new StackPanel();

                var label = new Label()
                {
                    Content = $"{user.FirstName} {user.MiddleName}  {user.LastName}",
                    Foreground = Brushes.White,
                    FontSize = 16,
                    FontWeight = FontWeights.DemiBold,
                    Margin = new Thickness(5)
                };

                var button = new Button()
                {
                    Name = "Name" + user.Id.ToString(),
                    IsEnabled = true,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Content = $"Посмотреть",
                    Margin = new Thickness(5),
                };


                panel.Children.Add(label);
                panel.Children.Add(button);
           
                button.Click += EventForUsers;

                ListUsers.Items.Add(panel);
            }
        }

        private void EventForUsers(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = Convert.ToInt32(button.Name.Replace("Name", ""));
            var window = new AdministratorViewUsers(users.First(u => u.Id == id));

            window.Show();
        }

        private void ViewEconomists_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=2");
        }

        private void ViewManagers_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=1");
        }

        private void ViewAdministrators_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=4");
        }

        private void ViewDirectors_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=5");
        }

        private void ViewUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=0");
        }

        private void ViewChefs_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers("select * from Users where LvlAccess=3");
        }

    }
}
