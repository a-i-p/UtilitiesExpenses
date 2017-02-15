using System.Collections.Generic;
using System.Linq;

namespace UtilitiesExpenses.Model
{
    public class TariffsService : ITariffsService
    {
        static private List<Tariff> tariffList;

        static TariffsService()
        {
            tariffList = new List<Tariff> {
                new Tariff { Id = 1, Name = "Одноставочный тариф", Rate = 3.37M },
                new Tariff { Id = 2, Name = "Холодное водоснабжение", Rate = 38.34M },
                new Tariff { Id = 3, Name = "Горячее водоснабжение", Rate = 140.87M },
                new Tariff { Id = 4, Name = "Водоотведение", Rate = 34.66M },
            };
        }

        public IEnumerable<Tariff> Refresh()
        {
            return tariffList;
        }

        public int Save(Tariff updatedTariff)
        {
            var tariff = tariffList.First(t => t.Id == updatedTariff.Id);
            if (tariff != null)
            {
                tariff.Name = updatedTariff.Name;
                tariff.Rate = updatedTariff.Rate;
            }
            else
            {
                tariffList.Add(updatedTariff);
            }
            return updatedTariff.Id;
        }
    }
}
