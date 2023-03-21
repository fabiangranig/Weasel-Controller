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
        private List<string> _good_blob;
        private string _Path;

        public txtParser(string PathToTxt1)
        {
            //Set the Path
            _Path = PathToTxt1;

            //Remove all comments
            _blob = File.ReadAllLines(_Path);
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

        public C_Map ParseToWeaselMap()
        {
            //Temporary map
            C_Map weasel_map = new C_Map();

            //Parsing the .txt input
            for (int i = 0; i < _good_blob.Count; i++)
            {
                if (_good_blob[i] == "---")
                {
                    for (int u = i + 1; u < _good_blob.Count; u++)
                    {
                        if (_good_blob[u] == "---")
                        {
                            break;
                        }

                        string[] split2 = _good_blob[u].Split('-');
                        weasel_map.ConnectTwoPoints(Int32.Parse(split2[0]), Int32.Parse(split2[1]));
                    }
                    break;
                }

                string[] split = _good_blob[i].Split('-');

                if (i == 0)
                {
                    weasel_map.AddNodeToNumber(new C_Waypoint(Int32.Parse(split[0])), Int32.Parse(split[1]));
                }
                else
                {
                    weasel_map.AddNodeToNumber(new C_Waypoint(Int32.Parse(split[1])), Int32.Parse(split[0]));
                }
            }

            //Returned the finished map
            return weasel_map;
        }
    }
}
