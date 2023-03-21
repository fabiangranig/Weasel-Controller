using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class C_Waypoint
    {
        //Waypoint
        public int _PointId;
        public List<C_Waypoint> _Next;

        //Constructor
        public C_Waypoint(int number1)
        {
            _PointId = number1;
            _Next = null;
        }
    }
}
