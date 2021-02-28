using Eth2Overwatch;
using Eth2Overwatch.Views;
using LockMyEthTool.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace LockMyEthTool
{
    public partial class MainForm : Form
    {
        private readonly List<ControlBox> Boxes = new List<ControlBox>();
        public string AppName = "Eth2Overwatch";
        public MainForm()
        {
            Trace.AutoFlush = true;

            // Copy user settings from previous application version
            if (Eth2OverwatchSettings.Default.UpdateSettings)
            {
                Eth2OverwatchSettings.Default.Upgrade();
                Eth2OverwatchSettings.Default.UpdateSettings = false;
                Eth2OverwatchSettings.Default.Save();
            }

            InitializeCustomComponents();
            InitializeComponent();
            SetSavedValues();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetSavedValues()
        {
            this.Eth2TestNet.Text = Eth2OverwatchSettings.Default.Eth2_TestNet;
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            this.StartOnStartupCheck.Checked = rk.GetValue(this.AppName) != null;
            if(this.StartOnStartupCheck.Checked)
            {
                // If the .exe is moved we store a the new path in the autostart regex
                if(this.AppName != Process.GetCurrentProcess().MainModule.FileName)
                {
                    this.SetAutoStart(true);
                }
            }
            this.UseLocalEth1NodeCheck.Checked = Eth2OverwatchSettings.Default.UseLocalEth1Node;

        }

        private void UpdateBoxConfigs()
        {
            this.Boxes.ForEach((box) =>
            {
                box.ConfigChanged();
            });
        }

        private void UseLocalEth1NodeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(Eth2OverwatchSettings.Default.UseLocalEth1Node != (sender as CheckBox).Checked)
            {
                Eth2OverwatchSettings.Default.UseLocalEth1Node = (sender as CheckBox).Checked;
                Eth2OverwatchSettings.Default.Save();
                this.UpdateBoxConfigs();
            }
        }

        private void StartOnStartupCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.SetAutoStart((sender as CheckBox).Checked);
        }

        private void SetAutoStart(bool check)
        {
            if (!Debugger.IsAttached)
            {
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (check)
                    rk.SetValue(AppName, Process.GetCurrentProcess().MainModule.FileName);
                else
                    rk.DeleteValue(AppName, false);

            }
        }

        private void InitialEth2SetupButton_Click(object sender, EventArgs e)
        {
            using InitialEth2SetupForm frm = new InitialEth2SetupForm();
            DialogResult res = frm.ShowDialog(this);
            this.UpdateBoxConfigs();
        }

        private void Eth2Testnet_TextChanged(object sender, EventArgs e)
        {
            Eth2OverwatchSettings.Default.Eth2_TestNet = (sender as TextBox).Text;
            Eth2OverwatchSettings.Default.Save();
            this.UpdateBoxConfigs();
        }
    }
}
