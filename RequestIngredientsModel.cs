using System.Collections.Generic;
using System.ComponentModel;


namespace CurseWork
{
    public class RequestIngredientsModel: ObservableObject, IDataErrorInfo
    {

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        private decimal _AdditionalAmount;

        private string _Unit;

        private decimal _Count;

        private string _Name;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(AdditionalAmount):
                        if (AdditionalAmount < 0)
                            result = "Колличество не может быть отрицательным";
                        else if (Validation.CountValidationWMB(AdditionalAmount.ToString()))
                            result = "Количество указано неверно.";
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

        public int Id { set; get; }

        public string Name
        {
            get { return _Name; }

            set
            {
                OnPropertyChanged(ref _Name, value);
            }
        }

        public decimal Count
        {
            get { return _Count; }

            set
            {
                OnPropertyChanged(ref _Count, value);
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

        public decimal AdditionalAmount
        {
            get { return _AdditionalAmount; }
            
            set
            {
                OnPropertyChanged(ref _AdditionalAmount, value);
            }
        }
    }
}
