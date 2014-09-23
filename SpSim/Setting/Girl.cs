using SpSim.Util;
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

        /// <summary>
        /// Used to determine initial clothes
        /// </summary>
        public List<ClothPrefItem> ClothPref = new List<ClothPrefItem>();

        /// <summary>
        /// Returns a description, what she's wearing
        /// </summary>
        public string GetClothesDescription(List<Clothing> clothes)
        {
            string output = Name + " ";
            bool topless = false;
            bool bottomless = false;
            bool barefoot = false;

            long[] visibleClothes = new long[2];
            long[] footwear = new long[2];

            //Examine her clothes
            if (WornClothes[2] != 0)
            {
                visibleClothes[0] = WornClothes[2];
            }
            else
            {
                if (WornClothes[0] != 0)
                {
                    visibleClothes[0] = WornClothes[0];
                }
                else
                {
                    if (WornClothes[3] != 0)
                    {
                        visibleClothes[0] = WornClothes[3];
                    }
                    else { topless = true; }
                }
                if (WornClothes[1] != 0)
                {
                    visibleClothes[1] = WornClothes[1];
                }
                else
                {
                    if (WornClothes[4] != 0)
                    {
                        visibleClothes[1] = WornClothes[4];
                    }
                    else { bottomless = true; }
                }
            }

            //Examine her footwear
            if (WornClothes[5] == 0 && WornClothes[6] == 0) { barefoot = true; }
            else
            {
                footwear[0] = WornClothes[5];
                footwear[1] = WornClothes[6];
            }

            //Build output string
            if (topless && bottomless && barefoot)
            {
                return String.Format("{0} doesn't wear any clothes at all.", Name);
            }
            else
            {
                if (topless || bottomless)
                {
                    output += "only wears ";
                    if (visibleClothes[0] != 0)
                    {
                        output += ActorUtil.GetClothingById(clothes, visibleClothes[0]).GetClothingDescription();
                    }
                    else
                    {
                        output += ActorUtil.GetClothingById(clothes, visibleClothes[1]).GetClothingDescription();
                    }
                }
                else
                {
                    output += String.Format("wears {0}, {1}", ActorUtil.GetClothingById(clothes, visibleClothes[0]).GetClothingDescription(), ActorUtil.GetClothingById(clothes, visibleClothes[1]).GetClothingDescription());
                    if (barefoot)
                    {
                        output += " and is barefoot.";
                        return output;
                    }
                }
                if (barefoot)
                {
                    output += " and is barefoot.";
                    return output;
                }
                else
                {
                    if (footwear[0] != 0 && footwear[1] != 0)
                    {
                        output += String.Format(", {0} and {1}", ActorUtil.GetClothingById(clothes, footwear[0]).GetClothingDescription(), ActorUtil.GetClothingById(clothes, footwear[1]).GetClothingDescription());
                    }
                    else
                    {
                        if (footwear[0] != 0)
                        {
                            output += String.Format(" and {0}.", ActorUtil.GetClothingById(clothes, footwear[0]).GetClothingDescription());
                            return output;
                        }
                        else
                        {
                            output += String.Format(" and {0}.", ActorUtil.GetClothingById(clothes, footwear[0]).GetClothingDescription());
                            return output;
                        }
                    }
                }
            }

            return output;
        }
    }
}
