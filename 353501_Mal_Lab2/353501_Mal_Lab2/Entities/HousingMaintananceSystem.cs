using _353501_Mal_Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _353501_Mal_Lab2.Entities
{
    public class HousingMaintananceSystem :IHMSystem 
    {
        public event Action<string> TariffsUpdated;
        public event Action<string> ResidentsUpdated;
        public event Action<string, string> ServicePurchased;
        public event Action<string, string> ServiceDeleted;

        private Dictionary<string?, Tariff> tariffsList;
        private List<Resident> residentsList;

        public HousingMaintananceSystem()
        {
            tariffsList = new();
            residentsList = new();
        }

        public void AddTariffToList(string? serviceName, decimal priceValue)
        {
            Tariff newTariff = new(serviceName, priceValue);
            tariffsList.Add(newTariff.Service, newTariff);
            TariffsUpdated?.Invoke($"Тариф '{serviceName}' добавлен.");
        }

        public void RemoveTariffFromList(string? surname)
        {
            try
            {
                if (tariffsList.ContainsKey(surname))
                {
                    tariffsList.Remove(surname);
                    TariffsUpdated?.Invoke($"Тариф '{surname}' удален.");
                }
            }
            catch
            {
                throw new Exception("Ошибка удаления тарифа жильца");
            }
        }

        public void AddResidentToList(Resident newResident)
        {
            residentsList.Add(newResident);
            ResidentsUpdated?.Invoke($"Жилец '{newResident.surname}' добавлен.");
        }

        public void RemoveResidentFromList(Resident newResident)
        {
            try
            {
                residentsList.Remove(newResident);
                ResidentsUpdated?.Invoke($"Жилец '{newResident.surname}' удален.");
            }
            catch
            {
                throw new Exception("Жилец не был найден в ЖЭС");
            }
        }

        public void AddTariffToResident(Resident resident, string? serviceName)
        {
            try
            {
                if (residentsList.Contains(resident) && tariffsList.ContainsKey(serviceName))
                {
                    resident.tariffs.Add(tariffsList[serviceName]);
                    ServicePurchased?.Invoke(resident.surname, serviceName);
                }
            }
            catch
            {
                throw new Exception("Ошибка добавления тарифа жильцу");
            }
        }
        public void RemoveTariffFromResident(Resident resident, string? serviceName)
        {
            try
            {
                if (residentsList.Contains(resident))
                {
                    var tariffToRemove = resident.tariffs.FirstOrDefault(t => t.Service == serviceName);
                    if (tariffToRemove != null)
                    {
                        resident.tariffs.Remove(tariffToRemove);
                        ServiceDeleted?.Invoke(resident.surname, serviceName);
                    }
                    else
                    {
                        Console.WriteLine("Тариф с указанным именем не найден у жильца.");
                    }
                }
                else
                {
                    Console.WriteLine("Жилец не найден в списке.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка удаления тарифа жильца", ex);
            }
        }

        public List<string> SortValueByName()
        {
            var sortedNames = tariffsList
                .OrderBy(tariff => tariff.Value.Price)
                .Select(tariff => tariff.Value.Service);



                //.ToList();
            foreach (var t in sortedNames)
            {
                Console.WriteLine($"{t}\t");
            }

            //
            return (sortedNames.ToList());
        }
        public decimal TotalCostOfOrderedServices()
        {
            decimal totalCost = residentsList
                .SelectMany(resident => resident.tariffs)
                .Where(tariff => tariff.Price != 0) 
                .Sum(tariff => tariff.Price); 

            return totalCost; 
        }
        public decimal TotalCostOfResidentServices(Resident resident)
        {
            if (resident == null)
                throw new ArgumentNullException(nameof(resident));

            decimal totalCost = resident.tariffs
                .Where(tariff => tariff.Price > 0) 
                .Sum(tariff => tariff.Price); 

            return totalCost; 
        }
        public string GetResidentWithMaxPayment()
        {
            if (!residentsList.Any())
                return "Жилец не найден.";

            var maxPayment = residentsList
                .Select(resident => new
                {
                    Resident = resident,
                    TotalCost = resident.tariffs.Sum(tariff => tariff.Price) 
                })
                .OrderByDescending(resident => resident.TotalCost)
                .FirstOrDefault(); 

            return maxPayment?.Resident.name ?? "Жилец не найден.";
        }
        public int CountResidentsAbovePayment(decimal threshold) // threshold - пороговая сумма
        {
            if (!residentsList.Any())
                return 0;

            int count = residentsList
                .Aggregate(0, (currentCount, resident) =>
                    currentCount + (resident.tariffs.Sum(tariff => tariff.Price) > threshold ? 1 : 0));

            return count; 
        }
        public Dictionary<string, decimal> GetPaymentsByService(Resident resident)
        {
            if (resident == null)
                throw new ArgumentNullException(nameof(resident));

            var paymentsByService = resident.tariffs
                .GroupBy(tariff => tariff.Service) 
                .ToDictionary(group => group.Key, group => group.Sum(tariff => tariff.Price)); 

            return paymentsByService; 
        }
    }
}
