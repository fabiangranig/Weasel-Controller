using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Weasel_Controller
{
    class txtParser
    {
        private string[] _blob;
        public List<string> _good_blob;

        public txtParser(string PathToTxt)
        {
            _blob = File.ReadAllLines(PathToTxt);

            _good_blob = new List<string>();
            for(int i = 0; i < _blob.Length; i++)
            {
                if(_blob[i][0] != '#')
                {
                    _good_blob.Add(_blob[i]);
                }
            }

            for(int i = 0; i < _good_blob.Count; i++)
            {
                Console.WriteLine(_good_blob[i]);
            }
        }
    }
}
