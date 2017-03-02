using GalaSoft.MvvmLight;

namespace UtilitiesExpenses.Model
{
    /// <summary>
    /// Тариф на услуги.
    /// </summary>
    public sealed class Tariff : ObservableObject
    {
        private string _name;
        private decimal _rate;

        public int Id { get; set; }

        /// <summary>
        /// Наименование тарифа.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                Set(nameof(Name), ref _name, value);
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
                Set(nameof(Rate), ref _rate, value);
            }
        }
    }
}
