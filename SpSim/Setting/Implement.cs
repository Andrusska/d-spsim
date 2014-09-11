using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    public class Implement : Actor
    {
        public int Strength = 1;
        public List<string> SFX = new List<string>();

        public Implement() : base()
        {
            this.Name = "Implement";
        }

        public override string ToString()
        {
            return String.Format("Implement: {0} | {1} | {2} | {3} | {4}", Id, Name, Strength, String.Join(",", SFX.ToArray()), CurrentRoom);
        }
    }
}
