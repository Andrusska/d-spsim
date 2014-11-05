using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpSim.Setting;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace SpSim.Util
{
    public class IOHelper
    {
        private static string affectionWildcard = "1,2,3,4";
        private static string painWildcard = "0,1,2,3,4,5";
        private static string contWildcard = "0,1,2";
        private static string reqposWildcard = "";
        private static string reqbyWildcard = "";

        /// <summary>
        /// Imports the Xml-File and creates the Setting
        /// </summary>
        public static Location ImportFile(string filename, TextBox display)
        {
            XmlDataDocument xml = new XmlDataDocument();

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            xml.Load(fs);

            Location output = new Location();
            output.Display = display;
            output.Protagonist = ImportProtagonist(xml);
            output.Rooms.AddRange(ImportRooms(xml));
            output.Implements.AddRange(ImportImplements(xml));
            output.Clothes.AddRange(ImportClothes(xml));
            output.Holdings.AddRange(ImportHoldings(xml));
            output.Girls.AddRange(ImportGirls(xml));
            output.Pool = ImportMessages(xml);
            output.Prepare();
            return output;
        }

        /// <summary>
        /// Imports the Rooms of the Setting.
        /// </summary>
        private static List<Room> ImportRooms(XmlDocument xml)
        {
            XmlNodeList xmlnode;

            List<Room> output = new List<Room>();
            Room r;
            string[] links;

            xmlnode = xml.GetElementsByTagName(Tags.ROOM);

            foreach (XmlNode xn in xmlnode)
            {
                r = new Room();

                r.Id = Convert.ToInt64(xn[Tags.ROOM_ID].InnerText.Trim());

                r.Name = xn[Tags.ROOM_NAME].InnerText.Trim();
                r.Description = xn[Tags.ROOM_DESCRIPTION].InnerText.Trim();

                links = xn[Tags.ROOM_LINKS].InnerText.Split(',');
                foreach(string s in links)
                {
                    r.Links.Add(Convert.ToInt64(s));
                }

                if (xn[Tags.ROOM_PRE] != null)
                {
                    r.Pre = xn[Tags.ROOM_PRE].InnerText.Trim();
                }
                if (xn[Tags.ROOM_BENDPLACE] != null)
                {
                    r.BendPlace = xn[Tags.ROOM_BENDPLACE].InnerText.Trim();
                }
                if (xn[Tags.ROOM_LIEPLACE] != null)
                {
                    r.LiePlace = xn[Tags.ROOM_LIEPLACE].InnerText.Trim();
                }
                if (xn[Tags.ROOM_SITPLACE] != null)
                {
                    r.SitPlace = xn[Tags.ROOM_SITPLACE].InnerText.Trim();
                }
                if (xn[Tags.ROOM_CLOTHES] != null)
                {
                    string[] types;
                    //If we want all types of clothes
                    if (xn[Tags.ROOM_CLOTHES].InnerText.ToLower() == "all")
                    {
                        //Fill an array with all types
                        types = new string[7];
                        for (int i = 0; i < types.Length; i++)
                        {
                            types[i] = i + 1 + "";
                        }
                    }
                    else {
                        types = xn[Tags.ROOM_CLOTHES].InnerText.Split(',');
                    }
                    
                    foreach(string s in types){
                        r.ScatteredTypes.Add(ActorUtil.GetClothingTypeByInt(Convert.ToInt32(s)));
                    }

                    if (xn[Tags.ROOM_CLOTHES].Attributes[Tags.ROOM_CLOTHES_MAX] != null)
                    {
                        r.ClothCount = Convert.ToInt32(xn[Tags.ROOM_CLOTHES].Attributes[Tags.ROOM_CLOTHES_MAX].Value);
                    }
                    else
                    {
                        r.ClothCount = 5;
                    }
                }
                if (xn[Tags.ROOM_CLOTHES_DUMP] != null)
                {
                    r.ClothingDump = true;
                }

                output.Add(r);
            }

            return output;
        }

        /// <summary>
        /// Imports the protagonist
        /// </summary>
        private static Protagonist ImportProtagonist(XmlDocument xml)
        {
            XmlNodeList xmlnode;
            Protagonist output = new Protagonist();

            xmlnode = xml.GetElementsByTagName(Tags.PROTAGONIST);
            XmlNode readProtagonist = xmlnode[0];

            output.Name = readProtagonist[Tags.PROTAGONIST_NAME].InnerText.Trim();
            output.Lore = readProtagonist[Tags.PROTAGONIST_LORE].InnerText.Trim();

            output.OwnRoom = Convert.ToInt64(readProtagonist[Tags.PROTAGONIST_OWNROOM].InnerText.Trim());
            output.CurrentRoom = Convert.ToInt64(readProtagonist[Tags.PROTAGONIST_OWNROOM].InnerText.Trim());
            if (readProtagonist[Tags.PROTAGONIST_GENDER].InnerText.Trim() == "male")
            {
                output.Gender = Gender.Male;
            }
            else
            {
                output.Gender = Gender.Female;
            }

            return output;
        }

        /// <summary>
        /// Imports the implements
        /// </summary>
        private static List<Implement> ImportImplements(XmlDocument xml)
        {
            XmlNodeList xmlnode;

            List<Implement> output = new List<Implement>();
            Implement impl;
            string[] sfx;

            xmlnode = xml.GetElementsByTagName(Tags.IMPLEMENT);

            foreach (XmlNode xn in xmlnode)
            {
                impl = new Implement();

                impl.Id = Convert.ToInt64(xn[Tags.IMPLEMENT_ID].InnerText.Trim());

                impl.Name = xn[Tags.IMPLEMENT_NAME].InnerText.Trim();

                impl.Strength = Convert.ToInt32(xn[Tags.IMPLEMENT_STRENGTH].InnerText.Trim());

                sfx = xn[Tags.IMPLEMENT_SFX].InnerText.Split(',');
                foreach (string s in sfx)
                {
                    impl.SFX.Add(s);
                }

                output.Add(impl);
            }

            return output;
        }

        /// <summary>
        /// Imports the clothes
        /// </summary>
        private static List<Clothing> ImportClothes(XmlDocument xml)
        {
            List<Clothing> output = new List<Clothing>();
            Clothing cloth;
            XmlNodeList xmlnode;

            xmlnode = xml.GetElementsByTagName(Tags.CLOTHING);

            foreach (XmlNode xn in xmlnode)
            {
                cloth = new Clothing();
    
                cloth.Id = Convert.ToInt64(xn[Tags.CLOTHING_ID].InnerText.Trim());
    
                cloth.Name = StringHelper.UnbreakLines(xn[Tags.CLOTHING_NAME].InnerText.Trim());
                cloth.Description = StringHelper.UnbreakLines(xn[Tags.CLOTHING_DESCRIPTION].InnerText.Trim());
    
                cloth.Type = ActorUtil.GetClothingTypeByInt(Convert.ToInt32(xn[Tags.CLOTHING_TYPE].InnerText));
                cloth.UndressT = ActorUtil.GetUndressTypeByInput(xn[Tags.CLOTHING_UNDRESSTYPE].InnerText);
    
                if (xn[Tags.CLOTHING_RESISTANCE] != null)
                {
                    cloth.Resistance = Convert.ToInt32(xn[Tags.CLOTHING_RESISTANCE].InnerText);
                }
    
                if (xn[Tags.CLOTHING_ARTICLE].InnerText == "true")
                {
                    cloth.Article = true;
                }
                else
                {
                    cloth.Article = false;
                }
    
                
                    output.Add(cloth);
            }

            return output;
        }

        /// <summary>
        /// Imports the clothes
        /// </summary>
        private static List<Holding> ImportHoldings(XmlDocument xml)
        {
            List<Holding> output = new List<Holding>();
            Holding hold;
            XmlNodeList xmlnode;

            xmlnode = xml.GetElementsByTagName(Tags.HOLDING);

            foreach (XmlNode xn in xmlnode)
            {
                hold = new Holding();

                hold.Id = Convert.ToInt64(xn[Tags.HOLDING_ID].InnerText.Trim());

                hold.Name = xn[Tags.HOLDING_NAME].InnerText.Trim();

                if (xn[Tags.HOLDING_REQUIREDCLOTHING] != null)
                {
                    hold.RequieredClothing.AddRange(xn[Tags.HOLDING_REQUIREDCLOTHING].InnerText.Split(','));
                }

                output.Add(hold);
                reqbyWildcard += hold.Id + ", ";
            }

            reqbyWildcard = reqbyWildcard.Substring(0, reqbyWildcard.Length - 2);

            return output;
        }

        /// <summary>
        /// Imports the girls
        /// </summary>
        private static List<Girl> ImportGirls(XmlDocument xml)
        {
            List<Girl> output = new List<Girl>();
            Girl girl;
            XmlNodeList xmlnode;
            XmlNodeList calls;
            XmlNodeList clothPrefItems;

            string[] clothVlaues;
            ClothPrefItem item;

            xmlnode = xml.GetElementsByTagName(Tags.GIRL);

            foreach (XmlNode xn in xmlnode)
            {
                girl = new Girl();

                girl.Id = Convert.ToInt64(xn[Tags.GIRL_ID].InnerText);

                girl.Name = xn[Tags.GIRL_NAME].InnerText;
                girl.Description = StringHelper.UnbreakLines(xn[Tags.GIRL_DESCRIPTION].InnerText);
                girl.Lore = StringHelper.UnbreakLines(xn[Tags.GIRL_LORE].InnerText);

                girl.Resistance = Convert.ToInt32(xn[Tags.GIRL_RESISTANCE].InnerText);
                girl.Affection = Convert.ToInt32(xn[Tags.GIRL_AFFECTION].InnerText);
                girl.Contritement = Girl.GetContritementByAffection(girl.Affection);
                girl.OwnRoom = Convert.ToInt64(xn[Tags.GIRL_OWN_ROOM].InnerText);
                girl.CurrentRoom = girl.OwnRoom;

                if (xn[Tags.GIRL_SPANKABLE] != null)
                {
                    if (xn[Tags.GIRL_SPANKABLE].InnerText.ToLower() == "true")
                    {
                        girl.Spankable = true;
                    }
                    else
                    {
                        girl.Spankable = false;
                    }
                }

                calls = xml.GetElementsByTagName(Tags.GIRL_CALL);
                foreach (XmlNode call in calls)
                {
                    if (Convert.ToInt64(call.Attributes["id"].Value) == girl.Id)
                    {
                        girl.Call = call.InnerText;
                        break;
                    }
                }

                clothPrefItems = xml.GetElementsByTagName(Tags.GIRL_TOPCLOTH);
                foreach (XmlNode cloth in clothPrefItems)
                {
                    if (Convert.ToInt64(cloth.Attributes["id"].Value) == girl.Id)
                    {
                        item = new ClothPrefItem();
                        clothVlaues = cloth.InnerText.Split(',');

                        item.Type = ClothingType.TOP;
                        item.Id = Convert.ToInt64(clothVlaues[0]);
                        item.Chance = Convert.ToInt32(clothVlaues[1]);

                        girl.ClothPref.Add(item);
                    }
                }

                clothPrefItems = xml.GetElementsByTagName(Tags.GIRL_BOTCLOTH);
                foreach (XmlNode cloth in clothPrefItems)
                {
                    if (Convert.ToInt64(cloth.Attributes["id"].Value) == girl.Id)
                    {
                        item = new ClothPrefItem();
                        clothVlaues = cloth.InnerText.Split(',');

                        item.Type = ClothingType.BOTTOM;
                        item.Id = Convert.ToInt64(clothVlaues[0]);
                        item.Chance = Convert.ToInt32(clothVlaues[1]);

                        girl.ClothPref.Add(item);
                    }
                }

                clothPrefItems = xml.GetElementsByTagName(Tags.GIRL_ONECLOTH);
                foreach (XmlNode cloth in clothPrefItems)
                {
                    if (Convert.ToInt64(cloth.Attributes["id"].Value) == girl.Id)
                    {
                        item = new ClothPrefItem();
                        clothVlaues = cloth.InnerText.Split(',');

                        item.Type = ClothingType.ONEPIECE;
                        item.Id = Convert.ToInt64(clothVlaues[0]);
                        item.Chance = Convert.ToInt32(clothVlaues[1]);

                        girl.ClothPref.Add(item);
                    }
                }

                clothPrefItems = xml.GetElementsByTagName(Tags.GIRL_BRACLOTH);
                foreach (XmlNode cloth in clothPrefItems)
                {
                    if (Convert.ToInt64(cloth.Attributes["id"].Value) == girl.Id)
                    {
                        item = new ClothPrefItem();
                        clothVlaues = cloth.InnerText.Split(',');

                        item.Type = ClothingType.BRA;
                        item.Id = Convert.ToInt64(clothVlaues[0]);
                        item.Chance = Convert.ToInt32(clothVlaues[1]);

                        girl.ClothPref.Add(item);
                    }
                }

                clothPrefItems = xml.GetElementsByTagName(Tags.GIRL_UNDCLOTH);
                foreach (XmlNode cloth in clothPrefItems)
                {
                    if (Convert.ToInt64(cloth.Attributes["id"].Value) == girl.Id)
                    {
                        item = new ClothPrefItem();
                        clothVlaues = cloth.InnerText.Split(',');

                        item.Type = ClothingType.PANTIES;
                        item.Id = Convert.ToInt64(clothVlaues[0]);
                        item.Chance = Convert.ToInt32(clothVlaues[1]);

                        girl.ClothPref.Add(item);
                    }
                }

                clothPrefItems = xml.GetElementsByTagName(Tags.GIRL_SOCCLOTH);
                foreach (XmlNode cloth in clothPrefItems)
                {
                    if (Convert.ToInt64(cloth.Attributes["id"].Value) == girl.Id)
                    {
                        item = new ClothPrefItem();
                        clothVlaues = cloth.InnerText.Split(',');

                        item.Type = ClothingType.SOCKS;
                        item.Id = Convert.ToInt64(clothVlaues[0]);
                        item.Chance = Convert.ToInt32(clothVlaues[1]);

                        girl.ClothPref.Add(item);
                    }
                }

                clothPrefItems = xml.GetElementsByTagName(Tags.GIRL_SHOCLOTH);
                foreach (XmlNode cloth in clothPrefItems)
                {
                    if (Convert.ToInt64(cloth.Attributes["id"].Value) == girl.Id)
                    {
                        item = new ClothPrefItem();
                        clothVlaues = cloth.InnerText.Split(',');

                        item.Type = ClothingType.SHOES;
                        item.Id = Convert.ToInt64(clothVlaues[0]);
                        item.Chance = Convert.ToInt32(clothVlaues[1]);

                        girl.ClothPref.Add(item);
                    }
                }


                output.Add(girl);
            }

            return output;
        }

        #region MessageImport

        /// <summary>
        /// Imports all the Messages
        /// </summary>
        private static MessagePool ImportMessages(XmlDocument xml)
        {
            MessagePool pool = new MessagePool();

            ImportMessageType(xml, pool, MessageType.LOOK_AT_GIRL, Tags.MESSAGE_LOOK_AT);
            ImportMessageType(xml, pool, MessageType.PAIN_DOWN, Tags.MESSAGE_PAIN_DOWN);

            ImportMessageType(xml, pool, MessageType.PICKUP_IMPL, Tags.MESSAGE_PICKUP_IMPLEMENT);
            ImportMessageType(xml, pool, MessageType.ENTER_IMPL, Tags.MESSAGE_ENTER_WITH_IMPLEMENT);
            ImportMessageType(xml, pool, MessageType.SWAP_IMPL, Tags.MESSAGE_SWAP_IMPLEMENT);
            ImportMessageType(xml, pool, MessageType.SWAP_WORSE_IMPL, Tags.MESSAGE_SWAP_WORSE);
            ImportMessageType(xml, pool, MessageType.DROP_IMPL, Tags.MESSAGE_DROP_IMPLEMENT);

            ImportMessageType(xml, pool, MessageType.HOLDING_ANNOUNCE, Tags.MESSAGE_HOLDING_ANNOUNCE);
            ImportMessageType(xml, pool, MessageType.HOLDING_REACT_ANNOUNCE, Tags.MESSAGE_HOLDING_ANNOUNCE_REACT);
            ImportMessageType(xml, pool, MessageType.HOLDING_WATCH_ANNOUNCE, Tags.MESSAGE_HOLDING_ANNOUNCE_WATCH);
            ImportMessageType(xml, pool, MessageType.HOLDING_START, Tags.MESSAGE_HOLDING_START);
            ImportMessageType(xml, pool, MessageType.HOLDING_START_REACT, Tags.MESSAGE_HOLDING_START_REACT);
            ImportMessageType(xml, pool, MessageType.HOLDING_START_WATCH, Tags.MESSAGE_HOLDING_START_WATCH);
            ImportMessageType(xml, pool, MessageType.DRAG, Tags.MESSAGE_HOLDING_DRAG);
            ImportMessageType(xml, pool, MessageType.DRAG_REACT, Tags.MESSAGE_HOLDING_DRAG_REACT);
            ImportMessageType(xml, pool, MessageType.DRAG_WATCH, Tags.MESSAGE_HOLDING_DRAG_WATCH);
            ImportMessageType(xml, pool, MessageType.HOLDING_STOP, Tags.MESSAGE_HOLDING_STOP);
            ImportMessageType(xml, pool, MessageType.HOLDING_STOP_REACT, Tags.MESSAGE_HOLDING_STOP_REACT);
            ImportMessageType(xml, pool, MessageType.HOLDING_STOP_WITHOUT_DRAG, Tags.MESSAGE_HOLDING_STOP_WITHOUTDRAG);

            return pool;
        }

        private static void ImportMessageType(XmlDocument xml, MessagePool pool, MessageType type, string nodeName){
            XmlNodeList xmlnode;
            SpSim.Setting.Message message;

            List<SpSim.Setting.Message> output = new List<Setting.Message>();
            bool[] filter = pool.GetFilterByMessageType(type);

            xmlnode = xml.GetElementsByTagName(nodeName);

            foreach (XmlNode xn in xmlnode)
            {
                message = new SpSim.Setting.Message(xn.InnerText, type);

                //Affection requirement
                if (filter[0])
                {
                    if (xn.Attributes[Tags.MESSAGE_FILTER_LIKE] != null)
                    {
                        message.Params.Add(xn.Attributes[Tags.MESSAGE_FILTER_LIKE].Value);
                    }
                    else
                    {
                        message.Params.Add(affectionWildcard);
                    }
                }
                
                //Pain Requirement
                if (filter[1])
                {
                    if (xn.Attributes[Tags.MESSAGE_FILTER_PAIN] != null)
                    {
                        message.Params.Add(xn.Attributes[Tags.MESSAGE_FILTER_PAIN].Value);
                    }
                    else
                    {
                        message.Params.Add(painWildcard);
                    }
                }
                

                //Contritement Requirement
                if (filter[2])
                {
                    if (xn.Attributes[Tags.MESSAGE_FILTER_CONTRITEMENT] != null)
                    {
                        message.Params.Add(xn.Attributes[Tags.MESSAGE_FILTER_CONTRITEMENT].Value);
                    }
                    else
                    {
                        message.Params.Add(contWildcard);
                    }
                }

                //Position Requirement
                if (filter[3])
                {
                    if (xn.Attributes[Tags.MESSAGE_FILTER_POSITION] != null)
                    {
                        message.Params.Add(xn.Attributes[Tags.MESSAGE_FILTER_POSITION].Value);
                    }
                    else
                    {
                        message.Params.Add(reqposWildcard);
                    }
                }

                //Holding Requirement
                if (filter[4])
                {
                    if (xn.Attributes[Tags.MESSAGE_FILTER_HOLDING] != null)
                    {
                        message.Params.Add(xn.Attributes[Tags.MESSAGE_FILTER_HOLDING].Value + "");
                    }
                    else
                    {
                        message.Params.Add(reqbyWildcard);
                    }
                }

                //Id Requirement
                if (xn.Attributes[Tags.MESSAGE_FILTER_ID] != null)
                {
                    message.Params.Add(xn.Attributes[Tags.MESSAGE_FILTER_ID].Value);
                }
                else
                {
                    message.Params.Add(0);
                }

                output.Add(message);
            }

            pool.AddMessages(output);
        }

        #endregion
    }
}
