using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class DestinationwithSleep
    {
        private int _SleepBefore;
        private int _Destination;

        //constructors
        public DestinationwithSleep(int Destination1)
        {
            _SleepBefore = 0;
            _Destination = Destination1;
        }

        public DestinationwithSleep(int SleepBefore1, int Destination1)
        {
            _SleepBefore = SleepBefore1;
            _Destination = Destination1;
        }

        //encapsulation
        public int SleepBefore
        {
            get { return _SleepBefore; }
        }
        
        public int Destination
        {
            get { return _Destination; }
        }
    }
}
