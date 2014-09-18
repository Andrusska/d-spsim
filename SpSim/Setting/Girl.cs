using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    /// <summary>
    /// Because there was no demand
    /// for male "victims", I' ll just add girls for now
    /// </summary>
    public class Girl : Actor
    {
        /// <summary>
        /// How does she look?
        /// </summary>
        public string Description = "";

        /// <summary>
        /// What's her background-story?
        /// </summary>
        public string Lore = "";

        /// <summary>
        /// How hard is her bottom
        /// </summary>
        public int Resistance = 3;

        /// <summary>
        /// Hows your Realationship?
        /// </summary>
        public int Affection = 2;

        /// <summary>
        /// Can you spank her?
        /// </summary>
        public bool Spankable = true;

        /// <summary>
        /// Her own Room
        /// </summary>
        public long OwnRoom;

        /// <summary>
        /// What does she wear?
        /// </summary>
        public long[] WornClothes = new long[7];

    }
}
