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

        //Constructor
        public C_Map()
        {
            _Head = null;
        }

        //Methods
        public void AddNodeToNumber(C_Waypoint waypoint1, int id1)
        {
            //When there is nothing in the map
            if(_Head == null)
            {
                _Head = waypoint1;
                return;
            }

            //When there is something in the map
            AddNodeToNumberBackend(_Head, waypoint1, id1);
        }

        private void AddNodeToNumberBackend(C_Waypoint temp_map, C_Waypoint waypoint1, int id1)
        {
            if(temp_map._Next != null && temp_map._PointId == id1)
            {
                temp_map._Next.Add(waypoint1);
                return;
            }

            if(temp_map._Next == null && temp_map._PointId == id1)
            {
                temp_map._Next = new List<C_Waypoint>();
                temp_map._Next.Add(waypoint1);
            }

            if(temp_map._Next == null)
            {
                Console.WriteLine("no Point to add too! bad input");
                return;
            }

            //Next lists
            for(int i = 0; i < temp_map._Next.Count; i++)
            {
                AddNodeToNumberBackend(temp_map._Next[i], waypoint1, id1);
            }
        }
    }
}
