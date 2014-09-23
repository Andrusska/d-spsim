using SpSim.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Util
{
    public class ClothPrefItem
    {
        /// <summary>
        /// What kind of clothing is this?
        /// </summary>
        public ClothingType Type;

        /// <summary>
        /// The Clothings's Id
        /// </summary>
        public long Id;

        /// <summary>
        /// Chance of beeing worn * 10% 
        /// </summary>
        public int Chance;

        public static List<ClothPrefItem> GetItemsByType(List<ClothPrefItem> items, ClothingType type)
        {
            List<ClothPrefItem> output = new List<ClothPrefItem>();

            foreach (ClothPrefItem item in items)
            {
                if (item.Type == type)
                {
                    output.Add(item);
                }
            }

            return output;
        }
    }
}
