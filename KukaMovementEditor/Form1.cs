using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KukaMovementEditor
{
    public partial class Form1 : Form
    {
        private KukaRoboter _KR;

        public Form1()
        {
            InitializeComponent();
            _KR = new KukaRoboter(false);
        }

        private void IncrementalMoveRadioCheckedChange(object sender, EventArgs e)
        {
            //Get the radio button
            RadioButton radio_sender = (RadioButton)sender;
            
            //If the button is not checked return it
            if (!radio_sender.Checked) { return; }

            //Change the buttons depending on the buttons names
            if (radio_sender.Text == "Reference") { SetIncrementalButtonsCartesian(); }
            if (radio_sender.Text == "Tool") { SetIncrementalButtonsCartesian(); }
            if (radio_sender.Text == "Joint Move") { SetIncrementalButtonsJoints(); }
        }

        private void SetIncrementalButtonsCartesian()
        {
            //Get the movement unit 
            string unit = "Step (mm):";

            //Display text for positive Cartesian movements:
            button_Xplus.Text = "+Tx";
            button_Yplus.Text = "+Ty";
            button_Zplus.Text = "+Tz";
            button_rXplus.Text = "+Rx";
            button_rYplus.Text = "+Ry";
            button_rZplus.Text = "+Rz";

            //Display text for negative Cartesian movements:
            button_Xminus.Text = "-Tx";
            button_Yminus.Text = "-Ty";
            button_Zminus.Text = "-Tz";
            button_rXminus.Text = "-Rx";
            button_rYminus.Text = "-Ry";
            button_rZminus.Text = "-Rz";
        }

        private void SetIncrementalButtonsJoints()
        {
            //Get the movement unit 
            string unit = "Step (deg):";

            //Display text for positive Cartesian movements:
            button_Xplus.Text = "+J1";
            button_Yplus.Text = "+J2";
            button_Zplus.Text = "+J3";
            button_rXplus.Text = "+J4";
            button_rYplus.Text = "+J5";
            button_rZplus.Text = "+J6";

            //Display text for negative Cartesian movements:
            button_Xminus.Text = "-J1";
            button_Yminus.Text = "-J2";
            button_Zminus.Text = "-J3";
            button_rXminus.Text = "-J4";
            button_rYminus.Text = "-J5";
            button_rZminus.Text = "-J6";
        }

        private void IncrementalMovementClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            _KR.Incremental_Move(btn.Text, Int32.Parse(textBox_DistanceStep.Text));
        }

        private void btn_RobotSimulation_Click(object sender, EventArgs e)
        {
            _KR.SwitchSimulationMode();
        }

        private void btn_RobotRealRobot_Click(object sender, EventArgs e)
        {
            _KR.SwitchRealMode();
            _KR.GreiferZu();
            _KR.GreiferAuf();
        }
    }
}
