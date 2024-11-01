using System;

namespace _353501_Mal_Lab1.Entities
{
    public class Journal
    {
        public delegate void EventLoggedHandler(object sender, string description, string entityName);

        public event EventLoggedHandler EventLogged;

        public Journal(HousingMaintenanceSystem system)
        {
            system.ResidentChanged += OnResidentChanged;
            system.ServicePurchased += OnServicePurchased;
        }

        public void LogEvent(string description, string entityName)
        {
            EventLogged?.Invoke(this, description, entityName);
        }

        private void OnResidentChanged(object sender, string description)
        {
            LogEvent(description, "Список жильцов");
        }

        private void OnServicePurchased(object sender, (string residentName, string serviceName) args)
        {
            LogEvent($"{args.residentName} приобрел {args.serviceName}", "Приобрел услугу");
        }

        private void OnEventLogged(object sender, string description, string entityName)
        {
            Console.WriteLine($"[{entityName}] {description}");
        }
    }
}