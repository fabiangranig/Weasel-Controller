
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
            this.SuspendLayout();
            // 
            // btn_WeaselPanel
            // 
            this.btn_WeaselPanel.Location = new System.Drawing.Point(334, 339);
            this.btn_WeaselPanel.Name = "btn_WeaselPanel";
            this.btn_WeaselPanel.Size = new System.Drawing.Size(115, 23);
            this.btn_WeaselPanel.TabIndex = 3;
            this.btn_WeaselPanel.Text = "Weasel Panel";
            this.btn_WeaselPanel.UseVisualStyleBackColor = true;
            this.btn_WeaselPanel.Click += new System.EventHandler(this.btn_WeaselPanel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_WeaselPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_WeaselPanel;
    }
}

