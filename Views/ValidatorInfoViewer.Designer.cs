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
            this.SuspendLayout();
            // 
            // FlowLayoutContainer
            // 
            this.FlowLayoutContainer.AutoScroll = true;
            this.FlowLayoutContainer.Location = new System.Drawing.Point(12, 12);
            this.FlowLayoutContainer.Name = "FlowLayoutContainer";
            this.FlowLayoutContainer.Size = new System.Drawing.Size(876, 426);
            this.FlowLayoutContainer.TabIndex = 0;
            // 
            // ValidatorInfoViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.Add(this.FlowLayoutContainer);
            this.Name = "ValidatorInfoViewer";
            this.Text = "Validor Infos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FlowLayoutContainer;
    }
}