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
        public Gender Gender;

        public string Lore;
        public long OwnRoom;
        public long CurrentRoom;

        public long LockedImplement = -1;
        public long VariableImplement = -1;

        public override string ToString()
        {
            return String.Format("Protagonist: {0} | {1} | {2} | {3}", Name, Gender, Lore, OwnRoom);
        }
    }

    public enum Gender {Male, Female }
}
