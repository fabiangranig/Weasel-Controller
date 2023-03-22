using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Weasel_Controller
{
    public partial class Form1 : Form
    {
        private C_Map weasel_map;
        private Weasel[] weasels;
        private int AppOnline;
        private string _input_adress;
        private int[] weasel_before_last_position;

        public Form1()
        {
            InitializeComponent();

            Timer tmr = new Timer();
            tmr.Interval = 100;
            tmr.Tick += fastUpdate;
            tmr.Start();

            Timer tmr2 = new Timer();
            tmr2.Interval = 1000;
            tmr2.Tick += slowUpdate;
            tmr2.Start();
        }

        private void fastUpdate(object sender, EventArgs e)
        {
            //Unreserve past nodes
            for(int i = 0; i < weasels.Length; i++)
            {
                weasels[i].UpdateInfos();
                if (weasels[i]._LastPosition != weasel_before_last_position[i])
                {
                    weasel_map.UnReserve(weasel_before_last_position[i]);
                    weasel_before_last_position[i] = weasels[i]._LastPosition;
                }
            }
        }

        public void slowUpdate(object sender, EventArgs e)
        {
            List<string> MapInList = weasel_map.ShowMap();
            string MapInString = "";
            for(int i = 0; i < MapInList.Count; i++)
            {
                MapInString += MapInList[i] + "\n";
            }
            File.WriteAllText(@"map.txt", MapInString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //User Input
            //User Input
            //User Input
            AppOnline = 0;
            _input_adress = @"input.txt";

            //Get the map from the .txt input
            txtParser txtparse1 = new txtParser(_input_adress);

            //Build the map from the parser
            weasel_map = txtparse1.ParseToWeaselMap();

            //Add weasels
            weasels = new[]
            {
                new Weasel("MC6", AppOnline, 0),
                new Weasel("AV002", AppOnline, 1),
                new Weasel("AV015", AppOnline, 2)
            };

            //Intialize for last position
            weasel_before_last_position = new Int32[weasels.Length];
            for(int i = 0; i < weasel_before_last_position.Length; i++)
            {
                weasel_before_last_position[i] = weasels[i]._LastPosition;
            }

            //Reserve spots on which the weasels are standing on
            for(int i = 0; i < weasels.Length; i++)
            {
                weasel_map.Reserve(weasels[i]._LastPosition);
            }
        }

        private void btn_Debug_Click(object sender, EventArgs e)
        {
            int weasel = Int32.Parse(txtBox_Weasel.Text);
            int[] path = weasel_map.FreePath(weasels[weasel]._LastPosition, Int32.Parse(txtBox_Debug.Text));

            if (!(path[0] == -1))
            {
                weasel_map.ReserveArr(path);
                weasels[weasel].MoveThroughCordinates(path);
            }
            if (path[0] == -1)
            {
                Console.WriteLine(weasel + ": Weg blockiert!");
            }
        }
    }
}
