using SpSim.Setting;
using SpSim.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpSim.Forms
{
    public partial class GUI : Form
    {
        private Location location;

        public GUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Quits the programm
        /// </summary>
        private void quit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void loadSetting_Click(object sender, EventArgs e)
        {
            string Path = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML-File (*.xml)|*.xml |All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK) { 
                Path = openFileDialog.FileName;
            }

            location = IOHelper.ImportFile(Path, Display);

            /*
            //Import testing block
            location.PrintProtagonist();
            location.PrintRooms();
            location.PrintImplements();*/
        }
    }
}
