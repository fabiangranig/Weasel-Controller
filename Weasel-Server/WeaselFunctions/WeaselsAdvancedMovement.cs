using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weasel_Controller
{
    class WeaselsAdvancedMovement : Form
    {
        private Weasel[] _Weasels;

        public WeaselsAdvancedMovement(ref Weasel[] Weasels1)
        {
            _Weasels = Weasels1;

            //Testing
            string input = "10000>10;5000>20;10000>39";
            StringToDestinations STD = new StringToDestinations(input);
            for(int i = 0; i < STD.Destinations.Count; i++)
            {
                _Weasels[0]._DestinationsWithInformation.Add(STD.Destinations[i]);
            }
        }
    }
}