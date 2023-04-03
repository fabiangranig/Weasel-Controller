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
        private ListBox _listBox_Destinations;
        private Label _label_Destinations;
        private Weasel[] _Weasels;
        private Button btn_StopMove;
        private WeaselMovementHandler[] _WeaselMovementHandlers;

        public WeaselControlPanel(ref Map map1, ref Weasel[] weasels1)
        {
            //Get weasels with map
            _WeaselMap = map1;
            _Weasels = weasels1;

            //Get the controls from the editor
            InitializeComponent();

            //Get the weasels into the dropdown and create Handlers
            _WeaselMovementHandlers = new WeaselMovementHandler[_Weasels.Length];
            for (int i = 0; i < _Weasels.Length; i++)
            {
                _WeaselDropDown.Items.Add(_Weasels[i].WeaselName);
                _WeaselMovementHandlers[i] = new WeaselMovementHandler(ref _Weasels[i], ref _WeaselMap);
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

            //Get the destination table
            _listBox_Destinations.Items.Clear();
            if(_WeaselDropDown.SelectedIndex != -1)
            {
                for (int i = 0; i < _Weasels[_WeaselDropDown.SelectedIndex]._Destinations.Count; i++)
                {
                    _listBox_Destinations.Items.Add(_Weasels[_WeaselDropDown.SelectedIndex]._Destinations[i]);
                }
            }

        }

        private void SendWeasel(int selected_weasel, int goal)
        {
            _WeaselMovementHandlers[selected_weasel].MoveWeasel(goal);
        }

        private void btnClick_SendWeasel(object sender, EventArgs e)
        {
            _Weasels[_WeaselDropDown.SelectedIndex]._Destinations.Add(Int32.Parse(_txtBox_Position.Text));
        }

        private void btnClick_WeaselHome(object sender, EventArgs e)
        {
            for (int i = 0; i < _Weasels.Length; i++)
            {
                _Weasels[i]._Destinations.Add(_Weasels[i]._HomePosition);
            }
        }

        private void btn_StopMove_Click(object sender, EventArgs e)
        {
            object remove_object = _listBox_Destinations.Items[Int32.Parse(_txtBox_Position.Text)];
            int remove = Convert.ToInt32(remove_object);
            _WeaselMovementHandlers[_WeaselDropDown.SelectedIndex].StopMovement(remove);
        }

        private void InitializeComponent()
        {
            this._WeaselDropDown = new System.Windows.Forms.ComboBox();
            this._txtBox_Position = new System.Windows.Forms.TextBox();
            this.btn_SendWeasel = new System.Windows.Forms.Button();
            this.btn_SendHome = new System.Windows.Forms.Button();
            this._listBox_Destinations = new System.Windows.Forms.ListBox();
            this._label_Destinations = new System.Windows.Forms.Label();
            this.btn_StopMove = new System.Windows.Forms.Button();
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
            // _listBox_Destinations
            // 
            this._listBox_Destinations.FormattingEnabled = true;
            this._listBox_Destinations.Location = new System.Drawing.Point(297, 39);
            this._listBox_Destinations.Name = "_listBox_Destinations";
            this._listBox_Destinations.Size = new System.Drawing.Size(144, 147);
            this._listBox_Destinations.TabIndex = 5;
            // 
            // _label_Destinations
            // 
            this._label_Destinations.AutoSize = true;
            this._label_Destinations.Location = new System.Drawing.Point(294, 15);
            this._label_Destinations.Name = "_label_Destinations";
            this._label_Destinations.Size = new System.Drawing.Size(68, 13);
            this._label_Destinations.TabIndex = 6;
            this._label_Destinations.Text = "Destinations:";
            // 
            // btn_StopMove
            // 
            this.btn_StopMove.Location = new System.Drawing.Point(460, 39);
            this.btn_StopMove.Name = "btn_StopMove";
            this.btn_StopMove.Size = new System.Drawing.Size(135, 23);
            this.btn_StopMove.TabIndex = 7;
            this.btn_StopMove.Text = "Stop Movement!";
            this.btn_StopMove.UseVisualStyleBackColor = true;
            this.btn_StopMove.Click += new System.EventHandler(this.btn_StopMove_Click);
            // 
            // WeaselControlPanel
            // 
            this.ClientSize = new System.Drawing.Size(664, 214);
            this.Controls.Add(this.btn_StopMove);
            this.Controls.Add(this._label_Destinations);
            this.Controls.Add(this._listBox_Destinations);
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
