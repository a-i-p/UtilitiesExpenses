using System.Collections.Generic;

namespace UtilitiesExpenses.Model
{
    public interface ITariffsService
    {
        IEnumerable<Tariff> Refresh();

        int Save(Tariff updatedTariff);
    }
}