using LockMyEthTool.Controllers;
using LockMyEthTool.Views;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Eth2Overwatch.Views
{
    public partial class InitialEth2SetupForm : Form
    {
        private readonly IProcessController BeaconController = new ProcessController(PROCESS_TYPES.BEACON_CHAIN);
        private readonly IProcessController ValidatorController = new ProcessController(PROCESS_TYPES.VALIDATOR);
        private bool prysmDownloaded = false;
        private bool generateKeysActive = false;

        public InitialEth2SetupForm()
        {
            InitializeComponent();
            if(!String.IsNullOrWhiteSpace(BeaconController.ExecutablePath) && BeaconController.ExecutablePath.IndexOf(@"\prysm") > 0)
            {
                this.PickPrysmFolderInput.Text = BeaconController.ExecutablePath.Substring(0, BeaconController.ExecutablePath.IndexOf(@"\prysm"));
                if(this.CheckPrysmBat())
                {
                    this.UpdateText("prysm.bat is already downloaded", Color.Green);
                }
            }

            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip
            {
                // Set up the delays for the ToolTip.
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 500,
                // Force the ToolTip text to be displayed whether or not the form is active.
                ShowAlways = true
            };

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.DeleteExistingFilesCheck, "If checked the prym folder and all its files will be delete before downloading the file");
        }

        private void PickPrysmFolderButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.PickPrysmFolderInput.Text = fbd.SelectedPath;
            }
        }

        private bool PrysmDownloaded
        {
            get
            {
                return this.prysmDownloaded;
            }
            set
            {
                if(this.prysmDownloaded != value)
                {
                    this.prysmDownloaded = value;
                    if (this.InvokeRequired)
                    {
                        Action act = () =>
                        {
                            this.BeaconChainIsReadyLabel.Visible = this.prysmDownloaded;
                            this.ShowInExplorerButton.Enabled = this.prysmDownloaded;
                            this.ValidatorGroup.Enabled = this.prysmDownloaded;
                        };
                        this.Invoke(act);
                    }
                    else
                    {
                        this.BeaconChainIsReadyLabel.Visible = this.prysmDownloaded;
                        this.ShowInExplorerButton.Enabled = this.prysmDownloaded;
                        this.ValidatorGroup.Enabled = this.prysmDownloaded;
                    }
                }
            }
        }

        private bool CheckPrysmBat()
        {
            if (File.Exists(this.PickPrysmFolderInput.Text + @"\prysm\prysm.bat"))
            {
                BeaconController.ExecutablePath = this.PickPrysmFolderInput.Text + @"\prysm";
                ValidatorController.ExecutablePath = BeaconController.ExecutablePath;
                this.PrysmDownloaded = true;
                return true;
            }
            return false;
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
            stateTimer = new Timer(this.CheckState, autoEvent, ms, ms);
        }

        public void CheckState(Object stateInfo = null)
        {
            if(this.generateKeysActive)
            {

            }
            else if (this.CheckPrysmBat())
            {
                this.UpdateText("prysm.bat sucessfully downloaded", Color.Green);
                Thread.Sleep(2000);
                BeaconController.Stop();
                return;
            }
            this.StartTimer(1000);
            Task.Run(() =>
            {
                if (this.generateKeysActive)
                {
                    this.UpdateText(this.ValidatorController.GetLogText(), Color.Beige);
                }
                else
                {
                    this.UpdateText(this.BeaconController.GetLogText(), Color.Beige);
                }
            }).ConfigureAwait(false);
            
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

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.PickPrysmFolderInput.Text))
            {
                this.UpdateText("Invalid folder path", Color.Red);
                return;
            }
            this.BeaconController.DownloadExecutable(this.PickPrysmFolderInput.Text, this.DeleteExistingFilesCheck.Checked);
            this.StartTimer(1000);
        }

        private void ShowInExplorerButton_Click(object sender, EventArgs e)
        {
            this.StopTimer();
            string folderPath = this.PickPrysmFolderInput.Text + @"\prysm";
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                this.UpdateText("prysm folder could not be created", Color.Red);
            }
            
        }

        private void PickPrysmFolderInput_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckPrysmBat())
            {
                this.UpdateText("prysm.bat is already downloaded", Color.Green);
            }
            else if(String.IsNullOrWhiteSpace(this.PickPrysmFolderInput.Text))
            {
                this.UpdateText("Please enter a folder", Color.Red);
            }
            else if (!Directory.Exists(this.PickPrysmFolderInput.Text))
            {
                this.UpdateText("Please enter a valid folder", Color.Red);
            }
            else
            {
                this.UpdateText("You can download the Executables now", Color.Beige);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasswordFilePathInput_TextChanged(object sender, EventArgs e)
        {
            if(!this.PrysmDownloaded)
            {
                return;
            }
            CheckPasswordFileFolder();
        }

        private bool CheckPasswordFileFolder()
        {            
            if (String.IsNullOrWhiteSpace(this.KeyFileFolderInput.Text))
            {
                this.UpdateText("Please pick the folder containing the medalla key files", Color.Red);
            }
            else if (!Directory.Exists(this.KeyFileFolderInput.Text))
            {
                this.UpdateText("Please pick a valid folder containing the medalla key files", Color.Red);
            }
            else if (String.IsNullOrWhiteSpace(this.PickWalletFolderInput.Text))
            {
                this.UpdateText("Please pick a folder where your wallet should be stored", Color.Red);
            }
            else if (!Directory.Exists(this.PickWalletFolderInput.Text))
            {
                this.UpdateText("Please pick a valid folder where your wallet should be stored", Color.Red);
            }
            else
            {
                this.UpdateText("Ready to import", Color.Beige);
                this.ValidatorController.WalletPath = this.PickWalletFolderInput.Text;
                return true;
            }
            return false;
        }

        private void CreatePasswordFilesButton_Click(object sender, EventArgs e)
        {
            if(CheckPasswordFileFolder())
            {
                this.ValidatorController.ImportKeys(this.KeyFileFolderInput.Text);
                this.generateKeysActive = true;

                this.StartTimer(1000);
            }
        }

        private void GotoLaunchpadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://medalla.launchpad.ethereum.org/overview";
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private void KeyFileSelect_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.KeyFileFolderInput.Text = fbd.SelectedPath;
            }
        }

        private void PickWalletFolderButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                this.PickWalletFolderInput.Text = fbd.SelectedPath;
            }
        }
    }
}
