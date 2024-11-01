using Lab_4.Entities;
using Lab_4.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

public class FileService<T> : IFileService<T> where T : Passenger
{
    public IEnumerable<T> ReadFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"Файл {fileName} не найден.");
        }

        try
        {
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                var passengers = new List<T>();
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string name = reader.ReadString();
                    bool withBenefit = reader.ReadBoolean();
                    passengers.Add((T)Activator.CreateInstance(typeof(T), name, withBenefit));
                }
                return passengers;
            }
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("Достигнут конец потока.");
            return new List<T>();
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
            return new List<T>();
        }
    }

    public void SaveData(IEnumerable<T> data, string fileName)
    {
        try
        {
            BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
            {
                foreach (var passenger in data)
                {
                    writer.Write(passenger.name);
                    writer.Write(passenger.withBenefit);
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
        }
    }
}