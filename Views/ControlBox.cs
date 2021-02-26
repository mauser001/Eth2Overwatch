using Eth2Overwatch.Views;
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
        private int successCounter = 0;
        public ControlBox(string Name, IProcessController Controller)
        {
            this.ControlName = Name;
            this.Controller = Controller;
            this.retryCount = this.Controller.GetInitialDelay();
            InitializeComponent();
            SetupControlls();
        }

        private void SetupControlls()
        {
            this.TitleLabel.Text = this.ControlName;
            this.StartButton.Text = "Start " + this.ControlName;
            this.StopButton.Text = "Stop " + this.ControlName;
            this.UpdateControls();
            this.OutputText.DataBindings.Add("Visible", this.HideCommandPromptCheck, "Checked");
            this.ShowErrorButton.DataBindings.Add("Visible", this.HideCommandPromptCheck, "Checked");
            this.ShowWarningButton.DataBindings.Add("Visible", this.HideCommandPromptCheck, "Checked");
            this.ShowInfoButton.DataBindings.Add("Visible", this.HideCommandPromptCheck, "Checked");
            this.StartTimer(1000 * this.Controller.GetInitialDelay());
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
            this.StateOutput.Height = !this.Controller.HideCommandPrompt ? 170 : 56;
            this.VersionLabel.Visible = this.LatestVersionCheckbox.Visible = this.Controller.SupportsVersion();
            this.CurrentVersionInput.Visible = this.Controller.SupportsVersion() && !this.Controller.UseLatestVersion;
            if(this.Controller.SupportsVersion())
            {
                this.LatestVersionCheckbox.Checked = this.Controller.UseLatestVersion;
                this.CurrentVersionInput.Text = this.Controller.CurrentVersion;
            }

            this.UpdateValidatorDetailsButton();
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
            this.StartTimer(0);
        }

        private void HideCommandPromptCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.Controller.HideCommandPrompt = (sender as CheckBox).Checked;

            if (this.InvokeRequired)
            {
                Action act = () =>
                {
                    this.StateOutput.Height = !this.Controller.HideCommandPrompt ? 170 : 56;

                };
                this.Invoke(act);
            }
            else
            {
                this.StateOutput.Height = !this.Controller.HideCommandPrompt ? 170 : 56;
            }
        }

        private void UpdateText(string text, Color backgroundColor)
        {
            string title = this.ControlName;
            if(this.Controller.CurrentVersion.Length > 0)
            {
                title += " (" + this.Controller.CurrentVersion;
                if(this.Controller.CurrentVersion != this.Controller.GetLastVersion())
                {
                    title += ", Latest: " + this.Controller.GetLastVersion();
                }
                title += ")";
            }

            if (this.InvokeRequired)
            {
                Action act = () =>
                {
                    this.StateOutput.Text = text;
                    this.StateOutput.BackColor = backgroundColor;
                    this.OutputText.Text = this.Controller.GetLogText(); 


                    this.TitleLabel.Text = title;
                };
                this.Invoke(act);
            }
            else
            {
                this.StateOutput.Text = text;
                this.StateOutput.BackColor = backgroundColor;
                this.OutputText.Text = this.Controller.GetLogText(); 
                this.TitleLabel.Text = title;
            }
        }

        private void UpdateValidatorDetailsButton()
        {
            try
            {
                Boolean vis = this.Controller.ValidatorsByKey.Count > 0;
                if (this.ValidatorDetailsButton.InvokeRequired)
                {
                    Action act = () =>
                    {
                        this.ValidatorDetailsButton.Visible = vis;

                    };
                    this.ValidatorDetailsButton.Invoke(act);
                }
                else
                {
                    this.ValidatorDetailsButton.Visible = vis;
                }
            }
            catch
            {
            }
        }

        private void UpdateOutputText()
        {
            if (this.InvokeRequired)
            {
                Action act = () =>
                {
                    this.OutputText.Text = this.Controller.GetLogText();

                };
                this.Invoke(act);
            }
            else
            {
                this.OutputText.Text = this.Controller.GetLogText();
            }
        }

        public void CheckState()
        {
            this.Controller.CheckState((bool success, string result) =>
            {
                this.UpdateText(result, success ? Color.LightGreen : Color.Red);
                this.UpdateValidatorDetailsButton();
                if (this.Controller.NewVersionAvailable)
                {
                    this.Controller.UpdateConfig();
                    this.StartProcess();
                    this.Controller.NewVersionAvailable = false;
                }
                else if(this.Controller.DownloadingExecutables)
                {
                    //Nothing to do here, we just wait until the download is complete
                }
                else if (this.Controller.SupportsVersion() && this.Controller.CheckExecutablePath() && !this.Controller.CheckExecutable())
                {
                    this.Controller.Stop();
                    this.Controller.DownloadExecutable();
                    this.StartTimer(10000);
                }
                else if (!success && this.AutostartCheck.Checked && this.retryCount <= 0)
                {
                    this.retryCount = 12;
                    this.StartProcess();
                }
                else if (!success)
                {
                    if (this.retryCount > 0)
                    {
                        this.retryCount--;
                    }

                    string prysmVersion = this.Controller.GetPrysmVersion();
                    if (this.Controller.GetLastVersion() != prysmVersion && !this.Controller.CheckExecutable(prysmVersion))
                    {
                        if(this.Controller.UseLatestVersion)
                        {
                            this.Controller.Stop();
                        }
                        this.Controller.DownloadExecutable(prysmVersion);
                    }
                    
                    this.StartTimer(10000);
                }
                else if (success && this.successCounter > 60)
                {
                    this.retryCount = 12;
                    this.successCounter = 0;
                    string prysmVersion = this.Controller.GetPrysmVersion();
                    if (this.Controller.GetLastVersion() != prysmVersion && !this.Controller.CheckExecutable(prysmVersion))
                    {
                        if (this.Controller.UseLatestVersion)
                        {
                            this.Controller.Stop();
                        }
                        this.Controller.DownloadExecutable(prysmVersion);
                    }
                }
                else if (success)
                {
                    this.successCounter++;
                }
                return "";
            });
        }


        public void StartProcess()
        {
            this.retryCount = 20;
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
                this.StartTimer(0);
            }
        }

        private void KeyPathSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new OpenFileDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
            {
                this.Controller.KeyPath = this.KeyPathInput.Text = fbd.FileName;
                this.StartTimer(0);
            }
        }

        private void DataDirInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.DataDir != (sender as TextBox).Text)
            {
                this.Controller.DataDir = (sender as TextBox).Text;
                this.StartTimer(0);
            }
        }

        private void WalletDirInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.WalletPath != (sender as TextBox).Text)
            {
                this.Controller.WalletPath = (sender as TextBox).Text;
                this.StartTimer(0);
            }
        }

        private void DataDirSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.Controller.DataDir = this.DataDirInput.Text = fbd.SelectedPath;
                this.StartTimer(0);
            }

        }

        private void WalletDirSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.Controller.WalletPath = this.WalletDirInput.Text = fbd.SelectedPath;
                this.StartTimer(0);
            }

        }

        private void ExecutablePathInput_TextChanged(object sender, EventArgs e)
        {
            if(this.Controller.ExecutablePath != (sender as TextBox).Text)
            {
                this.Controller.ExecutablePath = (sender as TextBox).Text;
                this.StartTimer(0);
            }
        }

        private void ExecutablePathSelectButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.Controller.ExecutablePath = this.ExecutablePathInput.Text = fbd.SelectedPath;
                this.StartTimer(0);
            }
        }

        private void AdditionalCommandsInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.AdditionalCommands != (sender as TextBox).Text)
            {
                this.Controller.AdditionalCommands = (sender as TextBox).Text;
                this.StartTimer(0);
            }
        }

        private void ShowErrorButton_Click(object sender, EventArgs e)
        {
            this.Controller.ShowError = !this.Controller.ShowError;
            this.SetBackgroundColorForToggleButton(this.ShowErrorButton, this.Controller.ShowError);
            this.UpdateOutputText();
        }

        private void ShowWarningButton_Click(object sender, EventArgs e)
        {
            this.Controller.ShowWarning = !this.Controller.ShowWarning;
            this.SetBackgroundColorForToggleButton(this.ShowWarningButton, this.Controller.ShowWarning);
            this.UpdateOutputText();
        }

        private void ShowInfoButton_Click(object sender, EventArgs e)
        {
            this.Controller.ShowInfo = !this.Controller.ShowInfo;
            this.SetBackgroundColorForToggleButton(this.ShowInfoButton, this.Controller.ShowInfo);
            this.UpdateOutputText();
        }
        private void SetBackgroundColorForToggleButton(Button toggleButton, bool show)
        {
            Color backClor = show ? Color.RoyalBlue : Color.Transparent;
            Color foreColor = show ? Color.GhostWhite : Color.Black;

            if (toggleButton.InvokeRequired)
            {
                Action act = () =>
                {
                    toggleButton.BackColor = backClor;
                    toggleButton.ForeColor = foreColor;
                };
                toggleButton.Invoke(act);
            }
            else
            {
                toggleButton.BackColor = backClor;
                toggleButton.ForeColor = foreColor;
            }
        }

        private void ValidatorDetailsButton_Click(object sender, EventArgs e)
        {
            using (ValidatorInfoViewer frm = new ValidatorInfoViewer(this.Controller))
            {
                frm.ShowDialog(this);
            }
        }

        private void LatestVersionCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(this.Controller.UseLatestVersion != (sender as CheckBox).Checked)
            {
                bool updateNeeded = this.Controller.GetLastVersion() != this.Controller.CurrentVersion;
                this.Controller.UseLatestVersion = (sender as CheckBox).Checked;

                if (this.Controller.UseLatestVersion)
                {
                    this.Controller.GetPrysmVersion();
                    this.UpdateControls();
                    if(updateNeeded && this.Controller.CheckExecutablePath() && this.Controller.CheckExecutable())
                    {
                        this.StartProcess();
                    }
                    else
                    {
                        this.StartTimer(0);
                    }
                }
                else
                {
                    this.UpdateControls();
                }
            }
        }

        private void CurrentVersionInput_TextChanged(object sender, EventArgs e)
        {
            if (this.Controller.CurrentVersion != (sender as TextBox).Text)
            {
                this.Controller.CurrentVersion = (sender as TextBox).Text;
                if (this.Controller.CheckExecutablePath() && this.Controller.CheckExecutable())
                {
                    this.StartProcess();
                }
                else
                {
                    this.StartTimer(0);
                }
            }
        }
    }
}
