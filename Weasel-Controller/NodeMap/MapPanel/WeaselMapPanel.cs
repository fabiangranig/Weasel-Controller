using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Weasel_Controller.NodeMap.MapPanel
{
    class WeaselMapPanel : Form
    {
        private Map _WeaselMap;
        private Button btn_AddPoint;
        private Button btn_SaveMap;
        private Button btn_MapLoad;
        private List<Label> _Labels_Waypoints;
        private List<Label> _Labels_Lanes;
        private Button btn_LaneAddHorizontal;
        private Button btn_LaneAddVertical;
        private Timer tmr;

        public WeaselMapPanel(ref Map WeaselMap1)
        {
            //Get Map
            _WeaselMap = WeaselMap1;

            //Intialise lists
            _Labels_Waypoints = new List<Label>();
            _Labels_Lanes = new List<Label>();

            //Get static components
            InitializeComponent();

            //Create an timer which checks what positions on the map are taken
            tmr = new Timer();
            tmr.Interval = 100;

            tmr.Tick += UpdatePoints;
            tmr.Start();
        }

        private void InitializeComponent()
        {
            this.btn_AddPoint = new System.Windows.Forms.Button();
            this.btn_SaveMap = new System.Windows.Forms.Button();
            this.btn_MapLoad = new System.Windows.Forms.Button();
            this.btn_LaneAddHorizontal = new System.Windows.Forms.Button();
            this.btn_LaneAddVertical = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_AddPoint
            // 
            this.btn_AddPoint.Location = new System.Drawing.Point(12, 12);
            this.btn_AddPoint.Name = "btn_AddPoint";
            this.btn_AddPoint.Size = new System.Drawing.Size(124, 27);
            this.btn_AddPoint.TabIndex = 0;
            this.btn_AddPoint.Text = "Add new point";
            this.btn_AddPoint.UseVisualStyleBackColor = true;
            this.btn_AddPoint.Click += new System.EventHandler(this.btn_AddPoint_Click);
            // 
            // btn_SaveMap
            // 
            this.btn_SaveMap.Location = new System.Drawing.Point(676, 12);
            this.btn_SaveMap.Name = "btn_SaveMap";
            this.btn_SaveMap.Size = new System.Drawing.Size(124, 25);
            this.btn_SaveMap.TabIndex = 1;
            this.btn_SaveMap.Text = "Save";
            this.btn_SaveMap.UseVisualStyleBackColor = true;
            this.btn_SaveMap.Click += new System.EventHandler(this.btn_SaveMap_Click);
            // 
            // btn_MapLoad
            // 
            this.btn_MapLoad.Location = new System.Drawing.Point(806, 12);
            this.btn_MapLoad.Name = "btn_MapLoad";
            this.btn_MapLoad.Size = new System.Drawing.Size(124, 25);
            this.btn_MapLoad.TabIndex = 2;
            this.btn_MapLoad.Text = "Load";
            this.btn_MapLoad.UseVisualStyleBackColor = true;
            this.btn_MapLoad.Click += new System.EventHandler(this.btn_MapLoad_Click);
            // 
            // btn_LaneAddHorizontal
            // 
            this.btn_LaneAddHorizontal.Location = new System.Drawing.Point(142, 12);
            this.btn_LaneAddHorizontal.Name = "btn_LaneAddHorizontal";
            this.btn_LaneAddHorizontal.Size = new System.Drawing.Size(124, 27);
            this.btn_LaneAddHorizontal.TabIndex = 3;
            this.btn_LaneAddHorizontal.Text = "Lane -> -";
            this.btn_LaneAddHorizontal.UseVisualStyleBackColor = true;
            this.btn_LaneAddHorizontal.Click += new System.EventHandler(this.btn_LaneAddHorizontal_Click);
            // 
            // btn_LaneAddVertical
            // 
            this.btn_LaneAddVertical.Location = new System.Drawing.Point(272, 12);
            this.btn_LaneAddVertical.Name = "btn_LaneAddVertical";
            this.btn_LaneAddVertical.Size = new System.Drawing.Size(124, 27);
            this.btn_LaneAddVertical.TabIndex = 4;
            this.btn_LaneAddVertical.Text = "Lane -> |";
            this.btn_LaneAddVertical.UseVisualStyleBackColor = true;
            this.btn_LaneAddVertical.Click += new System.EventHandler(this.btn_LaneAddVertical_Click);
            // 
            // WeaselMapPanel
            // 
            this.ClientSize = new System.Drawing.Size(942, 493);
            this.Controls.Add(this.btn_LaneAddVertical);
            this.Controls.Add(this.btn_LaneAddHorizontal);
            this.Controls.Add(this.btn_MapLoad);
            this.Controls.Add(this.btn_SaveMap);
            this.Controls.Add(this.btn_AddPoint);
            this.Name = "WeaselMapPanel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WeaselMapPanel_FormClosing);
            this.ResumeLayout(false);

        }

        private void btn_AddPoint_Click(object sender, EventArgs e)
        {
            //Initialise empty Label
            Label temp = new Label();
            temp.Size = new Size(40, 15);
            temp.Location = new Point(this.Size.Width / 2 - 100, this.Size.Height / 2 - 100);
            temp.Text = "empty";
            temp.BackColor = Color.LightGreen;
            temp.TextAlign = ContentAlignment.MiddleCenter;
            temp.Click += new EventHandler(Label_Options);
            _Labels_Waypoints.Add(temp);
            this.Controls.Add(temp);
        }

        private void Label_Options(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            WayPointLabelEdit LE = new WayPointLabelEdit(ref temp, ref _WeaselMap);
            LE.ShowDialog();
        }

        private void btn_SaveMap_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("MapPanel_Waypoints.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            //Write all important label properties to the file
            for(int i = 0; i < _Labels_Waypoints.Count; i++)
            {
                sw.WriteLine(_Labels_Waypoints[i].Text);
                sw.WriteLine(_Labels_Waypoints[i].Location.X + " " + _Labels_Waypoints[i].Location.Y);
            }

            sw.Close();
            fs.Close();

            FileStream fs2 = new FileStream("MapPanel_Lanes.txt", FileMode.Create);
            StreamWriter sw2 = new StreamWriter(fs2);

            //Write all important label properties to the file
            for (int i = 0; i < _Labels_Lanes.Count; i++)
            {
                sw2.WriteLine(_Labels_Lanes[i].Size.Width + " " + _Labels_Lanes[i].Size.Height);
                sw2.WriteLine(_Labels_Lanes[i].Location.X + " " + _Labels_Lanes[i].Location.Y);
            }

            sw2.Close();
            fs2.Close();
        }

        private void btn_MapLoad_Click(object sender, EventArgs e)
        {
            //Remove previous map and lanes
            for (int i = 0; i < _Labels_Waypoints.Count; i++)
            {
                this.Controls.Remove(_Labels_Waypoints[i]);
            }
            for (int i = 0; i < _Labels_Lanes.Count; i++)
            {
                this.Controls.Remove(_Labels_Lanes[i]);
            }

            _Labels_Waypoints = new List<Label>();
            _Labels_Lanes = new List<Label>();

            //Get Waypoints
            string path = "MapPanel_Waypoints.txt";
            string[] txt_MapPanel = System.IO.File.ReadAllLines(path);

            int count_Labels_Waypoints = 0;
            for(int i = 0; i < txt_MapPanel.Length; i = i + 2)
            {
                Label newLabel = new Label();
                newLabel.Text = txt_MapPanel[i];
                string[] split = txt_MapPanel[i + 1].Split(' ');
                newLabel.Location = new Point(Int32.Parse(split[0]), Int32.Parse(split[1]));
                newLabel.BackColor = Color.LightGreen;
                newLabel.Size = new Size(40, 15);
                newLabel.TextAlign = ContentAlignment.MiddleCenter;
                newLabel.Click += new EventHandler(Label_Options);
                _Labels_Waypoints.Add(newLabel);
                this.Controls.Add(_Labels_Waypoints[count_Labels_Waypoints]);
                count_Labels_Waypoints++;
            }

            //Get lanes
            string path2 = "MapPanel_Lanes.txt";
            string[] txt_MapPanel2 = System.IO.File.ReadAllLines(path2);

            for (int i = 0; i < txt_MapPanel2.Length; i = i + 2)
            {
                Label newLabel = new Label();
                newLabel.Text = "   ";
                newLabel.BackColor = Color.LightGray;
                newLabel.Click += new EventHandler(Label_Options_Lanes);
                string[] split1 = txt_MapPanel2[i + 1].Split(' ');
                newLabel.Location = new Point(Int32.Parse(split1[0]), Int32.Parse(split1[1]));
                newLabel.TextAlign = ContentAlignment.MiddleCenter;
                string[] split2 = txt_MapPanel2[i].Split(' ');
                newLabel.Size = new Size(Int32.Parse(split2[0]), Int32.Parse(split2[1]));
                _Labels_Lanes.Add(newLabel);
                this.Controls.Add(_Labels_Lanes[_Labels_Lanes.Count - 1]);
            }
        }

        private void UpdatePoints(object sender, EventArgs e)
        {
            for(int i = 0; i < _Labels_Waypoints.Count; i++)
            {
                if(_Labels_Waypoints[i].Text != "empty")
                {
                    Waypoint wp = _WeaselMap.FindWayPoint(Int32.Parse(_Labels_Waypoints[i].Text));

                    if(wp._Reserved == true)
                    {
                        _Labels_Waypoints[i].BackColor = Color.LightBlue;
                    }
                    else
                    {
                        _Labels_Waypoints[i].BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void WeaselMapPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Otherwise it will drop an reference error
            tmr.Stop();
        }

        private void btn_LaneAddHorizontal_Click(object sender, EventArgs e)
        {
            Label newLabel = new Label();
            newLabel.Text = "   ";
            newLabel.BackColor = Color.LightGray;
            newLabel.Click += new EventHandler(Label_Options_Lanes);
            newLabel.Location = new Point(this.Size.Width / 2 - 100, this.Size.Height / 2 - 100);
            newLabel.TextAlign = ContentAlignment.MiddleCenter;
            newLabel.Size = new Size(20, 5);
            _Labels_Lanes.Add(newLabel);
            this.Controls.Add(_Labels_Lanes[_Labels_Lanes.Count - 1]);
        }

        private void btn_LaneAddVertical_Click(object sender, EventArgs e)
        {
            Label newLabel = new Label();
            newLabel.Text = "   ";
            newLabel.BackColor = Color.LightGray;
            newLabel.Click += new EventHandler(Label_Options_Lanes);
            newLabel.Location = new Point(this.Size.Width / 2 - 100, this.Size.Height / 2 - 100);
            newLabel.TextAlign = ContentAlignment.MiddleCenter;
            newLabel.Size = new Size(5, 20);
            _Labels_Lanes.Add(newLabel);
            this.Controls.Add(_Labels_Lanes[_Labels_Lanes.Count - 1]);
        }

        private void Label_Options_Lanes(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            LanesLabelEdit LE = new LanesLabelEdit(ref temp);
            LE.ShowDialog();
        }
    }
}
