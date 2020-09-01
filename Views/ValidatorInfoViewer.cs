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
        public ValidatorInfoViewer(IProcessController controller)
        {
            InitializeComponent();
            foreach(KeyValuePair<string, ValidatorBo> keyValue in controller.ValidatorsByKey)
            {
                ValidatorInfoBox box = new ValidatorInfoBox(keyValue.Value);
                this.FlowLayoutContainer.Controls.Add(box);
            }
        }
    }
}
