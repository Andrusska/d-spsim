using SpSim.Setting;
using SpSim.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Location location = IOHelper.ImportFile(@"Resources/Rooms.xml");
            location.PrintRooms();

            //Waiting to terminate
            Console.ReadKey();
        }
    }
}
