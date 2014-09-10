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
        public const string ROOM_SITPLACE = "sitplace";
        public const string ROOM_LIEPLACE = "lieplace";
        public const string ROOM_BENDPLACE = "bendplace";
    }
}
