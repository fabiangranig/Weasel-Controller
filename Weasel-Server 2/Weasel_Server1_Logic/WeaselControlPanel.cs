using S7.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Weasel_Server_2.Weasel_Server1_Logic.NodeMap;
using Weasel_Server_2.Weasel_Server1_Logic.RoboDKLibraries;
using Weasel_Server_2.Weasel_Server1_Logic.WeaselFunctions;

namespace Weasel_Server_2.Weasel_Server1_Logic
{
    internal class WeaselControlPanel
    {
        private Map _WeaselMap;
        private Weasel[] _Weasels;
        private Label _lbl_Online;
        private Label lbl_RoboDKControls;
        private WeaselMovementHandler[] _WeaselMovementHandlers;
        private Label lbl_AllWeaselsAction;
        private KukaRoboter _KukaRobot;
        private bool _ChargingEnabled;

        public WeaselControlPanel(ref Map map1, ref Weasel[] weasels1, ref KukaRoboter kukaRoboter1)
        {
            //Get weasels with map
            _WeaselMap = map1;
            _Weasels = weasels1;

            //Set the KukaRobot
            _KukaRobot = kukaRoboter1;

            //Set the charging to zero
            this._ChargingEnabled = false;

            //Get the weasels into the dropdown and create Handlers
            _WeaselMovementHandlers = new WeaselMovementHandler[_Weasels.Length];
            for (int i = 0; i < _Weasels.Length; i++)
            {
                _WeaselMovementHandlers[i] = new WeaselMovementHandler(ref _Weasels[i], ref _WeaselMap, ref _KukaRobot);
            }

            //Create a Timer which is working on next paths
            System.Timers.Timer tmr = new System.Timers.Timer(1000);
            tmr.Elapsed += PathWorker;
            tmr.Start();

            //Check all weasels when the lowest weasels isn't driving, set him to the charging position
            System.Timers.Timer tmr3 = new System.Timers.Timer(60000);
            tmr3.Elapsed += SendWeaselCharging;
            tmr3.Start();

            //Check for sensor and if an box is placed move weasel and pick-up
            System.Threading.Thread tmr4 = new Thread(CheckForBox);
            tmr4.Start();
        }

        //Works through all Weasels and figures out which one needs to reposition
        private void PathWorker(object sender, EventArgs e)
        {
            //Gets paths and lets the weasels drive
            for (int i = 0; i < _Weasels.Length; i++)
            {
                if (_Weasels[i]._DestinationsWithInformation.Count > 0)
                {
                    //Safety check to reduce crashes
                    if (_Weasels[i]._DestinationsWithInformation.Count > 0)
                    {
                        //Set position if not set
                        if (_Weasels[i]._DestinationsWithInformation[0].Destination != _Weasels[i]._Destination)
                        {
                            _Weasels[i]._Destination = _Weasels[i]._DestinationsWithInformation[0].Destination;
                            _WeaselMovementHandlers[i].MoveWeasel(_Weasels[i]._DestinationsWithInformation[0]);
                        }
                    }
                }
            }
        }

        private void SendWeaselCharging(object sender, EventArgs e)
        {
            if (this._ChargingEnabled == true)
            {
                bool schalter_possible = true;
                for (int i = 0; i < _Weasels.Length; i++)
                {
                    if (_Weasels[i]._DestinationsWithInformation.Count != 0)
                    {
                        schalter_possible = false;
                    }
                }

                if (schalter_possible)
                {
                    Thread SendWeaselsChargingThread = new Thread(SendWeaselChargingBackend);
                    SendWeaselsChargingThread.Start();
                }
            }
        }

        private void SendWeaselChargingBackend()
        {
            //Get lowest weasel
            int lowest_battery_weasel_number = 0;
            int lowest_battery = _Weasels[lowest_battery_weasel_number].BatteryPercentage;
            for (int i = 1; i < _Weasels.Length; i++)
            {
                if (lowest_battery > _Weasels[i].BatteryPercentage)
                {
                    lowest_battery_weasel_number = i;
                    lowest_battery = _Weasels[i].BatteryPercentage;
                }
            }

            //Send all weasels to new positions
            //Send the right weasel to the charging position
            _Weasels[lowest_battery_weasel_number]._DestinationsWithInformation.Add(new DestinationWithInformation(46, "none", "Battery"));

            //Sleep to not create two movements at the exact same time
            Thread.Sleep(5000);

            //Send the rest of the weasels
            int[] still_alive_position = { 48, 39 };
            int u = 0;
            for (int i = 0; i < _Weasels.Length; i++)
            {
                if (i != lowest_battery_weasel_number)
                {
                    _Weasels[i]._DestinationsWithInformation.Add(new DestinationWithInformation(still_alive_position[u], "none", "Battery"));
                    u++;
                }
            }
        }

        public void SendWeasel(int weaselid, string position)
        {
            //Check if that position exists
            Waypoint temp = _WeaselMap.FindWayPoint(Int32.Parse(position));

            //Add it to the route table, if it exists
            if (temp != null)
            {
                _Weasels[weaselid]._DestinationsWithInformation.Add(new DestinationWithInformation(0, Int32.Parse(position)));
                return;
            }
        }

        private void btn_RandomPosition_Click(int weaselid, string position)
        {
            Random Filler = new Random();
            for (int i = 0; i < 30; i++)
            {
                int id = _WeaselMap.FindWayPoint(Filler.Next(1, 50))._PointId;
                _Weasels[weaselid]._DestinationsWithInformation.Add(new DestinationWithInformation(0, id));
            }
        }

        private void btnClick_WeaselHome(int weaselid)
        {
            _Weasels[weaselid]._DestinationsWithInformation.Add(new DestinationWithInformation(0, _Weasels[weaselid]._HomePosition));
        }

        private void btn_RandomPositionSPL_Click(int weaselid)
        {
            SetRandomPositionSPL(weaselid);
        }

        private void SetRandomPositionSPL(int selected_weasel)
        {
            Random Filler = new Random();
            for (int i = 0; i < 30; i++)
            {
                int id = _WeaselMap.FindWayPoint(Filler.Next(1, 50))._PointId;

                //Check of that waypoint is outside of the SPL Labor
                int[] route = _WeaselMap.FreePath(_Weasels[selected_weasel]._LastPosition, id, Color.Empty);
                bool schalter = false;
                for (int u = 0; u < route.Length; u++)
                {
                    if (route[u] == 1)
                    {
                        schalter = true;
                    }
                }

                if (schalter == false)
                {
                    _Weasels[selected_weasel]._DestinationsWithInformation.Add(new DestinationWithInformation(0, id));
                }
            }
        }

        private void btn_MoveRoboToPosition_Click(object sender, EventArgs e)
        {
            //Let the not occupied weasel get the box
            for (int i = 0; i < _Weasels.Length; i++)
            {
                if (_Weasels[i]._DestinationsWithInformation.Count == 0 && _Weasels[i]._HasBox == false)
                {
                    _Weasels[i]._DestinationsWithInformation.Add(new DestinationWithInformation(41, "Kuka1", "Server"));
                    break;
                }
            }
        }

        private void btn_RobotSimulation_Click(object sender, EventArgs e)
        {
            _KukaRobot.SwitchSimulationMode();
        }

        private void btn_RobotRealRobot_Click(object sender, EventArgs e)
        {
            _KukaRobot.SwitchRealMode();
            _KukaRobot.GreiferZu();
            _KukaRobot.GreiferAuf();
        }

        private void btn_AllWeaselsRandomPositionSPL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _Weasels.Length; i++)
            {
                SetRandomPositionSPL(i);
            }
        }

        private void btn_RemoveBox_Click(int weaselid)
        {
            _Weasels[weaselid]._HasBox = false;
        }

        private void CheckForBox()
        {
            //Only execute if the program is in online mode
            if (this._KukaRobot.AppOnline)
            {
                while (1 == 1)
                {
                    using (var plc = new Plc(CpuType.S7300, "10.0.9.100", 0, 2))
                    {
                        plc.Open();
                        bool AnfangFörderband = (bool)plc.Read("I510.1");

                        if (!AnfangFörderband == true)
                        {
                            //Leet the Thread sleep for an second to not disturb the next iteration
                            Thread.Sleep(1000);

                            //Let the not occupied weasel get the box
                            for (int i = 0; i < _Weasels.Length; i++)
                            {
                                if (_Weasels[i]._DestinationsWithInformation.Count == 0 && _Weasels[i]._HasBox == false)
                                {
                                    _Weasels[i]._DestinationsWithInformation.Add(new DestinationWithInformation(41, "Kuka1", "Server"));
                                    break;
                                }
                            }
                        }
                    }

                    //When one rotation is finished
                    Thread.Sleep(100);
                }
            }
        }
    }
}
