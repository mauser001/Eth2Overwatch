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
            this.DownloadButton = new System.Windows.Forms.Button();
            this.OutputText = new System.Windows.Forms.TextBox();
            this.ShowDownlaodedPrysm = new System.Windows.Forms.Label();
            this.ShowInExplorerButton = new System.Windows.Forms.Button();
            this.BeaconChainIsReadyLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ValidatorGroup = new System.Windows.Forms.GroupBox();
            this.GotoPrysmLink = new System.Windows.Forms.LinkLabel();
            this.CreateValidatorKeysButton = new System.Windows.Forms.Button();
            this.ValidatorKeyPathSelect = new System.Windows.Forms.Button();
            this.PickValidatorKeyPathLabel = new System.Windows.Forms.Label();
            this.ValidatorKeyPathInput = new System.Windows.Forms.TextBox();
            KeyDescription = new System.Windows.Forms.TextBox();
            this.ValidatorGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // KeyDescription
            // 
            KeyDescription.BackColor = System.Drawing.SystemColors.Window;
            KeyDescription.Enabled = false;
            KeyDescription.ForeColor = System.Drawing.Color.Red;
            KeyDescription.Location = new System.Drawing.Point(14, 112);
            KeyDescription.Multiline = true;
            KeyDescription.Name = "KeyDescription";
            KeyDescription.ReadOnly = true;
            KeyDescription.Size = new System.Drawing.Size(532, 55);
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
            // DownloadButton
            // 
            this.DownloadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DownloadButton.Location = new System.Drawing.Point(13, 83);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(159, 23);
            this.DownloadButton.TabIndex = 3;
            this.DownloadButton.Text = "2. Download prym.bat";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
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
            this.ShowDownlaodedPrysm.Location = new System.Drawing.Point(211, 87);
            this.ShowDownlaodedPrysm.Name = "ShowDownlaodedPrysm";
            this.ShowDownlaodedPrysm.Size = new System.Drawing.Size(164, 15);
            this.ShowDownlaodedPrysm.TabIndex = 5;
            this.ShowDownlaodedPrysm.Tag = "";
            this.ShowDownlaodedPrysm.Text = "Show downloaded prysm.bat:";
            // 
            // ShowInExplorerButton
            // 
            this.ShowInExplorerButton.Location = new System.Drawing.Point(381, 83);
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
            this.BeaconChainIsReadyLabel.Location = new System.Drawing.Point(13, 126);
            this.BeaconChainIsReadyLabel.Name = "BeaconChainIsReadyLabel";
            this.BeaconChainIsReadyLabel.Size = new System.Drawing.Size(296, 21);
            this.BeaconChainIsReadyLabel.TabIndex = 7;
            this.BeaconChainIsReadyLabel.Text = "Executable (prysm.bat) for the Beacon Chain is ready.";
            this.BeaconChainIsReadyLabel.UseCompatibleTextRendering = true;
            this.BeaconChainIsReadyLabel.Visible = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(1104, 415);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ValidatorGroup
            // 
            this.ValidatorGroup.Controls.Add(this.GotoPrysmLink);
            this.ValidatorGroup.Controls.Add(KeyDescription);
            this.ValidatorGroup.Controls.Add(this.CreateValidatorKeysButton);
            this.ValidatorGroup.Controls.Add(this.ValidatorKeyPathSelect);
            this.ValidatorGroup.Controls.Add(this.PickValidatorKeyPathLabel);
            this.ValidatorGroup.Controls.Add(this.ValidatorKeyPathInput);
            this.ValidatorGroup.Enabled = false;
            this.ValidatorGroup.Location = new System.Drawing.Point(22, 193);
            this.ValidatorGroup.Name = "ValidatorGroup";
            this.ValidatorGroup.Size = new System.Drawing.Size(569, 205);
            this.ValidatorGroup.TabIndex = 9;
            this.ValidatorGroup.TabStop = false;
            this.ValidatorGroup.Text = "Validator Setup";
            // 
            // GotoPrysmLink
            // 
            this.GotoPrysmLink.AutoSize = true;
            this.GotoPrysmLink.Location = new System.Drawing.Point(14, 174);
            this.GotoPrysmLink.Name = "GotoPrysmLink";
            this.GotoPrysmLink.Size = new System.Drawing.Size(551, 15);
            this.GotoPrysmLink.TabIndex = 6;
            this.GotoPrysmLink.TabStop = true;
            this.GotoPrysmLink.Text = "5. When you have the keys atart the beacon chain and validator and then goto prys" +
    "m labs for activation";
            this.GotoPrysmLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GotoPrysmLink_LinkClicked);
            // 
            // CreateValidatorKeysButton
            // 
            this.CreateValidatorKeysButton.Location = new System.Drawing.Point(15, 80);
            this.CreateValidatorKeysButton.Name = "CreateValidatorKeysButton";
            this.CreateValidatorKeysButton.Size = new System.Drawing.Size(180, 23);
            this.CreateValidatorKeysButton.TabIndex = 4;
            this.CreateValidatorKeysButton.Text = "4. Create validator keys";
            this.CreateValidatorKeysButton.UseVisualStyleBackColor = true;
            this.CreateValidatorKeysButton.Click += new System.EventHandler(this.CreateValidatorKeysButton_Click);
            // 
            // ValidatorKeyPathSelect
            // 
            this.ValidatorKeyPathSelect.Location = new System.Drawing.Point(360, 41);
            this.ValidatorKeyPathSelect.Name = "ValidatorKeyPathSelect";
            this.ValidatorKeyPathSelect.Size = new System.Drawing.Size(75, 23);
            this.ValidatorKeyPathSelect.TabIndex = 3;
            this.ValidatorKeyPathSelect.Text = "Select";
            this.ValidatorKeyPathSelect.UseVisualStyleBackColor = true;
            this.ValidatorKeyPathSelect.Click += new System.EventHandler(this.ValidatorKeyPathSelect_Click);
            // 
            // PickValidatorKeyPathLabel
            // 
            this.PickValidatorKeyPathLabel.AutoSize = true;
            this.PickValidatorKeyPathLabel.Location = new System.Drawing.Point(15, 23);
            this.PickValidatorKeyPathLabel.Name = "PickValidatorKeyPathLabel";
            this.PickValidatorKeyPathLabel.Size = new System.Drawing.Size(229, 15);
            this.PickValidatorKeyPathLabel.TabIndex = 2;
            this.PickValidatorKeyPathLabel.Text = "3. Pick a folder to store your validator keys";
            // 
            // ValidatorKeyPathInput
            // 
            this.ValidatorKeyPathInput.Location = new System.Drawing.Point(15, 41);
            this.ValidatorKeyPathInput.Name = "ValidatorKeyPathInput";
            this.ValidatorKeyPathInput.Size = new System.Drawing.Size(338, 23);
            this.ValidatorKeyPathInput.TabIndex = 1;
            this.ValidatorKeyPathInput.TextChanged += new System.EventHandler(this.ValidatorKeyPathInput_TextChanged);
            // 
            // InitialEth2SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 450);
            this.Controls.Add(this.ValidatorGroup);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BeaconChainIsReadyLabel);
            this.Controls.Add(this.ShowDownlaodedPrysm);
            this.Controls.Add(this.ShowInExplorerButton);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.DownloadButton);
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
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.TextBox OutputText;
        private System.Windows.Forms.Label ShowDownlaodedPrysm;
        private System.Windows.Forms.Button ShowInExplorerButton;
        private System.Windows.Forms.Label BeaconChainIsReadyLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.GroupBox ValidatorGroup;
        private System.Windows.Forms.Label PickValidatorKeyPathLabel;
        private System.Windows.Forms.TextBox ValidatorKeyPathInput;
        private System.Windows.Forms.Button ValidatorKeyPathSelect;
        private System.Windows.Forms.Button CreateValidatorKeysButton;
        private System.Windows.Forms.TextBox KeyDescription;
        private System.Windows.Forms.LinkLabel GotoPrysmLink;
    }
}