using Nethereum.Contracts;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace LockMyEthTool.Views
{
    public partial class ControlBox : UserControl
    {
        private readonly IProcessController Controller = null;
        private readonly string ControlName;
        private int retryCount = 0;
        public ControlBox(string Name, IProcessController Controller)
        {
            this.ControlName = Name;
            this.Controller = Controller;
            InitializeComponent();
            SetupControlls();
        }

        private void SetupControlls()
        {
            this.TitleLabel.Text = this.ControlName;
            this.StartButton.Text = "Start " + this.ControlName;
            this.StopButton.Text = "Stop " + this.ControlName;
            this.UpdateControls();
            this.StartTimer(1000);
        }

        private void UpdateControls()
        {
            if (this.InvokeRequired)
            {
                Action act = () =>
                {
                    this.SetInitStates();
                };
                this.Invoke(act);
            }
            else
            {
                this.SetInitStates();
            }
        }

        private void SetInitStates()
        {
            this.AutostartCheck.Checked = this.Controller.Autostart;
            this.HideCommandPromptCheck.Checked = this.Controller.HideCommandPrompt;
            this.DataDirInput.Text = this.Controller.DataDir;
            this.ExecutablePathInput.Text = this.Controller.ExecutablePath;
            this.KeyPathInput.Text = this.Controller.KeyPath;
            this.AdditionalCommandsInput.Text = this.Controller.AdditionalCommands;
            this.WalletDirInput.Text = this.Controller.WalletPath;
            this.KeyPathInput.Visible = this.KeyPathLabel.Visible = this.KeyPathSelectButton.Visible = this.Controller.RequiresPassword();
            this.DataDirInput.Visible = this.DataDirLabel.Visible = this.DataDirSelectButton.Visible = this.Controller.RequiresDataDir();
            this.WalletDirInput.Visible = this.WalletDirLabel.Visible = this.WalletDirSelectButton.Visible = this.Controller.RequiresWalletPath();
        }

        public void ConfigChanged()
        {
            this.Controller.UpdateConfig();
            this.UpdateControls();
            this.StartTimer(1000);
        }


        private Timer stateTimer;

        private void StopTimer()
        {
            if (stateTimer != null)
            {
                stateTimer.Dispose();
                stateTimer = null;
            }
        }

        private void StartTimer(int ms)
        {
            StopTimer();
            var autoEvent = new AutoResetEvent(false);
            stateTimer = new Timer(TaskCheckState, autoEvent, ms, ms);
        }

        private void TaskCheckState(Object stateInfo)
        {
            this.StartTimer(20000); // we alwas want to restart the timer because it got stuck after some time in auto-loop
            Task.Run(() =>
            {
                this.CheckState();
            }).ConfigureAwait(false);
        }


        private void StartButtonClicked(object sender, EventArgs e)
        {
            this.StartProcess();
        }

        private void StopButtonClicked(object sender, EventArgs e)
        {
            this.StopProcess();
        }

        private void AutostartCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.Controller.Autostart = (sender as CheckBox).Checked;
            this.CheckState();
        }

        private void HideCommandPromptCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.Controller.HideCommandPrompt = (sender as CheckBox).Checked;
        }

        private void UpdateText(string text, Color backgroundColor)
        {
            if (this.OutputText.InvokeRequired)
            {
                Action act = () =>
                {
                    this.OutputText.Text = text;
                    this.OutputText.BackColor = backgroundColor;
                };
                this.OutputText.Invoke(act);
            }
            else
            {
                this.OutputText.Text = text;
                this.OutputText.BackColor = backgroundColor;
            }
        }

        public void CheckState()
        {
            this.Controller.CheckState((bool success, string result) =>
            {
                this.UpdateText(result, success ? Color.LightGreen : Color.Red);
                if (!success && this.AutostartCheck.Checked && this.retryCount <= 0)
                {
                    this.StartProcess();
                }
                else if(!success)
                {
                    if(this.retryCount> 0)
                    {
                        this.retryCount--;
                    }
                    this.StartTimer(10000);
                }
                return "We dont want to give something back";
            });
        }


        public void StartProcess()
        {
            this.retryCount = 3;
            this.StartTimer(10000);
            this.UpdateText("Start Process", Color.Beige);
            this.Controller.Start();
        }


        public void StopProcess()
        {
            StopTimer();
            this.UpdateText("Stop Process", Color.Beige);
            this.Controller.Stop();
        }

        private void ValidatorControlBox_Load(object sender, EventArgs e)
        {

        }

        private void KeyPathInput_TextChanged(object sender, EventArgs e)
        {
            if(this.Controller.KeyPath != (sender as TextBox).Text)
            {
                this.Controller.KeyPath = (sender as TextBox).Text;
                this.CheckState();
            }
        }

        private void KeyPathSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.Controller.KeyPath = this.KeyPathInput.Text = fbd.SelectedPath;
                this.CheckState();
            }
        }

        private void DataDirInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.DataDir != (sender as TextBox).Text)
            {
                this.Controller.DataDir = (sender as TextBox).Text;
                this.CheckState();
            }
        }

        private void WalletDirInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.WalletPath != (sender as TextBox).Text)
            {
                this.Controller.WalletPath = (sender as TextBox).Text;
                this.CheckState();
            }
        }

        private void DataDirSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.Controller.DataDir = this.DataDirInput.Text = fbd.SelectedPath;
                this.CheckState();
            }

        }

        private void WalletDirSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.Controller.WalletPath = this.WalletDirInput.Text = fbd.SelectedPath;
                this.CheckState();
            }

        }

        private void ExecutablePathInput_TextChanged(object sender, EventArgs e)
        {
            if(this.Controller.ExecutablePath != (sender as TextBox).Text)
            {
                this.Controller.ExecutablePath = (sender as TextBox).Text;
                this.CheckState();
            }
        }

        private void ExecutablePathSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.Controller.ExecutablePath = this.ExecutablePathInput.Text = fbd.SelectedPath;
                this.CheckState();
            }
        }

        private void OutputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdditionalCommandsInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.AdditionalCommands != (sender as TextBox).Text)
            {
                this.Controller.AdditionalCommands = (sender as TextBox).Text;
                this.CheckState();
            }
        }
    }
}
