using System.Drawing;
using System.Windows.Forms;
using Eth2Overwatch.Models;
using Ethereum.Eth.v1alpha1;

namespace Eth2Overwatch.Views
{
    public partial class ValidatorInfoBox : UserControl
    {
        private ValidatorBo ValidatorInfo;
        public ValidatorInfoBox(ValidatorBo validatorInfo)
        {
            this.ValidatorInfo = validatorInfo;
            InitializeComponent();
            this.PublicKeyValue.Text = validatorInfo.PublicKey;
            this.BalanceValue.Text = Utils.GWeiToEthLabel(validatorInfo.Balance);
            this.CurrentEffectiveBalance.Text = Utils.GWeiToEthLabel(validatorInfo.CurrentEffectiveBalance);
            this.StateValue.Text = validatorInfo.State.ToString();
            Color color = Color.Gray;
            switch(validatorInfo.State)
            {
                case ValidatorStatus.Active:
                    color = Color.Green;
                    break;
                case ValidatorStatus.Exiting:
                    color = Color.RosyBrown;
                    break;
                case ValidatorStatus.Exited:
                    color = Color.Brown;
                    break;
                case ValidatorStatus.Slashing:
                    color = Color.Orange;
                    break;
                case ValidatorStatus.Pending:
                    color = Color.Blue;
                    break;
                case ValidatorStatus.Invalid:
                    color = Color.Red;
                    break;
                case ValidatorStatus.Deposited:
                    color = Color.LightBlue;
                    break;
            }
            this.StateValue.ForeColor = color;
            this.CorrectlyVotedValue.Text = validatorInfo.CorrectlyVoted ? "true" : "false";
            this.CorrectlyVotedValue.ForeColor = validatorInfo.CorrectlyVoted ? Color.Green : Color.Red;

        }
    }
}
