using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Drawing;

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
        public int _HomePosition;
        public Color _Colored;
        public int _Destination;
        public List<DestinationwithInformation> _DestinationsWithInformation;
        public bool _LastDestinationReached;
        private List<int> _OfflineMover;

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

        public Weasel(string weaselname, bool AppOnline1, int weaselId21, int HomePosition1, Color color1)
        {
            _IpAddress = "http://10.0.9.22:4567";
            _WeaselName = weaselname;
            _AppOnline = AppOnline1;
            _WeaselId = weaselId21;
            _HomePosition = HomePosition1;
            _Colored = color1;
            _Destination = -1;
            _LastDestinationReached = true;
            _DestinationsWithInformation = new List<DestinationwithInformation>();
            
            if(_AppOnline == true)
            {
                _LastPosition = GetPosition();
            }
            if(_AppOnline == false)
            {
                _LastPosition = _HomePosition;

                //Create Thread for moving weasel
                _OfflineMover = new List<int>();
                Thread OfflinerMover = new Thread(VirtualSetPosition);
                OfflinerMover.Start();
            }
        }

        public void SetPosition(int waypoint)
        {
            //Just for testing!
            Console.WriteLine(WeaselName + ": gesetzte Position " + waypoint);
            if(_AppOnline == false)
            {
                _OfflineMover.Add(waypoint);
            }

            if(_AppOnline == true)
            {
                var address = _IpAddress + "/controller/move/" + _WeaselName + "/" + waypoint;

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

        private void VirtualSetPosition()
        {
            Random r1 = new Random();
            while (_AppOnline == false)
            {
                Thread.Sleep(r1.Next(1000, 2000));
                if(_OfflineMover.Count > 0)
                {
                    _LastPosition = _OfflineMover[0];
                    _OfflineMover.RemoveAt(0);
                }
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

            //Offline Mode
            return _LastPosition;
        }

        public void UpdateInfos()
        {
            _LastPosition = GetPosition();
        }
    }
}
