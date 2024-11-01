using Lab_4.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab_4.Entities
{
    public class StationSystem
    {

        public Dictionary<Passenger, int> passengerList;
        private IdCreator _idCreator;
        public StationSystem() {
            passengerList = new();
            _idCreator = new IdCreator();
        }

        public void AddPassenger(Passenger passenger)
        {
            passengerList.Add(passenger, _idCreator.CreateId(passenger.name));
        }
        public int GetPassengerId(Passenger passenger)
        {
            if (passengerList.ContainsKey(passenger)) { 
                return passengerList[passenger]; 
            }
            else { return 0; }
        }
        public void RemovePassenger(Passenger passenger)
        {
            if (passengerList.ContainsKey(passenger))
            {
                int id = GetPassengerId(passenger);
                passengerList.Remove(passenger, out id);
            };
          
        }

    }
}
