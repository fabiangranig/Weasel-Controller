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
            //Parsing the .txt input
            txtParser parser = new txtParser("input.txt");

            for(int i = 0; i < parser._good_blob.Count; i++)
            {
                if(parser._good_blob[i] == "---")
                {
                    for(int u = i + 1; u < parser._good_blob.Count; u++)
                    {
                        if(parser._good_blob[u] == "---")
                        {
                            break;
                        }

                        string[] split2 = parser._good_blob[u].Split('-');
                        weasel_map.ConnectTwoPoints(Int32.Parse(split2[0]), Int32.Parse(split2[1]));
                    }
                    break;
                }

                string[] split = parser._good_blob[i].Split('-');

                if(i == 0)
                {
                    weasel_map.AddNodeToNumber(new C_Waypoint(Int32.Parse(split[0])), Int32.Parse(split[1]));
                }
                else
                {
                    weasel_map.AddNodeToNumber(new C_Waypoint(Int32.Parse(split[1])), Int32.Parse(split[0]));
                }
            }
            Console.WriteLine();
        }
    }
}
