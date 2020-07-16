namespace LockMyEthTool.Views
{
    partial class ControlBox
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


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.AutostartCheck = new System.Windows.Forms.CheckBox();
            this.PasswordInput = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.KeyPathInput = new System.Windows.Forms.TextBox();
            this.KeyPathILabel = new System.Windows.Forms.Label();
            this.DataDirInput = new System.Windows.Forms.TextBox();
            this.DataDirLabel = new System.Windows.Forms.Label();
            this.ExecutablePathInput = new System.Windows.Forms.TextBox();
            this.ExecutablePathLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.HideCommandPromptCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OutputText
            // 
            this.OutputText.Location = new System.Drawing.Point(265, 45);
            this.OutputText.Name = "OutputText";
            this.OutputText.ReadOnly = true;
            this.OutputText.Size = new System.Drawing.Size(397, 142);
            this.OutputText.TabIndex = 1;
            this.OutputText.Text = "";
            this.OutputText.TextChanged += new System.EventHandler(this.OutputText_TextChanged);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(11, 52);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(141, 24);
            this.StartButton.TabIndex = 0;
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClicked);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(11, 81);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(141, 24);
            this.StopButton.TabIndex = 0;
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButtonClicked);
            // 
            // AutostartCheck
            // 
            this.AutostartCheck.AutoSize = true;
            this.AutostartCheck.BackColor = System.Drawing.SystemColors.Control;
            this.AutostartCheck.Location = new System.Drawing.Point(11, 111);
            this.AutostartCheck.Name = "AutostartCheck";
            this.AutostartCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.AutostartCheck.Size = new System.Drawing.Size(98, 19);
            this.AutostartCheck.TabIndex = 2;
            this.AutostartCheck.Text = "Auto restart";
            this.AutostartCheck.UseVisualStyleBackColor = false;
            this.AutostartCheck.CheckedChanged += new System.EventHandler(this.AutostartCheck_CheckedChanged);
            // 
            // PasswordInput
            // 
            this.PasswordInput.Location = new System.Drawing.Point(80, 163);
            this.PasswordInput.Name = "PasswordInput";
            this.PasswordInput.PasswordChar = '*';
            this.PasswordInput.Size = new System.Drawing.Size(163, 23);
            this.PasswordInput.TabIndex = 3;
            this.PasswordInput.TextChanged += new System.EventHandler(this.PasswordInput_TextChanged);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(11, 163);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(63, 15);
            this.PasswordLabel.TabIndex = 4;
            this.PasswordLabel.Text = "Password: ";
            // 
            // KeyPathInput
            // 
            this.KeyPathInput.Location = new System.Drawing.Point(164, 201);
            this.KeyPathInput.Name = "KeyPathInput";
            this.KeyPathInput.Size = new System.Drawing.Size(427, 23);
            this.KeyPathInput.TabIndex = 5;
            this.KeyPathInput.TextChanged += new System.EventHandler(this.KeyPathInput_TextChanged);
            // 
            // KeyPathILabel
            // 
            this.KeyPathILabel.AutoSize = true;
            this.KeyPathILabel.Location = new System.Drawing.Point(11, 201);
            this.KeyPathILabel.Name = "KeyPathILabel";
            this.KeyPathILabel.Size = new System.Drawing.Size(72, 15);
            this.KeyPathILabel.TabIndex = 6;
            this.KeyPathILabel.Text = "Key file path";
            // 
            // DataDirInput
            // 
            this.DataDirInput.Location = new System.Drawing.Point(164, 259);
            this.DataDirInput.Name = "DataDirInput";
            this.DataDirInput.Size = new System.Drawing.Size(427, 23);
            this.DataDirInput.TabIndex = 7;
            this.DataDirInput.TextChanged += new System.EventHandler(this.DataDirInput_TextChanged);
            // 
            // DataDirLabel
            // 
            this.DataDirLabel.AutoSize = true;
            this.DataDirLabel.Location = new System.Drawing.Point(11, 259);
            this.DataDirLabel.Name = "DataDirLabel";
            this.DataDirLabel.Size = new System.Drawing.Size(58, 15);
            this.DataDirLabel.TabIndex = 8;
            this.DataDirLabel.Text = "Data Path";
            // 
            // ExecutablePathInput
            // 
            this.ExecutablePathInput.Location = new System.Drawing.Point(164, 230);
            this.ExecutablePathInput.Name = "ExecutablePathInput";
            this.ExecutablePathInput.Size = new System.Drawing.Size(427, 23);
            this.ExecutablePathInput.TabIndex = 9;
            this.ExecutablePathInput.TextChanged += new System.EventHandler(this.ExecutablePathInput_TextChanged);
            // 
            // ExecutablePathLabel
            // 
            this.ExecutablePathLabel.AutoSize = true;
            this.ExecutablePathLabel.Location = new System.Drawing.Point(11, 230);
            this.ExecutablePathLabel.Name = "ExecutablePathLabel";
            this.ExecutablePathLabel.Size = new System.Drawing.Size(125, 15);
            this.ExecutablePathLabel.TabIndex = 10;
            this.ExecutablePathLabel.Text = "Executable folder path";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.TitleLabel.Location = new System.Drawing.Point(11, 11);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(0, 15);
            this.TitleLabel.TabIndex = 11;
            // 
            // HideCommandPromptCheck
            // 
            this.HideCommandPromptCheck.AutoSize = true;
            this.HideCommandPromptCheck.BackColor = System.Drawing.SystemColors.Control;
            this.HideCommandPromptCheck.Location = new System.Drawing.Point(11, 136);
            this.HideCommandPromptCheck.Name = "HideCommandPromptCheck";
            this.HideCommandPromptCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.HideCommandPromptCheck.Size = new System.Drawing.Size(88, 19);
            this.HideCommandPromptCheck.TabIndex = 12;
            this.HideCommandPromptCheck.Text = "Hide cmd";
            this.HideCommandPromptCheck.UseVisualStyleBackColor = false;
            this.HideCommandPromptCheck.CheckedChanged += new System.EventHandler(this.HideCommandPromptCheck_CheckedChanged);
            // 
            // ControlBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.HideCommandPromptCheck);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.ExecutablePathLabel);
            this.Controls.Add(this.ExecutablePathInput);
            this.Controls.Add(this.DataDirLabel);
            this.Controls.Add(this.DataDirInput);
            this.Controls.Add(this.KeyPathILabel);
            this.Controls.Add(this.KeyPathInput);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.PasswordInput);
            this.Controls.Add(this.AutostartCheck);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.OutputText);
            this.Name = "ControlBox";
            this.Size = new System.Drawing.Size(679, 302);
            this.Load += new System.EventHandler(this.ValidatorControlBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox AutostartCheck;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.RichTextBox OutputText;
        private System.Windows.Forms.TextBox PasswordInput;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox KeyPathInput;
        private System.Windows.Forms.Label KeyPathILabel;
        private System.Windows.Forms.TextBox DataDirInput;
        private System.Windows.Forms.Label DataDirLabel;
        private System.Windows.Forms.TextBox ExecutablePathInput;
        private System.Windows.Forms.Label ExecutablePathLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.CheckBox HideCommandPromptCheck;
    }
}
