using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Server_2.Weasel_Server1_Logic
{
    internal class DestinationWithInformation
    {
        private string _ActionBeforeMovement;
        private int _Destination;
        private string _ActionAfterMovement;
        private string _SendBy;

        //constructors
        public DestinationWithInformation(int Destination1)
        {
            _ActionBeforeMovement = "none";
            _Destination = Destination1;
            _ActionAfterMovement = "none";
            _SendBy = "no_onwer";
        }

        public DestinationWithInformation(int SleepBefore1, int Destination1)
        {
            _ActionBeforeMovement = "Wait" + SleepBefore1;
            _Destination = Destination1;
            _ActionAfterMovement = "none";
            _SendBy = "no_owner";
        }

        public DestinationWithInformation(int SleepBefore1, int Destination1, string Owner1)
        {
            _ActionBeforeMovement = "Wait" + SleepBefore1;
            _Destination = Destination1;
            _ActionAfterMovement = "none";
            _SendBy = Owner1;
        }

        public DestinationWithInformation(int Destination1, string ActionAfterMovement1, string Owner1)
        {
            _ActionBeforeMovement = "none";
            _Destination = Destination1;
            _ActionAfterMovement = ActionAfterMovement1;
            _SendBy = Owner1;
        }

        //encapsulation
        public string ActionBeforeMovement
        {
            get { return _ActionBeforeMovement; }
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
