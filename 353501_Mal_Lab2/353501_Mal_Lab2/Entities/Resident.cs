using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _353501_Mal_Lab2.Entities
{
    public class Resident
    {
        public string? name { get; set; }
        public string? surname { get; set; }

        public List<Tariff> tariffs { get; set; }
        public Resident (string? name, string? surname)
        {
            this.name = name;
            this.surname = surname;
            tariffs = new();
        }

    }
}
