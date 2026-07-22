using Eth2Overwatch.Models;
using Eth2Overwatch.OverwatchUtils;
using LockMyEthTool.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LockMyEthTool.Controllers
{
    abstract class BaseProcessController : IProcessController
    {
        private const int GracefulStopTimeoutMs = 5000;

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
        private readonly ProcessLogStore logStore;
        private bool disposed;
        private volatile bool isShuttingDown;

        public BaseProcessController()
        {
            this.logStore = new ProcessLogStore(() =>
            {
                string processName = this.ProcessIdentifier;
                if (String.IsNullOrWhiteSpace(processName))
                {
                    processName = this.ProcessType.ToString().ToLowerInvariant();
                }

                return processName;
            });
            this.Init();
        }

        ~BaseProcessController()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Intentionally do not stop managed processes on Overseer shutdown.
                // Closing the UI must not impact already running clients.
                this.logStore.Dispose();
            }

            this.disposed = true;
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
            this.logStore.LoadRecentFromFile();

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

        public virtual bool IsValidVersion(string version = null)
        {
            return false;
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

        public void Start(bool skipCheck = false, bool showCommandPrompt = false, bool dontStop = false)
        {
            if (skipCheck != true && !this.AllConfigsSet())
            {
                return;
            }

            // When Overseer restarts, there is no tracked Process instance. If a matching
            // process is already running, do not interrupt it just to start again.
            if (!dontStop && this.process == null && this.ProcessIsRunning())
            {
                this.logStore.LoadRecentFromFile();
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
            this.process.StartInfo.CreateNoWindow = showCommandPrompt == false;
            this.process.StartInfo.RedirectStandardInput = true;


            if (this.logOutput)
            {
                this.process.StartInfo.RedirectStandardError = true;
                this.process.StartInfo.RedirectStandardOutput = true;
                this.process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    this.logStore.Append(e.Data);
                };
                this.process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                {
                    this.logStore.Append(e.Data);
                };
            }
            this.process.Start();
            if (this.logOutput)
            {
                this.process.BeginOutputReadLine();
                this.process.BeginErrorReadLine();
            }
            if (this.commands != null)
            {
                using StreamWriter sw = process.StandardInput;
                foreach (string command in this.commands)
                {
                    if (String.IsNullOrWhiteSpace(command))
                    {
                        continue;
                    }
                    sw.WriteLine(command);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        public bool ProcessIsRunning()
        {
            try
            {
                string identifier = this.ProcessIdentifier ?? "";
                if (String.IsNullOrWhiteSpace(identifier))
                {
                    return false;
                }

                Process[] localAll = Process.GetProcesses();
                foreach (Process proc in localAll)
                {
                    if (proc.ProcessName.IndexOf(identifier, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        continue;
                    }

                    if (!proc.HasExited)
                    {
                        return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public void Stop()
        {
            if (this.dryMode)
            {
                return;
            }
            this.isShuttingDown = true;
            Process trackedProcess = this.process;
            bool trackedProcessKilledByScan = false;
            try
            {
                string identifier = this.ProcessIdentifier ?? "";
                Process[] localAll = Process.GetProcesses();
                foreach (Process proc in localAll)
                {
                    if (!String.IsNullOrWhiteSpace(identifier) && proc.ProcessName.IndexOf(identifier, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (proc.HasExited)
                        {
                            continue;
                        }

                        if (trackedProcess != null && proc.Id == trackedProcess.Id)
                        {
                            trackedProcessKilledByScan = true;
                        }

                        Trace.WriteLine(String.Format("We found a {0}-> {1}", this.ProcessIdentifier, proc.ProcessName));
                        if (!this.TryGracefulStop(proc))
                        {
                            proc.Kill();
                        }
                        proc.Dispose();
                        proc.Close();
                    }
                }

                if (trackedProcess != null && !trackedProcessKilledByScan)
                {
                    if (!trackedProcess.HasExited && !this.TryGracefulStop(trackedProcess))
                    {
                        trackedProcess.Kill();
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (trackedProcess != null)
                {
                    try
                    {
                        trackedProcess.Dispose();
                        trackedProcess.Close();
                    }
                    catch
                    {

                    }
                }
                this.process = null;
                this.isShuttingDown = false;
            }
            this.logStore.ClearInMemory();
            this.logStore.FlushPendingToFile();
        }

        public abstract bool RequiresDataDir();

        public abstract bool RequiresPassword();

        public abstract bool RequiresWalletPath();

        public abstract bool SupportsVersion();

        public abstract void CheckState(Func<bool, string, string> resultFunction);

        protected void ClearProcessLogs()
        {
            this.logStore.ClearInMemory();
        }

        protected void AddProcessLog(string message)
        {
            this.logStore.Append(message);
        }

        private bool TryGracefulStop(Process targetProcess)
        {
            if (targetProcess == null)
            {
                return true;
            }

            try
            {
                if (targetProcess.HasExited)
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }

            try
            {
                // Best-effort for processes with a message loop/window.
                if (targetProcess.CloseMainWindow() && targetProcess.WaitForExit(GracefulStopTimeoutMs))
                {
                    return true;
                }
            }
            catch
            {

            }

            try
            {
                // Best-effort for the tracked process started by Overseer.
                if (this.process != null && targetProcess.Id == this.process.Id)
                {
                    targetProcess.StandardInput.WriteLine("exit");
                    targetProcess.StandardInput.Flush();
                    if (targetProcess.WaitForExit(GracefulStopTimeoutMs))
                    {
                        return true;
                    }
                }
            }
            catch
            {

            }

            return false;
        }

        public string GetLogText()
        {
            return this.logStore.GetFilteredText(this.showInfo, this.showWarning, this.showError);
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

        public bool IsShuttingDown
        {
            get
            {
                return this.isShuttingDown;
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

