using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Weasel_Controller.NodeMap.MapPanel
{
    class LabelEdit : Form
    {
        private Button btn_Ok;
        private Button btn_Cancel;
        private TextBox txtBox_Position;
        private Label label_Correct;
        private Button btn_Up;
        private Button btn_Right;
        private Button btn_Left;
        private Button btn_Down;
        private Label _MainLabel;
        private Map _WeaselMap;

        public LabelEdit(ref Label label1, ref Map WeaselMap1)
        {
            _MainLabel = label1;
            _WeaselMap = WeaselMap1;

            InitializeComponent();

            txtBox_Position.Text = _MainLabel.Text;
        }

        private void InitializeComponent()
        {
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txtBox_Position = new System.Windows.Forms.TextBox();
            this.label_Correct = new System.Windows.Forms.Label();
            this.btn_Up = new System.Windows.Forms.Button();
            this.btn_Right = new System.Windows.Forms.Button();
            this.btn_Left = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(93, 147);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(12, 147);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // txtBox_Position
            // 
            this.txtBox_Position.Location = new System.Drawing.Point(12, 12);
            this.txtBox_Position.Name = "txtBox_Position";
            this.txtBox_Position.Size = new System.Drawing.Size(100, 20);
            this.txtBox_Position.TabIndex = 2;
            this.txtBox_Position.TextChanged += new System.EventHandler(this.txtBox_Position_TextChanged);
            // 
            // label_Correct
            // 
            this.label_Correct.AutoSize = true;
            this.label_Correct.BackColor = System.Drawing.Color.Red;
            this.label_Correct.Location = new System.Drawing.Point(118, 15);
            this.label_Correct.Name = "label_Correct";
            this.label_Correct.Size = new System.Drawing.Size(31, 13);
            this.label_Correct.TabIndex = 3;
            this.label_Correct.Text = "        ";
            // 
            // btn_Up
            // 
            this.btn_Up.Location = new System.Drawing.Point(37, 38);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(75, 23);
            this.btn_Up.TabIndex = 4;
            this.btn_Up.Text = "Up";
            this.btn_Up.UseVisualStyleBackColor = true;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // btn_Right
            // 
            this.btn_Right.Location = new System.Drawing.Point(84, 67);
            this.btn_Right.Name = "btn_Right";
            this.btn_Right.Size = new System.Drawing.Size(75, 23);
            this.btn_Right.TabIndex = 5;
            this.btn_Right.Text = "Right";
            this.btn_Right.UseVisualStyleBackColor = true;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            // 
            // btn_Left
            // 
            this.btn_Left.Location = new System.Drawing.Point(3, 67);
            this.btn_Left.Name = "btn_Left";
            this.btn_Left.Size = new System.Drawing.Size(75, 23);
            this.btn_Left.TabIndex = 6;
            this.btn_Left.Text = "Left";
            this.btn_Left.UseVisualStyleBackColor = true;
            this.btn_Left.Click += new System.EventHandler(this.btn_Left_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.Location = new System.Drawing.Point(37, 96);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(75, 23);
            this.btn_Down.TabIndex = 7;
            this.btn_Down.Text = "Down";
            this.btn_Down.UseVisualStyleBackColor = true;
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // LabelEdit
            // 
            this.ClientSize = new System.Drawing.Size(184, 185);
            this.Controls.Add(this.btn_Down);
            this.Controls.Add(this.btn_Left);
            this.Controls.Add(this.btn_Right);
            this.Controls.Add(this.btn_Up);
            this.Controls.Add(this.label_Correct);
            this.Controls.Add(this.txtBox_Position);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Name = "LabelEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void txtBox_Position_TextChanged(object sender, EventArgs e)
        {
            int result = -1;
            bool tryparse = Int32.TryParse(txtBox_Position.Text, out result);

            if(tryparse == true)
            {
                Waypoint wp = _WeaselMap.FindWayPoint(result);

                if(wp != null)
                {
                    label_Correct.BackColor = Color.LightGreen;
                }
                else
                {
                    label_Correct.BackColor = Color.Red;
                }
            }
            else
            {
                label_Correct.BackColor = Color.Red;
            }
        }

        private void btn_Up_Click(object sender, EventArgs e)
        {
            _MainLabel.Location = new Point(_MainLabel.Location.X, _MainLabel.Location.Y - 10);
        }

        private void btn_Left_Click(object sender, EventArgs e)
        {
            _MainLabel.Location = new Point(_MainLabel.Location.X - 10, _MainLabel.Location.Y);
        }

        private void btn_Right_Click(object sender, EventArgs e)
        {
            _MainLabel.Location = new Point(_MainLabel.Location.X + 10, _MainLabel.Location.Y);
        }

        private void btn_Down_Click(object sender, EventArgs e)
        {
            _MainLabel.Location = new Point(_MainLabel.Location.X, _MainLabel.Location.Y + 10);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if(label_Correct.BackColor == Color.LightGreen)
            {
                _MainLabel.Text = txtBox_Position.Text;
                this.Close();
            }
        }
    }
}
