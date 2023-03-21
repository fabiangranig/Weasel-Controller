using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weasel_Controller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            C_Map weasel_map = new C_Map();
            weasel_map.AddNodeToNumber(new C_Waypoint(10), -1);
            weasel_map.AddNodeToNumber(new C_Waypoint(20), 10);
            weasel_map.AddNodeToNumber(new C_Waypoint(30), 10);
        }
    }
}
