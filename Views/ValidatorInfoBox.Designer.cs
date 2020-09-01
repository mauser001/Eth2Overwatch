namespace Eth2Overwatch.Views
{
    partial class ValidatorInfoBox
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.PublicKeyLabel = new System.Windows.Forms.Label();
            this.StateLabel = new System.Windows.Forms.Label();
            this.BalanceLabel = new System.Windows.Forms.Label();
            this.PublicKeyValue = new System.Windows.Forms.Label();
            this.StateValue = new System.Windows.Forms.Label();
            this.BalanceValue = new System.Windows.Forms.Label();
            this.CurrentEffectiveBalanceLabel = new System.Windows.Forms.Label();
            this.CurrentEffectiveBalance = new System.Windows.Forms.Label();
            this.CorrectlyVotedLabel = new System.Windows.Forms.Label();
            this.CorrectlyVotedValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PublicKeyLabel
            // 
            this.PublicKeyLabel.AutoSize = true;
            this.PublicKeyLabel.Location = new System.Drawing.Point(21, 27);
            this.PublicKeyLabel.Name = "PublicKeyLabel";
            this.PublicKeyLabel.Size = new System.Drawing.Size(67, 15);
            this.PublicKeyLabel.TabIndex = 0;
            this.PublicKeyLabel.Text = "Public key: ";
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Location = new System.Drawing.Point(21, 54);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(39, 15);
            this.StateLabel.TabIndex = 1;
            this.StateLabel.Text = "State: ";
            // 
            // BalanceLabel
            // 
            this.BalanceLabel.AutoSize = true;
            this.BalanceLabel.Location = new System.Drawing.Point(21, 83);
            this.BalanceLabel.Name = "BalanceLabel";
            this.BalanceLabel.Size = new System.Drawing.Size(54, 15);
            this.BalanceLabel.TabIndex = 2;
            this.BalanceLabel.Text = "Balance: ";
            // 
            // PublicKeyValue
            // 
            this.PublicKeyValue.AutoSize = true;
            this.PublicKeyValue.Location = new System.Drawing.Point(186, 27);
            this.PublicKeyValue.Name = "PublicKeyValue";
            this.PublicKeyValue.Size = new System.Drawing.Size(19, 15);
            this.PublicKeyValue.TabIndex = 3;
            this.PublicKeyValue.Text = "0x";
            // 
            // StateValue
            // 
            this.StateValue.AutoSize = true;
            this.StateValue.Location = new System.Drawing.Point(186, 54);
            this.StateValue.Name = "StateValue";
            this.StateValue.Size = new System.Drawing.Size(12, 15);
            this.StateValue.TabIndex = 4;
            this.StateValue.Text = "-";
            // 
            // BalanceValue
            // 
            this.BalanceValue.AutoSize = true;
            this.BalanceValue.Location = new System.Drawing.Point(186, 83);
            this.BalanceValue.Name = "BalanceValue";
            this.BalanceValue.Size = new System.Drawing.Size(13, 15);
            this.BalanceValue.TabIndex = 5;
            this.BalanceValue.Text = "0";
            // 
            // CurrentEffectiveBalanceLabel
            // 
            this.CurrentEffectiveBalanceLabel.AutoSize = true;
            this.CurrentEffectiveBalanceLabel.Location = new System.Drawing.Point(21, 113);
            this.CurrentEffectiveBalanceLabel.Name = "CurrentEffectiveBalanceLabel";
            this.CurrentEffectiveBalanceLabel.Size = new System.Drawing.Size(139, 15);
            this.CurrentEffectiveBalanceLabel.TabIndex = 6;
            this.CurrentEffectiveBalanceLabel.Text = "Current effective balance";
            // 
            // CurrentEffectiveBalance
            // 
            this.CurrentEffectiveBalance.AutoSize = true;
            this.CurrentEffectiveBalance.Location = new System.Drawing.Point(186, 113);
            this.CurrentEffectiveBalance.Name = "CurrentEffectiveBalance";
            this.CurrentEffectiveBalance.Size = new System.Drawing.Size(13, 15);
            this.CurrentEffectiveBalance.TabIndex = 7;
            this.CurrentEffectiveBalance.Text = "0";
            // 
            // CorrectlyVotedLabel
            // 
            this.CorrectlyVotedLabel.AutoSize = true;
            this.CorrectlyVotedLabel.Location = new System.Drawing.Point(21, 141);
            this.CorrectlyVotedLabel.Name = "CorrectlyVotedLabel";
            this.CorrectlyVotedLabel.Size = new System.Drawing.Size(137, 15);
            this.CorrectlyVotedLabel.TabIndex = 8;
            this.CorrectlyVotedLabel.Text = "Last vote was successfull";
            // 
            // CorrectlyVotedValue
            // 
            this.CorrectlyVotedValue.AutoSize = true;
            this.CorrectlyVotedValue.Location = new System.Drawing.Point(186, 141);
            this.CorrectlyVotedValue.Name = "CorrectlyVotedValue";
            this.CorrectlyVotedValue.Size = new System.Drawing.Size(12, 15);
            this.CorrectlyVotedValue.TabIndex = 9;
            this.CorrectlyVotedValue.Text = "-";
            // 
            // ValidatorInfoBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CorrectlyVotedValue);
            this.Controls.Add(this.CorrectlyVotedLabel);
            this.Controls.Add(this.CurrentEffectiveBalance);
            this.Controls.Add(this.CurrentEffectiveBalanceLabel);
            this.Controls.Add(this.BalanceValue);
            this.Controls.Add(this.StateValue);
            this.Controls.Add(this.PublicKeyValue);
            this.Controls.Add(this.BalanceLabel);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.PublicKeyLabel);
            this.Name = "ValidatorInfoBox";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 20, 20);
            this.Size = new System.Drawing.Size(228, 176);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PublicKeyLabel;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.Label BalanceLabel;
        private System.Windows.Forms.Label PublicKeyValue;
        private System.Windows.Forms.Label StateValue;
        private System.Windows.Forms.Label BalanceValue;
        private System.Windows.Forms.Label CurrentEffectiveBalanceLabel;
        private System.Windows.Forms.Label CurrentEffectiveBalance;
        private System.Windows.Forms.Label CorrectlyVotedLabel;
        private System.Windows.Forms.Label CorrectlyVotedValue;
    }
}
