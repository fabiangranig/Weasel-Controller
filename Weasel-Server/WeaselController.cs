﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Weasel_Controller.NodeMap.MapPanel;
using EasyEncryption;

namespace Weasel_Controller
{
    public partial class WeaselController : Form
    {
        //Public Variables
        private Map _WeaselMap;
        private Weasel[] _Weasels;
        private KukaRoboter _KukaRobot;
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
                new Weasel("MC6", _AppOnline, 0, 39, Color.LightPink),
                new Weasel("AV002", _AppOnline, 1, 48, Color.LightBlue),
                new Weasel("AV015", _AppOnline, 2, 46, Color.LightYellow)
            };
            //Start the Kuka Robot
            _KukaRobot = new KukaRoboter(_AppOnline);
        }

        public WeaselController()
        {
            //Standard VS Studio Forms Intialize
            InitializeComponent();

            //Timer -> uses are stated below this command
            Timer tmr = new Timer();
            tmr.Interval = 50;

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
            for (int i = 0; i < _Weasels.Length; i++) 
            {
                SetBeforeLastPosition(i);
                _WeaselMap.Reserve(_Weasels[i]._LastPosition, _Weasels[i]._Colored);
            }
        }

        //Unreserve nodes when Weasel changes position. Has to be used in an Timer Class
        private void UnreserveNodes()
        {
            for (int i = 0; i < _Weasels.Length; i++)
            {
                if (_Weasels[i]._LastPosition != _Weasels[i]._BeforeLastPosition)
                {
                    _WeaselMap.UnReserve(_Weasels[i]._BeforeLastPosition);
                    SetBeforeLastPosition(i);
                    _WeaselMap.Reserve(_Weasels[i]._LastPosition, _Weasels[i]._Colored);
                }

                Waypoint temp = _WeaselMap.FindWayPoint(_Weasels[i]._LastPosition);
                if(temp._Reserved_Color != _Weasels[i]._Colored)
                {
                    _WeaselMap.Reserve(_Weasels[i]._LastPosition, _Weasels[i]._Colored);
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
            WeaselInformationPanel wp1 = new WeaselInformationPanel(ref _Weasels);
            wp1.Show();
        }

        private void btn_WeaselControlPanel_Click(object sender, EventArgs e)
        {
            WeaselControlPanel WCP = new WeaselControlPanel(ref _WeaselMap, ref _Weasels, ref _KukaRobot);
            WCP.Show();
        }

        private void btn_WeaselManipulator_Click(object sender, EventArgs e)
        {
            WeaselManipulatorPanel WMP = new WeaselManipulatorPanel(ref _Weasels);
            WMP.Show();
        }

        private void btn_WeaselMap_Click(object sender, EventArgs e)
        {
            WeaselMapPanel WMP = new WeaselMapPanel(ref _WeaselMap, ref _Weasels);
            WMP.Show();
        }

        private void btn_ServerMode_Click(object sender, EventArgs e)
        {
            ServerWindow SW = new ServerWindow(ref _Weasels);
            SW.Show();
        }

        private void btn_RoboDKControlPanel_Click(object sender, EventArgs e)
        {
            if(SHA.ComputeSHA256Hash(SelfBuildDialogues.TextDialog("Passwort eingeben: ", "RobotDK öffnen")) == "37004bbd1a4089e6434721f151b4ae561996b160181f514ff2df4b53200b1c05")
            {
                RobotDKControlPanel RDKCP = new RobotDKControlPanel(_KukaRobot);
                RDKCP.Show();
            }
            else
            {
                MessageBox.Show("falsches Passwort!");
            }
        }
    }
}
