using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    public class Location
    {
        public List<Room> Rooms = new List<Room>();

        /// <summary>
        /// Prints all the Rooms of the Location (for Testing)
        /// </summary>
        public void PrintRooms()
        {
            foreach (Room r in Rooms)
            {
                Console.WriteLine(r);
            }
        }
    }
    
}
