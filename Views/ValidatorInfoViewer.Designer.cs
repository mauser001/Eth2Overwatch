namespace Eth2Overwatch.Views
{
    partial class ValidatorInfoViewer
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
            this.FlowLayoutContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.ReportPathLabel = new System.Windows.Forms.Label();
            this.ReportPathInput = new System.Windows.Forms.TextBox();
            this.ReportKeyLabel = new System.Windows.Forms.Label();
            this.ReportKeyInput = new System.Windows.Forms.TextBox();
            this.ReportLabelLabel = new System.Windows.Forms.Label();
            this.ReportLabelInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FlowLayoutContainer
            // 
            this.FlowLayoutContainer.AutoScroll = true;
            this.FlowLayoutContainer.Location = new System.Drawing.Point(12, 45);
            this.FlowLayoutContainer.Name = "FlowLayoutContainer";
            this.FlowLayoutContainer.Size = new System.Drawing.Size(876, 393);
            this.FlowLayoutContainer.TabIndex = 0;
            // 
            // ReportPathLabel
            // 
            this.ReportPathLabel.AutoSize = true;
            this.ReportPathLabel.Location = new System.Drawing.Point(10, 12);
            this.ReportPathLabel.Name = "ReportPathLabel";
            this.ReportPathLabel.Size = new System.Drawing.Size(72, 15);
            this.ReportPathLabel.TabIndex = 1;
            this.ReportPathLabel.Text = "Report path:";
            // 
            // ReportPathInput
            // 
            this.ReportPathInput.Location = new System.Drawing.Point(91, 9);
            this.ReportPathInput.Name = "ReportPathInput";
            this.ReportPathInput.Size = new System.Drawing.Size(400, 23);
            this.ReportPathInput.TabIndex = 2;
            this.ReportPathInput.TextChanged += new System.EventHandler(this.ReportPathInput_TextChanged);
            // 
            // ReportKeyLabel
            // 
            this.ReportKeyLabel.AutoSize = true;
            this.ReportKeyLabel.Location = new System.Drawing.Point(497, 12);
            this.ReportKeyLabel.Name = "ReportKeyLabel";
            this.ReportKeyLabel.Size = new System.Drawing.Size(66, 15);
            this.ReportKeyLabel.TabIndex = 3;
            this.ReportKeyLabel.Text = "Report key:";
            // 
            // ReportKeyInput
            // 
            this.ReportKeyInput.Location = new System.Drawing.Point(566, 9);
            this.ReportKeyInput.Name = "ReportKeyInput";
            this.ReportKeyInput.Size = new System.Drawing.Size(128, 23);
            this.ReportKeyInput.TabIndex = 4;
            this.ReportKeyInput.Text = "validatorInfo";
            this.ReportKeyInput.TextChanged += new System.EventHandler(this.ReportKeyInput_TextChanged);
            // 
            // ReportLabelLabel
            // 
            this.ReportLabelLabel.AutoSize = true;
            this.ReportLabelLabel.Location = new System.Drawing.Point(700, 12);
            this.ReportLabelLabel.Name = "ReportLabelLabel";
            this.ReportLabelLabel.Size = new System.Drawing.Size(38, 15);
            this.ReportLabelLabel.TabIndex = 5;
            this.ReportLabelLabel.Text = "Label:";
            // 
            // ReportLabelInput
            // 
            this.ReportLabelInput.Location = new System.Drawing.Point(744, 9);
            this.ReportLabelInput.Name = "ReportLabelInput";
            this.ReportLabelInput.Size = new System.Drawing.Size(129, 23);
            this.ReportLabelInput.TabIndex = 6;
            this.ReportLabelInput.Text = "MyValidator";
            this.ReportLabelInput.TextChanged += new System.EventHandler(this.ReportLabelInput_TextChanged);
            // 
            // ValidatorInfoViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.Add(this.ReportLabelInput);
            this.Controls.Add(this.ReportLabelLabel);
            this.Controls.Add(this.ReportKeyInput);
            this.Controls.Add(this.ReportKeyLabel);
            this.Controls.Add(this.ReportPathInput);
            this.Controls.Add(this.ReportPathLabel);
            this.Controls.Add(this.FlowLayoutContainer);
            this.Name = "ValidatorInfoViewer";
            this.Text = "Validor Infos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FlowLayoutContainer;
        private System.Windows.Forms.Label ReportPathLabel;
        private System.Windows.Forms.TextBox ReportPathInput;
        private System.Windows.Forms.Label ReportKeyLabel;
        private System.Windows.Forms.TextBox ReportKeyInput;
        private System.Windows.Forms.Label ReportLabelLabel;
        private System.Windows.Forms.TextBox ReportLabelInput;
    }
}