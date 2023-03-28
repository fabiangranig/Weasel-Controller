using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Weasel_Controller
{
    class Weasel
    {
        //Variables
        private static string _IpAddress;
        private string _WeaselName;
        private int _WeaselId;
        public int _LastPosition;
        public int _BeforeLastPosition;
        private bool _AppOnline;

        //encapsulation
        public string WeaselName
        {
            get { return _WeaselName; }
        }

        public int WeaselID
        {
            get { return _WeaselId; }
        }
        public bool AppOnline
        {
            get { return _AppOnline; }
        }

        public Weasel(string weaselname, bool AppOnline1, int weaselId21)
        {
            _IpAddress = "http://10.0.9.22:4567";
            _WeaselName = weaselname;
            _AppOnline = AppOnline1;
            _WeaselId = weaselId21;
            _LastPosition = GetPosition();
        }

        public void SetPosition(int waypoint)
        {
            if(_AppOnline == true)
            {
                var address = _IpAddress + "/controller/move/" + _WeaselName + "/" + waypoint.ToString();

                var request = WebRequest.Create(address);
                request.Method = "POST";

                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();
                if (webStream == null) return;
                var reader = new StreamReader(webStream);
                string temp = reader.ReadToEnd();
                return;
            }
        }

        public void MoveThroughCordinates(int[] input)
        {
            Thread t1 = new Thread(() => MoveThroughCordinatesBackend(input));
            t1.Start();
        }

        private void MoveThroughCordinatesBackend(int[] path)
        {
            //When there is only one or two cordinates
            if(path.Length < 3)
            {
                SetPosition(path[path.Length-1]);
                return;
            }

            int o = 0;
            while(_LastPosition != path[path.Length - 1])
            {
                if(_LastPosition == path[o])
                {
                    SetPosition(path[o+2]);

                    //Test
                    Console.WriteLine(_WeaselName + ": gesetzte Position: " + path[o + 2]);

                    if (path[o + 2] == path[path.Length - 1])
                    {
                        break;
                    }
                    o++;
                }

                //To not overuse processing units
                Thread.Sleep(100);

                //Test: for Offline Mode
                _LastPosition = GetPosition();
            }
        } 

        public int GetPosition()
        {
            if(_AppOnline == true)
            {
                WebClient wc = new WebClient();

                byte[] raw = wc.DownloadData("http://10.0.9.22:4567/weasels");

                //Convert in an string
                string webData = System.Text.Encoding.UTF8.GetString(raw);

                //Create the JSON Document
                JsonDocument doc = JsonDocument.Parse(webData);
                JsonElement root = doc.RootElement;

                //Weasels aufteilen
                var u1 = root[_WeaselId];

                //Create string values
                string test = u1.GetProperty("lastWaypoint") + "";
                return Int32.Parse(test);
            }
            else
            {
                Console.WriteLine(_WeaselName + ": App offline. Bitte derzeitige Position eingeben: ");
                return Convert.ToInt32(Console.ReadLine());
            }
        }

        public void UpdateInfos()
        {
            _LastPosition = GetPosition();
        }
    }
}
