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

        public string FreePath(int start, int end)
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

            //Sort the paths
            routes.Sort();

            //Get the top route
            return routes[0];
        }

        public string FreePathBackend(C_Waypoint way, int id, string path)
        {
            if(way._Next == null)
            {
                return FreePathBackend(_Head, id, path + way._PointId + " ");
            }

            if(way._PointId == id)
            {
                return path + way._PointId;
            }

            C_Waypoint RandomWay = way._Next[r1.Next(0, way._Next.Count)];
            return FreePathBackend(RandomWay, id, path + way._PointId + " ");
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
    }
}
