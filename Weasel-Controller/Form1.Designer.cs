
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
            this.btn_Debug = new System.Windows.Forms.Button();
            this.txtBox_Debug = new System.Windows.Forms.TextBox();
            this.txtBox_Weasel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Debug
            // 
            this.btn_Debug.Location = new System.Drawing.Point(713, 415);
            this.btn_Debug.Name = "btn_Debug";
            this.btn_Debug.Size = new System.Drawing.Size(75, 23);
            this.btn_Debug.TabIndex = 0;
            this.btn_Debug.Text = "Debug";
            this.btn_Debug.UseVisualStyleBackColor = true;
            this.btn_Debug.Click += new System.EventHandler(this.btn_Debug_Click);
            // 
            // txtBox_Debug
            // 
            this.txtBox_Debug.Location = new System.Drawing.Point(688, 387);
            this.txtBox_Debug.Name = "txtBox_Debug";
            this.txtBox_Debug.Size = new System.Drawing.Size(100, 22);
            this.txtBox_Debug.TabIndex = 1;
            // 
            // txtBox_Weasel
            // 
            this.txtBox_Weasel.Location = new System.Drawing.Point(573, 387);
            this.txtBox_Weasel.Name = "txtBox_Weasel";
            this.txtBox_Weasel.Size = new System.Drawing.Size(100, 22);
            this.txtBox_Weasel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtBox_Weasel);
            this.Controls.Add(this.txtBox_Debug);
            this.Controls.Add(this.btn_Debug);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Debug;
        private System.Windows.Forms.TextBox txtBox_Debug;
        private System.Windows.Forms.TextBox txtBox_Weasel;
    }
}

