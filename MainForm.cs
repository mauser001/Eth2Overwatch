using Eth2Overwatch;
using Eth2Overwatch.Views;
using LockMyEthTool.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace LockMyEthTool
{
    public partial class MainForm : Form
    {
        private List<ControlBox> Boxes = new List<ControlBox>();
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
            this.ConnectWithEth1Check.Checked = Eth2OverwatchSettings.Default.UseLocalEth1Node;
            this.UseGoerliCheck.Checked = Eth2OverwatchSettings.Default.UseGoerliTestnet;
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            this.StartOnStartupCheck.Checked = rk.GetValue(this.AppName) != null;
        }

        private void ConnectWithEth1Check_CheckedChanged(object sender, EventArgs e)
        {
            Eth2OverwatchSettings.Default.UseLocalEth1Node = (sender as CheckBox).Checked;
            Eth2OverwatchSettings.Default.Save();
            UpdateBoxConfigs();
        }

        private void UpdateBoxConfigs()
        {
            this.Boxes.ForEach((box) =>
            {
                box.ConfigChanged();
            });
        }

        private void UseGoerliCheck_CheckedChanged(object sender, EventArgs e)
        {
            Eth2OverwatchSettings.Default.UseGoerliTestnet = (sender as CheckBox).Checked;
            Eth2OverwatchSettings.Default.Save();
        }

        private void StartOnStartupCheck_CheckedChanged(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
           ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if ((sender as CheckBox).Checked)
                rk.SetValue(AppName, Application.ExecutablePath);
            else
                rk.DeleteValue(AppName, false);
        }

        private void InitialEth2SetupButton_Click(object sender, EventArgs e)
        {
            using (InitialEth2SetupForm frm = new InitialEth2SetupForm())
            {
                DialogResult res = frm.ShowDialog(this);
                this.UpdateBoxConfigs();
            }
        }
    }
}
