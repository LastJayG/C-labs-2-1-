using _353501_Mal_Lab2.Entities;

HousingMaintananceSystem system = new();
Journal journal = new(); 
journal.Subscribe(system);

Resident resident1 = new("Иван", "Иванов");
Resident resident2 = new("Петр", "Петров");

system.AddResidentToList(resident1);
system.AddResidentToList(resident2);

system.AddTariffToList("Водоснабжение", 80);
system.AddTariffToList("Электричество", 100);
system.AddTariffToList("Уборка", 10);

system.ServicePurchased += (residentName, serviceName) =>
{
    Console.WriteLine($"'{residentName}' приобрел услугу '{serviceName}'.");
};

system.ServiceDeleted += (residentName, serviceName) =>
{
    Console.WriteLine($"'{residentName}' отказался от услуги '{serviceName}'.");
};

system.AddTariffToResident(resident1, "Электричество");
system.AddTariffToResident(resident1, "Водоснабжение");
system.AddTariffToResident(resident2, "Электричество");

system.RemoveTariffFromResident(resident1, "Электричество");
system.RemoveTariffFromResident(resident2, "Водоснабжение");

List<string> sortedList = system.SortValueByName();
