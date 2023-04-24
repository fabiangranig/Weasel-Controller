using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class StringToDestinations
    {
        private string _Blob;
        private List<DestinationwithSleep> _SortedDestinations;

        public StringToDestinations(string input)
        {
            _Blob = input;
            _SortedDestinations = new List<DestinationwithSleep>();

            //Convert To Destinations
            ConvertToDestinations(_Blob);
        }

        public List<DestinationwithSleep> Destinations
        {
            get { return _SortedDestinations; }
        }

        private void ConvertToDestinations(string _Blob)
        {
            string[] actions = _Blob.Split(';');
            for(int i = 0; i < actions.Length; i++)
            {
                string[] MillisecondsAndDestinations = actions[i].Split('>');
                _SortedDestinations.Add(new DestinationwithSleep(Int32.Parse(MillisecondsAndDestinations[0]), Int32.Parse(MillisecondsAndDestinations[1])));
            }
        }
    }
}
