using System.Collections.Generic;

namespace UtilitiesExpenses.Model
{
    /// <summary>
    /// Тип (категория) услуги.
    /// </summary>
    public sealed class UtilitiesType
    {
        public UtilitiesType()
        {
            TariffList = new List<Tariff>();
        }

        /// <summary>
        /// Наименование услуги.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список тарифов, по которым оказывается услуга.
        /// </summary>
        public List<Tariff> TariffList { get; set; }
    }
}