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
        private List<DestinationwithInformation> _SortedDestinations;

        public StringToDestinations(string input)
        {
            _Blob = input;
            _SortedDestinations = new List<DestinationwithInformation>();

            //Convert To Destinations
            ConvertToDestinations(_Blob);
        }

        public List<DestinationwithInformation> Destinations
        {
            get { return _SortedDestinations; }
        }

        private void ConvertToDestinations(string _Blob)
        {
            string[] actions = _Blob.Split(';');
            for(int i = 0; i < actions.Length; i++)
            {
                string[] MillisecondsAndDestinations = actions[i].Split('>');
                _SortedDestinations.Add(new DestinationwithInformation(Int32.Parse(MillisecondsAndDestinations[0]), Int32.Parse(MillisecondsAndDestinations[1])));
            }
        }
    }
}
