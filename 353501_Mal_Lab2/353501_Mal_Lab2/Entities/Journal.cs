using System;

namespace _353501_Mal_Lab2.Entities
{
    public class Journal
    {
        public void Subscribe(HousingMaintananceSystem housingSystem)
        {
            housingSystem.TariffsUpdated += OnTariffsUpdated;
            housingSystem.ResidentsUpdated += OnResidentsUpdated;
        }

        private void OnTariffsUpdated(string message)
        {
            Console.WriteLine($"[Журнал] {message}");
        }

        private void OnResidentsUpdated(string message)
        {
            Console.WriteLine($"[Журнал] {message}");
        }
    }
}