using System.ComponentModel;

namespace UtilitiesExpenses.Model
{
    /// <summary>
    /// Тариф на услуги.
    /// </summary>
    public sealed class Tariff : INotifyPropertyChanged
    {
        private string _name;
        private decimal _rate;

        public event PropertyChangedEventHandler PropertyChanged = new PropertyChangedEventHandler((s, a) => { });

        public int Id { get; set; }

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

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}