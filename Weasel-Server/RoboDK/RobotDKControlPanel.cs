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
        private Button btn_Step1;
        private Button btn_PutDown;
        private Button btn_Step13;
        private Button btn_Step12;
        private Button btn_Step11;
        private Button btn_Step10;
        private Button btn_Step9;
        private Button btn_Step8;
        private Button btn_Step6;
        private Button btn_Step7;
        private Button btn_Step3;
        private Button btn_Step4;
        private Button btn_Step5;
        private Button btn_Step2;
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
            this.btn_Step13 = new System.Windows.Forms.Button();
            this.btn_Step12 = new System.Windows.Forms.Button();
            this.btn_Step11 = new System.Windows.Forms.Button();
            this.btn_Step10 = new System.Windows.Forms.Button();
            this.btn_Step9 = new System.Windows.Forms.Button();
            this.btn_Step8 = new System.Windows.Forms.Button();
            this.btn_Step6 = new System.Windows.Forms.Button();
            this.btn_Step7 = new System.Windows.Forms.Button();
            this.btn_Step3 = new System.Windows.Forms.Button();
            this.btn_Step4 = new System.Windows.Forms.Button();
            this.btn_Step5 = new System.Windows.Forms.Button();
            this.btn_Step2 = new System.Windows.Forms.Button();
            this.btn_PutDown = new System.Windows.Forms.Button();
            this.btn_Step1 = new System.Windows.Forms.Button();
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
            this.groupBox_IncrementalMove.Size = new System.Drawing.Size(172, 234);
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
            this.groupBox_CordinatesMovement.Size = new System.Drawing.Size(224, 234);
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
            this.txt_PositionMovement.Size = new System.Drawing.Size(211, 22);
            this.txt_PositionMovement.TabIndex = 1;
            this.txt_PositionMovement.Text = "11; -338; 561; 116; 1; 142";
            // 
            // txtBox_JointsMovement
            // 
            this.txtBox_JointsMovement.Location = new System.Drawing.Point(6, 33);
            this.txtBox_JointsMovement.Name = "txtBox_JointsMovement";
            this.txtBox_JointsMovement.Size = new System.Drawing.Size(211, 22);
            this.txtBox_JointsMovement.TabIndex = 0;
            this.txtBox_JointsMovement.Text = "87; -92; 98; 1; 19; 124";
            // 
            // groupBox_CustomMovement
            // 
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step13);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step12);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step11);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step10);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step9);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step8);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step6);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step7);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step3);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step4);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step5);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step2);
            this.groupBox_CustomMovement.Controls.Add(this.btn_PutDown);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Step1);
            this.groupBox_CustomMovement.Controls.Add(this.btn_PickUp);
            this.groupBox_CustomMovement.Controls.Add(this.btn_Home);
            this.groupBox_CustomMovement.Location = new System.Drawing.Point(420, 12);
            this.groupBox_CustomMovement.Name = "groupBox_CustomMovement";
            this.groupBox_CustomMovement.Size = new System.Drawing.Size(353, 234);
            this.groupBox_CustomMovement.TabIndex = 14;
            this.groupBox_CustomMovement.TabStop = false;
            this.groupBox_CustomMovement.Text = "Custom Movement";
            // 
            // btn_Step13
            // 
            this.btn_Step13.Location = new System.Drawing.Point(121, 175);
            this.btn_Step13.Name = "btn_Step13";
            this.btn_Step13.Size = new System.Drawing.Size(109, 23);
            this.btn_Step13.TabIndex = 15;
            this.btn_Step13.Text = "Step13";
            this.btn_Step13.UseVisualStyleBackColor = true;
            this.btn_Step13.Click += new System.EventHandler(this.btn_Step13_Click);
            // 
            // btn_Step12
            // 
            this.btn_Step12.Location = new System.Drawing.Point(121, 146);
            this.btn_Step12.Name = "btn_Step12";
            this.btn_Step12.Size = new System.Drawing.Size(109, 23);
            this.btn_Step12.TabIndex = 14;
            this.btn_Step12.Text = "Step12";
            this.btn_Step12.UseVisualStyleBackColor = true;
            this.btn_Step12.Click += new System.EventHandler(this.btn_Step12_Click);
            // 
            // btn_Step11
            // 
            this.btn_Step11.Location = new System.Drawing.Point(121, 117);
            this.btn_Step11.Name = "btn_Step11";
            this.btn_Step11.Size = new System.Drawing.Size(109, 23);
            this.btn_Step11.TabIndex = 13;
            this.btn_Step11.Text = "Step11";
            this.btn_Step11.UseVisualStyleBackColor = true;
            this.btn_Step11.Click += new System.EventHandler(this.btn_Step11_Click);
            // 
            // btn_Step10
            // 
            this.btn_Step10.Location = new System.Drawing.Point(121, 91);
            this.btn_Step10.Name = "btn_Step10";
            this.btn_Step10.Size = new System.Drawing.Size(109, 23);
            this.btn_Step10.TabIndex = 12;
            this.btn_Step10.Text = "Step10";
            this.btn_Step10.UseVisualStyleBackColor = true;
            this.btn_Step10.Click += new System.EventHandler(this.btn_Step10_Click);
            // 
            // btn_Step9
            // 
            this.btn_Step9.Location = new System.Drawing.Point(121, 62);
            this.btn_Step9.Name = "btn_Step9";
            this.btn_Step9.Size = new System.Drawing.Size(109, 23);
            this.btn_Step9.TabIndex = 11;
            this.btn_Step9.Text = "Step9";
            this.btn_Step9.UseVisualStyleBackColor = true;
            this.btn_Step9.Click += new System.EventHandler(this.btn_Step9_Click);
            // 
            // btn_Step8
            // 
            this.btn_Step8.Location = new System.Drawing.Point(121, 33);
            this.btn_Step8.Name = "btn_Step8";
            this.btn_Step8.Size = new System.Drawing.Size(109, 23);
            this.btn_Step8.TabIndex = 10;
            this.btn_Step8.Text = "Step8";
            this.btn_Step8.UseVisualStyleBackColor = true;
            this.btn_Step8.Click += new System.EventHandler(this.btn_Step8_Click);
            // 
            // btn_Step6
            // 
            this.btn_Step6.Location = new System.Drawing.Point(6, 175);
            this.btn_Step6.Name = "btn_Step6";
            this.btn_Step6.Size = new System.Drawing.Size(109, 23);
            this.btn_Step6.TabIndex = 9;
            this.btn_Step6.Text = "Step6";
            this.btn_Step6.UseVisualStyleBackColor = true;
            this.btn_Step6.Click += new System.EventHandler(this.btn_Step6_Click);
            // 
            // btn_Step7
            // 
            this.btn_Step7.Location = new System.Drawing.Point(6, 204);
            this.btn_Step7.Name = "btn_Step7";
            this.btn_Step7.Size = new System.Drawing.Size(109, 23);
            this.btn_Step7.TabIndex = 8;
            this.btn_Step7.Text = "Step7";
            this.btn_Step7.UseVisualStyleBackColor = true;
            this.btn_Step7.Click += new System.EventHandler(this.btn_Step7_Click);
            // 
            // btn_Step3
            // 
            this.btn_Step3.Location = new System.Drawing.Point(6, 90);
            this.btn_Step3.Name = "btn_Step3";
            this.btn_Step3.Size = new System.Drawing.Size(109, 23);
            this.btn_Step3.TabIndex = 7;
            this.btn_Step3.Text = "Step3";
            this.btn_Step3.UseVisualStyleBackColor = true;
            this.btn_Step3.Click += new System.EventHandler(this.btn_Step3_Click);
            // 
            // btn_Step4
            // 
            this.btn_Step4.Location = new System.Drawing.Point(6, 117);
            this.btn_Step4.Name = "btn_Step4";
            this.btn_Step4.Size = new System.Drawing.Size(109, 23);
            this.btn_Step4.TabIndex = 6;
            this.btn_Step4.Text = "Step4";
            this.btn_Step4.UseVisualStyleBackColor = true;
            this.btn_Step4.Click += new System.EventHandler(this.btn_Step4_Click);
            // 
            // btn_Step5
            // 
            this.btn_Step5.Location = new System.Drawing.Point(6, 146);
            this.btn_Step5.Name = "btn_Step5";
            this.btn_Step5.Size = new System.Drawing.Size(109, 23);
            this.btn_Step5.TabIndex = 5;
            this.btn_Step5.Text = "Step5";
            this.btn_Step5.UseVisualStyleBackColor = true;
            this.btn_Step5.Click += new System.EventHandler(this.btn_Step5_Click);
            // 
            // btn_Step2
            // 
            this.btn_Step2.Location = new System.Drawing.Point(6, 62);
            this.btn_Step2.Name = "btn_Step2";
            this.btn_Step2.Size = new System.Drawing.Size(109, 23);
            this.btn_Step2.TabIndex = 4;
            this.btn_Step2.Text = "Step2";
            this.btn_Step2.UseVisualStyleBackColor = true;
            this.btn_Step2.Click += new System.EventHandler(this.btn_Step2_Click);
            // 
            // btn_PutDown
            // 
            this.btn_PutDown.Location = new System.Drawing.Point(238, 90);
            this.btn_PutDown.Name = "btn_PutDown";
            this.btn_PutDown.Size = new System.Drawing.Size(109, 23);
            this.btn_PutDown.TabIndex = 3;
            this.btn_PutDown.Text = "Put Down";
            this.btn_PutDown.UseVisualStyleBackColor = true;
            this.btn_PutDown.Click += new System.EventHandler(this.btn_PutDown_Click);
            // 
            // btn_Step1
            // 
            this.btn_Step1.Location = new System.Drawing.Point(6, 33);
            this.btn_Step1.Name = "btn_Step1";
            this.btn_Step1.Size = new System.Drawing.Size(109, 23);
            this.btn_Step1.TabIndex = 2;
            this.btn_Step1.Text = "Step1";
            this.btn_Step1.UseVisualStyleBackColor = true;
            this.btn_Step1.Click += new System.EventHandler(this.btn_Step1_Click);
            // 
            // btn_PickUp
            // 
            this.btn_PickUp.Location = new System.Drawing.Point(238, 62);
            this.btn_PickUp.Name = "btn_PickUp";
            this.btn_PickUp.Size = new System.Drawing.Size(109, 23);
            this.btn_PickUp.TabIndex = 1;
            this.btn_PickUp.Text = "PickUp";
            this.btn_PickUp.UseVisualStyleBackColor = true;
            this.btn_PickUp.Click += new System.EventHandler(this.btn_PickUp_Click);
            // 
            // btn_Home
            // 
            this.btn_Home.Location = new System.Drawing.Point(238, 33);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(109, 23);
            this.btn_Home.TabIndex = 0;
            this.btn_Home.Text = "Home";
            this.btn_Home.UseVisualStyleBackColor = true;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // RobotDKControlPanel
            // 
            this.ClientSize = new System.Drawing.Size(784, 258);
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

        private void btn_Step1_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step1();
        }

        private void btn_PutDown_Click(object sender, EventArgs e)
        {
            _KukaRobot.PutDown();
        }

        private void btn_Step2_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step2();
        }

        private void btn_Step3_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step3();
        }

        private void btn_Step4_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step4();
        }

        private void btn_Step5_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step5();
        }

        private void btn_Step6_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step6();
        }

        private void btn_Step7_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step7();
        }

        private void btn_Step8_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step8();
        }

        private void btn_Step9_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step9();
        }

        private void btn_Step10_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step10();
        }

        private void btn_Step11_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step11();
        }

        private void btn_Step12_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step12();
        }

        private void btn_Step13_Click(object sender, EventArgs e)
        {
            _KukaRobot.Step13();
        }
    }
}
