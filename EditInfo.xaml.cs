using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditInfo.xaml
    /// </summary>
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

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null)
            {
                if (ChangePasswordValidation()) SaveNewPassword();
            }
            else if (Validation())
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
            }

            MessageBox.Show("Изменения прошли успешно.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }

        private bool ChangePasswordValidation()
        {
            if(string.IsNullOrWhiteSpace(Telephone.Text))
            {
                MessageBox.Show("Для смены пароля вы должны указать номер телефона", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (CheckStringField(Telephone.Text, "^([+375]{1}[0-9]{9})$"))
            {
                MessageBox.Show("Для смены пароля вы должны указать номер телефона", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckStringField(Password.Password, "\\S{6,20}"))
            {
                MessageBox.Show("В Пароле должны быть любые непробельные символы от 6 до 20 символов Пример: BigB0$");
                return false;
            }

            if (Password.Password != PasswordCheck.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }

            using (var context = new MSSQLContext())
            {
                if(null == context.Users.FirstOrDefault(u => u.Telephone == Telephone.Text))
                {
                    MessageBox.Show("Такого теефона мы не знаем", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
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

            Close();
        }

        private bool Validation()
        {
            if(string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(MiddleName.Text) 
                || string.IsNullOrWhiteSpace(LastName.Text) || string.IsNullOrWhiteSpace(Telephone.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckStringField(FirstName.Text, "^([А-я]{1}[а-я]+)$"))
            {
                MessageBox.Show("Фамилия может содержать только русские буквы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckStringField(MiddleName.Text, "^([А-я]{1}[а-я]+)$"))
            {
                MessageBox.Show("Имя может содержать только русские буквы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckStringField(LastName.Text, "^([А-я]{1}[а-я]+)$"))
            {
                MessageBox.Show("Отчество может содержать только русские буквы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckStringField(Telephone.Text, "^([+375]{1}[0-9]{9})$"))
            {
                MessageBox.Show("Телефон должен начинаться на +375 ,а дальше идут 9 цифр вашего номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool CheckStringField(string str, string pattern)
        {
            return Regex.IsMatch(str, pattern);
        }
    }
}
