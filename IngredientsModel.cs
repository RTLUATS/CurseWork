using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CurseWork
{
    public class IngredientsModel: ObservableObject, IDataErrorInfo
    {
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(IngredientName):
                        if (!Regex.IsMatch(IngredientName, "^\\p{Ll}{2,}(\\-?\\p{Ll}{2,})?$"))
                            result = "Название ингредиента должно быть записанно в нижнем регистре." +
                                "И слово должно быть от 2-ух символов с обоих сторон тире";
                        if (IngredientName.Length > 20)
                            result = "Количество символов должно быть < 20";
                        break;
                    case nameof(Weight):
                        if (Weight < 0)
                            result = "Колличество ингредиентов в блюде не может быть меньше нуля";
                        break;
                    case nameof(Unit):
                        if (Unit == "" || Unit == null)
                            result = "Единица измерения должна быть выбрана";
                        break; 
                }


                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);

                OnPropertyChanged("ErrorCollection");

                return result;
            }
        }

        public string Error { get { return null; } }

        private string _IngredientName;

        private decimal _Weight;

        private string _Unit;

        public string IngredientName
        {
            get { return _IngredientName; }

            set
            {
                OnPropertyChanged(ref _IngredientName, value);
            }
        }

        public decimal Weight
        {
            get { return _Weight; }

            set
            {
                OnPropertyChanged(ref _Weight, value);
            }
        }

        public string Unit 
        {
            get { return _Unit; }

            set 
            {
                OnPropertyChanged(ref _Unit, value);
            }   
        }
       
        public string CookingStep { get; set; }
    }
}
