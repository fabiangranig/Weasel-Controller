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
        private List<Label> _Labels;
        private Timer tmr;

        public WeaselMapPanel(ref Map WeaselMap1)
        {
            //Get Map
            _WeaselMap = WeaselMap1;

            //Intialise list
            _Labels = new List<Label>();

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
            // WeaselMapPanel
            // 
            this.ClientSize = new System.Drawing.Size(942, 493);
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
            _Labels.Add(temp);
            this.Controls.Add(temp);
        }

        private void Label_Options(object sender, EventArgs e)
        {
            Label temp = (Label)sender;
            LabelEdit LE = new LabelEdit(ref temp, ref _WeaselMap);
            LE.ShowDialog();
        }

        private void btn_SaveMap_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("MapPanel.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            //Write all important label properties to the file
            for(int i = 0; i < _Labels.Count; i++)
            {
                sw.WriteLine(_Labels[i].Text);
                sw.WriteLine(_Labels[i].Location.X + " " + _Labels[i].Location.Y);
            }

            sw.Close();
            fs.Close();
        }

        private void btn_MapLoad_Click(object sender, EventArgs e)
        {
            //Remove previous map
            for (int i = 0; i < _Labels.Count; i++)
            {
                this.Controls.Remove(_Labels[i]);
            }

            _Labels = new List<Label>();

            string path = "MapPanel.txt";
            string[] txt_MapPanel = System.IO.File.ReadAllLines(path);

            int count_labels = 0;
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
                _Labels.Add(newLabel);
                this.Controls.Add(_Labels[count_labels]);
                count_labels++;
            }
        }

        private void UpdatePoints(object sender, EventArgs e)
        {
            for(int i = 0; i < _Labels.Count; i++)
            {
                if(_Labels[i].Text != "empty")
                {
                    Waypoint wp = _WeaselMap.FindWayPoint(Int32.Parse(_Labels[i].Text));

                    if(wp._Reserved == true)
                    {
                        _Labels[i].BackColor = Color.LightBlue;
                    }
                    else
                    {
                        _Labels[i].BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private void WeaselMapPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Otherwise it will drop an reference error
            tmr.Stop();
        }
    }
}
