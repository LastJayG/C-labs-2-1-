using System;
using System.Collections.Generic;

namespace Lab_4.Entities
{
    public class MyCustomComparer<T> : IComparer<T> where T : Passenger
    {
        public int Compare(T x, T y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return string.Compare(x.name, y.name, StringComparison.Ordinal);
        }
    }
}