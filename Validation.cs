using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace CurseWork
{
    internal static class Validation
    {
        internal static bool IsImgeNull(ImageSource image)
        {
            if (image == null)
            {
                MessageBox.Show("Вы не добавили картинку!",
                                   "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        internal static bool ManagerBuyValidation()
        {
            var result = MessageBox.Show
                        (
                        "Вы уверены что хотите купить?",
                        "Внимание",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                        );

            if (result == MessageBoxResult.No)
                return false;

            return true;
        }

        internal static bool CloseWindowValidation()
        {
            var result = MessageBox.Show
                        (
                        "Вы уверены что хотите выйти?",
                        "Внимание",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                        );

            if (result == MessageBoxResult.No)
                return false;

            return true;
        }

        internal static bool NameCategoryValidation(string str)
        {

            if (!Regex.IsMatch(str, "^([А-я]{1}[а-я]+)$") || str.Length > 20)
            {
                MessageBox.Show("Категория указана неверно и количество сиволов < 20! Пример: Напиток",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        internal static bool NameIngredientValidation(string str)
        {
            if (!Regex.IsMatch(str, "^\\p{Ll}{2,}(\\-?\\p{Ll}{2,})?$") || str.Length > 20)
            {
                MessageBox.Show("имя ингредента указано не правильно и" +
                    " количество сиволов < 20! Пример:coca-cola",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        internal static bool NameValidation(string str)
        {
            if(!Regex.IsMatch(str, "^([А-я]{1}[а-я]+)$") || str.Length > 20)
            {
                MessageBox.Show("ФИО должно быть на русском и длина любой из " +
                    "частей ФИО должна быть  до 20 символов. Пример: Ивано Иван Иванович",
                    "Внимание" , MessageBoxButton.OK, MessageBoxImage.Error);
                
                return false;
            }
            
            return true;
        } 

        internal static bool NegativeValidation(string str)
        {
            if (Convert.ToDecimal(str) < 0)
            {
                MessageBox.Show("Это число не может быть отрицательным!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        internal static bool EmtyValidation(string str)
        {

            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.Show("Поля не должны быть пустыми или заполнеными пробелами",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                
                return false;
            }

            return true;
        }

        internal static bool PriceValidation(string str)
        {

            if (!Regex.IsMatch(str, "^([0-9]+([.]{1}[0-9]{1,2})?)$"))
            {
                MessageBox.Show("Стоймость указана неверно: 0.3",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
               
                return false;
            }

            return true;
        }

        internal static bool CountValidation(string str)
        {
            if (!Regex.IsMatch(str, "^([0-9]+([.]{1}[0-9]{1,3})?)$"))
            {
                MessageBox.Show("Количество указано неверно. Пример: 0.123",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
               
                return false;
            }

            return true;
        }

        internal static bool CountValidationWMB(string str)
        {
            if (!Regex.IsMatch(str, "^([0-9]+([.]{1}[0-9]{1,3})?)$"))
            {

                return false;
            }

            return true;
        }

        internal static bool TelephoneValidation(string str)
        {
            if (!Regex.IsMatch(str, "^(\\+375[0-9]{9})$"))
            {
                MessageBox.Show("Телефон указан неверно. Пример:+375123456789",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                
                    return false;
            }

            return true;
        }

        internal static bool CanUserBuy(List<FoodInBasket> foods, Food currentFood)
        {

            using (var context = new MSSQLContext())
            {
                var listIngredients = context.Ingredients.ToList();
                
                foreach (var food in foods)
                {
                    foreach (var structure in context.Structures.Where(s => s.FoodId == food.Id))
                    {
                        listIngredients.First(i => i.Id == structure.IngredientId).Count -= structure.Quantity;
                    }
                }

                if (listIngredients.FirstOrDefault(i => i.Count < 0) != null)
                {
                    MessageBox.Show("Приносим свои извенения, но у нас не хватает ингредиентов на все ваши " +
                        "заказы!!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);

                    return false;
                }
            }

            return true;
        }
       
        internal static bool LoginValidation(string str)
        {

            if (!Regex.IsMatch(str, "\\S{4,20}"))
            {
                MessageBox.Show("В логине не могут быть пробелы",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        internal static bool PasswordValidation(string str)
        {

            if (!Regex.IsMatch(str, "\\S{6,20}"))
            {
                MessageBox.Show("В пароле не могут быть пробелы",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        internal static bool IsNumValidation(string str)
        {
            if (!Regex.IsMatch(str, "[0-9]+"))
            {
                return false;
            }

            return true;
        }

        internal static bool CheckUser(string str)
        {
            using(var context = new MSSQLContext())
            {
                if(null != context.Users.FirstOrDefault(u => u.Login == str))
                {
                    MessageBox.Show("Такой логин уже есть, придумайте другой",
                       "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

                    return false;
                }
            }

            return true;
        }

        internal static bool CheckTelephone(string str)
        {
            using (var context = new MSSQLContext())
            {
                if (null != context.Users.FirstOrDefault(u => u.Telephone == str))
                {
                    MessageBox.Show("Такой телефон уже есть, воспользуйтесь другим",
                       "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);

                    return false;
                }
            }

            return true;
        }

    }
}
