using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Server_2.Weasel_Server1_Logic.NodeMap
{
    internal class StringToDestinations
    {
        private string _Blob;
        private List<DestinationWithInformation> _SortedDestinations;

        public StringToDestinations(string input)
        {
            _Blob = input;
            _SortedDestinations = new List<DestinationWithInformation>();

            //Convert To Destinations
            ConvertToDestinations(_Blob);
        }

        public List<DestinationWithInformation> Destinations
        {
            get { return _SortedDestinations; }
        }

        private void ConvertToDestinations(string _Blob)
        {
            string[] actions = _Blob.Split(';');
            for (int i = 0; i < actions.Length; i++)
            {
                string[] MillisecondsAndDestinations = actions[i].Split('>');
                _SortedDestinations.Add(new DestinationWithInformation(Int32.Parse(MillisecondsAndDestinations[0]), Int32.Parse(MillisecondsAndDestinations[1])));
            }
        }
    }
}
