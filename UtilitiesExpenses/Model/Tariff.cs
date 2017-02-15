using System.ComponentModel;

namespace UtilitiesExpenses.Model
{
    /// <summary>
    /// Тариф на услуги.
    /// </summary>
    public sealed class Tariff : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _name;
        /// <summary>
        /// Наименование тарифа.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        decimal _rate;
        /// <summary>
        /// Тарифная ставка.
        /// </summary>
        public decimal Rate
        {
            get { return _rate; }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    RaisePropertyChanged(nameof(Rate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = new PropertyChangedEventHandler((s, a) => { });

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}