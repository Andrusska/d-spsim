using SpSim.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpSim.Setting
{
    /// <summary>
    /// The general location, our little game takes place in
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Reference at the linearray of the displaybox to add some text
        /// </summary>
        public TextBox Display;

        /// <summary>
        /// List of all rooms
        /// </summary>
        public List<Room> Rooms = new List<Room>();

        /// <summary>
        /// The protagonist
        /// </summary>
        public Protagonist Protagonist = new Protagonist();

        #region Testingmethods

        /// <summary>
        /// Prints all the Rooms of the Location (for Testing)
        /// </summary>
        public void PrintRooms()
        {
            foreach (Room r in Rooms)
            {
                Display.AppendText(Environment.NewLine + StringHelper.UnbreakLines(r.ToString()) + Environment.NewLine);
            }
        }

        /// <summary>
        /// Prints information about the protagonist (for Testing)
        /// </summary>
        public void PrintProtagonist()
        {
            Display.AppendText(Environment.NewLine + StringHelper.UnbreakLines(Protagonist.ToString()) + Environment.NewLine);
        }

        #endregion
    }
    
}
