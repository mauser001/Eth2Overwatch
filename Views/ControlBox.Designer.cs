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
            this.KeyPathInput = new System.Windows.Forms.TextBox();
            this.KeyPathLabel = new System.Windows.Forms.Label();
            this.DataDirInput = new System.Windows.Forms.TextBox();
            this.DataDirLabel = new System.Windows.Forms.Label();
            this.WalletDirInput = new System.Windows.Forms.TextBox();
            this.WalletDirLabel = new System.Windows.Forms.Label();
            this.ExecutablePathInput = new System.Windows.Forms.TextBox();
            this.ExecutablePathLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.HideCommandPromptCheck = new System.Windows.Forms.CheckBox();
            this.AdditionalCommandsLabel = new System.Windows.Forms.Label();
            this.AdditionalCommandsInput = new System.Windows.Forms.TextBox();
            this.KeyPathSelectButton = new System.Windows.Forms.Button();
            this.ExecutablePathSelectButton = new System.Windows.Forms.Button();
            this.DataDirSelectButton = new System.Windows.Forms.Button();
            this.WalletDirSelectButton = new System.Windows.Forms.Button();
            this.StateOutput = new System.Windows.Forms.RichTextBox();
            this.ShowErrorButton = new System.Windows.Forms.Button();
            this.ShowWarningButton = new System.Windows.Forms.Button();
            this.ShowInfoButton = new System.Windows.Forms.Button();
            this.ValidatorDetailsButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.LatestVersionCheckbox = new System.Windows.Forms.CheckBox();
            this.CurrentVersionInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OutputText
            // 
            this.OutputText.Location = new System.Drawing.Point(276, 73);
            this.OutputText.Name = "OutputText";
            this.OutputText.ReadOnly = true;
            this.OutputText.Size = new System.Drawing.Size(397, 114);
            this.OutputText.TabIndex = 1;
            this.OutputText.Tag = "";
            this.OutputText.Text = "";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(19, 45);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(114, 24);
            this.StartButton.TabIndex = 0;
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClicked);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(139, 45);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(114, 24);
            this.StopButton.TabIndex = 0;
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButtonClicked);
            // 
            // AutostartCheck
            // 
            this.AutostartCheck.AutoSize = true;
            this.AutostartCheck.BackColor = System.Drawing.SystemColors.Control;
            this.AutostartCheck.Location = new System.Drawing.Point(11, 73);
            this.AutostartCheck.Name = "AutostartCheck";
            this.AutostartCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.AutostartCheck.Size = new System.Drawing.Size(98, 19);
            this.AutostartCheck.TabIndex = 2;
            this.AutostartCheck.Text = "Auto restart";
            this.AutostartCheck.UseVisualStyleBackColor = false;
            this.AutostartCheck.CheckedChanged += new System.EventHandler(this.AutostartCheck_CheckedChanged);
            // 
            // KeyPathInput
            // 
            this.KeyPathInput.Location = new System.Drawing.Point(164, 201);
            this.KeyPathInput.Name = "KeyPathInput";
            this.KeyPathInput.Size = new System.Drawing.Size(427, 23);
            this.KeyPathInput.TabIndex = 5;
            this.KeyPathInput.TextChanged += new System.EventHandler(this.KeyPathInput_TextChanged);
            // 
            // KeyPathLabel
            // 
            this.KeyPathLabel.AutoSize = true;
            this.KeyPathLabel.Location = new System.Drawing.Point(11, 201);
            this.KeyPathLabel.Name = "KeyPathLabel";
            this.KeyPathLabel.Size = new System.Drawing.Size(125, 15);
            this.KeyPathLabel.TabIndex = 6;
            this.KeyPathLabel.Text = "Plaintext password file";
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
            // WalletDirInput
            // 
            this.WalletDirInput.Location = new System.Drawing.Point(164, 259);
            this.WalletDirInput.Name = "WalletDirInput";
            this.WalletDirInput.Size = new System.Drawing.Size(427, 23);
            this.WalletDirInput.TabIndex = 7;
            this.WalletDirInput.TextChanged += new System.EventHandler(this.WalletDirInput_TextChanged);
            // 
            // WalletDirLabel
            // 
            this.WalletDirLabel.AutoSize = true;
            this.WalletDirLabel.Location = new System.Drawing.Point(11, 259);
            this.WalletDirLabel.Name = "WalletDirLabel";
            this.WalletDirLabel.Size = new System.Drawing.Size(67, 15);
            this.WalletDirLabel.TabIndex = 8;
            this.WalletDirLabel.Text = "Wallet Path";
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
            this.HideCommandPromptCheck.Location = new System.Drawing.Point(11, 98);
            this.HideCommandPromptCheck.Name = "HideCommandPromptCheck";
            this.HideCommandPromptCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.HideCommandPromptCheck.Size = new System.Drawing.Size(88, 19);
            this.HideCommandPromptCheck.TabIndex = 12;
            this.HideCommandPromptCheck.Text = "Hide cmd";
            this.HideCommandPromptCheck.UseVisualStyleBackColor = false;
            this.HideCommandPromptCheck.CheckedChanged += new System.EventHandler(this.HideCommandPromptCheck_CheckedChanged);
            // 
            // AdditionalCommandsLabel
            // 
            this.AdditionalCommandsLabel.AutoSize = true;
            this.AdditionalCommandsLabel.Location = new System.Drawing.Point(11, 288);
            this.AdditionalCommandsLabel.Name = "AdditionalCommandsLabel";
            this.AdditionalCommandsLabel.Size = new System.Drawing.Size(125, 15);
            this.AdditionalCommandsLabel.TabIndex = 13;
            this.AdditionalCommandsLabel.Text = "Additional commands";
            // 
            // AdditionalCommandsInput
            // 
            this.AdditionalCommandsInput.Location = new System.Drawing.Point(164, 288);
            this.AdditionalCommandsInput.Name = "AdditionalCommandsInput";
            this.AdditionalCommandsInput.Size = new System.Drawing.Size(427, 23);
            this.AdditionalCommandsInput.TabIndex = 14;
            this.AdditionalCommandsInput.TextChanged += new System.EventHandler(this.AdditionalCommandsInput_TextChanged);
            // 
            // KeyPathSelectButton
            // 
            this.KeyPathSelectButton.Location = new System.Drawing.Point(598, 201);
            this.KeyPathSelectButton.Name = "KeyPathSelectButton";
            this.KeyPathSelectButton.Size = new System.Drawing.Size(75, 23);
            this.KeyPathSelectButton.TabIndex = 15;
            this.KeyPathSelectButton.Text = "Select";
            this.KeyPathSelectButton.UseVisualStyleBackColor = true;
            this.KeyPathSelectButton.Click += new System.EventHandler(this.KeyPathSelectButton_Click);
            // 
            // ExecutablePathSelectButton
            // 
            this.ExecutablePathSelectButton.Location = new System.Drawing.Point(598, 230);
            this.ExecutablePathSelectButton.Name = "ExecutablePathSelectButton";
            this.ExecutablePathSelectButton.Size = new System.Drawing.Size(75, 23);
            this.ExecutablePathSelectButton.TabIndex = 16;
            this.ExecutablePathSelectButton.Text = "Select";
            this.ExecutablePathSelectButton.UseVisualStyleBackColor = true;
            this.ExecutablePathSelectButton.Click += new System.EventHandler(this.ExecutablePathSelectButton_Click);
            // 
            // DataDirSelectButton
            // 
            this.DataDirSelectButton.Location = new System.Drawing.Point(598, 260);
            this.DataDirSelectButton.Name = "DataDirSelectButton";
            this.DataDirSelectButton.Size = new System.Drawing.Size(75, 23);
            this.DataDirSelectButton.TabIndex = 17;
            this.DataDirSelectButton.Text = "Select";
            this.DataDirSelectButton.UseVisualStyleBackColor = true;
            this.DataDirSelectButton.Click += new System.EventHandler(this.DataDirSelectButton_Click);
            // 
            // WalletDirSelectButton
            // 
            this.WalletDirSelectButton.Location = new System.Drawing.Point(598, 260);
            this.WalletDirSelectButton.Name = "WalletDirSelectButton";
            this.WalletDirSelectButton.Size = new System.Drawing.Size(75, 23);
            this.WalletDirSelectButton.TabIndex = 17;
            this.WalletDirSelectButton.Text = "Select";
            this.WalletDirSelectButton.UseVisualStyleBackColor = true;
            this.WalletDirSelectButton.Click += new System.EventHandler(this.WalletDirSelectButton_Click);
            // 
            // StateOutput
            // 
            this.StateOutput.Location = new System.Drawing.Point(276, 11);
            this.StateOutput.Name = "StateOutput";
            this.StateOutput.ReadOnly = true;
            this.StateOutput.Size = new System.Drawing.Size(397, 56);
            this.StateOutput.TabIndex = 18;
            this.StateOutput.Text = "";
            // 
            // ShowErrorButton
            // 
            this.ShowErrorButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.ShowErrorButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.ShowErrorButton.Location = new System.Drawing.Point(244, 76);
            this.ShowErrorButton.Name = "ShowErrorButton";
            this.ShowErrorButton.Size = new System.Drawing.Size(26, 23);
            this.ShowErrorButton.TabIndex = 19;
            this.ShowErrorButton.Text = "E";
            this.ShowErrorButton.UseVisualStyleBackColor = false;
            this.ShowErrorButton.Click += new System.EventHandler(this.ShowErrorButton_Click);
            // 
            // ShowWarningButton
            // 
            this.ShowWarningButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.ShowWarningButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.ShowWarningButton.Location = new System.Drawing.Point(244, 105);
            this.ShowWarningButton.Name = "ShowWarningButton";
            this.ShowWarningButton.Size = new System.Drawing.Size(26, 23);
            this.ShowWarningButton.TabIndex = 20;
            this.ShowWarningButton.Text = "W";
            this.ShowWarningButton.UseVisualStyleBackColor = false;
            this.ShowWarningButton.Click += new System.EventHandler(this.ShowWarningButton_Click);
            // 
            // ShowInfoButton
            // 
            this.ShowInfoButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.ShowInfoButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.ShowInfoButton.Location = new System.Drawing.Point(244, 134);
            this.ShowInfoButton.Name = "ShowInfoButton";
            this.ShowInfoButton.Size = new System.Drawing.Size(26, 23);
            this.ShowInfoButton.TabIndex = 21;
            this.ShowInfoButton.Text = "I";
            this.ShowInfoButton.UseVisualStyleBackColor = false;
            this.ShowInfoButton.Click += new System.EventHandler(this.ShowInfoButton_Click);
            // 
            // ValidatorDetailsButton
            // 
            this.ValidatorDetailsButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.ValidatorDetailsButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.ValidatorDetailsButton.Location = new System.Drawing.Point(218, 11);
            this.ValidatorDetailsButton.Name = "ValidatorDetailsButton";
            this.ValidatorDetailsButton.Size = new System.Drawing.Size(52, 23);
            this.ValidatorDetailsButton.TabIndex = 22;
            this.ValidatorDetailsButton.Text = "Details";
            this.ValidatorDetailsButton.UseVisualStyleBackColor = false;
            this.ValidatorDetailsButton.Visible = false;
            this.ValidatorDetailsButton.Click += new System.EventHandler(this.ValidatorDetailsButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(19, 124);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(45, 15);
            this.VersionLabel.TabIndex = 23;
            this.VersionLabel.Text = "Version";
            // 
            // LatestVersionCheckbox
            // 
            this.LatestVersionCheckbox.AutoSize = true;
            this.LatestVersionCheckbox.Location = new System.Drawing.Point(70, 124);
            this.LatestVersionCheckbox.Name = "LatestVersionCheckbox";
            this.LatestVersionCheckbox.Size = new System.Drawing.Size(57, 19);
            this.LatestVersionCheckbox.TabIndex = 24;
            this.LatestVersionCheckbox.Text = "Latest";
            this.LatestVersionCheckbox.UseVisualStyleBackColor = true;
            this.LatestVersionCheckbox.CheckedChanged += new System.EventHandler(this.LatestVersionCheckbox_CheckedChanged);
            // 
            // CurrentVersionInput
            // 
            this.CurrentVersionInput.Location = new System.Drawing.Point(87, 120);
            this.CurrentVersionInput.Name = "CurrentVersionInput";
            this.CurrentVersionInput.Size = new System.Drawing.Size(122, 23);
            this.CurrentVersionInput.TabIndex = 25;
            this.CurrentVersionInput.TextChanged += new System.EventHandler(this.CurrentVersionInput_TextChanged);
            // 
            // ControlBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.CurrentVersionInput);
            this.Controls.Add(this.LatestVersionCheckbox);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.ValidatorDetailsButton);
            this.Controls.Add(this.ShowInfoButton);
            this.Controls.Add(this.ShowWarningButton);
            this.Controls.Add(this.ShowErrorButton);
            this.Controls.Add(this.StateOutput);
            this.Controls.Add(this.WalletDirSelectButton);
            this.Controls.Add(this.DataDirSelectButton);
            this.Controls.Add(this.ExecutablePathSelectButton);
            this.Controls.Add(this.KeyPathSelectButton);
            this.Controls.Add(this.AdditionalCommandsInput);
            this.Controls.Add(this.AdditionalCommandsLabel);
            this.Controls.Add(this.HideCommandPromptCheck);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.ExecutablePathLabel);
            this.Controls.Add(this.ExecutablePathInput);
            this.Controls.Add(this.WalletDirLabel);
            this.Controls.Add(this.WalletDirInput);
            this.Controls.Add(this.DataDirLabel);
            this.Controls.Add(this.DataDirInput);
            this.Controls.Add(this.KeyPathLabel);
            this.Controls.Add(this.KeyPathInput);
            this.Controls.Add(this.AutostartCheck);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.OutputText);
            this.Name = "ControlBox";
            this.Size = new System.Drawing.Size(679, 332);
            this.Load += new System.EventHandler(this.ValidatorControlBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ValidatorDetailsButton;
        private System.Windows.Forms.CheckBox AutostartCheck;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.RichTextBox OutputText;
        private System.Windows.Forms.TextBox KeyPathInput;
        private System.Windows.Forms.Label KeyPathLabel;
        private System.Windows.Forms.TextBox DataDirInput;
        private System.Windows.Forms.Label DataDirLabel;
        private System.Windows.Forms.TextBox WalletDirInput;
        private System.Windows.Forms.Label WalletDirLabel;
        private System.Windows.Forms.TextBox ExecutablePathInput;
        private System.Windows.Forms.Label ExecutablePathLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.CheckBox HideCommandPromptCheck;
        private System.Windows.Forms.Label AdditionalCommandsLabel;
        private System.Windows.Forms.TextBox AdditionalCommandsInput;
        private System.Windows.Forms.Button KeyPathSelectButton;
        private System.Windows.Forms.Button ExecutablePathSelectButton;
        private System.Windows.Forms.Button DataDirSelectButton;
        private System.Windows.Forms.Button WalletDirSelectButton;
        private System.Windows.Forms.RichTextBox StateOutput;
        private System.Windows.Forms.Button ShowErrorButton;
        private System.Windows.Forms.Button ShowWarningButton;
        private System.Windows.Forms.Button ShowInfoButton;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.CheckBox LatestVersionCheckbox;
        private System.Windows.Forms.TextBox CurrentVersionInput;
    }
}
