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
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private List<Window> windows;
        private Action<User> successAuth;
        private Button LogOut, WorkRoom, Autoris, Edit;

        public Authorization(Button Edit, Button LogOut, Button WorkRoom, Button Authorization, Action<User> successAuth)
        {
            InitializeComponent();

            windows = new List<Window>();
            this.LogOut = LogOut;
            this.WorkRoom = WorkRoom;
            this.successAuth = successAuth;
            this.Edit = Edit;
            Autoris = Authorization;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach(var item in windows)
            {
                if (item.IsVisible) item.Close();
            }
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            var login = Login.Text.TrimEnd();
            var password = Password.Password.TrimEnd();

            if (login == "" || password == "")
            {
                MessageBox.Show("Поле Логин и поле Пароль должны быть обязательно заполнены ");
                return;
            }
            else
            {
                using (var context = new MSSQLContext())
                {
                    var autUser = context.Database.SqlQuery<User>("SELECT * FROM Users").FirstOrDefault(u => u.Login == login && u.Password == password);

                    if (autUser == null)
                    {
                        MessageBox.Show("Проверьте правильность введённых данных");
                        return;
                    }

                    else
                    {
                        successAuth.Invoke(autUser);

                        Edit.IsEnabled = true;
                        LogOut.IsEnabled = true;
                        Autoris.IsEnabled = false;

                        Edit.Visibility = Visibility.Visible;
                        LogOut.Visibility = Visibility.Visible;
                        Autoris.Visibility = Visibility.Hidden;

                        if (autUser.LvlAccess > 0)
                        {
                            WorkRoom.Visibility = Visibility.Visible;
                            WorkRoom.IsEnabled = true;
                        }

                    }

                }
            }          

            Close();
        }

        private void EditPassword_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditInfo();
            window.Show();

            windows.Add(window);
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var registration = new Registration(successAuth, LogOut, Autoris, Edit);
            
            registration.Show();

            windows.Add(registration);
        }
    }
}
