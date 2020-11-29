using Eth2Overwatch.Models;
using LockMyEthTool.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Eth2Overwatch.Views
{
    public partial class ValidatorInfoViewer : Form
    {
        IProcessController Controller;
        public ValidatorInfoViewer(IProcessController controller)
        {
            InitializeComponent();
            this.Controller = controller;
            SetupControlls();
        }

        private void SetupControlls()
        {
            this.ReportKeyInput.Text = this.Controller.ReportKey;
            this.ReportLabelInput.Text = this.Controller.ReportLabel;
            this.ReportPathInput.Text = this.Controller.ReportPath;

            foreach (KeyValuePair<string, ValidatorBo> keyValue in this.Controller.ValidatorsByKey)
            {
                ValidatorInfoBox box = new ValidatorInfoBox(keyValue.Value);
                this.FlowLayoutContainer.Controls.Add(box);
            }
        }

        private void ReportKeyInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.ReportKey != (sender as TextBox).Text)
            {
                this.Controller.ReportKey = (sender as TextBox).Text;
            }
        }

        private void ReportPathInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.ReportPath != (sender as TextBox).Text)
            {
                this.Controller.ReportPath = (sender as TextBox).Text;
            }
        }

        private void ReportLabelInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.ReportLabel != (sender as TextBox).Text)
            {
                this.Controller.ReportLabel = (sender as TextBox).Text;
            }
        }
    }
}
