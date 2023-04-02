using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Weasel_Controller
{
    class Map
    {
        //Head Node
        private Waypoint _Head;
        private List<int[]> _CombinedNodes;

        //Constructor
        public Map()
        {
            _Head = null;
            _CombinedNodes = new List<int[]>();
        }

        //Waypoint related
        //Waypoint related
        //Waypoint related
        public void AddNodeToNumber(Waypoint waypoint1, int id1)
        {
            if (_Head == null)
            {
                _Head = waypoint1;
                return;
            }

            AddNodeToNumberBackend(_Head, waypoint1, id1);
        }

        private void AddNodeToNumberBackend(Waypoint temp_map, Waypoint waypoint1, int id1)
        {
            if (temp_map._Next != null && temp_map._PointId == id1)
            {
                temp_map._Next.Add(waypoint1);
                return;
            }

            if (temp_map._Next == null && temp_map._PointId == id1)
            {
                temp_map._Next = new List<Waypoint>();
                temp_map._Next.Add(waypoint1);
            }

            if (temp_map._Next == null)
            {
                return;
            }

            for (int i = 0; i < temp_map._Next.Count; i++)
            {
                AddNodeToNumberBackend(temp_map._Next[i], waypoint1, id1);
            }
        }

        public Waypoint FindWayPoint(int id1)
        {
            return FindWayPointBackend(_Head, id1);
        }

        private Waypoint FindWayPointBackend(Waypoint header, int id1)
        {
            if (header != null && header._PointId == id1)
            {
                return header;
            }

            if (header == null)
            {
                return null;
            }

            if (header._Next != null)
            {
                Waypoint[] waypoints = new Waypoint[header._Next.Count];
                for (int i = 0; i < waypoints.Length; i++)
                {
                    waypoints[i] = FindWayPointBackend(header._Next[i], id1);
                    if (waypoints[i] != null)
                    {
                        return waypoints[i];
                    }
                }
            }

            return null;
        }

        public void ConnectTwoPoints(int start_point, int end_point)
        {
            Waypoint waypoint = FindWayPointBackend(_Head, start_point);

            if (waypoint._Next == null)
            {
                waypoint._Next = new List<Waypoint>();
            }

            waypoint._Next.Add(FindWayPointBackend(_Head, end_point));
        }

        public void CombineTwoReservedNodes(int number1, int number2)
        {
            int[] arr = new int[2];
            arr[0] = number1;
            arr[1] = number2;
            _CombinedNodes.Add(arr);
        }

        public void Reserve(int id, Color color1)
        {
            Waypoint temp = FindWayPoint(id);
            temp._Reserved = true;
            temp._Reserved_Color = color1;

            //Check if it is an combined node
            for (int i = 0; i < _CombinedNodes.Count; i++)
            {
                if (id == _CombinedNodes[i][0])
                {
                    Waypoint temp2 = FindWayPoint(_CombinedNodes[i][1]);
                    temp2._Reserved = true;
                    temp2._Reserved_Color = color1;
                }
            }
        }

        public void UnReserve(int id)
        {
            Waypoint temp = FindWayPoint(id);
            temp._Reserved = false;
            temp._Reserved_Color = Color.LightGreen;

            //When one field of the combined field is unreserved, unreserve both
            for (int i = 0; i < _CombinedNodes.Count; i++)
            {
                if (id == _CombinedNodes[i][0])
                {
                    Waypoint temp2 = FindWayPoint(_CombinedNodes[i][1]);
                    temp2._Reserved = false;
                    temp2._Reserved_Color = Color.LightGreen;
                }
            }
        }

        public void ReserveArr(int[] arr, Color color1)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Reserve(arr[i], color1);
            }
        }

        public void UnReserveArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                UnReserve(arr[i]);
            }
        }

        public List<string> ShowMap()
        {
            Waypoint temp = _Head;
            List<string> points = new List<string>();
            ShowMapBackend(temp, ref points);
            points.Sort();
            return points;
        }

        private void ShowMapBackend(Waypoint way, ref List<string> points)
        {
            string toAdd = way._PointId + " " + way._Reserved;
            if (way._Next == null)
            {
                if (!points.Contains(toAdd))
                {
                    points.Add(toAdd);
                }
                return;
            }

            if (!points.Contains(toAdd))
            {
                points.Add(toAdd);
            }

            for (int i = 0; i < way._Next.Count; i++)
            {
                ShowMapBackend(way._Next[i], ref points);
            }
        }

        //Returns all information seperated by \n
        public override string ToString()
        {
            List<string> MapInList = ShowMap();
            string MapInString = "";
            for (int i = 0; i < MapInList.Count; i++)
            {
                MapInString += MapInList[i] + "\n";
            }
            return MapInString;
        }

        //Pathfindig related
        //Pathfindig related
        //Pathfindig related
        public int[] FreePath(int start, int end)
        {
            Waypoint start_position = FindWayPoint(start);

            List<string> routes = new List<string>();
            FreePathBackend(start_position, end, "", ref routes);

            if(routes.Count == 0)
            {
                FreePathBackendWithoutCheckBackend(start_position, end, "", ref routes);
            }

            if(routes.Count == 0)
            {
                return new int[] { -1 };
            }

            //Get the shortest route
            string shortest_route = routes[0];
            for (int i = 1; i < routes.Count; i++)
            {
                if (routes[i].Length < shortest_route.Length)
                {
                    shortest_route = routes[i];
                }
            }

            string[] split = shortest_route.Split(' ');
            int[] arr = new int[split.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = Int32.Parse(split[i]);
            }

            //Check if the route is actually possible
            arr = possibleRoute(arr);

            return arr;
        }

        private void FreePathBackend(Waypoint way, int id, string path, ref List<string> routes)
        {
            if(way._Next == null && way._PointId != id && _Head._Reserved == false)
            {
                FreePathBackend(_Head, id, path + way._PointId + " ", ref routes);
            }

            if(way._Next == null || path.Contains(" " + Convert.ToString(way._PointId) + " "))
            {
                return;
            }

            if(way._PointId == id && way._Reserved == false)
            {
                routes.Add(path + way._PointId);
                return;
            }

            for(int i = 0; i < way._Next.Count; i++)
            {
                if (way._Next[i]._Reserved == false)
                {
                    Waypoint RandomWay = way._Next[i];
                    FreePathBackend(RandomWay, id, path + way._PointId + " ", ref routes);
                }
            }
        }

        private void FreePathBackendWithoutCheckBackend(Waypoint way, int id, string path, ref List<string> routes)
        {
            if (way._Next == null && way._PointId != id)
            {
                FreePathBackendWithoutCheckBackend(_Head, id, path + way._PointId + " ", ref routes);
            }

            if (way._Next == null || path.Contains(" " + Convert.ToString(way._PointId) + " "))
            {
                return;
            }

            if (way._PointId == id)
            {
                routes.Add(path + way._PointId);
            }

            for(int i = 0; i < way._Next.Count; i++)
            {
                Waypoint RandomWay = way._Next[i];
                FreePathBackendWithoutCheckBackend(RandomWay, id, path + way._PointId + " ", ref routes);
            }
        }

        //Shrink the route to the possible route
        public int[] possibleRoute(int[] route)
        {
            List<int> liste = new List<int>();
            liste.Add(route[0]);
            for (int i = 1; i < route.Length; i++)
            {
                Waypoint temp = FindWayPoint(route[i]);
                
                if(temp._Reserved == true)
                {
                    break;
                }

                liste.Add(route[i]);
            }

            int[] newroute = new int[liste.Count];
            for(int i = 0; i < newroute.Length; i++)
            {
                newroute[i] = liste[i];
            }
            return newroute;
        }
    }
}
