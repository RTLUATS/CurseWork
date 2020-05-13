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

            if (!Validation.EmtyValidation(FirstName.Text) || !Validation.EmtyValidation(MiddleName.Text) 
                || !Validation.EmtyValidation(LastName.Text) || !Validation.EmtyValidation(Telephone.Text) 
                || !Validation.EmtyValidation(Login.Text) || !Validation.EmtyValidation(Password.Password)
                || !Validation.EmtyValidation(PasswordCheck.Password))
            {
                return;
            }
            

            if (!Validation.NameValidation(FirstName.Text)|| !Validation.NameValidation(MiddleName.Text) 
                || !Validation.NameValidation(LastName.Text))
            {
                return;
            }

            if (!Validation.LoginValidation(Login.Text)) return;
            if (!Validation.CheckUser(Login.Text)) return;
            if (!Validation.TelephoneValidation(Telephone.Text))  return;
            if (!Validation.CheckTelephone(Telephone.Text)) return;
            if (!Validation.PasswordValidation(Password.Password)) return;

            if (Password.Password != PasswordCheck.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            using (var context = new MSSQLContext())
            {
                var user = new User()
                {
                    Login = Login.Text,
                    Password = Password.Password,
                    LvlAccess = 0,
                    Telephone = Telephone.Text,
                    FirstName = FirstName.Text,
                    MiddleName = MiddleName.Text,
                    LastName = LastName.Text,
                    IsBlock = false
                };


                context.Users.Add(user);
                context.SaveChanges();

                success.Invoke(user);

                isDataDirty = false;

                Close();
            }

            LogOut.Visibility = Visibility.Visible;
            LogOut.IsEnabled = true;
            Authorization.Visibility = Visibility.Hidden;
            Authorization.IsEnabled = false;
            Edit.Visibility = Visibility.Visible;
            Edit.IsEnabled = true;
        }
    }
}
