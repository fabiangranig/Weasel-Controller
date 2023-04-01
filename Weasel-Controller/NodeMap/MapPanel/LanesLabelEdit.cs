using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Weasel_Controller.NodeMap.MapPanel
{
    class LanesLabelEdit : Form
    {
        private Button btn_Ok;
        private Button btn_Cancel;
        private Button btn_Up;
        private Button btn_Right;
        private Button btn_Left;
        private Button btn_Down;
        private Label _MainLabel;

        public LanesLabelEdit(ref Label label1)
        {
            _MainLabel = label1;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.btn_Right = new System.Windows.Forms.Button();
            this.btn_Left = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(84, 111);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(3, 111);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.Location = new System.Drawing.Point(37, 12);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(75, 23);
            this.btn_Up.TabIndex = 4;
            this.btn_Up.Text = "Up";
            this.btn_Up.UseVisualStyleBackColor = true;
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // btn_Right
            // 
            this.btn_Right.Location = new System.Drawing.Point(84, 41);
            this.btn_Right.Name = "btn_Right";
            this.btn_Right.Size = new System.Drawing.Size(75, 23);
            this.btn_Right.TabIndex = 5;
            this.btn_Right.Text = "Right";
            this.btn_Right.UseVisualStyleBackColor = true;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            // 
            // btn_Left
            // 
            this.btn_Left.Location = new System.Drawing.Point(3, 41);
            this.btn_Left.Name = "btn_Left";
            this.btn_Left.Size = new System.Drawing.Size(75, 23);
            this.btn_Left.TabIndex = 6;
            this.btn_Left.Text = "Left";
            this.btn_Left.UseVisualStyleBackColor = true;
            this.btn_Left.Click += new System.EventHandler(this.btn_Left_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.Location = new System.Drawing.Point(37, 70);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(75, 23);
            this.btn_Down.TabIndex = 7;
            this.btn_Down.Text = "Down";
            this.btn_Down.UseVisualStyleBackColor = true;
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // LanesLabelEdit
            // 
            this.ClientSize = new System.Drawing.Size(184, 185);
            this.Controls.Add(this.btn_Down);
            this.Controls.Add(this.btn_Left);
            this.Controls.Add(this.btn_Right);
            this.Controls.Add(this.btn_Up);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Name = "LanesLabelEdit";
            this.ResumeLayout(false);

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
            this.Close();
        }
    }
}
