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

        private void LoadUsers(int LvlAccess)
        {
            ListUsers.Items.Clear();

            using (var context = new MSSQLContext())
            {
                users = LvlAccess == 6 ? context.Users.ToList() : 
                    context.Users.Where(u => u.LvlAccess == LvlAccess).ToList();
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
                    HorizontalAlignment = HorizontalAlignment.Left,
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

        private void Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadUsers(Box.SelectedIndex);
        }
    }
}
