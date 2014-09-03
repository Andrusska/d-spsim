using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    public class Room
    {
        public long Id;

        public string Name = "";
        public string Desc = "";

        public List<long> Links = new List<long>();

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3}", Id, Name, Desc, Links);
        }
    }
}
