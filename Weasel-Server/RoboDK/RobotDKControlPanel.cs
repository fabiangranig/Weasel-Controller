using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weasel_Controller
{
    class RobotDKControlPanel : Form
    {
        private GroupBox groupBox_IncrementalMove;
        private Button btn_rZPlus;
        private Button btn_rYPlus;
        private Button btn_rXPlus;
        private Button btn_ZPlus;
        private Button btn_YPlus;
        private Button btn_XPlus;
        private Button btn_rZMinus;
        private Button btn_rYMinus;
        private Button btn_rXMinus;
        private Button btn_ZMinus;
        private Button btn_YMinus;
        private Button btn_XMinus;
        private GroupBox groupBox_CordinatesMovement;
        private Button btn_PositionMovement;
        private Button btn_JoinsMovement;
        private TextBox txt_PositionMovement;
        private TextBox txtBox_JointsMovement;
        private Button btn_UpdatePosition;
        private GroupBox groupBox_CustomMovement;
        private Button btn_PickUp;
        private Button btn_Home;
        KukaRoboter _KukaRobot;

        public RobotDKControlPanel(KukaRoboter kukaRoboter1)
        {
            _KukaRobot = kukaRoboter1;

            //Get the components
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.groupBox_IncrementalMove = new System.Windows.Forms.GroupBox();
            this.btn_rZPlus = new System.Windows.Forms.Button();
            this.btn_rYPlus = new System.Windows.Forms.Button();
            this.btn_rXPlus = new System.Windows.Forms.Button();
            this.btn_ZPlus = new System.Windows.Forms.Button();
            this.btn_YPlus = new System.Windows.Forms.Button();
            this.btn_XPlus = new System.Windows.Forms.Button();
            this.btn_rZMinus = new System.Windows.Forms.Button();
            this.btn_rYMinus = new System.Windows.Forms.Button();
            this.btn_rXMinus = new System.Windows.Forms.Button();
            this.btn_ZMinus = new System.Windows.Forms.Button();
            this.btn_YMinus = new System.Windows.Forms.Button();
            this.btn_XMinus = new System.Windows.Forms.Button();
            this.groupBox_CordinatesMovement = new System.Windows.Forms.GroupBox();
            this.btn_UpdatePosition = new System.Windows.Forms.Button();
            this.btn_PositionMovement = new System.Windows.Forms.Button();
            this.btn_JoinsMovement = new System.Windows.Forms.Button();
            this.txt_PositionMovement = new System.Windows.Forms.TextBox();
            this.txtBox_JointsMovement = new System.Windows.Forms.TextBox();
            this.groupBox_CustomMovement = new System.Windows.Forms.GroupBox();
            this.btn_PickUp = new System.Windows.Forms.Button();
            this.btn_Home = new System.Windows.Forms.Button();
            this.groupBox_IncrementalMove.SuspendLayout();
            this.groupBox_CordinatesMovement.SuspendLayout();
            this.groupBox_CustomMovement.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_IncrementalMove
            // 
            this.groupBox_IncrementalMove.Controls.Add(this.btn_rZPlus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_rYPlus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_rXPlus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_ZPlus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_YPlus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_XPlus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_rZMinus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_rYMinus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_rXMinus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_ZMinus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_YMinus);
            this.groupBox_IncrementalMove.Controls.Add(this.btn_XMinus);
            this.groupBox_IncrementalMove.Location = new System.Drawing.Point(12, 12);
            this.groupBox_IncrementalMove.Name = "groupBox_IncrementalMove";
            this.groupBox_IncrementalMove.Size = new System.Drawing.Size(172, 216);
            this.groupBox_IncrementalMove.TabIndex = 0;
            this.groupBox_IncrementalMove.TabStop = false;
            this.groupBox_IncrementalMove.Text = "Incremental Move";
            // 
            // btn_rZPlus
            // 
            this.btn_rZPlus.Location = new System.Drawing.Point(87, 178);
            this.btn_rZPlus.Name = "btn_rZPlus";
            this.btn_rZPlus.Size = new System.Drawing.Size(75, 23);
            this.btn_rZPlus.TabIndex = 12;
            this.btn_rZPlus.Text = "+Rz";
            this.btn_rZPlus.UseVisualStyleBackColor = true;
            this.btn_rZPlus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_rYPlus
            // 
            this.btn_rYPlus.Location = new System.Drawing.Point(87, 149);
            this.btn_rYPlus.Name = "btn_rYPlus";
            this.btn_rYPlus.Size = new System.Drawing.Size(75, 23);
            this.btn_rYPlus.TabIndex = 11;
            this.btn_rYPlus.Text = "+Ry";
            this.btn_rYPlus.UseVisualStyleBackColor = true;
            this.btn_rYPlus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_rXPlus
            // 
            this.btn_rXPlus.Location = new System.Drawing.Point(87, 120);
            this.btn_rXPlus.Name = "btn_rXPlus";
            this.btn_rXPlus.Size = new System.Drawing.Size(75, 23);
            this.btn_rXPlus.TabIndex = 10;
            this.btn_rXPlus.Text = "+Rx";
            this.btn_rXPlus.UseVisualStyleBackColor = true;
            this.btn_rXPlus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_ZPlus
            // 
            this.btn_ZPlus.Location = new System.Drawing.Point(87, 91);
            this.btn_ZPlus.Name = "btn_ZPlus";
            this.btn_ZPlus.Size = new System.Drawing.Size(75, 23);
            this.btn_ZPlus.TabIndex = 9;
            this.btn_ZPlus.Text = "+Tz";
            this.btn_ZPlus.UseVisualStyleBackColor = true;
            this.btn_ZPlus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_YPlus
            // 
            this.btn_YPlus.Location = new System.Drawing.Point(87, 62);
            this.btn_YPlus.Name = "btn_YPlus";
            this.btn_YPlus.Size = new System.Drawing.Size(75, 23);
            this.btn_YPlus.TabIndex = 8;
            this.btn_YPlus.Text = "+Ty";
            this.btn_YPlus.UseVisualStyleBackColor = true;
            this.btn_YPlus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_XPlus
            // 
            this.btn_XPlus.Location = new System.Drawing.Point(87, 33);
            this.btn_XPlus.Name = "btn_XPlus";
            this.btn_XPlus.Size = new System.Drawing.Size(75, 23);
            this.btn_XPlus.TabIndex = 7;
            this.btn_XPlus.Text = "+Tx";
            this.btn_XPlus.UseVisualStyleBackColor = true;
            this.btn_XPlus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_rZMinus
            // 
            this.btn_rZMinus.Location = new System.Drawing.Point(6, 179);
            this.btn_rZMinus.Name = "btn_rZMinus";
            this.btn_rZMinus.Size = new System.Drawing.Size(75, 23);
            this.btn_rZMinus.TabIndex = 6;
            this.btn_rZMinus.Text = "-Rz";
            this.btn_rZMinus.UseVisualStyleBackColor = true;
            this.btn_rZMinus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_rYMinus
            // 
            this.btn_rYMinus.Location = new System.Drawing.Point(6, 149);
            this.btn_rYMinus.Name = "btn_rYMinus";
            this.btn_rYMinus.Size = new System.Drawing.Size(75, 23);
            this.btn_rYMinus.TabIndex = 5;
            this.btn_rYMinus.Text = "-Ry";
            this.btn_rYMinus.UseVisualStyleBackColor = true;
            this.btn_rYMinus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_rXMinus
            // 
            this.btn_rXMinus.Location = new System.Drawing.Point(6, 120);
            this.btn_rXMinus.Name = "btn_rXMinus";
            this.btn_rXMinus.Size = new System.Drawing.Size(75, 23);
            this.btn_rXMinus.TabIndex = 4;
            this.btn_rXMinus.Text = "-Rx";
            this.btn_rXMinus.UseVisualStyleBackColor = true;
            this.btn_rXMinus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_ZMinus
            // 
            this.btn_ZMinus.Location = new System.Drawing.Point(6, 91);
            this.btn_ZMinus.Name = "btn_ZMinus";
            this.btn_ZMinus.Size = new System.Drawing.Size(75, 23);
            this.btn_ZMinus.TabIndex = 3;
            this.btn_ZMinus.Text = "-Tz";
            this.btn_ZMinus.UseVisualStyleBackColor = true;
            this.btn_ZMinus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_YMinus
            // 
            this.btn_YMinus.Location = new System.Drawing.Point(6, 62);
            this.btn_YMinus.Name = "btn_YMinus";
            this.btn_YMinus.Size = new System.Drawing.Size(75, 23);
            this.btn_YMinus.TabIndex = 2;
            this.btn_YMinus.Text = "-Ty";
            this.btn_YMinus.UseVisualStyleBackColor = true;
            this.btn_YMinus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // btn_XMinus
            // 
            this.btn_XMinus.Location = new System.Drawing.Point(6, 33);
            this.btn_XMinus.Name = "btn_XMinus";
            this.btn_XMinus.Size = new System.Drawing.Size(75, 23);
            this.btn_XMinus.TabIndex = 0;
            this.btn_XMinus.Text = "-Tx";
            this.btn_XMinus.UseVisualStyleBackColor = true;
            this.btn_XMinus.Click += new System.EventHandler(this.btnPos_Click);
            // 
            // groupBox_CordinatesMovement
            // 
            this.groupBox_CordinatesMovement.Controls.Add(this.btn_UpdatePosition);
            this.groupBox_CordinatesMovement.Controls.Add(this.btn_PositionMovement);
            this.groupBox_CordinatesMovement.Controls.Add(this.btn_JoinsMovement);
            this.groupBox_CordinatesMovement.Controls.Add(this.txt_PositionMovement);
            this.groupBox_CordinatesMovement.Controls.Add(this.txtBox_JointsMovement);
            this.groupBox_CordinatesMovement.Location = new System.Drawing.Point(190, 12);
            this.groupBox_CordinatesMovement.Name = "groupBox_CordinatesMovement";
            this.groupBox_CordinatesMovement.Size = new System.Drawing.Size(224, 216);
            this.groupBox_CordinatesMovement.TabIndex = 13;
            this.groupBox_CordinatesMovement.TabStop = false;
            this.groupBox_CordinatesMovement.Text = "Cordinates Movement";
            // 
            // btn_UpdatePosition
            // 
            this.btn_UpdatePosition.Location = new System.Drawing.Point(7, 179);
            this.btn_UpdatePosition.Name = "btn_UpdatePosition";
            this.btn_UpdatePosition.Size = new System.Drawing.Size(211, 23);
            this.btn_UpdatePosition.TabIndex = 4;
            this.btn_UpdatePosition.Text = "Update Position";
            this.btn_UpdatePosition.UseVisualStyleBackColor = true;
            this.btn_UpdatePosition.Click += new System.EventHandler(this.btn_UpdatePosition_Click);
            // 
            // btn_PositionMovement
            // 
            this.btn_PositionMovement.Location = new System.Drawing.Point(6, 117);
            this.btn_PositionMovement.Name = "btn_PositionMovement";
            this.btn_PositionMovement.Size = new System.Drawing.Size(211, 23);
            this.btn_PositionMovement.TabIndex = 3;
            this.btn_PositionMovement.Text = "Position Movement";
            this.btn_PositionMovement.UseVisualStyleBackColor = true;
            this.btn_PositionMovement.Click += new System.EventHandler(this.btn_PositionMovement_Click);
            // 
            // btn_JoinsMovement
            // 
            this.btn_JoinsMovement.Location = new System.Drawing.Point(6, 62);
            this.btn_JoinsMovement.Name = "btn_JoinsMovement";
            this.btn_JoinsMovement.Size = new System.Drawing.Size(211, 23);
            this.btn_JoinsMovement.TabIndex = 2;
            this.btn_JoinsMovement.Text = "Joints Movement";
            this.btn_JoinsMovement.UseVisualStyleBackColor = true;
            this.btn_JoinsMovement.Click += new System.EventHandler(this.btn_JoinsMovement_Click);
            // 
            // txt_PositionMovement
            // 
            this.txt_PositionMovement.Location = new System.Drawing.Point(7, 91);
            this.txt_PositionMovement.Name = "txt_PositionMovement";
            this.txt_PositionMovement.Size = new System.Drawing.Size(211, 20);
            this.txt_PositionMovement.TabIndex = 1;
            this.txt_PositionMovement.Text = "11; -338; 561; 116; 1; 142";
            // 
            // txtBox_JointsMovement
            // 
            this.txtBox_JointsMovement.Location = new System.Drawing.Point(6, 33);
            this.txtBox_JointsMovement.Name = "txtBox_JointsMovement";
            this.txtBox_JointsMovement.Size = new System.Drawing.Size(211, 20);
            this.txtBox_JointsMovement.TabIndex = 0;
            this.txtBox_JointsMovement.Text = "87; -92; 98; 1; 19; 124";
            // 
            // groupBox_CustomMovement
            // 
            this.groupBox_CustomMovement.Controls.Add(this.btn_PickUp);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Home);
            this.groupBox_CustomMovement.Location = new System.Drawing.Point(420, 12);
            this.groupBox_CustomMovement.Name = "groupBox_CustomMovement";
            this.groupBox_CustomMovement.Size = new System.Drawing.Size(120, 216);
            this.groupBox_CustomMovement.TabIndex = 14;
            this.groupBox_CustomMovement.TabStop = false;
            this.groupBox_CustomMovement.Text = "Custom Movement";
            // 
            // btn_PickUp
            // 
            this.btn_PickUp.Location = new System.Drawing.Point(6, 48);
            this.btn_PickUp.Name = "btn_PickUp";
            this.btn_PickUp.Size = new System.Drawing.Size(109, 23);
            this.btn_PickUp.TabIndex = 1;
            this.btn_PickUp.Text = "PickUp";
            this.btn_PickUp.UseVisualStyleBackColor = true;
            this.btn_PickUp.Click += new System.EventHandler(this.btn_PickUp_Click);
            // 
            // btn_Home
            // 
            this.btn_Home.Location = new System.Drawing.Point(6, 19);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(109, 23);
            this.btn_Home.TabIndex = 0;
            this.btn_Home.Text = "Home";
            this.btn_Home.UseVisualStyleBackColor = true;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // RobotDKControlPanel
            // 
            this.ClientSize = new System.Drawing.Size(554, 241);
            this.Controls.Add(this.groupBox_CustomMovement);
            this.Controls.Add(this.groupBox_CordinatesMovement);
            this.Controls.Add(this.groupBox_IncrementalMove);
            this.Name = "RobotDKControlPanel";
            this.Text = "RobotDK Controller";
            this.groupBox_IncrementalMove.ResumeLayout(false);
            this.groupBox_CordinatesMovement.ResumeLayout(false);
            this.groupBox_CordinatesMovement.PerformLayout();
            this.groupBox_CustomMovement.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            _KukaRobot.Incremental_Move(btn.Text);
        }

        private void btn_JoinsMovement_Click(object sender, EventArgs e)
        {
            _KukaRobot.MoveToJoints(txtBox_JointsMovement.Text);
        }

        private void btn_PositionMovement_Click(object sender, EventArgs e)
        {
            _KukaRobot.MoveToPose(txt_PositionMovement.Text);
        }

        private void btn_UpdatePosition_Click(object sender, EventArgs e)
        {
            txtBox_JointsMovement.Text = _KukaRobot.GetJointsPosition();
            txt_PositionMovement.Text = _KukaRobot.GetPositionCordinates();
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            _KukaRobot.HomeRobot();
        }

        private void btn_PickUp_Click(object sender, EventArgs e)
        {
            _KukaRobot.PickUp();
        }
    }
}
