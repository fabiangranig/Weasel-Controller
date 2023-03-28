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
        //Public Variables
        private Map _WeaselMap;
        private Weasel[] _Weasels;
        private bool _AppOnline;
        private string _InputAddress;

        //@USER INPUT
        //@USER INPUT
        //@USER INPUT
        private void InputtableInformations()
        {
            //Values to set by user
            //if app is online
            _AppOnline = false;
            //where the input files of the map is located
            _InputAddress = @"input.txt";
            //how many and which weasel names
            _Weasels = new[]
            {
                new Weasel("MC6", _AppOnline, 0),
                new Weasel("AV002", _AppOnline, 1),
                new Weasel("AV015", _AppOnline, 2)
            };
        }

        public Form1()
        {
            //Standard VS Studio Forms Intialize
            InitializeComponent();

            //Timer -> uses are stated below this command
            Timer tmr = new Timer();
            tmr.Interval = 100;
            tmr.Tick += Update100ms;
            tmr.Start();
        }

        private void Update100ms(object sender, EventArgs e)
        {
            //Unreserve past nodes with updating the weasels information
            if(_AppOnline == true)
            {
                UpdateWeaselInformation();
            }
            UnreserveNodes();

            //Write map to .txt
            File.WriteAllText(@"map.txt", _WeaselMap.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load the user input
            InputtableInformations();
            
            //Build the map through the .txt
            txtParser txtparse = new txtParser(_InputAddress);
            _WeaselMap = txtparse.ParseToWeaselMap();

            //Intialize for last position and reserve spot on which they are standing on
            for(int i = 0; i < _Weasels.Length; i++) 
            {
                SetBeforeLastPosition(i);
                _WeaselMap.Reserve(_Weasels[i]._LastPosition);
            }
        }

        //Unreserve nodes when Weasel changes position. Has to be used in an Update function
        private void UnreserveNodes()
        {
            for (int i = 0; i < _Weasels.Length; i++)
            {
                if (_Weasels[i]._LastPosition != _Weasels[i]._BeforeLastPosition)
                {
                    _WeaselMap.UnReserve(_Weasels[i]._BeforeLastPosition);
                    SetBeforeLastPosition(i);
                }
            }
        }

        private void UpdateWeaselInformation()
        {
            for(int i = 0; i < _Weasels.Length; i++)
            {
                _Weasels[i].UpdateInfos();
            }
        }

        private void SetBeforeLastPosition(int weaselindex1)
        {
            _Weasels[weaselindex1]._BeforeLastPosition = _Weasels[weaselindex1]._LastPosition;
        }

        private void btn_WeaselPanel_Click(object sender, EventArgs e)
        {
            WeaselPanel wp1 = new WeaselPanel(ref _Weasels);
            wp1.Show();
        }
    }
}
