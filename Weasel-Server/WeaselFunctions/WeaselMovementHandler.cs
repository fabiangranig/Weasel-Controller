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
        private Thread _Mover;
        private int[] _LastKnownRoute;

        public WeaselMovementHandler(ref Weasel weasel1, ref Map map1)
        {
            _Weasel = weasel1;
            _Map = map1;
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

                //Check if there is an Path
                if(Path.Length > 1)
                {
                    //When there is an longer path to travel
                    _Map.ReserveArr(Path, _Weasel._Colored);
                    int u = 0;
                    while (_Weasel._LastPosition != Path[Path.Length - 1])
                    {
                        if (_Weasel._LastPosition == Path[u] && u + 2 < Path.Length)
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

                            if(switcher == false && u + 2 < Path.Length)
                            {
                                _Weasel.SetPosition(Path[u + 1]);
                                _Weasel.SetPosition(Path[u + 2]);
                            }
                            
                            if(switcher == true && u + 2 < Path.Length)
                            {
                                _Weasel.SetPosition(Path[u + 2]);
                            }

                            //Get to the next position
                            u++;
                        }

                        if(_Weasel._LastPosition == Path[u] && 3 > Path.Length && Path.Length > 1)
                        {
                            _Weasel.SetPosition(Path[u]);
                            _Weasel.SetPosition(Path[u + 1]);

                            //Get to the next position
                            u++;
                        }

                        //Reduce processing usage
                        Thread.Sleep(100);
                    }
                }

                //When send let the Thread sleep for a short amount of time
                Thread.Sleep(100);
            }

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
