using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    /// <summary>
    /// Represents the game's protagonist
    /// </summary>
    public class Protagonist : Actor
    {
        public string Name;
        public Gender Gender;

        public string Lore;
        public long OwnRoom;
        public long CurrentRoom;

        public override string ToString()
        {
            return String.Format("Protagonist: {0} | {1} | {2} | {3}", Name, Gender, Lore, OwnRoom);
        }
    }

    public enum Gender {Male, Female }
}
