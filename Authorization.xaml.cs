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
        private User autUser = null;
        private User currentUser;
        private Action<User> successAuth;
        private Button LogOut, WorkRoom, Autoris;

        public Authorization(User user, Button LogOut, Button WorkRoom, Button Authorization, Action<User> successAuth)
        {
            InitializeComponent();
            currentUser = user;
            this.LogOut = LogOut;
            this.WorkRoom = WorkRoom;
            this.successAuth = successAuth;
            Autoris = Authorization;

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
                    autUser = context.Database.SqlQuery<User>("SELECT * FROM Users").FirstOrDefault(u => u.Login == login && u.Password == password);
                }
            }

            if (autUser == null)
            {
                MessageBox.Show("Проверьте правильность введённых данных");
                return;
            }
            
            else
            {
                LogOut.IsEnabled = true;
                LogOut.Visibility = Visibility.Visible;
                Autoris.IsEnabled = false;
                Autoris.Visibility = Visibility.Hidden;

                if (autUser.LvlAccess > 0)
                {
                    WorkRoom.Visibility = Visibility.Visible;
                    WorkRoom.IsEnabled = true;
                }

            }

            successAuth.Invoke(autUser);            

            Close();
        }


        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var registration = new Registration(autUser, LogOut, Autoris);
            
            registration.Show();
        }
    }
}
