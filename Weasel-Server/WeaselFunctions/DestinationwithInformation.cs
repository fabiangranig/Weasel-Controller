using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class DestinationwithInformation
    {
        private int _SleepBefore;
        private int _Destination;
        private string _ActionAfterMovement;
        private string _SendBy;

        //constructors
        public DestinationwithInformation(int Destination1)
        {
            _SleepBefore = 0;
            _Destination = Destination1;
            _ActionAfterMovement = "none";
            _SendBy = "no_onwer";
        }

        public DestinationwithInformation(int SleepBefore1, int Destination1)
        {
            _SleepBefore = SleepBefore1;
            _Destination = Destination1;
            _ActionAfterMovement = "none";
            _SendBy = "no_owner";
        }

        public DestinationwithInformation(int SleepBefore1, int Destination1, string Owner1)
        {
            _SleepBefore = SleepBefore1;
            _Destination = Destination1;
            _ActionAfterMovement = "none";
            _SendBy = Owner1;
        }

        public DestinationwithInformation(int Destination1, string ActionAfterMovement1, string Owner1)
        {
            _SleepBefore = 0;
            _Destination = Destination1;
            _ActionAfterMovement = ActionAfterMovement1;
            _SendBy = Owner1;
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

        public string SendBy
        {
            get { return _SendBy; }
        }

        public string ActionAfterMovement
        {
            get { return _ActionAfterMovement; }
        }
    }
}
