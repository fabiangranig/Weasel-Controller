using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Server_2.Weasel_Server1_Logic.NodeMap
{
    internal class txtParser
    {
        private string[] _Blob;
        private List<string> _GoodBlob;

        public txtParser(string PathToTxt)
        {
            //Remove all comments from string[]
            _Blob = File.ReadAllLines(PathToTxt);
            RemoveComments(_Blob);
        }

        private List<string> RemoveComments(string[] input)
        {
            _GoodBlob = new List<string>();
            for (int i = 0; i < _Blob.Length; i++)
            {
                //Comment removal
                if (input[i][0] != '#')
                {
                    _GoodBlob.Add(input[i]);
                }
            }

            //Return filtered list
            return _GoodBlob;
        }

        public Map ParseToWeaselMap()
        {
            //Temporary map
            Map weasel_map = new Map();

            //Parsing the .txt input
            for (int i = 0; i < _GoodBlob.Count; i++)
            {
                if (_GoodBlob[i] == "---")
                {
                    for (int u = i + 1; u < _GoodBlob.Count; u++)
                    {
                        if (_GoodBlob[u] == "---")
                        {
                            for (int z = u + 1; z < _GoodBlob.Count; z++)
                            {
                                if (_GoodBlob[z] == "---")
                                {
                                    break;
                                }

                                string[] split3 = _GoodBlob[z].Split('-');
                                weasel_map.CombineTwoReservedNodes(Int32.Parse(split3[0]), Int32.Parse(split3[1]));
                            }
                            break;
                        }

                        string[] split2 = _GoodBlob[u].Split('-');
                        weasel_map.ConnectTwoPoints(Int32.Parse(split2[0]), Int32.Parse(split2[1]));
                    }
                    break;
                }

                string[] split = _GoodBlob[i].Split('-');

                if (i == 0)
                {
                    weasel_map.AddNodeToNumber(new Waypoint(Int32.Parse(split[0])), Int32.Parse(split[1]));
                }
                else
                {
                    weasel_map.AddNodeToNumber(new Waypoint(Int32.Parse(split[1])), Int32.Parse(split[0]));
                }
            }

            //Returned the finished map
            return weasel_map;
        }
    }
}
