using System.Collections.Generic;
using System.ComponentModel;


namespace CurseWork
{
    internal class RequestIngredientsModel: ObservableObject, IDataErrorInfo
    {

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        private decimal _AdditionalAmount;

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case nameof(AdditionalAmount):
                        if (AdditionalAmount < 0)
                            result = "Колличество не может быть отрицательным" ;
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

        internal int Id { set; get; }

        internal string Name { set; get; }

        internal decimal Count { set; get; }

        internal string Unit { set; get; }

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
