using System;
using System.Collections.Generic;

namespace _353501_Mal_Lab1.Interfaces
{
    internal interface ICustomCollection<T>
    {
        T this[int index] { get; set; } // Индексатор

        void Reset(); // Сброс указателя
        void Next(); // Перемещение к следующему элементу
        T Current(); // Получение текущего элемента

        int Count { get; } // Количество элементов в коллекции

        void Add(T item); // Добавление элемента
        void Remove(T item); // Удаление элемента
        T RemoveCurrent(); // Удаление текущего элемента

        decimal GetTotalCost(Func<T, decimal> priceSelector); // Метод для получения общей стоимости
        int CountServiceOrders(Func<T, string> serviceSelector, string serviceName); // Подсчет заказов на услугу


    }
}