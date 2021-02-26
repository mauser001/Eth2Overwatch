﻿using Eth2Overwatch;
using Eth2Overwatch.Models;
using Ethereum.Eth.v1alpha1;
using Grpc.Net.Client;
using LockMyEthTool.Models;
using LockMyEthTool.Views;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace LockMyEthTool.Controllers
{
    class ProcessController : IProcessController
    {
        private Process process = null;
        public PROCESS_TYPES ProcessType;
        private bool dryMode = false; // Used for development
        private string fileName;
        private string directory = null;
        private string[] commands;
        private string arguments = null;
        private string processIdentifier = null;
        private bool autoStart = false;
        private string dataDir = "";
        private string executablePath = "";
        private string walletPath = "";
        private string keyPath = "";
        private bool hideCommandPrompt = false;
        private bool useLocalEth1Node = true;
        private string eth2TestNet = "";
        private string additionalCommands = "";
        private bool logOutput = true;
        private string latestVersion = "";
        private string currentVersion = "";
        private string filePrefix = "";
        private bool useLatestVersion = true;
        private bool downloadingExecutables = false;
        private bool showError = true;
        private bool showWarning = true;
        private bool newVersionAvailable = false;
        private bool showInfo = true;
        private string reportPath = "";
        private string reportKey = "";
        private string reportLabel = "";
        private Dictionary<string, ValidatorBo> validatorsByKey = new Dictionary<string, ValidatorBo>();

        public ProcessController(PROCESS_TYPES ProcessType)
        {
            this.ProcessType = ProcessType;
            this.Init();
        }

        private void InitConfig()
        {
            if(this.dryMode && this.executablePath.Length > 0)
            {
                return;
            }
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    this.filePrefix = "validator";
                    this.autoStart = Eth2OverwatchSettings.Default.Autostart_Validator;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_Validator;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_Validator;
                    this.keyPath = Eth2OverwatchSettings.Default.KeyPath_Validator;
                    this.walletPath = Eth2OverwatchSettings.Default.WalletPath_Validator;
                    this.additionalCommands = Eth2OverwatchSettings.Default.AdditionalCommands_Validator;
                    this.useLatestVersion = Eth2OverwatchSettings.Default.UseLatestVersion_Validator;
                    this.currentVersion = Eth2OverwatchSettings.Default.CurrentPrysmVersion_Validator;

                    this.eth2TestNet = Eth2OverwatchSettings.Default.Eth2_TestNet;
                    this.reportPath = Eth2OverwatchSettings.Default.ReportPath;
                    this.reportKey = Eth2OverwatchSettings.Default.ReportKey;
                    this.reportLabel = Eth2OverwatchSettings.Default.ReportLabel;
                    this.GetPrysmVersion();
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    this.filePrefix = "beacon";
                    this.autoStart = Eth2OverwatchSettings.Default.Autostart_BeaconChain;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_BeaconChain;
                    this.dataDir = Eth2OverwatchSettings.Default.DataDir_BeaconChain;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_BeaconChain;
                    this.additionalCommands = Eth2OverwatchSettings.Default.AdditionalCommands_BeaconChain;
                    this.useLatestVersion = Eth2OverwatchSettings.Default.UseLatestVersion_BeaconChain;
                    this.currentVersion = Eth2OverwatchSettings.Default.CurrentPrysmVersion_BeaconChain;

                    this.eth2TestNet = Eth2OverwatchSettings.Default.Eth2_TestNet;
                    this.useLocalEth1Node = Eth2OverwatchSettings.Default.UseLocalEth1Node;
                    this.GetPrysmVersion();
                    break;
                case PROCESS_TYPES.ETH_1:
                    this.autoStart = Eth2OverwatchSettings.Default.AutoStart_Eth1;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_Eth1;
                    this.dataDir = Eth2OverwatchSettings.Default.DataDir_Eth1;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_Eth1;
                    this.additionalCommands = Eth2OverwatchSettings.Default.AdditionalCommands_Eth1;
                    this.eth2TestNet = Eth2OverwatchSettings.Default.Eth2_TestNet;
                    this.useLocalEth1Node = Eth2OverwatchSettings.Default.UseLocalEth1Node;
                    break;
            }
        }
        private void SaveConfig()
        {
            if(this.dryMode)
            {
                InitConfig();
                return;
            }
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    Eth2OverwatchSettings.Default.Autostart_Validator = this.autoStart;
                    Eth2OverwatchSettings.Default.ExecutablePath_Validator = this.executablePath;
                    Eth2OverwatchSettings.Default.KeyPath_Validator = this.keyPath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_Validator = this.hideCommandPrompt;
                    Eth2OverwatchSettings.Default.AdditionalCommands_Validator = this.additionalCommands;
                    Eth2OverwatchSettings.Default.WalletPath_Validator = this.walletPath;
                    Eth2OverwatchSettings.Default.ReportPath = this.reportPath;
                    Eth2OverwatchSettings.Default.ReportLabel = this.reportLabel;
                    Eth2OverwatchSettings.Default.ReportKey = this.reportKey;
                    Eth2OverwatchSettings.Default.UseLatestVersion_Validator = this.useLatestVersion;
                    Eth2OverwatchSettings.Default.CurrentPrysmVersion_Validator = this.currentVersion;
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    Eth2OverwatchSettings.Default.Autostart_BeaconChain = this.autoStart;
                    Eth2OverwatchSettings.Default.DataDir_BeaconChain = this.dataDir;
                    Eth2OverwatchSettings.Default.ExecutablePath_BeaconChain = this.executablePath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_BeaconChain = this.hideCommandPrompt;
                    Eth2OverwatchSettings.Default.AdditionalCommands_BeaconChain = this.additionalCommands;
                    Eth2OverwatchSettings.Default.UseLatestVersion_BeaconChain = this.useLatestVersion;
                    Eth2OverwatchSettings.Default.CurrentPrysmVersion_BeaconChain = this.currentVersion;
                    break;
                case PROCESS_TYPES.ETH_1:
                    Eth2OverwatchSettings.Default.AutoStart_Eth1 = this.autoStart;
                    Eth2OverwatchSettings.Default.DataDir_Eth1 = this.dataDir;
                    Eth2OverwatchSettings.Default.ExecutablePath_Eth1 = this.executablePath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_Eth1 = this.hideCommandPrompt;
                    Eth2OverwatchSettings.Default.AdditionalCommands_Eth1 = this.additionalCommands;
                    break;
            }
            Eth2OverwatchSettings.Default.Save();
            Init();
        }

        private void Init()
        {
            InitConfig();

            if (!AllConfigsSet())
            {
                return;
            }

            var add = this.additionalCommands.Length > 0 ? " " + this.additionalCommands : "";
            var goerli = string.Empty != this.eth2TestNet && this.SupportsGoerliTestnet() ? " --goerli" : "";
            var testNet = string.Empty != this.eth2TestNet ? " --" + this.eth2TestNet : "";

            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    this.processIdentifier = "validator";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    this.commands[0] = String.Format(this.GetExecutableFileName() + " --accept-terms-of-use --wallet-dir=" + this.walletPath + " --wallet-password-file=" + this.keyPath + testNet + add);
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    this.processIdentifier = "beacon";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    var connectTo = useLocalEth1Node ? " --http-web3provider=http://127.0.0.1:8545/" : "";
                    this.commands[1] = String.Format(this.GetExecutableFileName() + @" --accept-terms-of-use --datadir=" + this.dataDir + connectTo + testNet + add);
                    break;
                case PROCESS_TYPES.ETH_1:
                    this.processIdentifier = "geth";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[1];
                    var ipc = useLocalEth1Node ? " --ipcpath=http://127.0.0.1:8545/ --rpc" : "";
                    this.commands[0] = String.Format(@"geth --datadir=" + this.dataDir + ipc + goerli + add);
                    break;
            }

        }

        public string GetLastVersion()
        {
            return this.latestVersion == null ? "" : this.latestVersion;
        }

        public string GetPrysmVersion()
        {
            if (this.ProcessType == PROCESS_TYPES.ETH_1)
            {
                return "";
            }

            try
            {
                HttpWebRequest webRequest = HttpWebRequest.CreateHttp("https://prysmaticlabs.com/releases/latest");

                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());

                this.latestVersion = streamReader.ReadToEnd().Replace("\n", "");
                Eth2OverwatchSettings.Default.LastPrysmVersion = this.latestVersion;
            }
            catch
            {
                this.latestVersion = Eth2OverwatchSettings.Default.LastPrysmVersion;
            }
            if(this.useLatestVersion)
            {
                this.currentVersion = this.latestVersion;
            }
            return this.latestVersion == null ? "" : this.latestVersion;
        }
        private bool URLExists(string url)
        {
            bool result = true;

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            try
            {
                webRequest.GetResponse();
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public void DownloadExecutable(string path = null, string version = null)
        {
            this.Logs = new List<string>();
            if (this.ProcessType != PROCESS_TYPES.ETH_1)
            {
                if (path == null)
                {
                    path = this.executablePath;
                }
                else
                {
                    path += @"\prysm";
                }
                bool pathExistis = Directory.Exists(path);
                if (!pathExistis)
                {
                    Directory.CreateDirectory(path);
                }

                int count = 0;
                if(!this.URLExists("https://prysmaticlabs.com/releases/" + this.RequiredFiles(version)[0]))
                {
                    this.Logs.Add("File does not exist");
                    if (this.Logs.Count > 100)
                    {
                        this.Logs.RemoveAt(0);
                    }
                    return;
                }

                if (version == null || this.currentVersion == version)
                {
                    this.downloadingExecutables = true;
                }
                foreach (string fileName in this.RequiredFiles(version))
                {                    
                    using WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler((object obj, System.ComponentModel.AsyncCompletedEventArgs args) =>
                    {
                        count++;
                        if (count < 3)
                        {
                            this.Logs.Add("Files downloaded: " + count + " of 3");
                        }
                        else
                        {
                            if (version == null || this.currentVersion == version)
                            {
                                this.downloadingExecutables = false;
                                this.newVersionAvailable = true;
                            }
                            this.Logs.Add("Executable download complete");
                        }
                    });
                    Uri url = new Uri("https://prysmaticlabs.com/releases/" + fileName);
                    webClient.DownloadFileAsync(url, path + @"\" + fileName);
                }
            }
        }

        public void ImportKeys(string keyPath)
        {
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    var testNet = string.Empty != this.eth2TestNet ? " --" + this.eth2TestNet : "";
                    this.logOutput = false;
                    this.processIdentifier = "no identifier";
                    this.fileName = this.ExecutablePath + "\\" + this.GetExecutables()[0];
                    this.directory = this.ExecutablePath;
                    this.commands = null;
                    this.arguments = "accounts import --keys-dir=" + keyPath + " --wallet-dir=" + this.walletPath + testNet;
                    this.Start(true, true);
                    break;
            }
        }

        public int GetInitialDelay()
        {
            return this.ProcessType == PROCESS_TYPES.VALIDATOR ? 2 : 0;
        }

        private string[] RequiredFiles(string version = null)
        {
            string[] files;
            if (this.ProcessType == PROCESS_TYPES.ETH_1)
            {
                files = new string[1];
                files[0] = "geth.exe";
            }
            else
            {
                files = new string[3];
                files[0] = this.GetExecutableFileName(version);
                files[1] = this.GetExecutableFileName(version) + ".sha256";
                files[2] = this.GetExecutableFileName(version) + ".sig";
            }

            return files;
        }

        public string[] GetExecutables()
        {
            // Get the files
            DirectoryInfo info = new DirectoryInfo(this.executablePath);
            FileInfo[] files = info.GetFiles();
            files = Array.FindAll(files, file => file.Name.IndexOf(this.filePrefix) == 0 && file.Name.IndexOf(".exe") == file.Name.Length - 4);

            // Sort by creation-time descending 
            Array.Sort(files, delegate (FileInfo f1, FileInfo f2)
            {
                return f2.CreationTime.CompareTo(f1.CreationTime);
            });

            return Array.ConvertAll<FileInfo, string>(files, file => file.Name);
        }

        public bool CheckWalletPath()
        {
            return !this.RequiresWalletPath() || !String.IsNullOrWhiteSpace(this.walletPath) && Directory.Exists(this.walletPath);
        }

        private bool CheckKeyPath()
        {
            return !this.RequiresPassword() || !String.IsNullOrWhiteSpace(this.keyPath) && File.Exists(this.keyPath);
        }

        private bool CheckDataDir()
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

        private string GetExecutableFileName(string version = null)
        {
            string fileName = "";
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    fileName = "validator-" + (version != null ? version : this.currentVersion) + "-windows-amd64.exe";
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    fileName = "beacon-chain-" + (version != null ? version : this.currentVersion) + "-windows-amd64.exe";
                    break;
                case PROCESS_TYPES.ETH_1:
                    fileName = "geth.exe";
                    break;
            }

            return fileName;
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

        public void SetPassword(string pw)
        {
            if (this.ProcessType == PROCESS_TYPES.VALIDATOR)
            {
                Eth2OverwatchSettings.Default.ValidatorPassword = Secure.EncryptString(Secure.ToSecureString(pw));
                Eth2OverwatchSettings.Default.Save();
            }
            this.Init();
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

        private List<string> Logs = new List<string>();

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
                    if (proc.ProcessName.IndexOf(this.processIdentifier) >= 0)
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
                    if (proc.ProcessName.IndexOf(this.processIdentifier) >= 0)
                    {

                        Trace.WriteLine(String.Format("We found a {0}-> {1}", this.processIdentifier, proc.ProcessName));
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

        public bool RequiresDataDir()
        {
            return this.ProcessType == PROCESS_TYPES.BEACON_CHAIN || this.ProcessType == PROCESS_TYPES.ETH_1;
        }

        public bool RequiresPassword()
        {
            return this.ProcessType == PROCESS_TYPES.VALIDATOR;
        }
        public bool RequiresWalletPath()
        {
            return this.ProcessType == PROCESS_TYPES.VALIDATOR;
        }

        public bool SupportsGoerliTestnet()
        {
            return this.ProcessType == PROCESS_TYPES.ETH_1;
        }

        public bool SupportsVersion()
        {
            return this.ProcessType != PROCESS_TYPES.ETH_1;
        }

        public bool SupportsEth1Connection()
        {
            return this.ProcessType == PROCESS_TYPES.ETH_1 || this.ProcessType == PROCESS_TYPES.BEACON_CHAIN;
        }

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

        public void CheckState(Func<bool, string, string> resultFunction)
        {
            if (this.downloadingExecutables)
            {
                resultFunction(false, "Downloading executables");
                return;
            }
            else if (!this.CheckDataDir())
            {
                resultFunction(false, "DataDir required");
                return;
            }
            else if (!this.CheckExecutablePath())
            {
                resultFunction(false, "ExecutablePath required");
                return;
            }
            else if (!this.CheckExecutable())
            {
                resultFunction(false, "Exacutlable not found: " + this.GetExecutableFileName());
                return;
            }
            else if (!this.CheckKeyPath())
            {
                resultFunction(false, "KeyPath required");
                return;
            }

            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:

                    try
                    {
                        if(this.validatorsByKey.Count == 0)
                        {
                            HttpWebRequest metricsRequest = HttpWebRequest.CreateHttp("http://localhost:8081/metrics");
                            using HttpWebResponse metricsResponse = (HttpWebResponse)metricsRequest.GetResponse();
                            using StreamReader streamReader = new StreamReader(metricsResponse.GetResponseStream());
                            string metricsText = streamReader.ReadToEnd();
                            string[] lines = metricsText.Split("\n");
                            foreach (string line in lines)
                            {
                                if (line.IndexOf("validator_statuses{pubkey=") == 0)
                                {
                                    string publicKey = line.Split("\"")[1];
                                    ValidatorBo bo = new ValidatorBo(publicKey);
                                    if (!this.validatorsByKey.ContainsKey(bo.PublicKeyBase64))
                                    {
                                        this.validatorsByKey[bo.PublicKeyBase64] = bo;
                                    }
                                }
                            }
                        }

                        var httpHandler = new System.Net.Http.HttpClientHandler();
                        httpHandler.ServerCertificateCustomValidationCallback =
                            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                        using var channel = GrpcChannel.ForAddress("http://127.0.0.1:4000", new GrpcChannelOptions { HttpHandler = httpHandler });
                        var validatorClient = new BeaconNodeValidator.BeaconNodeValidatorClient(channel);
                        var beaconClient = new BeaconChain.BeaconChainClient(channel);
                        string result = "";
                        ValidatorPerformanceRequest performanceRequest = new ValidatorPerformanceRequest();
                        MultipleValidatorStatusRequest statusRequest = new MultipleValidatorStatusRequest();

                        foreach (KeyValuePair<string, ValidatorBo> keyValue in this.validatorsByKey)
                        {
                            performanceRequest.PublicKeys.Add(keyValue.Value.PublicKeyByteString);
                            statusRequest.PublicKeys.Add(keyValue.Value.PublicKeyByteString);
                        }

                        ValidatorPerformanceResponse performance = beaconClient.GetValidatorPerformance(performanceRequest);
                        MultipleValidatorStatusResponse status = validatorClient.MultipleValidatorStatus(statusRequest);

                        ulong balances = 0;
                        Dictionary<string, int> stateCounter = new Dictionary<string, int>();
                        for (int i = 0; i < performance.PublicKeys.Count; i++)
                        {
                            ValidatorBo bo = this.validatorsByKey[performance.PublicKeys[i].ToBase64()];
                            bo.Balance = performance.BalancesAfterEpochTransition[i];
                            bo.CurrentEffectiveBalance = performance.CurrentEffectiveBalances[i];
                            bo.CorrectlyVotedHead = performance.CorrectlyVotedHead[i];
                            bo.CorrectlyVotedSource = performance.CorrectlyVotedSource[i];
                            bo.CorrectlyVotedTarget = performance.CorrectlyVotedTarget[i];
                            bo.InclusionDistance = performance.InclusionDistances[i];
                            balances += bo.Balance;
                            bo.State = status.Statuses[i].Status;
                            if (!stateCounter.ContainsKey(bo.State.ToString()))
                            {
                                stateCounter[bo.State.ToString()] = 1;
                            }
                            else
                            {
                                stateCounter[bo.State.ToString()]++;
                            }
                        }

                        result += "Total Balance: " + Utils.GWeiToEthLabel(balances) + "\n";
                        foreach (KeyValuePair<string, int> keyValue in stateCounter)
                        {
                           if(keyValue.Value > 0)
                            {
                                result += "State " + keyValue.Key + ": " + keyValue.Value + "\n";
                            }
                        }

                        HttpWebRequest healthzRequest = HttpWebRequest.CreateHttp("http://localhost:8081/healthz");

                        using HttpWebResponse healthzResponse = (HttpWebResponse)healthzRequest.GetResponse();
                        using StreamReader healthzReader = new StreamReader(healthzResponse.GetResponseStream());
                        result += healthzReader.ReadToEnd();
                        resultFunction(true, result);
                        if(this.reportPath.Length > 0)
                        {
                            try
                            {
                                var reportWebRequest = (HttpWebRequest)WebRequest.Create(this.reportPath);
                                reportWebRequest.ContentType = "application/json";
                                reportWebRequest.Method = "POST";
                                ReportBody body = new ReportBody();
                                body.data = new ReportData();
                                body.data.Version = this.currentVersion;
                                body.data.LatestVersion = this.latestVersion;
                                body.data.Label = this.reportLabel;
                                body.data.TS = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                                body.code = this.reportKey;
                                body.data.Validators = new List<ReportValidatorInfo>();
                                foreach (KeyValuePair<string, ValidatorBo> keyValue in ValidatorsByKey)
                                {
                                    body.data.Validators.Add(keyValue.Value.ReportInfo);
                                }
                                using (var streamWriter = new StreamWriter(reportWebRequest.GetRequestStream()))
                                {
                                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(body);

                                    streamWriter.Write(json);
                                }
                                using HttpWebResponse reportResponse = (HttpWebResponse)reportWebRequest.GetResponse();
                                using StreamReader reportResponseReader = new StreamReader(reportResponse.GetResponseStream());
                                string test = reportResponseReader.ReadToEnd();
                            }
                            catch
                            {

                            }
                        }
                        return;
                    }
                    catch
                    {
                        this.validatorsByKey.Clear();
                        if (this.ProcessIsRunning())
                        {
                           resultFunction(true, "Could not get healthz state, but process is still running.");
                        }
                        else
                        {
                            resultFunction(false, "Validator is not working properly");
                        }
                    }

                    break;
                case PROCESS_TYPES.BEACON_CHAIN:

                    try
                    {
                         HttpWebRequest webRequest = HttpWebRequest.CreateHttp("http://localhost:8080/healthz");

                        using HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                        using StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                        string response = streamReader.ReadToEnd();
                        resultFunction(true, response);
                    }
                    catch
                    {
                        if (this.ProcessIsRunning())
                        {
                            resultFunction(true, "Could not get healthz state, but process is still running.");
                        }
                        else
                        {
                            resultFunction(false, "Beacon-Chain is not working properly");
                        }
                    }
                    break;
                case PROCESS_TYPES.ETH_1:
                    var web3 = new Web3();
                    var blockNumber = web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
                    string text;
                    try
                    {
                        text = "Latest block number: " + blockNumber.Result.ToString();
                        resultFunction(true, text);
                    }
                    catch
                    {

                        if (this.ProcessIsRunning())
                        {
                            resultFunction(true, "Could not load last block, but process is still running.");
                        }
                        else
                        {
                            resultFunction(false, "Last block could not be loaded.");
                        }
                    }
                    break;
            }
        }

    }
}

