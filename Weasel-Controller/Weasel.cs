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
        private static string _IpAddress;

        private string _WeaselId;
        public int _LastPosition;
        private bool _AppOnline;

        public Weasel(string weaselid1, bool AppOnline1)
        {
            _IpAddress = "http://10.0.9.22:4567";
            _WeaselId = weaselid1;
            _LastPosition = GetPosition();
            _AppOnline = AppOnline1;
        }

        private void SetPosition(int waypoint)
        {
            if(_AppOnline == true)
            {
                var address = _IpAddress + "/controller/move/" + _WeaselId + "/" + waypoint.ToString();

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

        public void MoveThroughCordinates(string input)
        {
            Thread t1 = new Thread(() => MoveThroughCordinatesBackend(input));
            t1.Start();
        }

        private void MoveThroughCordinatesBackend(string path)
        {
            string[] split1 = path.Split(' ');
            int[] numbers = new Int32[split1.Length];
            for(int i = 0; i < split1.Length; i++)
            {
                numbers[i] = Int32.Parse(split1[i]);
            }

            int o = 0;
            while(_LastPosition != numbers[numbers.Length - 1])
            {
                if(_LastPosition == numbers[o])
                {
                    SetPosition(numbers[o+2]);

                    if(numbers[o + 2] == numbers[numbers.Length - 1])
                    {
                        break;
                    }

                    //Test
                    Console.WriteLine(_WeaselId + ": gesetzte Position: " + numbers[o + 2]);

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
                var u1 = root[0];

                //Create string values
                string test = u1.GetProperty("lastWaypoint") + "";
                return Int32.Parse(test);
            }

            Console.WriteLine(_WeaselId + ": App offline. Bitte derzeitige Position ausgeben: ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
