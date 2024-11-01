using _353501_Mal_Lab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _353501_Mal_Lab2.Interfaces
{
    internal interface IHMSystem
    {
        // Добавление и удаление тариффа из списка
        public void AddTariffToList(string? serviceName, decimal priceValue);
        public void RemoveTariffFromList(string? name);

        // Добавление и удаление жильца из ЖЭС
        public void AddResidentToList(Resident newResident);
        public void RemoveResidentFromList(Resident newResident);

        // Добавление и удаление тариффа жильца
        public void AddTariffToResident(Resident resident, string? serviceName);
        public void RemoveTariffFromResident(Resident resident, string? serviceName);

        // Получение списка названий всех услуг, отсортированного по стоимости 
        public List<string> SortValueByName();

        //Получение общей стоимости всех выполненных услуг ЖЭС. 
        public decimal TotalCostOfOrderedServices();

        // Получение  общей  стоимости  всех  услуг,
        // заказанных  жильцом  в соответствии с действующими тарифами;
        public decimal TotalCostOfResidentServices(Resident resident);

        //Получение имени жильца, заплатившего максимальную сумму.
        //Если таких жильцов несколько, получить имя первого в списке
        public string GetResidentWithMaxPayment();

        //Получение количества жильцов, заплативших больше определеной суммы (использовать Aggregate)
        public int CountResidentsAbovePayment(decimal threshold);

        //Получение  жильцом  списка  сумм,  заплаченных  по  каждой  услуге (использовать GroupBy). 
        public Dictionary<string, decimal> GetPaymentsByService(Resident resident);


    }
}
