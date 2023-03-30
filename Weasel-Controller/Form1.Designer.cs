
namespace Weasel_Controller
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // btn_WeaselPanel
            // 
            this.btn_WeaselPanel.Location = new System.Drawing.Point(12, 12);
            this.btn_WeaselPanel.Name = "btn_WeaselPanel";
            this.btn_WeaselPanel.Size = new System.Drawing.Size(391, 23);
            this.btn_WeaselPanel.TabIndex = 3;
            this.btn_WeaselPanel.Text = "Weasel Information Panel";
            this.btn_WeaselPanel.UseVisualStyleBackColor = true;
            this.btn_WeaselPanel.Click += new System.EventHandler(this.btn_WeaselPanel_Click);
            // 
            // btn_WeaselControlPanel
            // 
            this.btn_WeaselControlPanel.Location = new System.Drawing.Point(12, 41);
            this.btn_WeaselControlPanel.Name = "btn_WeaselControlPanel";
            this.btn_WeaselControlPanel.Size = new System.Drawing.Size(391, 23);
            this.btn_WeaselControlPanel.TabIndex = 4;
            this.btn_WeaselControlPanel.Text = "Weasel Control Panel";
            this.btn_WeaselControlPanel.UseVisualStyleBackColor = true;
            this.btn_WeaselControlPanel.Click += new System.EventHandler(this.btn_WeaselControlPanel_Click);
            // 
            // btn_WeaselManipulator
            // 
            this.btn_WeaselManipulator.Location = new System.Drawing.Point(12, 70);
            this.btn_WeaselManipulator.Name = "btn_WeaselManipulator";
            this.btn_WeaselManipulator.Size = new System.Drawing.Size(391, 23);
            this.btn_WeaselManipulator.TabIndex = 5;
            this.btn_WeaselManipulator.Text = "Weasel Manipulator";
            this.btn_WeaselManipulator.UseVisualStyleBackColor = true;
            this.btn_WeaselManipulator.Click += new System.EventHandler(this.btn_WeaselManipulator_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 305);
            this.Controls.Add(this.btn_WeaselManipulator);
            this.Controls.Add(this.btn_WeaselControlPanel);
            this.Controls.Add(this.btn_WeaselPanel);
            this.Name = "Form1";
            this.Text = "Weasel Controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_WeaselPanel;
        private System.Windows.Forms.Button btn_WeaselControlPanel;
        private System.Windows.Forms.Button btn_WeaselManipulator;
    }
}

