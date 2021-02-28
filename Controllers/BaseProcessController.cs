using Eth2Overwatch;
using Eth2Overwatch.Models;
using LockMyEthTool.Models;
using LockMyEthTool.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LockMyEthTool.Controllers
{
    abstract class BaseProcessController : IProcessController
    {
        protected Process process = null;
        protected readonly bool dryMode = false; // Used for development
        protected string fileName;
        protected string directory = null;
        protected string[] commands;
        protected string arguments = null;
        protected bool autoStart = false;
        protected string dataDir = "";
        protected string executablePath = "";
        protected string walletPath = "";
        protected string keyPath = "";
        protected bool hideCommandPrompt = false;
        protected bool useLocalEth1Node = true;
        protected string eth2TestNet = "";
        protected string additionalCommands = "";
        protected bool logOutput = true;
        protected string currentVersion = "";
        protected bool useLatestVersion = true;
        protected bool downloadingExecutables = false;
        protected bool showError = true;
        protected bool showWarning = true;
        protected bool newVersionAvailable = false;
        protected bool showInfo = true;
        protected string reportPath = "";
        protected string reportKey = "";
        protected string reportLabel = "";
        protected Dictionary<string, ValidatorBo> validatorsByKey = new Dictionary<string, ValidatorBo>();

        public BaseProcessController()
        {
            this.Init();
        }

        public abstract PROCESS_TYPES ProcessType
        {
            get;
        }

        protected virtual string ProcessIdentifier
        {
            get 
            {
                return "";
            }
        }

        private void Init()
        {
            InitConfig();

            if (!AllConfigsSet())
            {
                return;
            }

            SetupProcessConfig();
        }

        public abstract string GetLastVersion();

        public abstract string GetPrysmVersion();

        public abstract void DownloadExecutable(string path = null, string version = null);

        public abstract int GetInitialDelay();

        public virtual bool CheckWalletPath()
        {
            return true;
        }

        protected bool CheckKeyPath()
        {
            return true;
        }

        protected abstract void SetupProcessConfig();

        protected abstract void InitConfig();

        protected abstract void SaveConfig();

        protected bool CheckDataDir()
        {
            return !this.RequiresDataDir() || !String.IsNullOrWhiteSpace(this.dataDir) && Directory.Exists(this.dataDir);
        }

        public bool CheckExecutablePath(string path = null)
        {
            if (path == null)
            {
                path = this.executablePath;
            }
            return !String.IsNullOrWhiteSpace(path) && Directory.Exists(path);
        }

        protected virtual string GetExecutableFileName(string version = null)
        {
            return "geth.exe";
        }

        public bool CheckExecutable(string path = null)
        {
            if (this.downloadingExecutables)
            {
                return false;
            }
            if (path == null)
            {
                path = this.executablePath;
            }
            return this.CheckExecutablePath(path) && File.Exists(path + @"\" + this.GetExecutableFileName()) && new FileInfo(path + @"\" + this.GetExecutableFileName()).Length > 0;
        }

        public bool AllConfigsSet()
        {
            return this.CheckDataDir() && this.CheckExecutable() && this.CheckKeyPath() && this.CheckWalletPath();
        }

        public bool Autostart
        {
            get
            {
                return this.autoStart;
            }
            set
            {

                this.autoStart = value;
                SaveConfig();
            }
        }
        public bool HideCommandPrompt
        {
            get
            {
                return this.hideCommandPrompt;
            }
            set
            {

                this.hideCommandPrompt = value;
                SaveConfig();
            }
        }

        public string DataDir
        {
            get
            {
                return this.dataDir;
            }
            set
            {

                this.dataDir = value;
                SaveConfig();
            }
        }

        public string AdditionalCommands
        {
            get
            {
                return this.additionalCommands;
            }
            set
            {

                this.additionalCommands = value;
                SaveConfig();
            }
        }

        public string ExecutablePath
        {
            get
            {
                return this.executablePath;
            }
            set
            {

                this.executablePath = value;
                SaveConfig();
            }
        }

        public string KeyPath
        {
            get
            {
                return this.keyPath;
            }
            set
            {

                this.keyPath = value;
                SaveConfig();
            }

        }

        public string WalletPath
        {
            get
            {
                return this.walletPath;
            }
            set
            {

                this.walletPath = value;
                SaveConfig();
            }
        }

        public string CurrentVersion
        {
            get
            {
                return this.currentVersion;
            }
            set
            {

                this.currentVersion = value;
                SaveConfig();
            }
        }

        public bool UseLatestVersion
        {
            get
            {
                return this.useLatestVersion;
            }
            set
            {

                this.useLatestVersion = value;
                SaveConfig();
            }
        }
        public string ReportPath
        {
            get
            {
                return this.reportPath;
            }
            set
            {

                this.reportPath = value;
                SaveConfig();
            }
        }
        public string ReportKey
        {
            get
            {
                return this.reportKey;
            }
            set
            {

                this.reportKey = value;
                SaveConfig();
            }
        }
        public string ReportLabel
        {
            get
            {
                return this.reportLabel;
            }
            set
            {

                this.reportLabel = value;
                SaveConfig();
            }
        }

        public void UpdateConfig()
        {
            this.Init();
        }

        protected List<string> Logs = new List<string>();

        public void Start(bool skipCheck = false, bool showCommandPrompt = false, bool dontStop = false)
        {
            if (skipCheck != true && !this.AllConfigsSet())
            {
                return;
            }
            if (!dontStop)
            {
                this.Stop();
            }

            if (this.dryMode)
            {
                return;
            }
            this.process = new Process(); // Declare New Process
            this.process.StartInfo.FileName = this.fileName;
            if (this.arguments != null)
            {
                this.process.StartInfo.Arguments = this.arguments;
            }
            if (this.directory != null)
            {
                this.process.StartInfo.WorkingDirectory = this.directory;
            }

            this.process.StartInfo.UseShellExecute = false;
            this.process.StartInfo.CreateNoWindow = showCommandPrompt == false && this.hideCommandPrompt;
            if (this.commands != null)
            {
                this.process.StartInfo.RedirectStandardInput = true;
            }


            if (this.logOutput && this.hideCommandPrompt)
            {
                this.process.StartInfo.RedirectStandardError = true;
                this.process.StartInfo.RedirectStandardOutput = true;
                this.process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    this.Logs.Add(e.Data);
                    if (this.Logs.Count > 100)
                    {
                        this.Logs.RemoveAt(0);
                    }
                };
                this.process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    this.Logs.Add(e.Data);
                    if (this.Logs.Count > 100)
                    {
                        this.Logs.RemoveAt(0);
                    }
                };
            }
            this.process.Start();
            if (this.logOutput && this.hideCommandPrompt)
            {
                this.process.BeginOutputReadLine();
                this.process.BeginErrorReadLine();
            }
            if (this.commands != null)
            {
                using StreamWriter sw = process.StandardInput;
                foreach (string command in this.commands)
                {
                    sw.WriteLine(command);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        public bool ProcessIsRunning()
        {
            bool running = false;
            try
            {
                Process[] localAll = Process.GetProcesses();
                foreach (Process proc in localAll)
                {
                    if (proc.ProcessName.IndexOf(this.ProcessIdentifier) >= 0)
                    {
                        running = !proc.HasExited;
                    }
                }
            }
            catch
            {

            }
            return running;
        }

        public void Stop()
        {
            if (this.dryMode)
            {
                return;
            }
            try
            {
                Process[] localAll = Process.GetProcesses();
                foreach (Process proc in localAll)
                {
                    if (proc.ProcessName.IndexOf(this.ProcessIdentifier) >= 0)
                    {

                        Trace.WriteLine(String.Format("We found a {0}-> {1}", this.ProcessIdentifier, proc.ProcessName));
                        proc.Kill();
                        proc.Dispose();
                        proc.Close();
                    }
                }
                if (this.process != null)
                {
                    process.Kill();
                    process.Dispose();
                    process.Close();
                    process = null;
                }
            }
            catch
            {

            }
            this.Logs = new List<string>();
        }

        public abstract bool RequiresDataDir();

        public abstract bool RequiresPassword();

        public abstract bool RequiresWalletPath();

        public abstract bool SupportsVersion();

        public abstract void CheckState(Func<bool, string, string> resultFunction);

        public string GetLogText()
        {
            return String.Join("\n", Array.FindAll(this.Logs.ToArray(), message =>
            {
                return !(!this.showInfo && message.IndexOf("level=info") >= 0) && !(!this.showWarning && message.IndexOf("level=warning") >= 0) && !(!this.showError && message.IndexOf("level=error") >= 0);
            }));
        }

        public bool ShowError
        {
            get
            {
                return this.showError;
            }
            set
            {
                this.showError = value;
            }
        }
        public bool ShowInfo
        {
            get
            {
                return this.showInfo;
            }
            set
            {
                this.showInfo = value;
            }
        }
        public bool ShowWarning
        {
            get
            {
                return this.showWarning;
            }
            set
            {
                this.showWarning = value;
            }
        }

        public bool NewVersionAvailable
        {
            get
            {
                return this.newVersionAvailable;
            }
            set
            {
                this.newVersionAvailable = value;
            }
        }
        public bool DownloadingExecutables
        {
            get
            {
                return this.downloadingExecutables;
            }
            set
            {
                this.downloadingExecutables = value;
            }
        }

        public Dictionary<string, ValidatorBo> ValidatorsByKey
        {
            get
            {
                return this.validatorsByKey;
            }
        }


    }
}

