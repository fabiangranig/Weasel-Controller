using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Timers;
using Weasel_Server_2.Weasel_Server1_Logic.NodeMap;
using Weasel_Server_2.Weasel_Server1_Logic.RoboDKLibraries;
using Weasel_Server_2.Weasel_Server1_Logic.WeaselFunctions;

namespace Weasel_Server_2.Weasel_Server1_Logic
{
    internal class WeaselControllerFoundation
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

        public WeaselControllerFoundation()
        {
            //Load the user input
            InputtableInformations();

            //Timer -> uses are stated below this command
            Timer tmr = new Timer(10);
            tmr.Elapsed += Update10ms;
            tmr.Start();

            //Start the WeaselControlPanel
            WeaselControlPanel WCP = new WeaselControlPanel(ref _WeaselMap, ref _Weasels, ref _KukaRobot);
        }

        private void Update10ms(object sender, EventArgs e)
        {
            //Unreserve past nodes with updating the weasels information
            UpdateWeaselInformation();
            UnreserveNodes();

            //Write map to .txt
            File.WriteAllText(@"map.txt", _WeaselMap.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

                    //Also take a look at the past four points
                    Waypoint[] waypoints = new Waypoint[4];
                    for (int u = 0; u < waypoints.Length; u++)
                    {
                        if (u == 0)
                        {
                            waypoints[u] = _WeaselMap.FindWayPointBeforeNumber(_Weasels[i]._LastPosition);
                        }
                        else
                        {
                            if (waypoints[u - 1] != null)
                            {
                                waypoints[u] = _WeaselMap.FindWayPointBeforeNumber(waypoints[u - 1]._PointId);
                            }
                        }
                    }

                    //Check if the past four point were unreserved
                    for (int u = 0; u < waypoints.Length; u++)
                    {
                        if (waypoints[u] != null && waypoints[u]._Reserved_Color == _Weasels[i]._Colored && waypoints[u]._PointId != _Weasels[i]._LastPosition)
                        {
                            _WeaselMap.UnReserve(waypoints[u]._PointId);
                        }
                    }
                }

                Waypoint temp = _WeaselMap.FindWayPoint(_Weasels[i]._LastPosition);
                if (temp._Reserved_Color != _Weasels[i]._Colored)
                {
                    _WeaselMap.Reserve(_Weasels[i]._LastPosition, _Weasels[i]._Colored);
                }
            }
        }

        private void UpdateWeaselInformation()
        {
            for (int i = 0; i < _Weasels.Length; i++)
            {
                if (_AppOnline == true)
                {
                    _Weasels[i].UpdateInfos();
                }
                _Weasels[i].UpdateBattery();
            }
        }

        private void SetBeforeLastPosition(int weaselindex1)
        {
            _Weasels[weaselindex1]._BeforeLastPosition = _Weasels[weaselindex1]._LastPosition;
        }
    }
}