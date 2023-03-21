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
        private C_Map weasel_map = new C_Map();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Get the map from the .txt input
            txtParser txtparse1 = new txtParser(@"input.txt");

            //Build the map from the parser
            weasel_map = txtparse1.ParseToWeaselMap();

            //Testcase
            Weasel w1 = new Weasel("MC6", false);
            string path = weasel_map.FreePath(w1._LastPosition, 41);
            Console.WriteLine("Path: " + path);
            w1.MoveThroughCordinates(path);
        }
    }
}
