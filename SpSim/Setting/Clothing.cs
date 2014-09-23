using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    /// <summary>
    /// Clothes for our girls/victims
    /// </summary>
    public class Clothing : Actor
    {
        /// <summary>
        /// How it's described
        /// </summary>
        public string Description = "a piece of cloth";

        /// <summary>
        /// What kind of clothing is it?
        /// </summary>
        public ClothingType Type = ClothingType.SHOES;

        /// <summary>
        /// Does it "tank" some hits?
        /// Only interessting for things, covering her behind
        /// </summary>
        public int Resistance = 1;

        /// <summary>
        /// Does it new a special article?
        /// e.g :  "she's wearing shorts" or "she's wearing a skirt"
        /// </summary>
        public bool Article = false;

        /// <summary>
        /// How is this clothing removed?
        /// </summary>
        public UndressType UndressT;

        public string GetInventroyDescription()
        {
            if (Article) 
            {
                return String.Format("a {0}", Description);
            }

            return String.Format("some {0}", Description);
        }

        public override string ToString()
        {
            return String.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", Id, Name, Description, Type, Resistance, Article, UndressT, CurrentRoom);
        }

        public string GetClothingDescription()
        {
            if (Article)
            {
                return String.Format("a {0}", Description);
            }
            else
            {
                return String.Format("some {0}", Description);
            }
        }

        public static string GetReadableTypeName(ClothingType type)
        {
            switch (type)
            {
                case ClothingType.BOTTOM:
                    return "bottom part";
                case ClothingType.BRA:
                    return "bra";
                case ClothingType.ONEPIECE:
                    return "one piece";
                case ClothingType.PANTIES:
                    return "panties";
                case ClothingType.SHOES:
                    return "shoes";
                case ClothingType.SOCKS:
                    return "socks";
                case ClothingType.TOP:
                    return "top";
                default: break;
            }

            return "Type";
        }
    }

    /// <summary>
    /// Determines what this piece of cloth is covering
    /// </summary>
    public enum ClothingType
    {
        TOP = 1, BOTTOM, ONEPIECE, BRA, PANTIES, SOCKS, SHOES
    }

    /// <summary>
    /// Determines how the clothing is removed from it's owner
    /// </summary>
    public enum UndressType
    {
        UP, DOWN, OFF
    }
}
