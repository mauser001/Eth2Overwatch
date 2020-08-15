namespace Eth2Overwatch.Views
{
    partial class InitialEth2SetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TextBox KeyDescription;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialEth2SetupForm));
            this.PickPrysmFolderILabel = new System.Windows.Forms.Label();
            this.PickPrysmFolderInput = new System.Windows.Forms.TextBox();
            this.PickPrysmFolderButton = new System.Windows.Forms.Button();
            this.DownloadBeaconButton = new System.Windows.Forms.Button();
            this.OutputText = new System.Windows.Forms.TextBox();
            this.ShowDownlaodedPrysm = new System.Windows.Forms.Label();
            this.ShowInExplorerButton = new System.Windows.Forms.Button();
            this.BeaconChainIsReadyLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ValidatorGroup = new System.Windows.Forms.GroupBox();
            this.PickWalletFolderSelect = new System.Windows.Forms.Button();
            this.PickWalletFolderInput = new System.Windows.Forms.TextBox();
            this.PickWalletFolder = new System.Windows.Forms.Label();
            this.KeyFileSelect = new System.Windows.Forms.Button();
            this.KeyFileFolderInput = new System.Windows.Forms.TextBox();
            this.complete = new System.Windows.Forms.LinkLabel();
            this.PickMedallaKeyFilesLabel = new System.Windows.Forms.Label();
            this.CreatePasswordFilesButton = new System.Windows.Forms.Button();
            this.DownloadValidatorButton = new System.Windows.Forms.Button();
            this.ValidatorReadyLabel = new System.Windows.Forms.Label();
            KeyDescription = new System.Windows.Forms.TextBox();
            this.ValidatorGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // KeyDescription
            // 
            KeyDescription.BackColor = System.Drawing.SystemColors.Window;
            KeyDescription.Enabled = false;
            KeyDescription.ForeColor = System.Drawing.Color.Red;
            KeyDescription.Location = new System.Drawing.Point(15, 244);
            KeyDescription.Multiline = true;
            KeyDescription.Name = "KeyDescription";
            KeyDescription.ReadOnly = true;
            KeyDescription.Size = new System.Drawing.Size(532, 56);
            KeyDescription.TabIndex = 5;
            KeyDescription.Text = resources.GetString("KeyDescription.Text");
            // 
            // PickPrysmFolderILabel
            // 
            this.PickPrysmFolderILabel.AutoSize = true;
            this.PickPrysmFolderILabel.Location = new System.Drawing.Point(13, 13);
            this.PickPrysmFolderILabel.Name = "PickPrysmFolderILabel";
            this.PickPrysmFolderILabel.Size = new System.Drawing.Size(492, 15);
            this.PickPrysmFolderILabel.TabIndex = 0;
            this.PickPrysmFolderILabel.Text = "1. Pick a folder to store the prysm.bat (Executable to run the beacon chain and t" +
    "he validator)";
            // 
            // PickPrysmFolderInput
            // 
            this.PickPrysmFolderInput.Location = new System.Drawing.Point(13, 42);
            this.PickPrysmFolderInput.Name = "PickPrysmFolderInput";
            this.PickPrysmFolderInput.Size = new System.Drawing.Size(362, 23);
            this.PickPrysmFolderInput.TabIndex = 1;
            this.PickPrysmFolderInput.TextChanged += new System.EventHandler(this.PickPrysmFolderInput_TextChanged);
            // 
            // PickPrysmFolderButton
            // 
            this.PickPrysmFolderButton.Location = new System.Drawing.Point(381, 41);
            this.PickPrysmFolderButton.Name = "PickPrysmFolderButton";
            this.PickPrysmFolderButton.Size = new System.Drawing.Size(75, 23);
            this.PickPrysmFolderButton.TabIndex = 2;
            this.PickPrysmFolderButton.Text = "Select";
            this.PickPrysmFolderButton.UseVisualStyleBackColor = true;
            this.PickPrysmFolderButton.Click += new System.EventHandler(this.PickPrysmFolderButton_Click);
            // 
            // DownloadBeaconButton
            // 
            this.DownloadBeaconButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DownloadBeaconButton.Location = new System.Drawing.Point(13, 83);
            this.DownloadBeaconButton.Name = "DownloadBeaconButton";
            this.DownloadBeaconButton.Size = new System.Drawing.Size(239, 23);
            this.DownloadBeaconButton.TabIndex = 3;
            this.DownloadBeaconButton.Text = "2. Download beacon chain executables";
            this.DownloadBeaconButton.UseVisualStyleBackColor = true;
            this.DownloadBeaconButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // OutputText
            // 
            this.OutputText.Location = new System.Drawing.Point(597, 41);
            this.OutputText.Multiline = true;
            this.OutputText.Name = "OutputText";
            this.OutputText.ReadOnly = true;
            this.OutputText.Size = new System.Drawing.Size(582, 357);
            this.OutputText.TabIndex = 4;
            // 
            // ShowDownlaodedPrysm
            // 
            this.ShowDownlaodedPrysm.AutoSize = true;
            this.ShowDownlaodedPrysm.Location = new System.Drawing.Point(309, 169);
            this.ShowDownlaodedPrysm.Name = "ShowDownlaodedPrysm";
            this.ShowDownlaodedPrysm.Size = new System.Drawing.Size(173, 15);
            this.ShowDownlaodedPrysm.TabIndex = 5;
            this.ShowDownlaodedPrysm.Tag = "";
            this.ShowDownlaodedPrysm.Text = "Show downloaded executables:";
            // 
            // ShowInExplorerButton
            // 
            this.ShowInExplorerButton.Location = new System.Drawing.Point(488, 165);
            this.ShowInExplorerButton.Name = "ShowInExplorerButton";
            this.ShowInExplorerButton.Size = new System.Drawing.Size(103, 22);
            this.ShowInExplorerButton.TabIndex = 6;
            this.ShowInExplorerButton.Text = "Show in explorer";
            this.ShowInExplorerButton.UseVisualStyleBackColor = true;
            this.ShowInExplorerButton.Click += new System.EventHandler(this.ShowInExplorerButton_Click);
            // 
            // BeaconChainIsReadyLabel
            // 
            this.BeaconChainIsReadyLabel.AutoSize = true;
            this.BeaconChainIsReadyLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.BeaconChainIsReadyLabel.Location = new System.Drawing.Point(13, 110);
            this.BeaconChainIsReadyLabel.Name = "BeaconChainIsReadyLabel";
            this.BeaconChainIsReadyLabel.Size = new System.Drawing.Size(228, 21);
            this.BeaconChainIsReadyLabel.TabIndex = 7;
            this.BeaconChainIsReadyLabel.Text = "Executable for the Beacon Chain is ready.";
            this.BeaconChainIsReadyLabel.UseCompatibleTextRendering = true;
            this.BeaconChainIsReadyLabel.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(1102, 490);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ValidatorGroup
            // 
            this.ValidatorGroup.Controls.Add(this.PickWalletFolderSelect);
            this.ValidatorGroup.Controls.Add(this.PickWalletFolderInput);
            this.ValidatorGroup.Controls.Add(this.PickWalletFolder);
            this.ValidatorGroup.Controls.Add(this.KeyFileSelect);
            this.ValidatorGroup.Controls.Add(this.KeyFileFolderInput);
            this.ValidatorGroup.Controls.Add(this.complete);
            this.ValidatorGroup.Controls.Add(this.PickMedallaKeyFilesLabel);
            this.ValidatorGroup.Controls.Add(KeyDescription);
            this.ValidatorGroup.Controls.Add(this.CreatePasswordFilesButton);
            this.ValidatorGroup.Enabled = false;
            this.ValidatorGroup.Location = new System.Drawing.Point(22, 193);
            this.ValidatorGroup.Name = "ValidatorGroup";
            this.ValidatorGroup.Size = new System.Drawing.Size(569, 320);
            this.ValidatorGroup.TabIndex = 9;
            this.ValidatorGroup.TabStop = false;
            this.ValidatorGroup.Text = "Validator Setup (from Medalla launchpad";
            // 
            // PickWalletFolderSelect
            // 
            this.PickWalletFolderSelect.Location = new System.Drawing.Point(360, 127);
            this.PickWalletFolderSelect.Name = "PickWalletFolderSelect";
            this.PickWalletFolderSelect.Size = new System.Drawing.Size(75, 23);
            this.PickWalletFolderSelect.TabIndex = 12;
            this.PickWalletFolderSelect.Text = "Select";
            this.PickWalletFolderSelect.UseVisualStyleBackColor = true;
            this.PickWalletFolderSelect.Click += new System.EventHandler(this.PickWalletFolderButton_Click);
            // 
            // PickWalletFolderInput
            // 
            this.PickWalletFolderInput.Location = new System.Drawing.Point(15, 127);
            this.PickWalletFolderInput.Name = "PickWalletFolderInput";
            this.PickWalletFolderInput.Size = new System.Drawing.Size(338, 23);
            this.PickWalletFolderInput.TabIndex = 11;
            this.PickWalletFolderInput.TextChanged += new System.EventHandler(this.PasswordFilePathInput_TextChanged);
            // 
            // PickWalletFolder
            // 
            this.PickWalletFolder.AutoSize = true;
            this.PickWalletFolder.Location = new System.Drawing.Point(15, 108);
            this.PickWalletFolder.Name = "PickWalletFolder";
            this.PickWalletFolder.Size = new System.Drawing.Size(271, 15);
            this.PickWalletFolder.TabIndex = 10;
            this.PickWalletFolder.Text = "5. Pick a folder where your wallet should be stored";
            // 
            // KeyFileSelect
            // 
            this.KeyFileSelect.Location = new System.Drawing.Point(360, 72);
            this.KeyFileSelect.Name = "KeyFileSelect";
            this.KeyFileSelect.Size = new System.Drawing.Size(75, 23);
            this.KeyFileSelect.TabIndex = 9;
            this.KeyFileSelect.Text = "Select";
            this.KeyFileSelect.UseVisualStyleBackColor = true;
            this.KeyFileSelect.Click += new System.EventHandler(this.KeyFileSelect_Click);
            // 
            // KeyFileFolderInput
            // 
            this.KeyFileFolderInput.Location = new System.Drawing.Point(15, 72);
            this.KeyFileFolderInput.Name = "KeyFileFolderInput";
            this.KeyFileFolderInput.Size = new System.Drawing.Size(338, 23);
            this.KeyFileFolderInput.TabIndex = 8;
            this.KeyFileFolderInput.TextChanged += new System.EventHandler(this.PasswordFilePathInput_TextChanged);
            // 
            // complete
            // 
            this.complete.AutoSize = true;
            this.complete.Location = new System.Drawing.Point(15, 23);
            this.complete.Name = "complete";
            this.complete.Size = new System.Drawing.Size(156, 15);
            this.complete.TabIndex = 7;
            this.complete.TabStop = true;
            this.complete.Text = "3. Complete Eth2 launchpad";
            this.complete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GotoLaunchpadLink_LinkClicked);
            // 
            // PickMedallaKeyFilesLabel
            // 
            this.PickMedallaKeyFilesLabel.AutoSize = true;
            this.PickMedallaKeyFilesLabel.Location = new System.Drawing.Point(15, 54);
            this.PickMedallaKeyFilesLabel.Name = "PickMedallaKeyFilesLabel";
            this.PickMedallaKeyFilesLabel.Size = new System.Drawing.Size(245, 15);
            this.PickMedallaKeyFilesLabel.TabIndex = 6;
            this.PickMedallaKeyFilesLabel.Text = "4. Pick folder containing the Medalla key files";
            // 
            // CreatePasswordFilesButton
            // 
            this.CreatePasswordFilesButton.Location = new System.Drawing.Point(15, 170);
            this.CreatePasswordFilesButton.Name = "CreatePasswordFilesButton";
            this.CreatePasswordFilesButton.Size = new System.Drawing.Size(180, 24);
            this.CreatePasswordFilesButton.TabIndex = 4;
            this.CreatePasswordFilesButton.Text = "7. Import Medalla account";
            this.CreatePasswordFilesButton.UseVisualStyleBackColor = true;
            this.CreatePasswordFilesButton.Click += new System.EventHandler(this.CreatePasswordFilesButton_Click);
            // 
            // DownloadValidatorButton
            // 
            this.DownloadValidatorButton.Location = new System.Drawing.Point(12, 134);
            this.DownloadValidatorButton.Name = "DownloadValidatorButton";
            this.DownloadValidatorButton.Size = new System.Drawing.Size(240, 23);
            this.DownloadValidatorButton.TabIndex = 11;
            this.DownloadValidatorButton.Text = "3. Download validator exaecutables";
            this.DownloadValidatorButton.UseVisualStyleBackColor = true;
            this.DownloadValidatorButton.Click += new System.EventHandler(this.DownloadValidatorButton_Click);
            // 
            // ValidatorReadyLabel
            // 
            this.ValidatorReadyLabel.AutoSize = true;
            this.ValidatorReadyLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.ValidatorReadyLabel.Location = new System.Drawing.Point(13, 163);
            this.ValidatorReadyLabel.Name = "ValidatorReadyLabel";
            this.ValidatorReadyLabel.Size = new System.Drawing.Size(203, 21);
            this.ValidatorReadyLabel.TabIndex = 12;
            this.ValidatorReadyLabel.Text = "Executable for the Validator is ready.";
            this.ValidatorReadyLabel.UseCompatibleTextRendering = true;
            this.ValidatorReadyLabel.Visible = false;
            // 
            // InitialEth2SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 525);
            this.Controls.Add(this.ValidatorReadyLabel);
            this.Controls.Add(this.DownloadValidatorButton);
            this.Controls.Add(this.ValidatorGroup);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BeaconChainIsReadyLabel);
            this.Controls.Add(this.ShowDownlaodedPrysm);
            this.Controls.Add(this.ShowInExplorerButton);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.DownloadBeaconButton);
            this.Controls.Add(this.PickPrysmFolderButton);
            this.Controls.Add(this.PickPrysmFolderInput);
            this.Controls.Add(this.PickPrysmFolderILabel);
            this.Name = "InitialEth2SetupForm";
            this.Text = "Initial Eth2 beacon chain and validator setup";
            this.ValidatorGroup.ResumeLayout(false);
            this.ValidatorGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PickPrysmFolderILabel;
        private System.Windows.Forms.TextBox PickPrysmFolderInput;
        private System.Windows.Forms.Button PickPrysmFolderButton;
        private System.Windows.Forms.Button DownloadBeaconButton;
        private System.Windows.Forms.TextBox OutputText;
        private System.Windows.Forms.Label ShowDownlaodedPrysm;
        private System.Windows.Forms.Button ShowInExplorerButton;
        private System.Windows.Forms.Label BeaconChainIsReadyLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.GroupBox ValidatorGroup;
        private System.Windows.Forms.Button CreatePasswordFilesButton;
        private System.Windows.Forms.TextBox KeyDescription;
        private System.Windows.Forms.LinkLabel complete;
        private System.Windows.Forms.Label PickMedallaKeyFilesLabel;
        private System.Windows.Forms.Button KeyFileSelect;
        private System.Windows.Forms.TextBox KeyFileFolderInput;
        private System.Windows.Forms.Button PickWalletFolderSelect;
        private System.Windows.Forms.TextBox PickWalletFolderInput;
        private System.Windows.Forms.Label PickWalletFolder;
        private System.Windows.Forms.Button DownloadValidatorButton;
        private System.Windows.Forms.Label ValidatorReadyLabel;
    }
}