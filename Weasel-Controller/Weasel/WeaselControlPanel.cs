using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Weasel_Controller
{
    class WeaselControlPanel : Form
    {
        private Map _WeaselMap;
        private ComboBox _WeaselDropDown;
        private TextBox _txtBox_Position;
        private Button btn_SendWeasel;
        private Button btn_SendHome;
        private Weasel[] _Weasels;

        public WeaselControlPanel(ref Map map1, ref Weasel[] weasels1)
        {
            //Get weasels with map
            _WeaselMap = map1;
            _Weasels = weasels1;

            //Get the controls from the editor
            InitializeComponent();

            //Get the weasels into the dropdown
            for (int i = 0; i < _Weasels.Length; i++)
            {
                _WeaselDropDown.Items.Add(_Weasels[i].WeaselName);
            }

            //Create a Timer which is working on next paths
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 500;
            tmr.Tick += PathWorker;
            tmr.Start();
        }

        //Works through all Weasels and figures out which one needs to reposition
        private void PathWorker(object sender, EventArgs e)
        {
            for(int i = 0; i < _Weasels.Length; i++)
            {
                if(_Weasels[i]._Destinations.Count > 0)
                {
                    //When position is reached remove the goal
                    while(_Weasels[i]._Destinations[0] == _Weasels[i]._LastPosition)
                    {
                        _Weasels[i]._Destinations.RemoveAt(0);

                        if (!(_Weasels[i]._Destinations.Count > 0))
                        {
                            break;
                        }
                    }

                    //Safety check to reduce crashes
                    if(_Weasels[i]._Destinations.Count > 0)
                    {
                        //Set position if not set
                        if (_Weasels[i]._Destinations[0] != _Weasels[i]._Destination)
                        {
                            SendWeasel(i, _Weasels[i]._Destinations[0]);
                            _Weasels[i]._Destination = _Weasels[i]._Destinations[0];
                        }
                    }
                }
            }
        }

        private void SendWeasel(int selected_weasel, int position)
        {
            MovePartly(selected_weasel, position);
        }

        private void btnClick_SendWeasel(object sender, EventArgs e)
        {
            _Weasels[_WeaselDropDown.SelectedIndex]._Destinations.Add(Int32.Parse(_txtBox_Position.Text));
        }


        private void btnClick_WeaselHome(object sender, EventArgs e)
        {
            for(int i = 0; i < _Weasels.Length; i++)
            {
                _Weasels[i]._Destinations.Add(_Weasels[i]._HomePosition);
            }
        }

        private void MovePartly(int selected_weasel, int goal)
        {
            Thread t1 = new Thread(() => MovePartlyBackend(selected_weasel, goal));
            t1.Start();
        }

        private void MovePartlyBackend(int weasel, int goal)
        {
            //Set goal to find if the right parts where found
            int last_goal = _Weasels[weasel]._LastPosition;

            while (_Weasels[weasel]._LastPosition != goal)
            {
                //Get the new route to the goal
                int[] route = _WeaselMap.FreePath(_Weasels[weasel]._LastPosition, goal);
                
                //Check if there is an route and if last goal was reached
                if(route[0] != -1 && _Weasels[weasel]._LastPosition == last_goal)
                {
                    //Check how much of the route is possible
                    int[] route2 = _WeaselMap.possibleRoute(route);

                    //When is is not the same position move
                    if(route2.Length > 1)
                    {
                        //Move to that position
                        _WeaselMap.ReserveArr(route2, _Weasels[weasel]._Colored);
                        MoveThroughCordinates(route2, weasel);
                        last_goal = route2[route2.Length - 1];
                    }
                }

                //Not overusing processing units
                Thread.Sleep(100);
            }
        }

        public void MoveThroughCordinates(int[] input, int weasel)
        {
            //Move weasel through set of cordinates
            Thread t1 = new Thread(() => MoveThroughCordinatesBackend(input, weasel));
            t1.Start();

            //When the weasels are not connected, simulate movement
            if(_Weasels[weasel].AppOnline == false)
            {
                Thread t2 = new Thread(() => OfflineMover(weasel, input));
                t2.Start();
            }
        }

        private void MoveThroughCordinatesBackend(int[] path, int weasel)
        {
            //When there is only one or two cordinates
            if (path.Length < 3)
            {
                _Weasels[weasel].SetPosition(path[path.Length - 1]);
                return;
            }

            int o = 0;
            while (_Weasels[weasel]._LastPosition != path[path.Length - 1])
            {
                if (_Weasels[weasel]._LastPosition == path[o])
                {
                    _Weasels[weasel].SetPosition(path[o + 2]);

                    if (path[o + 2] == path[path.Length - 1])
                    {
                        break;
                    }
                    o++;
                }

                //To not overuse processing units
                Thread.Sleep(100);
            }
        }

        private void OfflineMover(int weasel, int[] path)
        {
            for(int i = 0; i < path.Length; i++)
            {
                Thread.Sleep(1000);
                _Weasels[weasel]._LastPosition = path[i];
            }
        }

        private void InitializeComponent()
        {
            this._WeaselDropDown = new System.Windows.Forms.ComboBox();
            this._txtBox_Position = new System.Windows.Forms.TextBox();
            this.btn_SendWeasel = new System.Windows.Forms.Button();
            this.btn_SendHome = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _WeaselDropDown
            // 
            this._WeaselDropDown.FormattingEnabled = true;
            this._WeaselDropDown.Location = new System.Drawing.Point(12, 12);
            this._WeaselDropDown.Name = "_WeaselDropDown";
            this._WeaselDropDown.Size = new System.Drawing.Size(121, 21);
            this._WeaselDropDown.TabIndex = 1;
            // 
            // _txtBox_Position
            // 
            this._txtBox_Position.Location = new System.Drawing.Point(139, 13);
            this._txtBox_Position.Name = "_txtBox_Position";
            this._txtBox_Position.Size = new System.Drawing.Size(100, 20);
            this._txtBox_Position.TabIndex = 2;
            // 
            // btn_SendWeasel
            // 
            this.btn_SendWeasel.Location = new System.Drawing.Point(21, 39);
            this.btn_SendWeasel.Name = "btn_SendWeasel";
            this.btn_SendWeasel.Size = new System.Drawing.Size(209, 23);
            this.btn_SendWeasel.TabIndex = 3;
            this.btn_SendWeasel.Text = "Send Weasel!";
            this.btn_SendWeasel.UseVisualStyleBackColor = true;
            this.btn_SendWeasel.Click += new System.EventHandler(this.btnClick_SendWeasel);
            // 
            // btn_SendHome
            // 
            this.btn_SendHome.Location = new System.Drawing.Point(21, 102);
            this.btn_SendHome.Name = "btn_SendHome";
            this.btn_SendHome.Size = new System.Drawing.Size(209, 23);
            this.btn_SendHome.TabIndex = 4;
            this.btn_SendHome.Text = "Send Home!";
            this.btn_SendHome.UseVisualStyleBackColor = true;
            this.btn_SendHome.Click += new System.EventHandler(this.btnClick_WeaselHome);
            // 
            // WeaselControlPanel
            // 
            this.ClientSize = new System.Drawing.Size(274, 236);
            this.Controls.Add(this.btn_SendHome);
            this.Controls.Add(this.btn_SendWeasel);
            this.Controls.Add(this._txtBox_Position);
            this.Controls.Add(this._WeaselDropDown);
            this.Name = "WeaselControlPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
