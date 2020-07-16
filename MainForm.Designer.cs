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
            this.BeaconControlBox = new LockMyEthTool.Views.ControlBox("Beacon", new ProcessController(PROCESS_TYPES.BEACON_CHAIN));
            this.Eth1ControlBox = new LockMyEthTool.Views.ControlBox("Eth 1", new ProcessController(PROCESS_TYPES.ETH_1));
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ValidatorControlBox
            // 
            this.ValidatorControlBox.BackColor = System.Drawing.SystemColors.Control;
            this.ValidatorControlBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ValidatorControlBox.Location = new System.Drawing.Point(0, 300);
            this.ValidatorControlBox.Name = "ValidatorControlBox";
            this.ValidatorControlBox.Size = new System.Drawing.Size(700, 300);
            this.ValidatorControlBox.TabIndex = 0;
            // 
            // BeaconControlBox
            // 
            this.BeaconControlBox.BackColor = System.Drawing.SystemColors.Control;
            this.BeaconControlBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BeaconControlBox.Location = new System.Drawing.Point(700, 0);
            this.BeaconControlBox.Name = "BeaconControlBox";
            this.BeaconControlBox.Size = new System.Drawing.Size(700, 300);
            this.BeaconControlBox.TabIndex = 0;
            // 
            // Eth1ControlBox
            // 
            this.Eth1ControlBox.BackColor = System.Drawing.SystemColors.Control;
            this.Eth1ControlBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Eth1ControlBox.Location = new System.Drawing.Point(0, 0);
            this.Eth1ControlBox.Name = "Eth1ControlBox";
            this.Eth1ControlBox.Size = new System.Drawing.Size(700, 300);
            this.Eth1ControlBox.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1415, 650);
            this.Controls.Add(this.ValidatorControlBox);
            this.Controls.Add(this.BeaconControlBox);
            this.Controls.Add(this.Eth1ControlBox);
            this.Name = "MainForm";
            this.Text = "Eth2 Overwatch";
            this.ResumeLayout(false);

        }

        #endregion

        private ControlBox ValidatorControlBox;
        private ControlBox BeaconControlBox;
        private ControlBox Eth1ControlBox;
    }
}

