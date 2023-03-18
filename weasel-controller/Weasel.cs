using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace weasel_controller
{
    public class Weasel
    {
        public int serialNumber;
        public string weaselId;
        public bool online;
        public string orderId;
        public int lastWaypoint;
        public int currentSegment;
        public string currentDestinations;
        public string currentReason;
        public string internalRoute;
        public string externalRoute;
        public int battery;
        public string lastCharging;
        public string lastMovement;
        public string lastTelegram;
        public string mode;
        public string movement;
        public string obstacleSensor;
        public string guideline;
        public string rssi;

        public Weasel(string weaselId)
        {
            this.weaselId = weaselId;
        }

        public string SetPosition(int waypoint)   //Zu Position schicken
        {
            try
            {
                Form1.WriteLog("Weasel " + this.weaselId + " set Position: " + waypoint);
                var address = Form1.ConIpAddress + "/controller/move/" + this.weaselId + "/" + waypoint.ToString();

                var request = WebRequest.Create(address);
                request.Method = "POST";

                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();
                if (webStream == null) return "";
                var reader = new StreamReader(webStream);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        
        public string SetPositionAndWait(int waypoint)   //Zu Position schicken
        {
            try
            {
                Form1.WriteLog("Weasel " + this.weaselId + " set Position: " + waypoint);
                var address = Form1.ConIpAddress + "/controller/move/" + this.weaselId + "/" + waypoint.ToString();

                var request = WebRequest.Create(address);
                request.Method = "POST";

                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();
                if (webStream == null) return "";
                var reader = new StreamReader(webStream);
                
                while (true)
                {
                    Weasel w = JsonConvert.DeserializeObject<Weasel>(GetWeaselInfo(this.weaselId));
                    if (w.lastWaypoint == waypoint)
                        break;
                }
                
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public string SetPositionAndWaitBefore(int waypoint)   //Zu Position schicken
        {
            try
            {
                Form1.WriteLog("Weasel " + this.weaselId + " set Position: " + waypoint);
                var address = Form1.ConIpAddress + "/controller/move/" + this.weaselId + "/" + waypoint.ToString();

                var request = WebRequest.Create(address);
                request.Method = "POST";

                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();
                if (webStream == null) return "";
                var reader = new StreamReader(webStream);

                while (true)
                {
                    Weasel w = JsonConvert.DeserializeObject<Weasel>(GetWeaselInfo(this.weaselId));
                    if (w.lastWaypoint == waypoint - 1 || w.lastWaypoint == waypoint)
                        break;
                }

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private static string GetWeaselInfo(string weaselId)
        {
            try
            {
                var address = Form1.ConIpAddress + "/weasels/" + weaselId;

                var request = WebRequest.Create(address);
                request.Method = "GET";

                var webResponse = request.GetResponse();
                var webStream = webResponse.GetResponseStream();
                if (webStream == null) return "";
                var reader = new StreamReader(webStream);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
