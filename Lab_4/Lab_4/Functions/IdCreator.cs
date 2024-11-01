using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Functions
{
    public class IdCreator
    {
        public IdCreator() { }
        public int CreateId (string name) 
        {
            return name.GetHashCode()%1000000;
        }
    }
}
