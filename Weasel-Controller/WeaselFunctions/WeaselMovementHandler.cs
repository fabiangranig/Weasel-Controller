using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Weasel_Controller
{
    class WeaselMovementHandler
    {
        private Weasel _Weasel;
        private Map _Map;
        private Thread _Mover;
        private int[] _current_path;

        //for testing only -> don't remove
        private Thread _OfflineMover;

        public WeaselMovementHandler(ref Weasel weasel1, ref Map map1)
        {
            _Weasel = weasel1;
            _Map = map1;
        }

        public void MoveWeasel(int goal)
        {
            //Move to the designated position
            _Mover = new Thread(() => MovePartlyBackend(goal));
            _Mover.Start();
        }

        private void MovePartlyBackend(int goal)
        {
            //Set goal to find if the right parts where found
            int last_goal = _Weasel._LastPosition;

            while (_Weasel._LastPosition != goal)
            {
                //Get the new route to the goal
                int[] route = _Map.FreePath(_Weasel._LastPosition, goal);

                //Check if there is an route and if last goal was reached
                if (route[0] != -1 && _Weasel._LastPosition == last_goal)
                {
                    //Check how much of the route is possible
                    int[] route2 = _Map.possibleRoute(route);

                    //Shrink the route to prevent interception from other weasels
                    if(route2.Length > 5)
                    {
                        route2 = _Map.radiusRoute(route2);
                    }

                    //When is is not the same position move
                    if (route2.Length > 1)
                    {
                        //Move to that position
                        //Reserve the nodes
                        _Map.ReserveArr(route2, _Weasel._Colored);

                        //Start Thread for offline testing
                        if (_Weasel.AppOnline == false)
                        {
                            _OfflineMover = new Thread(() => OfflineMover(route2));
                            _OfflineMover.Start();
                        }

                        //Move through nodes and tell if the last goal has been reached
                        _current_path = route2;
                        MoveThroughCordinatesBackend(route2);
                        last_goal = route2[route2.Length - 1];
                    }
                }

                //Not overusing processing units
                Thread.Sleep(100);
            }
        }

        private void MoveThroughCordinatesBackend(int[] path)
        {
            //When there is only one or two cordinates, in which case there is no skipping required
            if (path.Length < 3)
            {
                _Weasel.SetPosition(path[path.Length - 1]);
                return;
            }

            //Move through the path with the 2 step method
            int o = 0;
            while (_Weasel._LastPosition != path[path.Length - 1])
            {
                if (_Weasel._LastPosition == path[o])
                {
                    _Weasel.SetPosition(path[o + 2]);

                    if (path[o + 2] == path[path.Length - 1])
                    {
                        break;
                    }
                    o++;
                }

                //To not overuse processing units
                Thread.Sleep(100);
            }
        }

        //Move through path without actually weasels driving
        private void OfflineMover(int[] path)
        {
            for (int i = 0; i < path.Length; i++)
            {
                Thread.Sleep(1000);
                _Weasel._LastPosition = path[i];
            }
        }

        public void StopMovement(int index)
        {
            if(index == 0)
            {
                //Kill the threads
                _Mover.Abort();
                _OfflineMover.Abort();

                //Remove the destination
                _Weasel._Destinations.RemoveAt(index);

                //Unreserve nodes which where occupied
                for (int i = 0; i < _current_path.Length; i++)
                {
                    if (_Map.FindWayPoint(_current_path[i])._Reserved_Color == _Weasel._Colored)
                    {
                        if (_Weasel._LastPosition != _Map.FindWayPoint(_current_path[i])._PointId)
                        {
                            _Map.UnReserve(_current_path[i]);
                        }
                    }
                }
            }
            else
            {
                //Remove the destination
                _Weasel._Destinations.RemoveAt(index);
            }
        }
    }
}
