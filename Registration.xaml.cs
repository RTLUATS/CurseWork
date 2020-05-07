using System.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private User currentUser;
        private Button LogOut, Authorization;
        
        public Registration(User user, Button LogOut, Button Authorization)
        {
            InitializeComponent();

            currentUser = user;
            this.LogOut = LogOut;
            this.Authorization = Authorization;

        }

        private void Access_Click(object sender, RoutedEventArgs e)
        {
            var firstName = FirstName.Text;
            var middleName = MiddleName.Text;
            var lastName = LastName.Text;
            var telephone = Telephone.Text;
            var login = Login.Text;
            var password1 = Password.Password;
            var password2 = PasswordCheck.Password;

            if (firstName == "" || middleName == "" || lastName == "" || telephone == "" || login == "" ||
                password1 == "" || password2 == "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            
            var checkName1 = checkStringField(firstName, "[А-Я]{1}[а-я]+");
            var checkName2 = checkStringField(middleName, "[А-Я]{1}[а-я]+");
            var checkName3 = checkStringField(lastName, "[А-Я]{1}[а-я]+");

            if (checkName1!=true || checkName2!=true || checkName3!=true)
            {
                MessageBox.Show("В ФИО должны быть только символы кириллицы Пример:Иванов Иван Иванович");
                return;
            }

            var checkTelephone = checkStringField(telephone, "\\+375[0-9]{9}");

            if (!checkTelephone)
            {
                MessageBox.Show("В Телефоне должны быть только цифры Пример: +375447856909");
                return;
            }

            var checkLogin = checkStringField(login, "\\S{4,20}");

            if (!checkLogin)
            {
                MessageBox.Show("В Логине должны быть любые непробельные символы от 4 до 20 символов Пример: Adam123&");
                return;
            }

            var checkPassword1 = checkStringField(password1, "\\S{6,20}");
            
            if (!checkPassword1)
            {
                MessageBox.Show("В Пароле должны быть любые непробельные символы от 6 до 20 символов Пример: BigB0$");
                return;
            }

            var checkPassword2 = checkStringField(password2, $"{password1}");

            if (!checkPassword2)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }


            // добавление нового пользователя в бд

            using (var context = new MSSQLContext())
            {

                if (null != context.Users.FirstOrDefault(u =>u.Login==login ))
                {
                    MessageBox.Show("Пользователь с таким логином уже есть. Придумайте новый");
                    return;
                }

                var user = new User()
                {
                    Login = login,
                    Password = password1,
                    LvlAccess = 0,
                    Telephone = telephone,
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName
                };


                context.Users.Add(user);

                context.SaveChanges();

                currentUser = user;

            }

            LogOut.Visibility = Visibility.Visible;
            LogOut.IsEnabled = true;
            Authorization.Visibility = Visibility.Hidden;
            Authorization.IsEnabled = false;
        }

        private bool checkStringField(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }

    }
}
