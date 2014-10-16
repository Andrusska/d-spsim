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
            Display.Clear();


            OpenFileDialog openFileDialog = new OpenFileDialog();
            /*openFileDialog.Filter = "XML-File (*.xml)|*.xml |All files (*.*)|*.*";*/
            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK) { 
                Path = openFileDialog.FileName;

                location = IOHelper.ImportFile(Path, Display);

                /*
                //Import testing block
                location.PrintProtagonist();
                location.PrintRooms();
                location.PrintImplements();
                location.PrintClothes();*/
            }

            Input.Focus();

        }

        /// <summary>
        /// Handels the players input
        /// </summary>
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            //Enter starts the Evaluation
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    location.HandleSelection(Convert.ToInt32(Input.Text)); 
                }
                catch (Exception ex)
                {
                    Display.AppendText(String.Format(
                        "{0}An error has okued :V{0}Either your input was invalid or something is f*cked up.{0} If it seems to be the last case, please complain (and ideally post a screenshot with the stactrace) in 4chan/d's spanking thread and/or 8chan/spank's game thread and I`ll try my best to fix it.{0}{1}{0}{2}{0}{3}{0}"
                        , Environment.NewLine, ex.Message, ex.TargetSite, ex.StackTrace));
                    location.PrintDefaultStatus();
                    location.EvaluateDefaultActions();
                    location.PrintAvailableActions();
                }

                Input.Text = "";
            }
        }


    }
}
