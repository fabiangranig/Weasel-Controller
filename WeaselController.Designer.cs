
namespace Weasel_Controller
{
    partial class WeaselController
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_WeaselPanel = new System.Windows.Forms.Button();
            this.btn_WeaselControlPanel = new System.Windows.Forms.Button();
            this.btn_WeaselManipulator = new System.Windows.Forms.Button();
            this.btn_WeaselMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_WeaselPanel
            // 
            this.btn_WeaselPanel.Enabled = false;
            this.btn_WeaselPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_WeaselPanel.Location = new System.Drawing.Point(11, 69);
            this.btn_WeaselPanel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_WeaselPanel.Name = "btn_WeaselPanel";
            this.btn_WeaselPanel.Size = new System.Drawing.Size(293, 25);
            this.btn_WeaselPanel.TabIndex = 3;
            this.btn_WeaselPanel.Text = "Weasel Information Panel";
            this.btn_WeaselPanel.UseVisualStyleBackColor = true;
            this.btn_WeaselPanel.Click += new System.EventHandler(this.btn_WeaselPanel_Click);
            // 
            // btn_WeaselControlPanel
            // 
            this.btn_WeaselControlPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_WeaselControlPanel.Location = new System.Drawing.Point(11, 11);
            this.btn_WeaselControlPanel.Margin = new System.Windows.Forms.Padding(2);
            this.btn_WeaselControlPanel.Name = "btn_WeaselControlPanel";
            this.btn_WeaselControlPanel.Size = new System.Drawing.Size(293, 25);
            this.btn_WeaselControlPanel.TabIndex = 4;
            this.btn_WeaselControlPanel.Text = "Weasel Control Panel";
            this.btn_WeaselControlPanel.UseVisualStyleBackColor = true;
            this.btn_WeaselControlPanel.Click += new System.EventHandler(this.btn_WeaselControlPanel_Click);
            // 
            // btn_WeaselManipulator
            // 
            this.btn_WeaselManipulator.Enabled = false;
            this.btn_WeaselManipulator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_WeaselManipulator.Location = new System.Drawing.Point(7, 98);
            this.btn_WeaselManipulator.Margin = new System.Windows.Forms.Padding(2);
            this.btn_WeaselManipulator.Name = "btn_WeaselManipulator";
            this.btn_WeaselManipulator.Size = new System.Drawing.Size(297, 25);
            this.btn_WeaselManipulator.TabIndex = 5;
            this.btn_WeaselManipulator.Text = "Weasel Manipulator";
            this.btn_WeaselManipulator.UseVisualStyleBackColor = true;
            this.btn_WeaselManipulator.Click += new System.EventHandler(this.btn_WeaselManipulator_Click);
            // 
            // btn_WeaselMap
            // 
            this.btn_WeaselMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_WeaselMap.Location = new System.Drawing.Point(11, 40);
            this.btn_WeaselMap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_WeaselMap.Name = "btn_WeaselMap";
            this.btn_WeaselMap.Size = new System.Drawing.Size(293, 25);
            this.btn_WeaselMap.TabIndex = 6;
            this.btn_WeaselMap.Text = "Weasel Map";
            this.btn_WeaselMap.UseVisualStyleBackColor = true;
            this.btn_WeaselMap.Click += new System.EventHandler(this.btn_WeaselMap_Click);
            // 
            // WeaselController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(310, 133);
            this.Controls.Add(this.btn_WeaselMap);
            this.Controls.Add(this.btn_WeaselManipulator);
            this.Controls.Add(this.btn_WeaselControlPanel);
            this.Controls.Add(this.btn_WeaselPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(326, 172);
            this.MinimumSize = new System.Drawing.Size(326, 172);
            this.Name = "WeaselController";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weasel Controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_WeaselPanel;
        private System.Windows.Forms.Button btn_WeaselControlPanel;
        private System.Windows.Forms.Button btn_WeaselManipulator;
        private System.Windows.Forms.Button btn_WeaselMap;
    }
}

