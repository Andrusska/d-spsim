using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Util
{
    /// <summary>
    /// Collection of Tags used in the game's .xml file
    /// </summary>
    public class Tags
    {
        //Clothes
        public const string CLOTHING = "clothing";
        public const string CLOTHING_ID = "id";
        public const string CLOTHING_NAME = "name";
        public const string CLOTHING_DESCRIPTION = "description";
        public const string CLOTHING_TYPE = "type";
        public const string CLOTHING_UNDRESSTYPE = "und";
        public const string CLOTHING_RESISTANCE = "resistance";
        public const string CLOTHING_ARTICLE = "reqart";

        //Girls
        public const string GIRL = "girl";
        public const string GIRL_ID = "id";
        public const string GIRL_NAME = "name";
        public const string GIRL_DESCRIPTION = "description";
        public const string GIRL_LORE = "lore";
        public const string GIRL_RESISTANCE = "resistance";
        public const string GIRL_AFFECTION = "affection";
        public const string GIRL_SPANKABLE = "spankable";
        public const string GIRL_OWN_ROOM = "own_room";
        public const string GIRL_TOPCLOTH = "topcloth";
        public const string GIRL_BOTCLOTH = "botcloth";
        public const string GIRL_ONECLOTH = "onecloth";
        public const string GIRL_BRACLOTH = "bracloth";
        public const string GIRL_UNDCLOTH = "undcloth";
        public const string GIRL_SOCCLOTH = "soccloth";
        public const string GIRL_SHOCLOTH = "shocloth";


        //Implements
        public const string IMPLEMENT = "implement";
        public const string IMPLEMENT_NAME = "name";
        public const string IMPLEMENT_ID = "id";
        public const string IMPLEMENT_STRENGTH = "strength";
        public const string IMPLEMENT_SFX = "sfx";

        //Protagonist
        public const string PROTAGONIST = "protagonist";
        public const string PROTAGONIST_NAME = "name";
        public const string PROTAGONIST_GENDER = "gender";
        public const string PROTAGONIST_LORE = "lore";
        public const string PROTAGONIST_OWNROOM = "own_room";
        public const string PROTAGONIST_TEXTCOLOR = "textcolor";

        //Rooms
        public const string ROOM = "room";
        public const string ROOM_ID = "id";
        public const string ROOM_NAME = "name";
        public const string ROOM_DESCRIPTION = "description";
        public const string ROOM_LINKS = "links";
        public const string ROOM_PRE = "pre";
        public const string ROOM_SITPLACE = "sitplace";
        public const string ROOM_LIEPLACE = "lieplace";
        public const string ROOM_BENDPLACE = "bendplace";
        public const string ROOM_CLOTHES = "scatteredClothes";
        public const string ROOM_CLOTHES_MAX = "max";
    }
}
