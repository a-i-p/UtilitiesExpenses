using GalaSoft.MvvmLight;

namespace UtilitiesExpenses.Model
{
    /// <summary>
    /// Тариф на услуги.
    /// </summary>
    public sealed class Tariff : ObservableObject
    {
        private bool _isDirty;
        private string _name;
        private decimal _rate;

        public int Id { get; set; }

        /// <summary>
        /// Указывает, что сущность изменена.
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                Set(nameof(IsDirty), ref _isDirty, value);
            }
        }

        /// <summary>
        /// Наименование тарифа.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (Set(nameof(Name), ref _name, value))
                {
                    IsDirty = true;
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
                if (Set(nameof(Rate), ref _rate, value))
                {
                    IsDirty = true;
                }
            }
        }
    }
}
