using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace Weasel_Controller
{
    class WeaselMovementHandler
    {
        private Weasel _Weasel;
        private Map _Map;
        private KukaRoboter _KukaRobot;
        private Thread _Mover;
        private int[] _LastKnownRoute;

        public WeaselMovementHandler(ref Weasel weasel1, ref Map map1, ref KukaRoboter KukaRobot1)
        {
            _Weasel = weasel1;
            _Map = map1;
            _KukaRobot = KukaRobot1;
        }

        public void MoveWeasel(DestinationwithInformation DWS)
        {
            //Move to the designated position
            _Mover = new Thread(() => MovePartlyBackend(DWS));
            _Mover.Start();
        }

        private void MovePartlyBackend(DestinationwithInformation DWS)
        {
            //Get the sleep time before moving
            Thread.Sleep(DWS.SleepBefore);

            //Move
            while(_Weasel._LastPosition != DWS.Destination)
            {
                //Get the best possible path
                int[] Path = _Map.FreePath(_Weasel._LastPosition, DWS.Destination, _Weasel._Colored);
                Path = _Map.RadiusRoute(Path);
                _LastKnownRoute = Path;

                //When the whole way to the destination only takes 3 steps
                if (Path.Length < 5 && Path[Path.Length - 1] == DWS.Destination)
                {
                    _Map.ReserveArr(Path, _Weasel._Colored);

                    _Weasel.SetPosition(DWS.Destination);

                    if (_Weasel.AppOnline == false)
                    {
                        bool schalter = false;
                        for (int i = 0; i < Path.Length; i++)
                        {
                            if(_Weasel._OfflineMover.Count == 0)
                            {
                                schalter = true;
                            }

                            if(schalter == true)
                            {
                                _Weasel._OfflineMover.Add(Path[i]);
                            }

                            if (_Weasel._OfflineMover.Count > 0 && _Weasel._OfflineMover[0] == Path[i])
                            {
                                schalter = true;
                            }
                        }
                    }

                    while(DWS.Destination != _Weasel._LastPosition)
                    {
                        Thread.Sleep(500);
                    }

                    break;
                }

                //Check if there is an Path
                if (Path.Length > 1)
                {
                    //When there is an longer path to travel
                    _Map.ReserveArr(Path, _Weasel._Colored);
                    int u = 0;
                    while (Path.Length > 2 &&  _Weasel._LastPosition != Path[Path.Length - 3])
                    {
                        if (_Weasel._LastPosition == Path[u] && u + 3 < Path.Length)
                        {
                            //Discover new paths
                            int[] path_temp = _Map.FreePath(_Weasel._LastPosition, DWS.Destination, _Weasel._Colored);
                            path_temp = _Map.RadiusRoute(path_temp);
                            bool switcher = false;
                            if (Path[0] != path_temp[0])
                            {
                                Path = path_temp;
                                _LastKnownRoute = Path;
                                _Map.ReserveArr(Path, _Weasel._Colored);
                                u = 0;
                                switcher = true;
                            }

                            if(switcher == false && u + 3 < Path.Length)
                            {
                                //_Weasel.SetPosition(Path[u + 1]);
                                _Weasel.SetPosition(Path[u + 3]);

                                if (_Weasel.AppOnline == false)
                                {
                                    _Weasel._OfflineMover.Add(Path[u + 1]);
                                    _Weasel._OfflineMover.Add(Path[u + 2]);
                                    _Weasel._OfflineMover.Add(Path[u + 3]);
                                }
                            }
                            
                            if(switcher == true && u + 2 < Path.Length)
                            {
                                _Weasel.SetPosition(Path[u + 3]);

                                if (_Weasel.AppOnline == false)
                                {
                                    _Weasel._OfflineMover.Add(Path[u + 1]);
                                    _Weasel._OfflineMover.Add(Path[u + 2]);
                                    _Weasel._OfflineMover.Add(Path[u + 3]);
                                }
                            }

                            //Get to the next position, when the weasel needs to be send
                            u = u + 3;
                        }

                        //Reduce processing usage
                        Thread.Sleep(150);
                    }
                }

                //When send let the Thread sleep for a short amount of time
                Thread.Sleep(150);
            }

            //Action when there is an action
            if(_Weasel._DestinationsWithInformation[0].ActionAfterMovement == "Kuka1")
            {
                _Weasel._DestinationsWithInformation.Add(new DestinationwithInformation(_Weasel._HomePosition));
                _KukaRobot.PickUp();
            }

            //Remove the position and if the next is also the same remove that also
            while(_Weasel._DestinationsWithInformation.Count > 0 && _Weasel._LastPosition == _Weasel._DestinationsWithInformation[0].Destination)
            {
                _Weasel._DestinationsWithInformation.RemoveAt(0);
            }

            //Output finish
            Console.WriteLine(_Weasel.WeaselName + ": Ziel erreicht {" + DWS.Destination + "}");
        }

        public void DestroyAction()
        {
            //Remove the thread
            _Mover.Abort();

            //Unreserve the route it was taking
            _Map.UnReserveArr(_LastKnownRoute);

            //Make the destination to something notable
            _Weasel._Destination = -1;
        }
    }
}
