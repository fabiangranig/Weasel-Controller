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
        private Weasel[] _Weasels;
        private Button btn_AddPoint;
        private Button btn_SaveMap;
        private Button btn_MapLoad;
        private List<Label> _Labels_Waypoints;
        private List<Label> _Labels_Lanes;
        private Button btn_LaneAddHorizontal;
        private Button btn_LaneAddVertical;
        private GroupBox groupBox_Builder;
        private GroupBox groupBox_Savestate;
        private Timer tmr;

        public WeaselMapPanel(ref Map WeaselMap1, ref Weasel[] Weasels1)
        {
            //Get Map and Weasels
            _WeaselMap = WeaselMap1;
            _Weasels = Weasels1;

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

            //Load the map on startup
            btn_MapLoad_Click(new Object(), new EventArgs());
        }

        private void InitializeComponent()
        {
            this.btn_AddPoint = new System.Windows.Forms.Button();
            this.btn_SaveMap = new System.Windows.Forms.Button();
            this.btn_MapLoad = new System.Windows.Forms.Button();
            this.btn_LaneAddHorizontal = new System.Windows.Forms.Button();
            this.btn_LaneAddVertical = new System.Windows.Forms.Button();
            this.groupBox_Builder = new System.Windows.Forms.GroupBox();
            this.groupBox_Savestate = new System.Windows.Forms.GroupBox();
            this.groupBox_Builder.SuspendLayout();
            this.groupBox_Savestate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_AddPoint
            // 
            this.btn_AddPoint.Location = new System.Drawing.Point(6, 21);
            this.btn_AddPoint.Name = "btn_AddPoint";
            this.btn_AddPoint.Size = new System.Drawing.Size(124, 27);
            this.btn_AddPoint.TabIndex = 0;
            this.btn_AddPoint.Text = "Add new point";
            this.btn_AddPoint.UseVisualStyleBackColor = true;
            this.btn_AddPoint.Click += new System.EventHandler(this.btn_AddPoint_Click);
            // 
            // btn_SaveMap
            // 
            this.btn_SaveMap.Enabled = false;
            this.btn_SaveMap.Location = new System.Drawing.Point(6, 23);
            this.btn_SaveMap.Name = "btn_SaveMap";
            this.btn_SaveMap.Size = new System.Drawing.Size(124, 25);
            this.btn_SaveMap.TabIndex = 1;
            this.btn_SaveMap.Text = "Save";
            this.btn_SaveMap.UseVisualStyleBackColor = true;
            this.btn_SaveMap.Click += new System.EventHandler(this.btn_SaveMap_Click);
            // 
            // btn_MapLoad
            // 
            this.btn_MapLoad.Location = new System.Drawing.Point(136, 23);
            this.btn_MapLoad.Name = "btn_MapLoad";
            this.btn_MapLoad.Size = new System.Drawing.Size(124, 25);
            this.btn_MapLoad.TabIndex = 2;
            this.btn_MapLoad.Text = "Load";
            this.btn_MapLoad.UseVisualStyleBackColor = true;
            this.btn_MapLoad.Click += new System.EventHandler(this.btn_MapLoad_Click);
            // 
            // btn_LaneAddHorizontal
            // 
            this.btn_LaneAddHorizontal.Location = new System.Drawing.Point(136, 21);
            this.btn_LaneAddHorizontal.Name = "btn_LaneAddHorizontal";
            this.btn_LaneAddHorizontal.Size = new System.Drawing.Size(124, 27);
            this.btn_LaneAddHorizontal.TabIndex = 3;
            this.btn_LaneAddHorizontal.Text = "Lane -> -";
            this.btn_LaneAddHorizontal.UseVisualStyleBackColor = true;
            this.btn_LaneAddHorizontal.Click += new System.EventHandler(this.btn_LaneAddHorizontal_Click);
            // 
            // btn_LaneAddVertical
            // 
            this.btn_LaneAddVertical.Location = new System.Drawing.Point(266, 21);
            this.btn_LaneAddVertical.Name = "btn_LaneAddVertical";
            this.btn_LaneAddVertical.Size = new System.Drawing.Size(124, 27);
            this.btn_LaneAddVertical.TabIndex = 4;
            this.btn_LaneAddVertical.Text = "Lane -> |";
            this.btn_LaneAddVertical.UseVisualStyleBackColor = true;
            this.btn_LaneAddVertical.Click += new System.EventHandler(this.btn_LaneAddVertical_Click);
            // 
            // groupBox_Builder
            // 
            this.groupBox_Builder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox_Builder.Controls.Add(this.btn_AddPoint);
            this.groupBox_Builder.Controls.Add(this.btn_LaneAddVertical);
            this.groupBox_Builder.Controls.Add(this.btn_LaneAddHorizontal);
            this.groupBox_Builder.Location = new System.Drawing.Point(12, 12);
            this.groupBox_Builder.Name = "groupBox_Builder";
            this.groupBox_Builder.Size = new System.Drawing.Size(402, 71);
            this.groupBox_Builder.TabIndex = 5;
            this.groupBox_Builder.TabStop = false;
            this.groupBox_Builder.Text = "Builder";
            // 
            // groupBox_Savestate
            // 
            this.groupBox_Savestate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox_Savestate.Controls.Add(this.btn_SaveMap);
            this.groupBox_Savestate.Controls.Add(this.btn_MapLoad);
            this.groupBox_Savestate.Location = new System.Drawing.Point(420, 12);
            this.groupBox_Savestate.Name = "groupBox_Savestate";
            this.groupBox_Savestate.Size = new System.Drawing.Size(270, 71);
            this.groupBox_Savestate.TabIndex = 6;
            this.groupBox_Savestate.TabStop = false;
            this.groupBox_Savestate.Text = "Savestate";
            // 
            // WeaselMapPanel
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(880, 549);
            this.Controls.Add(this.groupBox_Savestate);
            this.Controls.Add(this.groupBox_Builder);
            this.MaximumSize = new System.Drawing.Size(898, 596);
            this.MinimumSize = new System.Drawing.Size(898, 596);
            this.Name = "WeaselMapPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weasel Map Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WeaselMapPanel_FormClosing);
            this.groupBox_Builder.ResumeLayout(false);
            this.groupBox_Savestate.ResumeLayout(false);
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
            temp.AccessibleDescription = "WayPointEdit";
            _Labels_Waypoints.Add(temp);
            this.Controls.Add(temp);
        }

        private void Label_Options(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            GeneralLabelEdit LE = new GeneralLabelEdit(ref temp, ref _WeaselMap, temp.AccessibleDescription);
            LE.ShowDialog();
        }

        private void btn_SaveMap_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("MapPanel_Waypoints.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            //Write all important label properties to the file
            for(int i = 0; i < _Labels_Waypoints.Count; i++)
            {
                if(_Labels_Waypoints[i].Visible == true)
                {
                    sw.WriteLine(_Labels_Waypoints[i].Text);
                    sw.WriteLine(_Labels_Waypoints[i].Location.X + " " + _Labels_Waypoints[i].Location.Y);
                }
            }

            sw.Close();
            fs.Close();

            FileStream fs2 = new FileStream("MapPanel_Lanes.txt", FileMode.Create);
            StreamWriter sw2 = new StreamWriter(fs2);

            //Write all important label properties to the file
            for (int i = 0; i < _Labels_Lanes.Count; i++)
            {
                if(_Labels_Lanes[i].Visible == true)
                {
                    sw2.WriteLine(_Labels_Lanes[i].Size.Width + " " + _Labels_Lanes[i].Size.Height);
                    sw2.WriteLine(_Labels_Lanes[i].Location.X + " " + _Labels_Lanes[i].Location.Y);
                }
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
                newLabel.AccessibleDescription = "WayPointEdit";
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
                newLabel.AccessibleDescription = "LaneEdit";
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
                //Update waypoints colour accourding to current status
                if(_Labels_Waypoints[i].Text != "empty")
                {
                    Waypoint wp = _WeaselMap.FindWayPoint(Int32.Parse(_Labels_Waypoints[i].Text));
                    _Labels_Waypoints[i].BackColor = wp._Reserved_Color;

                    //When an weasel is on the node make the text of the node red
                    for(int u = 0; u < _Weasels.Length; u++)
                    {
                        //Set where the weasel is located
                        if(_Weasels[u]._LastPosition == Int32.Parse(_Labels_Waypoints[i].Text))
                        {
                            //When the weasel has a box on top
                            if(_Weasels[u]._HasBox == true)
                            {
                                _Labels_Waypoints[i].ForeColor = Color.Red;
                            }
                            else
                            {
                                _Labels_Waypoints[i].ForeColor = Color.DarkGreen;
                            }
                            break;
                        }
                        else
                        {
                            _Labels_Waypoints[i].ForeColor = Color.Black;
                        }
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
            newLabel.AccessibleDescription = "LaneEdit";
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
            newLabel.AccessibleDescription = "LaneEdit";
            _Labels_Lanes.Add(newLabel);
            this.Controls.Add(_Labels_Lanes[_Labels_Lanes.Count - 1]);
        }

        private void Label_Options_Lanes(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            GeneralLabelEdit LE = new GeneralLabelEdit(ref temp, ref _WeaselMap, temp.AccessibleDescription);
            LE.ShowDialog();
        }
    }
}
