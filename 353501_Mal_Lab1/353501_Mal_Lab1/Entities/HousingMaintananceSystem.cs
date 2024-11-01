using System;
using System.Collections.Generic;
using Generic.Math;
using _353501_Mal_Lab1.Collections;
using _353501_Mal_Lab1.Entities;
using _353501_Mal_Lab1.Contracts;
using System.Numerics;

namespace _353501_Mal_Lab1.Entities
{
    public class HousingMaintenanceSystem : IHMSystem
    {
        private MyCustomCollection<Resident> residents;

        // События для уведомления об изменении жильцов и покупке услуг
        public event EventHandler<string> ResidentChanged;
        public event EventHandler<(string residentName, string serviceName)> ServicePurchased;

        public HousingMaintenanceSystem()
        {
            residents = new MyCustomCollection<Resident>();
        }

        public void AddResident(Resident newResident)
        {
            residents.Add(newResident);
            OnResidentChanged($"Жилец добавлен: {newResident.Surname}");
        }

        public void RemoveResident(Resident resident)
        {
            try
            {
                residents.Remove(resident);
                OnResidentChanged($"Жилец удален: {resident.Surname}");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Жилец не найден: {resident.Surname}");
            }
        }

        public void PurchaseService(string residentName, string serviceName)
        {

            OnServicePurchased((residentName, serviceName));
            Console.WriteLine($"{residentName} приобрел услугу: {serviceName}");
        }

        public decimal GetTotalCostOfServices()
        {
            decimal totalCost = 0;
            foreach (var resident in residents)
            {
                totalCost = GenericMath.Add(totalCost, resident.GetTotalCost());
            }
            return totalCost;
        }

        public int CountTotalServiceOrders(string serviceName)
        {
            int totalCount = 0;
            foreach (var resident in residents)
            {
                totalCount += resident.GetOrderCount(serviceName);
            }
            return totalCount;
        }


        protected virtual void OnResidentChanged(string description)
        {
            ResidentChanged?.Invoke(this, description);
        }

        protected virtual void OnServicePurchased((string residentName, string serviceName) args)
        {
            ServicePurchased?.Invoke(this, args);
        }
    }
}