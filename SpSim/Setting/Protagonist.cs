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

        /// <summary>
        /// The protagonists backstory
        /// </summary>
        public string Lore;
        /// <summary>
        /// The protagonists room
        /// </summary>
        public long OwnRoom;

        /// <summary>
        /// The Implement locked to the protagonist
        /// Normally it´s a hand
        /// </summary>
        public long LockedImplement = -1;
        /// <summary>
        /// Implement that can be carried around and picked up
        /// </summary>
        public long VariableImplement = -1;

        /// <summary>
        /// The girl, the protagonist is holding
        /// </summary>
        public long HoldingGirl = 0;

        /// <summary>
        /// Determines if the girl has been dragged around
        /// </summary>
        public bool Dragged = false;

        /// <summary>
        /// Determines, how the girl is held
        /// </summary>
        public long HoldingId = 0;

        public override string ToString()
        {
            return String.Format("Protagonist: {0} | {1} | {2} | {3}", Name, Gender, Lore, OwnRoom);
        }
    }

    public enum Gender {Male, Female }
}
