using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class Map
    {
        //Head Node
        private Waypoint _Head;
        private Random _Random1;

        //Constructor
        public Map()
        {
            _Head = null;
            _Random1 = new Random();
        }

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

        public int[] FreePath(int start, int end)
        {
            Waypoint start_position = FindWayPoint(start);

            List<string> routes = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string path = FreePathBackend(start_position, end, "");

                if(path != null)
                {
                    routes.Add(path);
                }
            }

            if(routes.Count == 0)
            {
                return new int[] { -1 };
            }

            routes.Sort();

            string[] split = routes[0].Split(' ');
            int[] arr = new int[split.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = Int32.Parse(split[i]);
            }

            return arr;
        }

        private string FreePathBackend(Waypoint way, int id, string path)
        {
            if(way._Next == null && way._PointId != id && _Head._Reserved == false)
            {
                return FreePathBackend(_Head, id, path + way._PointId + " ");
            }

            if(way._Next == null)
            {
                return null;
            }

            if(way._PointId == id && way._Reserved == false)
            {
                return path + way._PointId;
            }

            int rnd = _Random1.Next(0, way._Next.Count);
            if(way._Next[rnd]._Reserved == false)
            {
                Waypoint RandomWay = way._Next[rnd];
                return FreePathBackend(RandomWay, id, path + way._PointId + " ");
            }

            return null;
        }

        public int[] FreePathWithoutCheck(int start, int end)
        {
            Waypoint start_position = FindWayPoint(start);

            List<string> routes = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                string path = FreePathBackendWithoutCheck(start_position, end, "");

                if (path != null)
                {
                    routes.Add(path);
                }
            }

            if (routes.Count == 0)
            {
                return new int[] { -1 };
            }

            //Get the shortest route
            string shortest_route = routes[0];
            for (int i = 1; i < routes.Count; i++)
            {
                if(routes[i].Length < shortest_route.Length)
                {
                    shortest_route = routes[i];
                }
            }

            string[] split = shortest_route.Split(' ');
            int[] arr = new int[split.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Int32.Parse(split[i]);
            }

            return arr;
        }

        private string FreePathBackendWithoutCheck(Waypoint way, int id, string path)
        {
            if (way._Next == null && way._PointId != id)
            {
                return FreePathBackendWithoutCheck(_Head, id, path + way._PointId + " ");
            }

            if (way._Next == null)
            {
                return null;
            }

            if (way._PointId == id)
            {
                return path + way._PointId;
            }

            int rnd = _Random1.Next(0, way._Next.Count);
            Waypoint RandomWay = way._Next[rnd];
            return FreePathBackendWithoutCheck(RandomWay, id, path + way._PointId + " ");
        }

        public void Reserve(int id)
        {
            Waypoint temp = FindWayPoint(id);
            temp._Reserved = true;
        }

        public void UnReserve(int id)
        {
            Waypoint temp = FindWayPoint(id);
            temp._Reserved = false;
        }

        public void ReserveArr(int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                Reserve(arr[i]);
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
            if(way._Next == null)
            {
                if(!points.Contains(toAdd))
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
