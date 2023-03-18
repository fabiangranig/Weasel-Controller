using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace weasel_controller
{
    public partial class Form1 : Form
    {
        public const string ConIpAddress = "http://10.0.9.22:4567";   //Controller IP-Adresse

        private static Weasel[] _weasels = new []
        {
            new Weasel("AV002"),
            new Weasel("AV015"),
            new Weasel("MC6")
        };

        private static GroupBox[] _gbs = new GroupBox[_weasels.Length];
        private static Label[,] _lbs = new Label[_weasels.Length, 6];

        private readonly int[][] _chargingPoints =
        {
            new [] {40,41,42,1,36,37,38,39}
        };

        private readonly int[][] _problemBereiche =
        {
            new [] {40,41,42,1,36}
        };

        private readonly int[] _parkPositionen = { 39, 48, 46 };


        private bool _lsbt;  //Ladestationbesetzt
        private bool _finChaBool = true; //Finished Charging Bool
        
        private const string LogPath = @"log.txt";

        private int _counter; //Weasel Lade-Intervall wenn Batterie auf 100%
        
        
        public Form1()
        {
            InitializeComponent();
            WriteLog("Programm start");
            SendTts("Wesel Kontroller gestartet");
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton4.Checked = true;
            radioButton3.Checked = false;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            Start();
            SetWeaselInfo();
        }

        private void Start()
        {
            int x = 20;
            
            for(int i = 0; i < _weasels.Length; i++)
            {
                _gbs[i] = new GroupBox();
                _lbs[i, 0] = new Label();
                _lbs[i, 1] = new Label();
                _lbs[i, 2] = new Label();
                _lbs[i, 3] = new Label();
                _lbs[i, 4] = new Label();
                _lbs[i, 5] = new Label();


                _gbs[i].Text = _weasels[i].weaselId;
                _gbs[i].Width = 200;
                _gbs[i].Height = 200;
                _gbs[i].Location = new System.Drawing.Point(x, 30);
                _lbs[i, 0].Location = new System.Drawing.Point(x + 20, 60);
                _lbs[i, 0].Text = "lastWaypoint:";
                _lbs[i, 0].Width = 100;
                _lbs[i, 1].Location = new System.Drawing.Point(x + 20 + 100, 60);
                _lbs[i, 1].Text = "";
                _lbs[i, 1].Width = 50;
                _lbs[i, 2].Location = new System.Drawing.Point(x + 20, 85);
                _lbs[i, 2].Text = "battery:";
                _lbs[i, 2].Width = 50;
                _lbs[i, 3].Location = new System.Drawing.Point(x + 20 + 100, 85);
                _lbs[i, 3].Text = "" + "%";
                _lbs[i, 3].Width = 50;
                _lbs[i, 4].Location = new System.Drawing.Point(x + 20, 110);
                _lbs[i, 4].Text = "online:";
                _lbs[i, 4].Width = 50;
                _lbs[i, 5].Location = new System.Drawing.Point(x + 20 + 100, 110);
                _lbs[i, 5].Text = "";
                _lbs[i, 5].Width = 50;
                Controls.Add(_lbs[i, 0]);
                Controls.Add(_lbs[i, 1]);
                Controls.Add(_lbs[i, 2]);
                Controls.Add(_lbs[i, 3]);
                Controls.Add(_lbs[i, 4]);
                Controls.Add(_lbs[i, 5]);
                Controls.Add(_gbs[i]);
                x = 200 * (i + 1) + 20;
            }
            
        }

        public static void WriteLog(string text)
        {
            File.AppendAllText(LogPath, DateTime.Now.ToString() + " - " + text + "\n");
        }

        private static void SendWeaselHome()
        {
            _weasels[0].SetPosition(39);
            _weasels[1].SetPosition(48);
            _weasels[2].SetPosition(46);
        }

        private static string GetWeaselInfo(Weasel weasel)
        {
            try
            {
                var address = ConIpAddress + "/weasels/" + weasel.weaselId;

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
                WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        private static void SendTts(string message)
        {
            try
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["message"] = message;

                    wb.UploadValues("http://10.0.9.61:1880/http/tts/", "POST", data);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetWeaselInfo()
        {
            try
            {
                for (int i = 0; i < _weasels.Length; i++)
                {
                    _weasels[i] = JsonConvert.DeserializeObject<Weasel>(GetWeaselInfo(_weasels[i]));
                    _lbs[i, 1].Text = _weasels[i].lastWaypoint.ToString();
                    _lbs[i, 3].Text = _weasels[i].battery.ToString() + "%";
                    _lbs[i, 5].Text = _weasels[i].online.ToString();
                }

                lbLastRef.Text = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FillLogBox();
            SetWeaselInfo();
            if (radioButton3.Checked) FinCha();
            if (radioButton2.Checked) Tick();
            
        }

        private void FillLogBox()
        {
            var temp = File.ReadAllLines(LogPath);
            Array.Reverse(temp);
            richTextBox1.Lines = temp;
        }

        private void Tick()
        {
            if (_lsbt) return;
            for (int i = 0; i < _weasels.Length; i++)
            {
                if (_weasels[i].battery < 80)
                {
                    for (int x = 0; x < _chargingPoints.Length; x++)
                    {
                        SetWeaselInfo();
                        if (IsPositionClear(_chargingPoints[x][0]))
                        {
                            Laden(i);
                            break;
                        }
                    }
                }

            }

        }

        private void FinCha()
        {
            try
            {
                for (int i = 0; i < _weasels.Length; i++)
                {
                    for(int x = 0; x < _chargingPoints.Length; x++)
                    {
                        if(_chargingPoints[x].Contains(_weasels[i].lastWaypoint) && _weasels[i].battery == 100)
                        {
                            if (_finChaBool)
                            {
                                WriteLog("Weasel " + _weasels[i].weaselId + " fertig geladen!");

                                _finChaBool = false;
                            }

                            if (_counter <= 600)
                            {
                                _counter++;
                                return;
                            }

                            WriteLog("10 Minuten Counter abgelaufen!");
                            SendTts("Weasel " + _weasels[i].weaselId + " fertig geladen!");
                            _counter = 0;
                            _lsbt = false;
                            _finChaBool = true;

                            for (int z = 0; z < _parkPositionen.Length; z++)
                            {
                                if (IsPositionClear(_parkPositionen[z]))
                                {
                                    _weasels[i].SetPosition(_parkPositionen[z]);
                                }
                            }
                        }
                        
                    }
                    
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void Laden(int xi)
        {
            try
            {
                WriteLog("Weasel " + _weasels[xi].weaselId + " muss laden!");

                for (int i = 0; i < _problemBereiche.Length; i++)
                {
                    for (int x = 0; x < _problemBereiche[i].Length; x++)
                    {
                        SetWeaselInfo();
                        if (!IsPositionClear(_problemBereiche[i][x]))
                        {
                            Weasel w = WhoIsOn(_problemBereiche[i][x]);
                            for (int z = 0; z < _parkPositionen.Length; z++)
                            {
                                if (IsPositionClear(_parkPositionen[z]))
                                {
                                    w.SetPositionAndWait(_parkPositionen[z]);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < _chargingPoints.Length; i++)
                {
                    Dictionary<int, Weasel> tempPositions = new Dictionary<int, Weasel>();
                    for (int x = 1; x < _chargingPoints[i].Length; x++)
                    {
                        SetWeaselInfo();
                        if (!IsPositionClear(_chargingPoints[i][x]))
                        {
                            Weasel w = WhoIsOn(_chargingPoints[i][x]);
                            bool pPosition = false;
                            
                            for (int z = 0; z < _parkPositionen.Length; z++)
                            {
                                if (IsPositionClear(_parkPositionen[z]))
                                {
                                    pPosition = true;
                                    w.SetPositionAndWait(_parkPositionen[z]);
                                }
                            }

                            if (!pPosition)
                            {
                                tempPositions.Add(_weasels[xi].lastWaypoint, w);
                                w.SetPositionAndWaitBefore(_weasels[xi].lastWaypoint);
                            }
                                

                        }
                    }

                    if (_chargingPoints[i][0] == 40)
                        _weasels[xi].SetPositionAndWait(39);
                    _weasels[xi].SetPositionAndWait(_chargingPoints[i][0]);
                    //if (tempPositions.Count != 0)
                    //{
                    //    foreach(var item in tempPositions)
                    //    {
                    //        int key = item.Key;
                    //        item.Value.SetPositionAndWait(key);
                    //    }
                    //}
                    
                }
                
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsPositionClear(int wp)
        {
            try
            {
                for (int i = 0; i < _weasels.Length; i++)
                {
                    if (_weasels[i].lastWaypoint == wp)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        private static Weasel WhoIsOn(int wp)
        {
            try
            {
                for (int i = 0; i < _weasels.Length; i++)
                {
                    if (_weasels[i].lastWaypoint == wp)
                        return _weasels[i];
                }
                return null;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendWeaselHome();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetWeaselInfo();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteLog("Programm wird beendet!");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteLog("Programm beendet!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            File.WriteAllText(LogPath, "");
        }
    }
}
