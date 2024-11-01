using _353501_Mal_Lab1.Collections;

namespace _353501_Mal_Lab1.Entities
{
    public class Resident
    {
        private string surname;
        private MyCustomCollection<Tariff> tariffs;

        public Resident(string surname)
        {
            this.surname = surname;
            tariffs = new MyCustomCollection<Tariff>();
        }

        public void AddTariff(Tariff tariff)
        {
            tariffs.Add(tariff);
        }

        public string Surname
        {
            get
            {
                return surname;
            }
        }
        public decimal GetTotalCost()
        {
            return tariffs.GetTotalCost(t => t.Price);
        }

        public int GetOrderCount(string serviceName)
        {
            return tariffs.CountServiceOrders(t => t.Service, serviceName);
        }

        // Часть второй лабораторной

        public delegate void ResidentActionHandler(object sender, string actionDescription);
        public event ResidentActionHandler? ResidentAction;

        protected virtual void OnResidentAction(string actionDescription)
        {
            ResidentAction?.Invoke(this, actionDescription);
        }

        public void PerformAction(string action)
        {
            OnResidentAction(action);
        }
    }
}
