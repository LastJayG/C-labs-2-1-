using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab_4.Entities;
using Lab_4.Interfaces;

string folderName = "Malinovskaya_Lab4"; 
string path = Path.Combine(Directory.GetCurrentDirectory(), folderName);

// Проверка существования папки
if (Directory.Exists(path))
{
    var files = Directory.GetFiles(path);
    foreach (var file in files)
    {
        File.Delete(file);
    }
    Console.WriteLine("Папка уже существует. Все содержимое удалено.");
}
else
{
    Directory.CreateDirectory(path);
    Console.WriteLine("Папка создана.");
}

// 10 случайных файлов
string[] extensions = { ".txt", ".rtf", ".dat", ".inf" };
Random random = new Random();

for (int i = 0; i < 10; i++)
{
    string fileName = Path.GetRandomFileName() + extensions[random.Next(extensions.Length)];
    File.Create(Path.Combine(path, fileName)).Close();
}

// вывод списка файлов
var createdFiles = Directory.GetFiles(path);
foreach (var file in createdFiles)
{
    Console.WriteLine($"Файл: {Path.GetFileName(file)} имеет расширение {Path.GetExtension(file)}");
}

List<Passenger> passengers = new List<Passenger>
            {
                new Passenger("Иван", true),
                new Passenger("Сергей", false),
                new Passenger("Ева", true),
                new Passenger("Марк", false),
                new Passenger("Егор", true)
            };

// запись коллекции в файл
string dataFileName = "passengers.dat";
IFileService<Passenger> fileService = new FileService<Passenger>();
fileService.SaveData(passengers, Path.Combine(path, dataFileName));
Console.WriteLine($"Данные пассажиров сохранены в файл: {dataFileName}");

// переименовываем файл
string newDataFileName = "passengers_updated.dat";
File.Move(Path.Combine(path, dataFileName), Path.Combine(path, newDataFileName));

// пустая коллекция и чтение данных из файла
List<Passenger> loadedPassengers = new List<Passenger>();
loadedPassengers = fileService.ReadFile(Path.Combine(path, newDataFileName)).ToList();

// Сортировка с помощью LINQ
var sortedPassengers = loadedPassengers
    .OrderBy(p => p.name) 
    .ToList();

Console.WriteLine("Исходная коллекция:");
foreach (var passenger in loadedPassengers)
{
    Console.WriteLine($"Имя: {passenger.name}, С льготой: {passenger.withBenefit}");
}

Console.WriteLine("\nОтсортированная коллекция:");
foreach (var passenger in sortedPassengers)
{
    Console.WriteLine($"Имя: {passenger.name}, С льготой: {passenger.withBenefit}");
}

// сортировка по свойству withBenefit
var sortedByBenefit = loadedPassengers.OrderBy(p => p.withBenefit).ToList();

Console.WriteLine("\nОтсортированная коллекция по свойству withBenefit:");
foreach (var passenger in sortedByBenefit)
{
    Console.WriteLine($"Имя: {passenger.name}, С льготой: {passenger.withBenefit}");
}
