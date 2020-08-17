﻿using Eth2Overwatch;
using LockMyEthTool.Models;
using LockMyEthTool.Views;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security;

namespace LockMyEthTool.Controllers
{
    class ProcessController : IProcessController
    {
        private Process process = null;
        public PROCESS_TYPES ProcessType;
        private string fileName;
        private string directory = null;
        private string[] commands;
        private string arguments = null;
        private string processIdentifier = null;
        private SecureString password = null;
        private bool autoStart = false;
        private string dataDir = "";
        private string executablePath = "";
        private string walletPath = "";
        private string keyPath = "";
        private bool hideCommandPrompt = false;
        private bool useLocalEth1Node = false;
        private bool useGoerliTestnet = false;
        private string additionalCommands = "";
        private bool logOutput = true;
        private string latestVersion = "";
        private string filePrefix = "";
        private bool downloadingExecutables = false;

        public ProcessController(PROCESS_TYPES ProcessType)
        {
            this.ProcessType = ProcessType;
            this.Init();
        }

        private void InitConfig()
        {
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

                    this.useGoerliTestnet = Eth2OverwatchSettings.Default.UseGoerliTestnet;
                    try
                    {
                        if (Eth2OverwatchSettings.Default.ValidatorPassword != String.Empty)
                        {
                            this.password = Secure.DecryptString(Eth2OverwatchSettings.Default.ValidatorPassword);
                        }
                    }
                    catch
                    {
                        this.password = null;
                    }
                    this.GetPrysmVersion();                   
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    this.filePrefix = "beacon";
                    this.autoStart = Eth2OverwatchSettings.Default.Autostart_BeaconChain;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_BeaconChain;
                    this.dataDir = Eth2OverwatchSettings.Default.DataDir_BeaconChain;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_BeaconChain;
                    this.additionalCommands = Eth2OverwatchSettings.Default.AdditionalCommands_BeaconChain;
                    this.useLocalEth1Node = Eth2OverwatchSettings.Default.UseLocalEth1Node;
                    this.useGoerliTestnet = Eth2OverwatchSettings.Default.UseGoerliTestnet;
                    this.GetPrysmVersion();
                    break;
                case PROCESS_TYPES.ETH_1:
                    this.autoStart = Eth2OverwatchSettings.Default.AutoStart_Eth1;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_Eth1;
                    this.dataDir = Eth2OverwatchSettings.Default.DataDir_Eth1;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_Eth1;
                    this.additionalCommands = Eth2OverwatchSettings.Default.AdditionalCommands_Eth1;
                    this.useLocalEth1Node = Eth2OverwatchSettings.Default.UseLocalEth1Node;
                    this.useGoerliTestnet = Eth2OverwatchSettings.Default.UseGoerliTestnet;
                    break;
            }
        }
        private void SaveConfig()
        {
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    Eth2OverwatchSettings.Default.Autostart_Validator = this.autoStart;
                    Eth2OverwatchSettings.Default.ExecutablePath_Validator = this.executablePath;
                    Eth2OverwatchSettings.Default.KeyPath_Validator = this.keyPath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_Validator = this.hideCommandPrompt;
                    Eth2OverwatchSettings.Default.AdditionalCommands_Validator = this.additionalCommands;
                    Eth2OverwatchSettings.Default.UseGoerliTestnet = this.useGoerliTestnet;
                    Eth2OverwatchSettings.Default.WalletPath_Validator= this.walletPath;
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    Eth2OverwatchSettings.Default.Autostart_BeaconChain = this.autoStart;
                    Eth2OverwatchSettings.Default.DataDir_BeaconChain = this.dataDir;
                    Eth2OverwatchSettings.Default.ExecutablePath_BeaconChain = this.executablePath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_BeaconChain = this.hideCommandPrompt;
                    Eth2OverwatchSettings.Default.AdditionalCommands_BeaconChain = this.additionalCommands;
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
            var goerli = this.useGoerliTestnet && this.SupportsGoerliTestnet() ? " --goerli" : "";

            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    this.processIdentifier = "validator";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    this.commands[0] = String.Format(this.getExecutables()[0] + " --wallet-dir=" + this.walletPath + " --wallet-password-file=" + this.keyPath + add);
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    this.processIdentifier = "beacon";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    var connectTo = useLocalEth1Node ? " --http-web3provider=http://127.0.0.1:8545/" : "";
                    this.commands[1] = String.Format(this.getExecutables()[0] + @" --datadir=" + this.dataDir + connectTo + add);
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
            return this.latestVersion;
        }

        public string GetPrysmVersion()
        {
            if(this.ProcessType == PROCESS_TYPES.ETH_1)
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
            return this.latestVersion;
        }

        public void DownloadExecutable(string path = null)
        {
            this.Logs = new List<string>();
            if (this.ProcessType != PROCESS_TYPES.ETH_1)
            {
                this.downloadingExecutables = true;
                if(path == null)
                {
                    path = this.executablePath;
                }
                else
                {
                    path = path + @"\prysm";
                }
                bool pathExistis = Directory.Exists(path);
                if(!pathExistis)
                {
                    Directory.CreateDirectory(path);
                }

                int count = 0;
                foreach (string fileName in this.RequiredFiles())
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler((object obj, System.ComponentModel.AsyncCompletedEventArgs args) =>
                        {
                            count++;
                            if(count < 3)
                            {
                                this.Logs.Add("Files downloaded: " + count + " of 3");
                            }
                            else
                            {
                                this.downloadingExecutables = false;
                                this.Logs.Add("Executable download complete");
                            }
                        });
                        Uri url = new Uri("https://prysmaticlabs.com/releases/" + fileName);
                        webClient.DownloadFileAsync(url, path + @"\"+fileName);
                    }
                }
            }
        }

        public void ImportKeys(string medallaKeyPath)
        {
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    this.logOutput = false;
                    this.processIdentifier = "no identifier";
                    this.fileName = this.ExecutablePath + "\\"+this.getExecutables()[0];
                    this.directory = this.ExecutablePath;
                    this.commands = null;
                    this.arguments = "accounts-v2 import --keys-dir=" + medallaKeyPath + " --wallet-dir=" + this.walletPath;
                    this.Start(true, true);
                    break;
            }
        }

        private string[] RequiredFiles()
        {
            string[] files = null;
            if(this.ProcessType == PROCESS_TYPES.ETH_1)
            {
                files = new string[1];
                files[0] = "geth.exe";
            }
            else
            {
                files = new string[3];
                files[0] = this.GetExecutableFileName();
                files[1] = this.GetExecutableFileName() + ".sha256";
                files[2] = this.GetExecutableFileName() + ".sig";
            }

            return files;
        }

        public string[] getExecutables()
        {
            // Get the files
            DirectoryInfo info = new DirectoryInfo(this.executablePath);
            FileInfo[] files = info.GetFiles();
            files = Array.FindAll(files, file => file.Name.IndexOf(this.filePrefix) == 0 && file.Name.IndexOf(".exe") == file.Name.Length-4);

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

        private bool CheckExecutablePath(string path = null)
        {
            if(path == null)
            {
                path = this.executablePath;
            }
            return !String.IsNullOrWhiteSpace(path) && Directory.Exists(path);
        }

        private string GetExecutableFileName()
        {
            string fileName = "";
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    fileName = "validator-" + latestVersion + "-windows-amd64.exe";
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    fileName = "beacon-chain-" + latestVersion + "-windows-amd64.exe";
                    break;
                case PROCESS_TYPES.ETH_1:
                    fileName = "geth.exe";
                    break;
            }

            return fileName;
        }

        public bool CheckExecutable(string path = null)
        {
            if(this.downloadingExecutables)
            {
                return false;
            }
            if(path == null)
            {
                path = this.executablePath;
            }
           return this.CheckExecutablePath(path) && Array.Find(RequiredFiles(), name => !File.Exists(path + @"\" + name)) == null;
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
        public bool UseGoerliTestnet
        {
            get
            {
                return this.useGoerliTestnet;
            }
            set
            {
                if(!this.SupportsGoerliTestnet())
                {
                    return;
                }
                this.useGoerliTestnet = value;
                SaveConfig();
            }
        }
        public bool UseLocalEth1Node
        {
            get
            {
                return this.useLocalEth1Node;
            }
            set
            {

                this.useLocalEth1Node = value;
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

        public void UpdateConfig()
        {
            this.Init();
        }

        private List<string> Logs = new List<string>();

        public void Start(bool skipCheck = false, bool showCommandPrompt = false, bool dontStop = false)
        {
            if(skipCheck != true && !this.AllConfigsSet())
            {
                return;
            }
            if(!dontStop)
            {
                this.Stop();
            }
            this.process = new Process(); // Declare New Process
            this.process.StartInfo.FileName = this.fileName;
            if(this.arguments != null)
            {
                this.process.StartInfo.Arguments = this.arguments;
            }
            if (this.directory != null)
            {
                this.process.StartInfo.WorkingDirectory = this.directory;
            }

            this.process.StartInfo.UseShellExecute = false;
            this.process.StartInfo.CreateNoWindow = showCommandPrompt == false && this.hideCommandPrompt;
            if(this.commands != null)
            {
                this.process.StartInfo.RedirectStandardInput = true;
            }


            if(this.logOutput && this.hideCommandPrompt)
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

        public void Stop()
        {
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

        public bool SupportsEth1Connection()
        {
            return this.ProcessType == PROCESS_TYPES.ETH_1 || this.ProcessType == PROCESS_TYPES.BEACON_CHAIN;
        }

        public string GetLogText()
        {
            return String.Join("\n", this.Logs.ToArray());
        }

        public void CheckState(Func<bool, string, string> resultFunction)
        {
            if(this.downloadingExecutables)
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
                        HttpWebRequest webRequest = HttpWebRequest.CreateHttp("http://localhost:8081/healthz");

                        using HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                        using StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
                        string response = streamReader.ReadToEnd();
                        resultFunction(true, response);
                    }
                    catch
                    {
                        resultFunction(false, "Validator is not working properly");
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
                        resultFunction(false, "Beacon-Chain is not working properly");
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
                        if (this.Logs.Count > 0)
                        {
                            resultFunction(false, String.Join("\n", this.Logs.ToArray()));
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
