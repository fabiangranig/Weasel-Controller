using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class Waypoint
    {
        //Waypoint
        public int _PointId;
        public List<Waypoint> _Next;
        public bool _Reserved;

        //Constructor
        public Waypoint(int number1)
        {
            _PointId = number1;
            _Next = null;
            _Reserved = false;
        }
    }
}
