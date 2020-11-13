using LockMyEthTool.Controllers;
using LockMyEthTool.Views;

namespace LockMyEthTool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeCustomComponents()
        {
            this.ValidatorControlBox = new LockMyEthTool.Views.ControlBox("Validator", new ProcessController(PROCESS_TYPES.VALIDATOR));
            this.Boxes.Add(this.ValidatorControlBox);
            this.BeaconControlBox = new LockMyEthTool.Views.ControlBox("Beacon", new ProcessController(PROCESS_TYPES.BEACON_CHAIN));
            this.Boxes.Add(this.BeaconControlBox);
            this.Eth1ControlBox = new LockMyEthTool.Views.ControlBox("Eth 1", new ProcessController(PROCESS_TYPES.ETH_1));
            this.Boxes.Add(this.Eth1ControlBox);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StartOnStartupCheck = new System.Windows.Forms.CheckBox();
            this.InitialEth2SetupButton = new System.Windows.Forms.Button();
            this.Eth2TestNetLabel = new System.Windows.Forms.Label();
            this.Eth2TestNet = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ValidatorControlBox
            // 
            this.ValidatorControlBox.BackColor = System.Drawing.SystemColors.Control;
            this.ValidatorControlBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ValidatorControlBox.Location = new System.Drawing.Point(0, 320);
            this.ValidatorControlBox.Name = "ValidatorControlBox";
            this.ValidatorControlBox.Size = new System.Drawing.Size(700, 320);
            this.ValidatorControlBox.TabIndex = 0;
            // 
            // BeaconControlBox
            // 
            this.BeaconControlBox.BackColor = System.Drawing.SystemColors.Control;
            this.BeaconControlBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BeaconControlBox.Location = new System.Drawing.Point(700, 0);
            this.BeaconControlBox.Name = "BeaconControlBox";
            this.BeaconControlBox.Size = new System.Drawing.Size(700, 320);
            this.BeaconControlBox.TabIndex = 0;
            // 
            // Eth1ControlBox
            // 
            this.Eth1ControlBox.BackColor = System.Drawing.SystemColors.Control;
            this.Eth1ControlBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Eth1ControlBox.Location = new System.Drawing.Point(700, 320);
            this.Eth1ControlBox.Name = "Eth1ControlBox";
            this.Eth1ControlBox.Size = new System.Drawing.Size(700, 320);
            this.Eth1ControlBox.TabIndex = 0;
            // 
            // StartOnStartupCheck
            // 
            this.StartOnStartupCheck.AutoSize = true;
            this.StartOnStartupCheck.Checked = true;
            this.StartOnStartupCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StartOnStartupCheck.Location = new System.Drawing.Point(12, 62);
            this.StartOnStartupCheck.Name = "StartOnStartupCheck";
            this.StartOnStartupCheck.Size = new System.Drawing.Size(143, 19);
            this.StartOnStartupCheck.TabIndex = 3;
            this.StartOnStartupCheck.Text = "Start on windows start";
            this.StartOnStartupCheck.UseVisualStyleBackColor = true;
            this.StartOnStartupCheck.CheckedChanged += new System.EventHandler(this.StartOnStartupCheck_CheckedChanged);
            // 
            // InitialEth2SetupButton
            // 
            this.InitialEth2SetupButton.Location = new System.Drawing.Point(12, 276);
            this.InitialEth2SetupButton.Name = "InitialEth2SetupButton";
            this.InitialEth2SetupButton.Size = new System.Drawing.Size(424, 23);
            this.InitialEth2SetupButton.TabIndex = 4;
            this.InitialEth2SetupButton.Text = "Eth2 Setup (Download executables and import validator keys)";
            this.InitialEth2SetupButton.UseVisualStyleBackColor = true;
            this.InitialEth2SetupButton.Click += new System.EventHandler(this.InitialEth2SetupButton_Click);
            // 
            // Eth2TestNetLabel
            // 
            this.Eth2TestNetLabel.AutoSize = true;
            this.Eth2TestNetLabel.Location = new System.Drawing.Point(12, 25);
            this.Eth2TestNetLabel.Name = "Eth2TestNetLabel";
            this.Eth2TestNetLabel.Size = new System.Drawing.Size(185, 15);
            this.Eth2TestNetLabel.TabIndex = 5;
            this.Eth2TestNetLabel.Text = "Eth2 testnet (emtpy for main net):";
            // 
            // Eth2TestNet
            // 
            this.Eth2TestNet.Location = new System.Drawing.Point(203, 22);
            this.Eth2TestNet.Name = "Eth2Testnet";
            this.Eth2TestNet.Size = new System.Drawing.Size(164, 23);
            this.Eth2TestNet.TabIndex = 6;
            this.Eth2TestNet.TextChanged += new System.EventHandler(this.Eth2Testnet_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1415, 650);
            this.Controls.Add(this.Eth2TestNet);
            this.Controls.Add(this.Eth2TestNetLabel);
            this.Controls.Add(this.InitialEth2SetupButton);
            this.Controls.Add(this.StartOnStartupCheck);
            this.Controls.Add(this.ValidatorControlBox);
            this.Controls.Add(this.BeaconControlBox);
            this.Controls.Add(this.Eth1ControlBox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Eth2 Overwatch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlBox ValidatorControlBox;
        private ControlBox BeaconControlBox;
        private ControlBox Eth1ControlBox;
        private System.Windows.Forms.CheckBox StartOnStartupCheck;
        private System.Windows.Forms.Button InitialEth2SetupButton;
        private System.Windows.Forms.Label Eth2TestNetLabel;
        private System.Windows.Forms.TextBox Eth2TestNet;
    }
}

