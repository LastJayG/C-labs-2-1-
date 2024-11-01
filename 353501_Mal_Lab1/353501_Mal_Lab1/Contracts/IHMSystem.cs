using _353501_Mal_Lab1.Entities;

namespace _353501_Mal_Lab1.Contracts
{
    internal interface IHMSystem
    {
        void AddResident(Resident newResident);
        void RemoveResident(Resident resident);
        void PurchaseService(string residentName, string serviceName);
        decimal GetTotalCostOfServices();
        int CountTotalServiceOrders(string serviceName);

        event EventHandler<string> ResidentChanged;
        event EventHandler<(string residentName, string serviceName)> ServicePurchased;
    }
}