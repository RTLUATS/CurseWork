using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CurseWork
{
   
    public partial class EditInfo : Window
    {
        private User currentUser;
        private Action<User> success;
        private bool isDataDirty = false;

        public EditInfo(User user = null, Action<User> success = null)
        {

            InitializeComponent();

            if (user == null)
                LoadToChangePass();
            else
                LoadToEdit(user, success);
        }

        private void LoadToEdit(User user, Action<User> success)
        {
            FirstName.Text = user.FirstName;
            MiddleName.Text = user.MiddleName;
            LastName.Text = user.LastName;
            Telephone.Text = user.Telephone;

            currentUser = user;

            this.success = success;
        }

        private void LoadToChangePass()
        {
            FirstName.Visibility = Visibility.Hidden;
            MiddleName.Visibility = Visibility.Hidden;
            LastName.Visibility = Visibility.Hidden;

            FirstName.IsEnabled = false;
            MiddleName.IsEnabled = false;
            LastName.IsEnabled = false;

            Label.Content = "Смена пароля";
        }

        private void TextChanged(object sender, EventArgs e)
        {
            isDataDirty = true;
        }

        void Edit_Closing(object sender, CancelEventArgs e)
        {
            // If data is dirty, notify user and ask for a response
            if (isDataDirty)
            {
                e.Cancel = !Validation.CloseWindowValidation();
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null)
            {
                if (ChangePasswordValidation()) SaveNewPassword();
            }
            else if (Check())
            {
                SaveChanges();
            }
        }

        private void SaveNewPassword()
        {
            using(var context = new MSSQLContext())
            {
                currentUser = context.Users.First(u => u.Telephone == Telephone.Text);
                currentUser.Password = Password.Password;
                context.SaveChanges();
            }

            MessageBox.Show("Изменения прошли успешно.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            
            isDataDirty = false;

            Close(); ;
        }

        private bool ChangePasswordValidation()
        {
            if(!Validation.TelephoneValidation(Telephone.Text)) return false;
            if (!Validation.PasswordValidation(Password.Password)) return false;

            if (Password.Password != PasswordCheck.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            using (var context = new MSSQLContext())
            {
                if(null == context.Users.FirstOrDefault(u => u.Telephone == Telephone.Text))
                {
                    MessageBox.Show("Такого теефона мы не знаем", "Ошабка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }

        private void SaveChanges()
        {
            using (var context = new MSSQLContext())
            {

                string telepnone = currentUser.Telephone;

                currentUser = context.Users.Find(currentUser.Id);

                if (currentUser.Telephone != telepnone)
                {
                    if (null != context.Users.FirstOrDefault(u => u.Telephone == telepnone))
                    {
                        MessageBox.Show("Этот теефон недоступен для повторной регистрации. Укажите другой", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                }

                currentUser.LastName = LastName.Text;
                currentUser.FirstName = FirstName.Text;
                currentUser.MiddleName = MiddleName.Text;
                currentUser.Telephone = telepnone;
                context.SaveChanges();
            }

            success.Invoke(currentUser);

            MessageBox.Show("Изменения прошли успешно.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            
            isDataDirty = false;

            Close();
        }

        private bool Check()
        {

            if (!Validation.NameValidation(FirstName.Text) || !Validation.NameValidation(MiddleName.Text)
                    || !Validation.NameValidation(LastName.Text))
            {
                return false;
            }

            if (!Validation.TelephoneValidation(Telephone.Text)) return false;

            return true;
        }
    }
}
