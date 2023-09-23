using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Server_2.Weasel_Server1_Logic.NodeMap
{
    internal class Waypoint
    {
        //Waypoint
        public int _PointId;
        public List<Waypoint> _Next;
        public bool _Reserved;
        public Color _Reserved_Color;

        //Constructor
        public Waypoint(int number1)
        {
            _PointId = number1;
            _Next = null;
            _Reserved = false;
            _Reserved_Color = Color.LightGreen;
        }
    }
}
