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
        private Button btn_AdvancedMovement;
        private Button btn_RandomPositionSPL;
        private Button btn_CollectBox;
        private Label lbl_RoboDKControls;
        private WeaselMovementHandler[] _WeaselMovementHandlers;
        private Button btn_RobotRealRobot;
        private Button btn_RobotSimulation;
        private Button btn_AllWeaselsRandomPositionSPL;
        private Label lbl_AllWeaselsAction;
        private Button btn_StopAllMovement;
        private Button btn_RemoveBox;
        private KukaRoboter _KukaRobot;

        public WeaselControlPanel(ref Map map1, ref Weasel[] weasels1, ref KukaRoboter kukaRoboter1)
        {
            //Get weasels with map
            _WeaselMap = map1;
            _Weasels = weasels1;

            //Set the KukaRobot
            _KukaRobot = kukaRoboter1;

            //Get the controls from the editor
            InitializeComponent();

            //Get the weasels into the dropdown and create Handlers
            _WeaselMovementHandlers = new WeaselMovementHandler[_Weasels.Length];
            for (int i = 0; i < _Weasels.Length; i++)
            {
                _WeaselDropDown.Items.Add(_Weasels[i].WeaselName);
                _WeaselMovementHandlers[i] = new WeaselMovementHandler(ref _Weasels[i], ref _WeaselMap, ref _KukaRobot);
            }

            //Select the first weasel into the combobox
            _WeaselDropDown.SelectedIndex = 0;
            btn_SendWeasel.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_RemoveBox.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_RandomPosition.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_RandomPositionSPL.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_SendHome.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_StopMove.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_StopAllMovement.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;

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
                if(_Weasels[i]._DestinationsWithInformation.Count > 0)
                {
                    //Safety check to reduce crashes
                    if(_Weasels[i]._DestinationsWithInformation.Count > 0)
                    {
                        //Set position if not set
                        if (_Weasels[i]._DestinationsWithInformation[0].Destination != _Weasels[i]._Destination)
                        {
                            _WeaselMovementHandlers[i].MoveWeasel(_Weasels[i]._DestinationsWithInformation[0]);
                            _Weasels[i]._Destination = _Weasels[i]._DestinationsWithInformation[0].Destination;
                        }
                    }
                }
            }

            //Get the destination table
            _listBox_Destinations.Items.Clear();
            if(_WeaselDropDown.SelectedIndex != -1)
            {
                for (int i = 0; i < _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation.Count; i++)
                {
                    _listBox_Destinations.Items.Add(_Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation[i].ActionBeforeMovement + " | " + _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation[i].Destination + " | " + _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation[i].ActionAfterMovement + " |  " + _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation[i].SendBy);
                }
            }

        }

        private void CheckOnlineStatus(object sender, EventArgs e)
        {
            //Check if weasel is online
            if (_Weasels[_WeaselDropDown.SelectedIndex].AppOnline == true)
            {
                _lbl_Online.BackColor = Color.Green;
            }
            else
            {
                _lbl_Online.BackColor = Color.Red;
            }

            //Show battery for the weasel
            _lbl_Online.Text = Convert.ToString(_Weasels[_WeaselDropDown.SelectedIndex].BatteryPercentage) + "%";
        }

        private void btnClick_SendWeasel(object sender, EventArgs e)
        {
            //Check if that position exists
            Waypoint temp = _WeaselMap.FindWayPoint(Int32.Parse(_txtBox_Position.Text));

            //Add it to the route table, if it exists
            if(temp != null)
            {
                _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation.Add(new DestinationwithInformation(0, Int32.Parse(_txtBox_Position.Text)));
                return;
            }

            //When it doesn't exist show warning
            MessageBox.Show("That waypoint doesn't exist!");
        }

        private void btn_RandomPosition_Click(object sender, EventArgs e)
        {
            Random Filler = new Random();
            for(int i = 0; i < 30; i++)
            {
                int id = _WeaselMap.FindWayPoint(Filler.Next(1,50))._PointId;
                _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation.Add(new DestinationwithInformation(0, id));
            }
        }

        private void btnClick_WeaselHome(object sender, EventArgs e)
        {
            _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation.Add(new DestinationwithInformation(0, _Weasels[_WeaselDropDown.SelectedIndex]._HomePosition));
        }

        private void btn_StopMove_Click(object sender, EventArgs e)
        {
            if(_listBox_Destinations.Items.Count > 0)
            {
                _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation.RemoveAt(0);
                _WeaselMovementHandlers[_WeaselDropDown.SelectedIndex].DestroyAction();
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
            btn_RemoveBox.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_RandomPosition.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_RandomPositionSPL.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_SendHome.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_StopMove.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
            btn_StopAllMovement.BackColor = _Weasels[_WeaselDropDown.SelectedIndex]._Colored;
        }

        private void btn_AdvancedMovement_Click(object sender, EventArgs e)
        {
            WeaselsAdvancedMovement WAM = new WeaselsAdvancedMovement(ref _Weasels);
            WAM.Show();
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
            this.btn_RemoveBox = new System.Windows.Forms.Button();
            this.btn_StopAllMovement = new System.Windows.Forms.Button();
            this.btn_AllWeaselsRandomPositionSPL = new System.Windows.Forms.Button();
            this.lbl_AllWeaselsAction = new System.Windows.Forms.Label();
            this.btn_RobotRealRobot = new System.Windows.Forms.Button();
            this.btn_RobotSimulation = new System.Windows.Forms.Button();
            this.btn_CollectBox = new System.Windows.Forms.Button();
            this.lbl_RoboDKControls = new System.Windows.Forms.Label();
            this.btn_RandomPositionSPL = new System.Windows.Forms.Button();
            this.btn_AdvancedMovement = new System.Windows.Forms.Button();
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
            this._WeaselDropDown.Size = new System.Drawing.Size(95, 24);
            this._WeaselDropDown.TabIndex = 1;
            this._WeaselDropDown.SelectionChangeCommitted += new System.EventHandler(this._WeaselDropDown_SelectionChangeCommitted);
            this._WeaselDropDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._WeaselDropDown_KeyPress);
            // 
            // _txtBox_Position
            // 
            this._txtBox_Position.Location = new System.Drawing.Point(107, 19);
            this._txtBox_Position.Name = "_txtBox_Position";
            this._txtBox_Position.Size = new System.Drawing.Size(89, 22);
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
            this._listBox_Destinations.ItemHeight = 16;
            this._listBox_Destinations.Location = new System.Drawing.Point(242, 39);
            this._listBox_Destinations.Name = "_listBox_Destinations";
            this._listBox_Destinations.Size = new System.Drawing.Size(237, 100);
            this._listBox_Destinations.TabIndex = 5;
            // 
            // _label_Destinations
            // 
            this._label_Destinations.AutoSize = true;
            this._label_Destinations.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._label_Destinations.Location = new System.Drawing.Point(239, 19);
            this._label_Destinations.Name = "_label_Destinations";
            this._label_Destinations.Size = new System.Drawing.Size(238, 15);
            this._label_Destinations.TabIndex = 6;
            this._label_Destinations.Text = "Action | Movement | Action | Send by";
            // 
            // btn_StopMove
            // 
            this.btn_StopMove.Location = new System.Drawing.Point(9, 186);
            this.btn_StopMove.Name = "btn_StopMove";
            this.btn_StopMove.Size = new System.Drawing.Size(227, 23);
            this.btn_StopMove.TabIndex = 7;
            this.btn_StopMove.Text = "Stop Movement!";
            this.btn_StopMove.UseVisualStyleBackColor = true;
            this.btn_StopMove.Click += new System.EventHandler(this.btn_StopMove_Click);
            // 
            // btn_RandomPosition
            // 
            this.btn_RandomPosition.Location = new System.Drawing.Point(9, 128);
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
            this.groupBox_MoveWeasel.Controls.Add(this.btn_RemoveBox);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_StopAllMovement);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_AllWeaselsRandomPositionSPL);
            this.groupBox_MoveWeasel.Controls.Add(this.lbl_AllWeaselsAction);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_RobotRealRobot);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_RobotSimulation);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_CollectBox);
            this.groupBox_MoveWeasel.Controls.Add(this.lbl_RoboDKControls);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_RandomPositionSPL);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_AdvancedMovement);
            this.groupBox_MoveWeasel.Controls.Add(this._lbl_Online);
            this.groupBox_MoveWeasel.Controls.Add(this._WeaselDropDown);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_StopMove);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_RandomPosition);
            this.groupBox_MoveWeasel.Controls.Add(this._label_Destinations);
            this.groupBox_MoveWeasel.Controls.Add(this._txtBox_Position);
            this.groupBox_MoveWeasel.Controls.Add(this._listBox_Destinations);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_SendWeasel);
            this.groupBox_MoveWeasel.Controls.Add(this.btn_SendHome);
            this.groupBox_MoveWeasel.Location = new System.Drawing.Point(12, 12);
            this.groupBox_MoveWeasel.Name = "groupBox_MoveWeasel";
            this.groupBox_MoveWeasel.Size = new System.Drawing.Size(740, 278);
            this.groupBox_MoveWeasel.TabIndex = 9;
            this.groupBox_MoveWeasel.TabStop = false;
            this.groupBox_MoveWeasel.Text = "Move Weasel";
            // 
            // btn_RemoveBox
            // 
            this.btn_RemoveBox.Location = new System.Drawing.Point(9, 99);
            this.btn_RemoveBox.Name = "btn_RemoveBox";
            this.btn_RemoveBox.Size = new System.Drawing.Size(227, 23);
            this.btn_RemoveBox.TabIndex = 19;
            this.btn_RemoveBox.Text = "Remove box!";
            this.btn_RemoveBox.UseVisualStyleBackColor = true;
            this.btn_RemoveBox.Click += new System.EventHandler(this.btn_RemoveBox_Click);
            // 
            // btn_StopAllMovement
            // 
            this.btn_StopAllMovement.Location = new System.Drawing.Point(9, 215);
            this.btn_StopAllMovement.Name = "btn_StopAllMovement";
            this.btn_StopAllMovement.Size = new System.Drawing.Size(227, 23);
            this.btn_StopAllMovement.TabIndex = 18;
            this.btn_StopAllMovement.Text = "Stop every Movement!";
            this.btn_StopAllMovement.UseVisualStyleBackColor = true;
            this.btn_StopAllMovement.Click += new System.EventHandler(this.btn_StopAllMovement_Click);
            // 
            // btn_AllWeaselsRandomPositionSPL
            // 
            this.btn_AllWeaselsRandomPositionSPL.Location = new System.Drawing.Point(485, 121);
            this.btn_AllWeaselsRandomPositionSPL.Name = "btn_AllWeaselsRandomPositionSPL";
            this.btn_AllWeaselsRandomPositionSPL.Size = new System.Drawing.Size(227, 23);
            this.btn_AllWeaselsRandomPositionSPL.TabIndex = 17;
            this.btn_AllWeaselsRandomPositionSPL.Text = "Send to Random Position! (SPL)";
            this.btn_AllWeaselsRandomPositionSPL.UseVisualStyleBackColor = true;
            this.btn_AllWeaselsRandomPositionSPL.Click += new System.EventHandler(this.btn_AllWeaselsRandomPositionSPL_Click);
            // 
            // lbl_AllWeaselsAction
            // 
            this.lbl_AllWeaselsAction.AutoSize = true;
            this.lbl_AllWeaselsAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AllWeaselsAction.Location = new System.Drawing.Point(482, 105);
            this.lbl_AllWeaselsAction.Name = "lbl_AllWeaselsAction";
            this.lbl_AllWeaselsAction.Size = new System.Drawing.Size(92, 17);
            this.lbl_AllWeaselsAction.TabIndex = 16;
            this.lbl_AllWeaselsAction.Text = "All Weasels";
            // 
            // btn_RobotRealRobot
            // 
            this.btn_RobotRealRobot.Location = new System.Drawing.Point(598, 39);
            this.btn_RobotRealRobot.Name = "btn_RobotRealRobot";
            this.btn_RobotRealRobot.Size = new System.Drawing.Size(114, 23);
            this.btn_RobotRealRobot.TabIndex = 15;
            this.btn_RobotRealRobot.Text = "Real Robot";
            this.btn_RobotRealRobot.UseVisualStyleBackColor = true;
            this.btn_RobotRealRobot.Click += new System.EventHandler(this.btn_RobotRealRobot_Click);
            // 
            // btn_RobotSimulation
            // 
            this.btn_RobotSimulation.Location = new System.Drawing.Point(485, 39);
            this.btn_RobotSimulation.Name = "btn_RobotSimulation";
            this.btn_RobotSimulation.Size = new System.Drawing.Size(114, 23);
            this.btn_RobotSimulation.TabIndex = 14;
            this.btn_RobotSimulation.Text = "Simulation";
            this.btn_RobotSimulation.UseVisualStyleBackColor = true;
            this.btn_RobotSimulation.Click += new System.EventHandler(this.btn_RobotSimulation_Click);
            // 
            // btn_CollectBox
            // 
            this.btn_CollectBox.Location = new System.Drawing.Point(485, 68);
            this.btn_CollectBox.Name = "btn_CollectBox";
            this.btn_CollectBox.Size = new System.Drawing.Size(227, 23);
            this.btn_CollectBox.TabIndex = 13;
            this.btn_CollectBox.Text = "Collect Box";
            this.btn_CollectBox.UseVisualStyleBackColor = true;
            this.btn_CollectBox.Click += new System.EventHandler(this.btn_MoveRoboToPosition_Click);
            // 
            // lbl_RoboDKControls
            // 
            this.lbl_RoboDKControls.AutoSize = true;
            this.lbl_RoboDKControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RoboDKControls.Location = new System.Drawing.Point(482, 19);
            this.lbl_RoboDKControls.Name = "lbl_RoboDKControls";
            this.lbl_RoboDKControls.Size = new System.Drawing.Size(137, 17);
            this.lbl_RoboDKControls.TabIndex = 12;
            this.lbl_RoboDKControls.Text = "RoboDK Controls:";
            // 
            // btn_RandomPositionSPL
            // 
            this.btn_RandomPositionSPL.Location = new System.Drawing.Point(7, 157);
            this.btn_RandomPositionSPL.Name = "btn_RandomPositionSPL";
            this.btn_RandomPositionSPL.Size = new System.Drawing.Size(227, 23);
            this.btn_RandomPositionSPL.TabIndex = 11;
            this.btn_RandomPositionSPL.Text = "Send to Random Position! (SPL)";
            this.btn_RandomPositionSPL.UseVisualStyleBackColor = true;
            this.btn_RandomPositionSPL.Click += new System.EventHandler(this.btn_RandomPositionSPL_Click);
            // 
            // btn_AdvancedMovement
            // 
            this.btn_AdvancedMovement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_AdvancedMovement.Enabled = false;
            this.btn_AdvancedMovement.Location = new System.Drawing.Point(9, 244);
            this.btn_AdvancedMovement.Name = "btn_AdvancedMovement";
            this.btn_AdvancedMovement.Size = new System.Drawing.Size(227, 23);
            this.btn_AdvancedMovement.TabIndex = 10;
            this.btn_AdvancedMovement.Text = "Advanced Movement";
            this.btn_AdvancedMovement.UseVisualStyleBackColor = false;
            this.btn_AdvancedMovement.Click += new System.EventHandler(this.btn_AdvancedMovement_Click);
            // 
            // _lbl_Online
            // 
            this._lbl_Online.AutoSize = true;
            this._lbl_Online.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._lbl_Online.Location = new System.Drawing.Point(202, 22);
            this._lbl_Online.Name = "_lbl_Online";
            this._lbl_Online.Size = new System.Drawing.Size(32, 17);
            this._lbl_Online.TabIndex = 9;
            this._lbl_Online.Text = "      ";
            this._lbl_Online.Click += new System.EventHandler(this._lbl_Online_Click);
            // 
            // WeaselControlPanel
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(765, 300);
            this.Controls.Add(this.groupBox_MoveWeasel);
            this.Name = "WeaselControlPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weasel Control Panel";
            this.Shown += new System.EventHandler(this.WeaselControlPanel_Shown);
            this.groupBox_MoveWeasel.ResumeLayout(false);
            this.groupBox_MoveWeasel.PerformLayout();
            this.ResumeLayout(false);

        }

        private void btn_RandomPositionSPL_Click(object sender, EventArgs e)
        {
            SetRandomPositionSPL(_WeaselDropDown.SelectedIndex);
        }

        private void SetRandomPositionSPL(int selected_weasel)
        {
            Random Filler = new Random();
            for (int i = 0; i < 30; i++)
            {
                int id = _WeaselMap.FindWayPoint(Filler.Next(1, 50))._PointId;

                //Check of that waypoint is outside of the SPL Labor
                int[] route = _WeaselMap.FreePath(_Weasels[selected_weasel]._LastPosition, id, Color.Empty);
                bool schalter = false;
                for (int u = 0; u < route.Length; u++)
                {
                    if (route[u] == 1)
                    {
                        schalter = true;
                    }
                }

                if (schalter == false)
                {
                    _Weasels[selected_weasel]._DestinationsWithInformation.Add(new DestinationwithInformation(0, id));
                }
            }
        }

        private void btn_MoveRoboToPosition_Click(object sender, EventArgs e)
        {
            //Let the not occupied weasel get the box
            for(int i = 0; i < _Weasels.Length; i++)
            {
                if(_Weasels[i]._DestinationsWithInformation.Count == 0 && _Weasels[i]._HasBox == false)
                {
                    _Weasels[i]._DestinationsWithInformation.Add(new DestinationwithInformation(41, "Kuka1", "Server"));
                    break;
                }
            }
        }

        private void btn_RobotSimulation_Click(object sender, EventArgs e)
        {
            _KukaRobot.SwitchSimulationMode();
        }

        private void btn_RobotRealRobot_Click(object sender, EventArgs e)
        {
            _KukaRobot.SwitchRealMode();
            _KukaRobot.GreiferZu();
            _KukaRobot.GreiferAuf();
        }

        private void btn_AllWeaselsRandomPositionSPL_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < _Weasels.Length; i++)
            {
                SetRandomPositionSPL(i);
            }
        }

        private void WeaselControlPanel_Shown(object sender, EventArgs e)
        {
            //Everything for the Kuka Roboter
            MessageBox.Show("Geschwindigkeit des Kuka Roboters muss auf 25% eingestellt werden!", "WARNUNG!", MessageBoxButtons.OK);
        }

        private void btn_StopAllMovement_Click(object sender, EventArgs e)
        {
            if (_listBox_Destinations.Items.Count > 0)
            {
                _Weasels[_WeaselDropDown.SelectedIndex]._DestinationsWithInformation.Clear();
                _WeaselMovementHandlers[_WeaselDropDown.SelectedIndex].DestroyAction();
            }
        }

        private void btn_RemoveBox_Click(object sender, EventArgs e)
        {
            _Weasels[_WeaselDropDown.SelectedIndex]._HasBox = false;
        }

        private void _lbl_Online_Click(object sender, EventArgs e)
        {
            //Try to parse the result
            string result = SelfBuildDialogues.TextDialog("Enter new value: ", "Change battery");
            int value = -1;
            bool switcher = Int32.TryParse(result, out value);
            if(switcher == true)
            {
                _Weasels[_WeaselDropDown.SelectedIndex].ChangeBattery(value);
            }
        }
    }
}
