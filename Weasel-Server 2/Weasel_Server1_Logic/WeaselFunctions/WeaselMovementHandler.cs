﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Weasel_Server_2.Weasel_Server1_Logic.NodeMap;
using Weasel_Server_2.Weasel_Server1_Logic.RoboDKLibraries;

namespace Weasel_Server_2.Weasel_Server1_Logic.WeaselFunctions
{
    internal class WeaselMovementHandler
    {
        private Weasel _Weasel;
        private Map _Map;
        private KukaRoboter _KukaRobot;
        private Thread _Mover;
        private int[] _LastKnownRoute;
        private int _LastSetPosition;

        public WeaselMovementHandler(ref Weasel weasel1, ref Map map1, ref KukaRoboter KukaRobot1)
        {
            _Weasel = weasel1;
            _Map = map1;
            _KukaRobot = KukaRobot1;
            _LastSetPosition = -1;
        }

        public void MoveWeasel(DestinationWithInformation DWS)
        {
            //Move to the designated position
            _Mover = new Thread(() => MovePartlyBackend(DWS));
            _Mover.Name = "Mover: " + _Weasel.WeaselName;
            _Mover.Start();
        }

        private void MovePartlyBackend(DestinationWithInformation DWS)
        {
            //Get the action before moving
            int result = -1;
            if (Int32.TryParse(DWS.ActionBeforeMovement, out result))
            {
                Thread.Sleep(result);
            }

            //Move
            int timeout_count = 0;
            int targeted_position = -1;
            int set_position = -1;
            while (_Weasel._LastPosition != DWS.Destination)
            {
                //When one or two step are called to often
                if (timeout_count > 2)
                {
                    Console.WriteLine(_Weasel.WeaselName + " : Timeout");
                    Thread.Sleep(8000);
                    timeout_count = 0;
                }

                //Get the best possible path
                int[] Path = _Map.FreePath(_Weasel._LastPosition, DWS.Destination, _Weasel._Colored);
                Path = _Map.RadiusRoute(Path);
                _Map.ReserveArr(Path, _Weasel._Colored);
                _LastKnownRoute = Path;

                //Path Modes for all possible Path prompts
                if (Path.Length == 0)
                {
                    Thread.Sleep(150);
                    targeted_position = _Weasel._LastPosition;
                }
                if (Path.Length == 1)
                {
                    Thread.Sleep(150);
                    targeted_position = _Weasel._LastPosition;
                }
                if (Path.Length == 2)
                {
                    Console.WriteLine(_Weasel.WeaselName + " Movement Option 1-Step");
                    _Weasel.SetPosition(Path[1]);
                    set_position = Path[1];
                    targeted_position = Path[1];

                    if (_Weasel.AppOnline == false)
                    {
                        AddToOfflineMovementHandler(Path[1]);
                    }

                    //Increase timeout count
                    timeout_count++;
                }
                if (Path.Length == 3)
                {
                    Console.WriteLine(_Weasel.WeaselName + " Movement Option 2-Step");
                    _Weasel.SetPosition(Path[2]);
                    set_position = Path[2];
                    targeted_position = Path[1];

                    if (_Weasel.AppOnline == false)
                    {
                        AddToOfflineMovementHandler(Path[1]);
                        AddToOfflineMovementHandler(Path[2]);
                    }

                    //Increase timeout count
                    timeout_count++;
                }
                if (Path.Length == 4)
                {
                    Console.WriteLine(_Weasel.WeaselName + " Movement Option 3-Step");
                    _Weasel.SetPosition(Path[3]);
                    set_position = Path[3];
                    targeted_position = Path[2];

                    if (_Weasel.AppOnline == false)
                    {
                        AddToOfflineMovementHandler(Path[1]);
                        AddToOfflineMovementHandler(Path[2]);
                        AddToOfflineMovementHandler(Path[3]);
                    }

                    //Remove timeout count
                    timeout_count = 0;
                }
                if (Path.Length == 5)
                {
                    Console.WriteLine(_Weasel.WeaselName + " Movement Option 4-Step");
                    _Weasel.SetPosition(Path[4]);
                    set_position = Path[4];
                    targeted_position = Path[3];

                    if (_Weasel.AppOnline == false)
                    {
                        AddToOfflineMovementHandler(Path[1]);
                        AddToOfflineMovementHandler(Path[2]);
                        AddToOfflineMovementHandler(Path[3]);
                        AddToOfflineMovementHandler(Path[4]);
                    }

                    //Remove timeout count
                    timeout_count = 0;
                }

                //Wait for the targeted set new position
                while (targeted_position != _Weasel._LastPosition && _Weasel._LastPosition != set_position)
                {
                    Thread.Sleep(10);
                }
            }

            //Action when there is an action
            if (_Weasel._DestinationsWithInformation[0].ActionAfterMovement == "Kuka1")
            {
                _Weasel._DestinationsWithInformation.Add(new DestinationWithInformation(_Weasel._HomePosition));
                _KukaRobot.PickUp();
                _Weasel._HasBox = true;
            }
            result = -1;
            if (Int32.TryParse(DWS.ActionBeforeMovement, out result))
            {
                Thread.Sleep(result);
            }

            //Remove the position and if the next is also the same remove that also
            while (_Weasel._DestinationsWithInformation.Count > 0 && _Weasel._LastPosition == _Weasel._DestinationsWithInformation[0].Destination)
            {
                _Weasel._DestinationsWithInformation.RemoveAt(0);
                _Weasel._Destination = -1;
            }

            //Output finish
            Console.WriteLine(_Weasel.WeaselName + ": Goal reached {" + DWS.Destination + "}");
        }

        public void DestroyAction()
        {
            //Remove the thread
            _Mover.Abort();

            //Get the last known route and remove last position the unreserve it
            int[] lastRoute = _LastKnownRoute;
            int[] lastRoutewithoutLastPosition = new int[_LastKnownRoute.Length - 1];
            int u = 0;
            for (int i = 0; i < _LastKnownRoute.Length; i++)
            {
                if (lastRoute[i] != _Weasel._LastPosition)
                {
                    lastRoutewithoutLastPosition[u] = lastRoute[i];
                    u++;
                }
            }

            //Unreserve the route it was taking
            _Map.UnReserveArr(lastRoutewithoutLastPosition);

            //Make the destination to something notable
            _Weasel._Destination = -1;
        }

        private void AddToOfflineMovementHandler(int number)
        {
            if (!_Weasel._OfflineMover.Contains(number))
            {
                Console.WriteLine(_Weasel.WeaselName + ": " + number + " added to OfflinerMove.");
                _Weasel._OfflineMover.Add(number);
            }
        }
    }
}
