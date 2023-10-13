namespace KukaMovementEditor
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
            this.groupBox_IncrementalMove = new System.Windows.Forms.GroupBox();
            this.radioButton_Reference = new System.Windows.Forms.RadioButton();
            this.radioButton_Tool = new System.Windows.Forms.RadioButton();
            this.radioButton_JointMove = new System.Windows.Forms.RadioButton();
            this.button_Xminus = new System.Windows.Forms.Button();
            this.button_Yminus = new System.Windows.Forms.Button();
            this.button_Zminus = new System.Windows.Forms.Button();
            this.button_rXminus = new System.Windows.Forms.Button();
            this.button_rYminus = new System.Windows.Forms.Button();
            this.button_rZminus = new System.Windows.Forms.Button();
            this.button_Xplus = new System.Windows.Forms.Button();
            this.button_Yplus = new System.Windows.Forms.Button();
            this.button_Zplus = new System.Windows.Forms.Button();
            this.button_rXplus = new System.Windows.Forms.Button();
            this.button_rYplus = new System.Windows.Forms.Button();
            this.button_rZplus = new System.Windows.Forms.Button();
            this.textBox_DistanceStep = new System.Windows.Forms.TextBox();
            this.groupBox_Mode = new System.Windows.Forms.GroupBox();
            this.button_Simulation = new System.Windows.Forms.Button();
            this.button_RealRobot = new System.Windows.Forms.Button();
            this.groupBox_IncrementalMove.SuspendLayout();
            this.groupBox_Mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_IncrementalMove
            // 
            this.groupBox_IncrementalMove.Controls.Add(this.textBox_DistanceStep);
            this.groupBox_IncrementalMove.Controls.Add(this.button_rZplus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_Zplus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_rYplus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_Yplus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_rXplus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_Xplus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_rZminus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_rYminus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_rXminus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_Zminus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_Yminus);
            this.groupBox_IncrementalMove.Controls.Add(this.button_Xminus);
            this.groupBox_IncrementalMove.Controls.Add(this.radioButton_JointMove);
            this.groupBox_IncrementalMove.Controls.Add(this.radioButton_Tool);
            this.groupBox_IncrementalMove.Controls.Add(this.radioButton_Reference);
            this.groupBox_IncrementalMove.Location = new System.Drawing.Point(12, 12);
            this.groupBox_IncrementalMove.Name = "groupBox_IncrementalMove";
            this.groupBox_IncrementalMove.Size = new System.Drawing.Size(226, 354);
            this.groupBox_IncrementalMove.TabIndex = 0;
            this.groupBox_IncrementalMove.TabStop = false;
            this.groupBox_IncrementalMove.Text = "Incremental Move";
            // 
            // radioButton_Reference
            // 
            this.radioButton_Reference.AutoSize = true;
            this.radioButton_Reference.Location = new System.Drawing.Point(17, 32);
            this.radioButton_Reference.Name = "radioButton_Reference";
            this.radioButton_Reference.Size = new System.Drawing.Size(91, 20);
            this.radioButton_Reference.TabIndex = 0;
            this.radioButton_Reference.TabStop = true;
            this.radioButton_Reference.Text = "Reference";
            this.radioButton_Reference.UseVisualStyleBackColor = true;
            this.radioButton_Reference.CheckedChanged += new System.EventHandler(this.IncrementalMoveRadioCheckedChange);
            // 
            // radioButton_Tool
            // 
            this.radioButton_Tool.AutoSize = true;
            this.radioButton_Tool.Location = new System.Drawing.Point(17, 58);
            this.radioButton_Tool.Name = "radioButton_Tool";
            this.radioButton_Tool.Size = new System.Drawing.Size(56, 20);
            this.radioButton_Tool.TabIndex = 1;
            this.radioButton_Tool.TabStop = true;
            this.radioButton_Tool.Text = "Tool";
            this.radioButton_Tool.UseVisualStyleBackColor = true;
            this.radioButton_Tool.CheckedChanged += new System.EventHandler(this.IncrementalMoveRadioCheckedChange);
            // 
            // radioButton_JointMove
            // 
            this.radioButton_JointMove.AutoSize = true;
            this.radioButton_JointMove.Location = new System.Drawing.Point(17, 84);
            this.radioButton_JointMove.Name = "radioButton_JointMove";
            this.radioButton_JointMove.Size = new System.Drawing.Size(93, 20);
            this.radioButton_JointMove.TabIndex = 2;
            this.radioButton_JointMove.TabStop = true;
            this.radioButton_JointMove.Text = "Joint Move";
            this.radioButton_JointMove.UseVisualStyleBackColor = true;
            this.radioButton_JointMove.CheckedChanged += new System.EventHandler(this.IncrementalMoveRadioCheckedChange);
            // 
            // button_Xminus
            // 
            this.button_Xminus.Location = new System.Drawing.Point(17, 141);
            this.button_Xminus.Name = "button_Xminus";
            this.button_Xminus.Size = new System.Drawing.Size(75, 23);
            this.button_Xminus.TabIndex = 3;
            this.button_Xminus.Text = "X-";
            this.button_Xminus.UseVisualStyleBackColor = true;
            this.button_Xminus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_Yminus
            // 
            this.button_Yminus.Location = new System.Drawing.Point(17, 170);
            this.button_Yminus.Name = "button_Yminus";
            this.button_Yminus.Size = new System.Drawing.Size(75, 23);
            this.button_Yminus.TabIndex = 4;
            this.button_Yminus.Text = "Y-";
            this.button_Yminus.UseVisualStyleBackColor = true;
            this.button_Yminus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_Zminus
            // 
            this.button_Zminus.Location = new System.Drawing.Point(17, 199);
            this.button_Zminus.Name = "button_Zminus";
            this.button_Zminus.Size = new System.Drawing.Size(75, 23);
            this.button_Zminus.TabIndex = 5;
            this.button_Zminus.Text = "Z-";
            this.button_Zminus.UseVisualStyleBackColor = true;
            this.button_Zminus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_rXminus
            // 
            this.button_rXminus.Location = new System.Drawing.Point(17, 228);
            this.button_rXminus.Name = "button_rXminus";
            this.button_rXminus.Size = new System.Drawing.Size(75, 23);
            this.button_rXminus.TabIndex = 6;
            this.button_rXminus.Text = "rX-";
            this.button_rXminus.UseVisualStyleBackColor = true;
            this.button_rXminus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_rYminus
            // 
            this.button_rYminus.Location = new System.Drawing.Point(17, 257);
            this.button_rYminus.Name = "button_rYminus";
            this.button_rYminus.Size = new System.Drawing.Size(75, 23);
            this.button_rYminus.TabIndex = 7;
            this.button_rYminus.Text = "rY-";
            this.button_rYminus.UseVisualStyleBackColor = true;
            this.button_rYminus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_rZminus
            // 
            this.button_rZminus.Location = new System.Drawing.Point(17, 286);
            this.button_rZminus.Name = "button_rZminus";
            this.button_rZminus.Size = new System.Drawing.Size(75, 23);
            this.button_rZminus.TabIndex = 8;
            this.button_rZminus.Text = "rZ-";
            this.button_rZminus.UseVisualStyleBackColor = true;
            this.button_rZminus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_Xplus
            // 
            this.button_Xplus.Location = new System.Drawing.Point(98, 141);
            this.button_Xplus.Name = "button_Xplus";
            this.button_Xplus.Size = new System.Drawing.Size(75, 23);
            this.button_Xplus.TabIndex = 9;
            this.button_Xplus.Text = "X+";
            this.button_Xplus.UseVisualStyleBackColor = true;
            this.button_Xplus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_Yplus
            // 
            this.button_Yplus.Location = new System.Drawing.Point(98, 170);
            this.button_Yplus.Name = "button_Yplus";
            this.button_Yplus.Size = new System.Drawing.Size(75, 23);
            this.button_Yplus.TabIndex = 10;
            this.button_Yplus.Text = "Y+";
            this.button_Yplus.UseVisualStyleBackColor = true;
            this.button_Yplus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_Zplus
            // 
            this.button_Zplus.Location = new System.Drawing.Point(98, 199);
            this.button_Zplus.Name = "button_Zplus";
            this.button_Zplus.Size = new System.Drawing.Size(75, 23);
            this.button_Zplus.TabIndex = 11;
            this.button_Zplus.Text = "Z+";
            this.button_Zplus.UseVisualStyleBackColor = true;
            this.button_Zplus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_rXplus
            // 
            this.button_rXplus.Location = new System.Drawing.Point(98, 228);
            this.button_rXplus.Name = "button_rXplus";
            this.button_rXplus.Size = new System.Drawing.Size(75, 23);
            this.button_rXplus.TabIndex = 12;
            this.button_rXplus.Text = "rX+";
            this.button_rXplus.UseVisualStyleBackColor = true;
            this.button_rXplus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_rYplus
            // 
            this.button_rYplus.Location = new System.Drawing.Point(98, 257);
            this.button_rYplus.Name = "button_rYplus";
            this.button_rYplus.Size = new System.Drawing.Size(75, 23);
            this.button_rYplus.TabIndex = 13;
            this.button_rYplus.Text = "rY+";
            this.button_rYplus.UseVisualStyleBackColor = true;
            this.button_rYplus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // button_rZplus
            // 
            this.button_rZplus.Location = new System.Drawing.Point(98, 286);
            this.button_rZplus.Name = "button_rZplus";
            this.button_rZplus.Size = new System.Drawing.Size(75, 23);
            this.button_rZplus.TabIndex = 14;
            this.button_rZplus.Text = "rZ+";
            this.button_rZplus.UseVisualStyleBackColor = true;
            this.button_rZplus.Click += new System.EventHandler(this.IncrementalMovementClick);
            // 
            // textBox_DistanceStep
            // 
            this.textBox_DistanceStep.Location = new System.Drawing.Point(17, 113);
            this.textBox_DistanceStep.Name = "textBox_DistanceStep";
            this.textBox_DistanceStep.Size = new System.Drawing.Size(100, 22);
            this.textBox_DistanceStep.TabIndex = 15;
            this.textBox_DistanceStep.Text = "10";
            // 
            // groupBox_Mode
            // 
            this.groupBox_Mode.Controls.Add(this.button_RealRobot);
            this.groupBox_Mode.Controls.Add(this.button_Simulation);
            this.groupBox_Mode.Location = new System.Drawing.Point(244, 16);
            this.groupBox_Mode.Name = "groupBox_Mode";
            this.groupBox_Mode.Size = new System.Drawing.Size(111, 86);
            this.groupBox_Mode.TabIndex = 1;
            this.groupBox_Mode.TabStop = false;
            this.groupBox_Mode.Text = "Mode";
            // 
            // button_Simulation
            // 
            this.button_Simulation.Location = new System.Drawing.Point(6, 25);
            this.button_Simulation.Name = "button_Simulation";
            this.button_Simulation.Size = new System.Drawing.Size(94, 23);
            this.button_Simulation.TabIndex = 0;
            this.button_Simulation.Text = "Simulation";
            this.button_Simulation.UseVisualStyleBackColor = true;
            this.button_Simulation.Click += new System.EventHandler(this.btn_RobotSimulation_Click);
            // 
            // button_RealRobot
            // 
            this.button_RealRobot.Location = new System.Drawing.Point(6, 53);
            this.button_RealRobot.Name = "button_RealRobot";
            this.button_RealRobot.Size = new System.Drawing.Size(94, 23);
            this.button_RealRobot.TabIndex = 1;
            this.button_RealRobot.Text = "Real Robot";
            this.button_RealRobot.UseVisualStyleBackColor = true;
            this.button_RealRobot.Click += new System.EventHandler(this.btn_RobotRealRobot_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox_Mode);
            this.Controls.Add(this.groupBox_IncrementalMove);
            this.Name = "Form1";
            this.Text = "KukaMovementEditorWindow";
            this.groupBox_IncrementalMove.ResumeLayout(false);
            this.groupBox_IncrementalMove.PerformLayout();
            this.groupBox_Mode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_IncrementalMove;
        private System.Windows.Forms.RadioButton radioButton_JointMove;
        private System.Windows.Forms.RadioButton radioButton_Tool;
        private System.Windows.Forms.RadioButton radioButton_Reference;
        private System.Windows.Forms.Button button_rZplus;
        private System.Windows.Forms.Button button_rYplus;
        private System.Windows.Forms.Button button_rXplus;
        private System.Windows.Forms.Button button_rXminus;
        private System.Windows.Forms.Button button_Zminus;
        private System.Windows.Forms.Button button_Yminus;
        private System.Windows.Forms.Button button_Xminus;
        private System.Windows.Forms.Button button_rYminus;
        private System.Windows.Forms.Button button_rZminus;
        private System.Windows.Forms.Button button_Xplus;
        private System.Windows.Forms.Button button_Yplus;
        private System.Windows.Forms.Button button_Zplus;
        private System.Windows.Forms.TextBox textBox_DistanceStep;
        private System.Windows.Forms.GroupBox groupBox_Mode;
        private System.Windows.Forms.Button button_RealRobot;
        private System.Windows.Forms.Button button_Simulation;
    }
}

