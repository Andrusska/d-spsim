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
        /// <param name="xml"></param>
        /// <returns></returns>
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

    }
}
