using _353501_Mal_Lab1.Entities;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        HousingMaintenanceSystem housingSystem = new();
        Journal journal = new(housingSystem);




        try
        {
            Resident resident1 = new("Первый");
            resident1.AddTariff(new Tariff("Услуга 1", 100));
            resident1.AddTariff(new Tariff("Услуга 2", 150));

            Resident resident2 = new("Второй");
            resident2.AddTariff(new Tariff("Услуга 1", 200));
            resident2.AddTariff(new Tariff("Услуга 3", 300));

            housingSystem.AddResident(resident1);
            housingSystem.AddResident(resident2);

            decimal totalCost = housingSystem.GetTotalCostOfServices();
            Console.WriteLine($"Общая стоимость всех услуг: {totalCost}");


            int orderCount = housingSystem.CountTotalServiceOrders("Услуга 1");
            Console.WriteLine($"Общее количество заказов на 'Услуга 1': {orderCount}");

            orderCount = housingSystem.CountTotalServiceOrders("Услуга 2");
            Console.WriteLine($"Общее количество заказов на 'Услуга 2': {orderCount}");

            orderCount = housingSystem.CountTotalServiceOrders("Услуга 3");
            Console.WriteLine($"Общее количество заказов на 'Услуга 3': {orderCount}");

            resident1.ResidentAction += (sender, actionDescription) =>
            {
                journal.LogEvent($"Резидент {((Resident)sender).Surname} совершил действие: {actionDescription}", resident1.Surname);
            };

            housingSystem.ResidentChanged += (sender, description) =>
            {
                journal.LogEvent(description, "Список жильцов");
            };

            housingSystem.ServicePurchased += (sender, args) =>
            {
                journal.LogEvent($"{args.residentName} приобрел {args.serviceName}", "Приобретена услуга");
            };

            housingSystem.PurchaseService(resident1.Surname, "Услуга 1");

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка индекса: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }
}