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
        private string processIdentifier = null;
        private SecureString password = null;
        private bool autoStart = false;
        private string dataDir = "";
        private string executablePath = "";
        private string keyPath = "";
        private bool hideCommandPrompt = false;
        private bool useLocalEth1Node = false;
        private bool useGoerliTestnet = false;
        private string additionalCommands = "";

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
                    this.autoStart = Eth2OverwatchSettings.Default.Autostart_Validator;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_Validator;
                    this.dataDir = Eth2OverwatchSettings.Default.DataDir_Validator;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_Validator;
                    this.keyPath = Eth2OverwatchSettings.Default.KeyPath_Validator;
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
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    this.autoStart = Eth2OverwatchSettings.Default.Autostart_BeaconChain;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_BeaconChain;
                    this.dataDir = Eth2OverwatchSettings.Default.DataDir_BeaconChain;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_BeaconChain;
                    this.additionalCommands = Eth2OverwatchSettings.Default.AdditionalCommands_BeaconChain;
                    this.useLocalEth1Node = Eth2OverwatchSettings.Default.UseLocalEth1Node;
                    this.useGoerliTestnet = Eth2OverwatchSettings.Default.UseGoerliTestnet;
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
                    Eth2OverwatchSettings.Default.DataDir_Validator = this.dataDir;
                    Eth2OverwatchSettings.Default.ExecutablePath_Validator = this.executablePath;
                    Eth2OverwatchSettings.Default.KeyPath_Validator = this.keyPath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_Validator = this.hideCommandPrompt;
                    Eth2OverwatchSettings.Default.AdditionalCommands_Validator = this.additionalCommands;
                    Eth2OverwatchSettings.Default.UseGoerliTestnet = this.useGoerliTestnet;
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
                    string validatorpath =  this.keyPath.Replace("\\", "\\\\");
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    this.commands[0] = "prysm validator --datadir=" + this.dataDir + " --keymanageropts=\"{\\\"path\\\":\\\"" + validatorpath + "\\\",\\\"passphrase\\\":\\\"" + Secure.SecureStringToString(this.password) + "\\\"}\"" + goerli + add;
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    this.processIdentifier = "beacon";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    var connectTo = useLocalEth1Node ? " --http-web3provider=$HOME/Goerli/geth.ipc" : "";
                    this.commands[1] = String.Format(@"prysm.bat beacon-chain --datadir=" + this.dataDir + connectTo  + goerli + add);
                    break;
                case PROCESS_TYPES.ETH_1:
                    this.processIdentifier = "geth";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[1];
                    var ipc = useLocalEth1Node ? " --ipcpath=r=$HOME/Goerli/geth.ipc --rpc" : "";
                    this.commands[0] = String.Format(@"geth --datadir=" + this.dataDir + ipc + goerli + add);
                    break;
            }

        }

        public bool CheckPassword()
        {
            return !this.RequiresPassword() || this.password != null;
        }

        private bool CheckKeyPath()
        {
            return !this.RequiresPassword() || this.keyPath != String.Empty;
        }

        private bool CheckDataDir()
        {
            return this.dataDir != String.Empty;
        }

        private bool CheckExecutablePath()
        {
            return this.executablePath != String.Empty;
        }

        public bool AllConfigsSet()
        {
            return CheckDataDir() && CheckExecutablePath() && CheckKeyPath() && CheckPassword();
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

        public void UpdateConfig()
        {
            this.Init();
        }

        private List<string> Logs = new List<string>();

        public void Start()
        {
            if(!this.AllConfigsSet())
            {
                return;
            }
            this.Stop();
            this.process = new Process(); // Declare New Process
            this.process.StartInfo.FileName = this.fileName;
            if (this.directory != null)
            {
                this.process.StartInfo.WorkingDirectory = this.directory;
            }

            this.process.StartInfo.UseShellExecute = false;
            this.process.StartInfo.CreateNoWindow = this.hideCommandPrompt;
            this.process.StartInfo.RedirectStandardOutput = true;
            this.process.StartInfo.RedirectStandardInput = true;

            this.process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                this.Logs.Add(e.Data);
                if (this.Logs.Count > 100)
                {
                    this.Logs.RemoveAt(0);
                }
            };
            this.process.Start();
            this.process.BeginOutputReadLine();
            if (this.commands != null)
            {
                using StreamWriter sw = process.StandardInput;
                foreach (string command in this.commands)
                {
                    sw.WriteLine(command);
                    //System.Threading.Thread.Sleep(20000);
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

        public bool RequiresPassword()
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

        public void CheckState(Func<bool, string, string> resultFunction)
        {
            if (!this.CheckPassword())
            {
                resultFunction(false, "Password required");
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
                        if(this.Logs.Count > 0)
                        {
                            resultFunction(false, String.Join("\n", this.Logs.ToArray()));
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
                        if (this.Logs.Count > 0)
                        {
                            resultFunction(false, String.Join("\n", this.Logs.ToArray()));
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