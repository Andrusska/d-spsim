using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    /// <summary>
    /// Baseclass for all acting "Objects" in the setting.
    /// Basically everything that can be assigned to a room is ab actor
    /// e.g. Implements, the girls etc.
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// The actors id. Because it references this Actor
        /// it should be unique.
        /// </summary>
        public long Id;

        /// <summary>
        /// The actor's name
        /// </summary>
        public string Name;

        /// <summary>
        /// The id of the containing room
        /// </summary>
        public long CurrentRoom;

        /// <summary>
        /// returns if the Actor 
        /// </summary>
        public bool isInRoom(long room)
        {
            return room == CurrentRoom;
        }
    }
}
