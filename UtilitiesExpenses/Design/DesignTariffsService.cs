using System;
using System.Collections.Generic;
using UtilitiesExpenses.Model;

namespace UtilitiesExpenses.Design
{
    public class DesignTariffsService : ITariffsService
    {
        public IEnumerable<Tariff> Refresh()
        {
            var list = new List<Tariff>(10);
            var r = new Random();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Tariff
                {
                    Id = i,
                    Name = $"Наименование тарифа №{i}",
                    Rate = decimal.Round(((decimal)r.NextDouble() * 100M), 2)
                });
            }
            return list;
        }

        public int Save(Tariff updatedTariff)
        {
            // не используется в режиме дизайна.
            throw new NotImplementedException();
        }
    }
}