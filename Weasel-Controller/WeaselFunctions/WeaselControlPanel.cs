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
        private Button btn_RandomPosition;
        private GroupBox groupBox_MoveWeasel;
        private Label _lbl_Online;
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

            //Select the first weasel into the combobox
            _WeaselDropDown.SelectedIndex = 0;
            btn_SendWeasel.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_RandomPosition.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_SendHome.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_StopMove.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored; ;

            //Create a Timer which is working on next paths
            System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 500;
            tmr.Tick += PathWorker;
            tmr.Start();

            //Timer which checks if weasel got online
            System.Windows.Forms.Timer tmr2 = new System.Windows.Forms.Timer();
            tmr2.Interval = 500;
            tmr2.Tick += CheckOnlineStatus;
            tmr2.Start();
        }

        //Works through all Weasels and figures out which one needs to reposition
        private void PathWorker(object sender, EventArgs e)
        {
            //Gets paths and lets the weasels drive
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
                            _WeaselMovementHandlers[i].MoveWeasel(_Weasels[i]._Destinations[0]);
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

        private void CheckOnlineStatus(object sender, EventArgs e)
        {
            if (_Weasels[_WeaselDropDown.SelectedIndex].AppOnline == true)
            {
                _lbl_Online.BackColor = Color.Green;
            }
            else
            {
                _lbl_Online.BackColor = Color.Red;
            }
        }

        private void btnClick_SendWeasel(object sender, EventArgs e)
        {
            _Weasels[_WeaselDropDown.SelectedIndex]._Destinations.Add(Int32.Parse(_txtBox_Position.Text));
        }

        private void btn_RandomPosition_Click(object sender, EventArgs e)
        {
            Random Filler = new Random();
            for(int i = 0; i < 30; i++)
            {
                int id = _WeaselMap.FindWayPoint(Filler.Next(1,50))._PointId;
                _Weasels[_WeaselDropDown.SelectedIndex]._Destinations.Add(id);
            }
        }

        private void btnClick_WeaselHome(object sender, EventArgs e)
        {
            _Weasels[_WeaselDropDown.SelectedIndex]._Destinations.Add(_Weasels[_WeaselDropDown.SelectedIndex]._HomePosition);
        }

        private void btn_StopMove_Click(object sender, EventArgs e)
        {
            if(_listBox_Destinations.Items.Count > 0)
            {
                _WeaselMovementHandlers[_WeaselDropDown.SelectedIndex].DestroyAction();
                _Weasels[_WeaselDropDown.SelectedIndex]._Destinations.RemoveAt(0);
            }
        }

        private void _WeaselDropDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = '\0';
        }

        private void _txtBox_Position_KeyPress(object sender, KeyPressEventArgs e)
        {
            int temp;
            bool switcher = Int32.TryParse(Convert.ToString(e.KeyChar), out temp);

            if(switcher == false && Convert.ToInt32(e.KeyChar) != 8)
            {
                e.KeyChar = '\0';
            }
        }

        private void _WeaselDropDown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btn_SendWeasel.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_RandomPosition.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_SendHome.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_StopMove.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
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
            this.btn_RandomPosition = new System.Windows.Forms.Button();
            this.groupBox_MoveWeasel = new System.Windows.Forms.GroupBox();
            this._lbl_Online = new System.Windows.Forms.Label();
            this.groupBox_MoveWeasel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _WeaselDropDown
            // 
            this._WeaselDropDown.BackColor = System.Drawing.SystemColors.Window;
            this._WeaselDropDown.FormattingEnabled = true;
            this._WeaselDropDown.Location = new System.Drawing.Point(6, 19);
            this._WeaselDropDown.Name = "_WeaselDropDown";
            this._WeaselDropDown.Size = new System.Drawing.Size(95, 21);
            this._WeaselDropDown.TabIndex = 1;
            this._WeaselDropDown.SelectionChangeCommitted += new System.EventHandler(this._WeaselDropDown_SelectionChangeCommitted);
            this._WeaselDropDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._WeaselDropDown_KeyPress);
            // 
            // _txtBox_Position
            // 
            this._txtBox_Position.Location = new System.Drawing.Point(107, 19);
            this._txtBox_Position.Name = "_txtBox_Position";
            this._txtBox_Position.Size = new System.Drawing.Size(89, 20);
            this._txtBox_Position.TabIndex = 2;
            this._txtBox_Position.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtBox_Position_KeyPress);
            // 
            // btn_SendWeasel
            // 
            this.btn_SendWeasel.Location = new System.Drawing.Point(6, 45);
            this.btn_SendWeasel.Name = "btn_SendWeasel";
            this.btn_SendWeasel.Size = new System.Drawing.Size(227, 23);
            this.btn_SendWeasel.TabIndex = 3;
            this.btn_SendWeasel.Text = "Send Weasel!";
            this.btn_SendWeasel.UseVisualStyleBackColor = true;
            this.btn_SendWeasel.Click += new System.EventHandler(this.btnClick_SendWeasel);
            // 
            // btn_SendHome
            // 
            this.btn_SendHome.Location = new System.Drawing.Point(6, 71);
            this.btn_SendHome.Name = "btn_SendHome";
            this.btn_SendHome.Size = new System.Drawing.Size(227, 23);
            this.btn_SendHome.TabIndex = 4;
            this.btn_SendHome.Text = "Send Home!";
            this.btn_SendHome.UseVisualStyleBackColor = true;
            this.btn_SendHome.Click += new System.EventHandler(this.btnClick_WeaselHome);
            // 
            // _listBox_Destinations
            // 
            this._listBox_Destinations.FormattingEnabled = true;
            this._listBox_Destinations.Location = new System.Drawing.Point(239, 32);
            this._listBox_Destinations.Name = "_listBox_Destinations";
            this._listBox_Destinations.Size = new System.Drawing.Size(144, 147);
            this._listBox_Destinations.TabIndex = 5;
            // 
            // _label_Destinations
            // 
            this._label_Destinations.AutoSize = true;
            this._label_Destinations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._label_Destinations.Location = new System.Drawing.Point(239, 19);
            this._label_Destinations.Name = "_label_Destinations";
            this._label_Destinations.Size = new System.Drawing.Size(81, 13);
            this._label_Destinations.TabIndex = 6;
            this._label_Destinations.Text = "Destinations:";
            // 
            // btn_StopMove
            // 
            this.btn_StopMove.Location = new System.Drawing.Point(6, 129);
            this.btn_StopMove.Name = "btn_StopMove";
            this.btn_StopMove.Size = new System.Drawing.Size(227, 23);
            this.btn_StopMove.TabIndex = 7;
            this.btn_StopMove.Text = "Stop Movement!";
            this.btn_StopMove.UseVisualStyleBackColor = true;
            this.btn_StopMove.Click += new System.EventHandler(this.btn_StopMove_Click);
            // 
            // btn_RandomPosition
            // 
            this.btn_RandomPosition.Location = new System.Drawing.Point(6, 100);
            this.btn_RandomPosition.Name = "btn_RandomPosition";
            this.btn_RandomPosition.Size = new System.Drawing.Size(227, 23);
            this.btn_RandomPosition.TabIndex = 8;
            this.btn_RandomPosition.Text = "Send to Random Position!";
            this.btn_RandomPosition.UseVisualStyleBackColor = true;
            this.btn_RandomPosition.Click += new System.EventHandler(this.btn_RandomPosition_Click);
            // 
            // groupBox_MoveWeasel
            // 
            this.groupBox_MoveWeasel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox_MoveWeasel.Controls.Add(this._lbl_Online);
            this.groupBox_MoveWeasel.Controls.Add(this._WeaselDropDown);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_StopMove);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_RandomPosition);
            this.groupBox_MoveWeasel.Controls.Add(this._label_Destinations);
            this.groupBox_MoveWeasel.Controls.Add(this._txtBox_Position);
            this.groupBox_MoveWeasel.Controls.Add(this._listBox_Destinations);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_SendWeasel);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_SendHome);
            this.groupBox_MoveWeasel.Location = new System.Drawing.Point(21, 12);
            this.groupBox_MoveWeasel.Name = "groupBox_MoveWeasel";
            this.groupBox_MoveWeasel.Size = new System.Drawing.Size(392, 190);
            this.groupBox_MoveWeasel.TabIndex = 9;
            this.groupBox_MoveWeasel.TabStop = false;
            this.groupBox_MoveWeasel.Text = "Move Weasel";
            // 
            // _lbl_Online
            // 
            this._lbl_Online.AutoSize = true;
            this._lbl_Online.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._lbl_Online.Location = new System.Drawing.Point(202, 22);
            this._lbl_Online.Name = "_lbl_Online";
            this._lbl_Online.Size = new System.Drawing.Size(25, 13);
            this._lbl_Online.TabIndex = 9;
            this._lbl_Online.Text = "      ";
            // 
            // WeaselControlPanel
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(434, 215);
            this.Controls.Add(this.groupBox_MoveWeasel);
            this.MaximumSize = new System.Drawing.Size(450, 254);
            this.MinimumSize = new System.Drawing.Size(450, 254);
            this.Name = "WeaselControlPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weasel Control Panel";
            this.groupBox_MoveWeasel.ResumeLayout(false);
            this.groupBox_MoveWeasel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
