using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class C_Map
    {
        //Head Node
        private C_Waypoint _Head;
        private Random r1;

        //Constructor
        public C_Map()
        {
            _Head = null;
            r1 = new Random();
        }

        //Methods
        public void AddNodeToNumber(C_Waypoint waypoint1, int id1)
        {
            //When there is nothing in the map
            if (_Head == null)
            {
                _Head = waypoint1;
                return;
            }

            AddNodeToNumberBackend(_Head, waypoint1, id1);
        }

        private void AddNodeToNumberBackend(C_Waypoint temp_map, C_Waypoint waypoint1, int id1)
        {
            if (temp_map._Next != null && temp_map._PointId == id1)
            {
                temp_map._Next.Add(waypoint1);
                return;
            }

            if (temp_map._Next == null && temp_map._PointId == id1)
            {
                temp_map._Next = new List<C_Waypoint>();
                temp_map._Next.Add(waypoint1);
            }

            if (temp_map._Next == null)
            {
                return;
            }

            //Next lists
            for (int i = 0; i < temp_map._Next.Count; i++)
            {
                AddNodeToNumberBackend(temp_map._Next[i], waypoint1, id1);
            }
        }

        public C_Waypoint FindWayPoint(int id1)
        {
            return FindWayPointBackend(_Head, id1);
        }

        public C_Waypoint FindWayPointBackend(C_Waypoint header, int id1)
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
                C_Waypoint[] waypoints = new C_Waypoint[header._Next.Count];
                for (int i = 0; i < waypoints.Length; i++)
                {
                    waypoints[i] = FindWayPointBackend(header._Next[i], id1);
                    if (waypoints[i] != null)
                    {
                        return waypoints[i];
                    }
                }
            }

            //When nothing works return null
            return null;
        }

        public void ConnectTwoPoints(int start_point, int end_point)
        {
            C_Waypoint waypoint = FindWayPointBackend(_Head, start_point);

            if (waypoint._Next == null)
            {
                waypoint._Next = new List<C_Waypoint>();
            }

            waypoint._Next.Add(FindWayPointBackend(_Head, end_point));
        }

        public int[] FreePath(int start, int end)
        {
            C_Waypoint start_position = FindWayPoint(start);

            //The randomness should be killed through about 20 iterations
            List<string> routes = new List<string>();
            for (int i = 0; i < 20; i++)
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

            //Sort the paths
            routes.Sort();

            //Convert to Int32 Array
            string[] split = routes[0].Split(' ');
            int[] arr = new int[split.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = Int32.Parse(split[i]);
            }

            //Get the top route
            return arr;
        }

        public string FreePathBackend(C_Waypoint way, int id, string path)
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

            int rnd = r1.Next(0, way._Next.Count);
            if(way._Next[rnd]._Reserved == false)
            {
                C_Waypoint RandomWay = way._Next[rnd];
                return FreePathBackend(RandomWay, id, path + way._PointId + " ");
            }

            //When all ways are blocked off
            return null;
        }

        public void Reserve(int id)
        {
            C_Waypoint temp = FindWayPoint(id);
            temp._Reserved = true;
        }

        public void UnReserve(int id)
        {
            C_Waypoint temp = FindWayPoint(id);
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
            C_Waypoint temp = _Head;
            List<string> points = new List<string>();
            ShowMapBackend(temp, ref points);
            points.Sort();
            return points;
        }

        public void ShowMapBackend(C_Waypoint way, ref List<string> points)
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
    }
}
