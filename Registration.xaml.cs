using System.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System;
using System.ComponentModel;

namespace CurseWork
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private Action<User> success;
        private bool isDataDirty = false;
        private Button LogOut, Authorization, Edit;
        
        public Registration(Action<User> success, Button LogOut, Button Authorization, Button Edit)
        {
            InitializeComponent();

            this.LogOut = LogOut;
            this.Authorization = Authorization;
            this.Edit = Edit;
            this.success = success;
        }

        private void TextChanged(object sender, EventArgs e)
        {
            isDataDirty = true;
        }

        void Registration_Closing(object sender, CancelEventArgs e)
        {
            // If data is dirty, notify user and ask for a response
            if (isDataDirty)
            {
                string msg = "Данные были изменены. Вы точно хотите закрыть окно?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }
        }

        private void Access_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(MiddleName.Text) 
                || string.IsNullOrWhiteSpace(LastName.Text) || string.IsNullOrWhiteSpace(Telephone.Text) 
                || string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Password)
                || string.IsNullOrWhiteSpace(PasswordCheck.Password))
            {
                MessageBox.Show("Все поля должны быть заполнены", "Внимание", MessageBoxButton.OK , MessageBoxImage.Error);
                return;
            }
            

            if (!CheckStringField(FirstName.Text, "[А-Я]{1}[а-я]+") || !CheckStringField(MiddleName.Text, "[А-Я]{1}[а-я]+") 
                || !CheckStringField(LastName.Text, "[А-Я]{1}[а-я]+"))
            {
                MessageBox.Show("В ФИО должны быть только символы кириллицы Пример:Иванов Иван Иванович");
                return;
            }

 

            if (!CheckStringField(Telephone.Text, "^([+375]{1}[0-9]{9})$"))
            {
                MessageBox.Show("В Телефоне должны быть только цифры Пример: +375447856909");
                return;
            }

            if (!CheckStringField(Login.Text, "\\S{4,20}"))
            {
                MessageBox.Show("В Логине должны быть любые непробельные символы от 4 до 20 символов Пример: Adam123&");
                return;
            }

            if (!CheckStringField(Password.Password, "\\S{6,20}"))
            {
                MessageBox.Show("В Пароле должны быть любые непробельные символы от 6 до 20 символов Пример: BigB0$");
                return;
            }

            if (Password.Password != PasswordCheck.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            using (var context = new MSSQLContext())
            {

                if (null != context.Users.FirstOrDefault(u =>u.Login==Login.Text ))
                {
                    MessageBox.Show("Пользователь с таким логином уже есть. Придумайте новый");
                    return;
                }

                if (null != context.Users.FirstOrDefault(u => u.Telephone == Telephone.Text))
                {
                    MessageBox.Show("Этот телефон уже привязан к пользователю");
                    return;
                }

                var user = new User()
                {
                    Login = Login.Text,
                    Password = Password.Password,
                    LvlAccess = 0,
                    Telephone = Telephone.Text,
                    FirstName = FirstName.Text,
                    MiddleName = MiddleName.Text,
                    LastName = LastName.Text
                };


                context.Users.Add(user);

                context.SaveChanges();

                success.Invoke(user);
                
                Close();

            }

            LogOut.Visibility = Visibility.Visible;
            LogOut.IsEnabled = true;
            Authorization.Visibility = Visibility.Hidden;
            Authorization.IsEnabled = false;
            Edit.Visibility = Visibility.Visible;
            Edit.IsEnabled = true;
        }

        private bool CheckStringField(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }

    }
}
