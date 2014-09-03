using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpSim.Setting;
using System.Xml;

namespace SpSim.Util
{
    public class IOHelper
    {
        /// <summary>
        /// Imports the Xml-File and creates the Setting
        /// </summary>
        public static Location ImportFile(string filename)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(filename);

            Location output = new Location();
            output.Rooms.AddRange(ImportRooms(xml));

            return output;
        }

        /// <summary>
        /// Imports the Rooms of the Setting.
        /// </summary>
        private static List<Room> ImportRooms(XmlDocument xml)
        {
            List<Room> output = new List<Room>();
            Room r;
            string[] links;

            //Selects all Roomnodes in Rooms
            XmlNodeList xnList = xml.SelectNodes("/Setting/Rooms/Room");
            foreach (XmlNode xn in xnList)
            {
                r = new Room();

                r.Id = Convert.ToInt64(xn["Id"].InnerText);

                r.Name = xn["Name"].InnerText;
                r.Desc = xn["Desc"].InnerText;

                links = xn["Links"].InnerText.Split(',');
                foreach(string s in links)
                {
                    r.Links.Add(Convert.ToInt64(s));
                }

                output.Add(r);
            }

            return output;
        }
    }
}
