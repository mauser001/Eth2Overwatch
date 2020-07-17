using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LockMyEthTool.Controllers;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Timer = System.Threading.Timer;

namespace LockMyEthTool.Views
{
    public partial class ControlBox : UserControl
    {
        private readonly IProcessController Controller = null;
        private readonly string ControlName;
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
            this.PasswordInput.Visible = this.PasswordLabel.Visible = this.KeyPathInput.Visible = this.KeyPathILabel.Visible = this.Controller.RequiresPassword();
            if(this.Controller.RequiresPassword())
            {
                if(this.Controller.CheckPassword())
                {
                    this.PasswordInput.Text = "***";
                }
            }
            this.AutostartCheck.Checked = this.Controller.Autostart;
            this.HideCommandPromptCheck.Checked = this.Controller.HideCommandPrompt;
            this.DataDirInput.Text = this.Controller.DataDir;
            this.ExecutablePathInput.Text = this.Controller.ExecutablePath;
            this.KeyPathInput.Text = this.Controller.KeyPath;
            this.AdditionalCommandsInput.Text = this.Controller.AdditionalCommands;

            this.StartTimer(1000);
        }

        public void ConfigChanged()
        {
            this.Controller.UpdateConfig();
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
            stateTimer = new Timer(CheckState, autoEvent, ms, ms);
        }

        private void CheckState(Object stateInfo)
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
                if (!success && this.AutostartCheck.Checked)
                {
                    this.StartProcess();
                }
                else if(!success)
                {
                    this.StartTimer(10000);
                }
                return "We dont want to give something back";
            });
        }


        public void StartProcess()
        {
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

        private void PasswordInput_TextChanged(object sender, EventArgs e)
        {
            if((sender as TextBox).Text != "***")
            {
                this.Controller.SetPassword((sender as TextBox).Text);
                this.CheckState();
            }
        }

        private void KeyPathInput_TextChanged(object sender, EventArgs e)
        {
            if(this.Controller.KeyPath != (sender as TextBox).Text)
            {
                this.Controller.KeyPath = (sender as TextBox).Text;
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

        private void ExecutablePathInput_TextChanged(object sender, EventArgs e)
        {
            if(this.Controller.ExecutablePath != (sender as TextBox).Text)
            {
                this.Controller.ExecutablePath = (sender as TextBox).Text;
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
