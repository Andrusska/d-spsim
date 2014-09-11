using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    /// <summary>
    /// Represents a room in your house
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Should be unique
        /// </summary>
        public long Id;

        /// <summary>
        /// The readable Name that's displayed during the game
        /// </summary>
        public string Name = "Room";
        /// <summary>
        /// This is displayed during the "Look around action"
        /// </summary>
        public string Description = "";

        /// <summary>
        /// Where can you go from here?
        /// </summary>
        public List<long> Links = new List<long>();

        /// <summary>
        /// An optional attribute to correct some language-mistakes
        /// e.g. "I'm in the roof" with <pre>at</pre> would change to -> "I'm at the roof"
        /// </summary>
        public string Pre = "in";

        /*
         *  Positionplaces during the spanking scenes with some defaults 
         */
        public string SitPlace = "the floor";
        public string LiePlace = "the floor";
        public string BendPlace = "";

        public override string ToString()
        {
            return String.Format("Room: {0} | {1} | {2} | {3} | {4} | {5} | {6}", Id, Name, Description, String.Join(",", Links.ToArray()), BendPlace, LiePlace, SitPlace);
        }

        public string GetStatus()
        {
            return Pre + " " + Name;
        }
    }
}
