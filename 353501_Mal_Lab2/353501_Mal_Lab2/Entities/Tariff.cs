using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _353501_Mal_Lab2.Entities
{
    public class Tariff
    {
        public string Service { get; }
        public decimal Price { get; }

        public Tariff(string service, decimal price)
        {
            Service = service;
            Price = price;
        }
       
    }
}
