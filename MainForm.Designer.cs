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
            this.ConnectWithEth1Check = new System.Windows.Forms.CheckBox();
            this.UseGoerliCheck = new System.Windows.Forms.CheckBox();
            this.StartOnStartupCheck = new System.Windows.Forms.CheckBox();
            this.InitialEth2SetupButton = new System.Windows.Forms.Button();
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
            // ConnectWithEth1Check
            // 
            this.ConnectWithEth1Check.AutoSize = true;
            this.ConnectWithEth1Check.Checked = true;
            this.ConnectWithEth1Check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ConnectWithEth1Check.Location = new System.Drawing.Point(12, 37);
            this.ConnectWithEth1Check.Name = "ConnectWithEth1Check";
            this.ConnectWithEth1Check.Size = new System.Drawing.Size(153, 19);
            this.ConnectWithEth1Check.TabIndex = 1;
            this.ConnectWithEth1Check.Text = "Connect with Eth1 node";
            this.ConnectWithEth1Check.UseVisualStyleBackColor = true;
            this.ConnectWithEth1Check.CheckedChanged += new System.EventHandler(this.ConnectWithEth1Check_CheckedChanged);
            //this.ConnectWithEth1Check.Enabled = false;
            // 
            // UseGoerliCheck
            // 
            this.UseGoerliCheck.AutoSize = true;
            this.UseGoerliCheck.Checked = true;
            this.UseGoerliCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseGoerliCheck.Location = new System.Drawing.Point(12, 12);
            this.UseGoerliCheck.Name = "UseGoerliCheck";
            this.UseGoerliCheck.Size = new System.Drawing.Size(167, 19);
            this.UseGoerliCheck.TabIndex = 2;
            this.UseGoerliCheck.Text = "Use Görli test net (for Eth1)";
            this.UseGoerliCheck.UseVisualStyleBackColor = true;
            this.UseGoerliCheck.CheckedChanged += new System.EventHandler(this.UseGoerliCheck_CheckedChanged);
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
            this.InitialEth2SetupButton.Text = "Initial Eth2 Setup (Download prysm and import medalla validator keys)";
            this.InitialEth2SetupButton.UseVisualStyleBackColor = true;
            this.InitialEth2SetupButton.Click += new System.EventHandler(this.InitialEth2SetupButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1415, 650);
            this.Controls.Add(this.InitialEth2SetupButton);
            this.Controls.Add(this.StartOnStartupCheck);
            this.Controls.Add(this.UseGoerliCheck);
            this.Controls.Add(this.ConnectWithEth1Check);
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
        private System.Windows.Forms.CheckBox ConnectWithEth1Check;
        private System.Windows.Forms.CheckBox UseGoerliCheck;
        private System.Windows.Forms.CheckBox StartOnStartupCheck;
        private System.Windows.Forms.Button InitialEth2SetupButton;
    }
}

