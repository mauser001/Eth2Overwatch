using Eth2Overwatch;
using LockMyEthTool.Models;
using LockMyEthTool.Views;
using Nethereum.Web3;
using System;
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
                    break;
                case PROCESS_TYPES.ETH_1:
                    this.autoStart = Eth2OverwatchSettings.Default.AutoStart_Eth1;
                    this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_Eth1;
                    this.dataDir = Eth2OverwatchSettings.Default.DataDir_Eth1;
                    this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_Eth1;
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
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    Eth2OverwatchSettings.Default.Autostart_BeaconChain = this.autoStart;
                    Eth2OverwatchSettings.Default.DataDir_BeaconChain = this.dataDir;
                    Eth2OverwatchSettings.Default.ExecutablePath_BeaconChain = this.executablePath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_BeaconChain = this.hideCommandPrompt;
                    break;
                case PROCESS_TYPES.ETH_1:
                    Eth2OverwatchSettings.Default.AutoStart_Eth1 = this.autoStart;
                    Eth2OverwatchSettings.Default.DataDir_Eth1 = this.dataDir;
                    Eth2OverwatchSettings.Default.ExecutablePath_Eth1 = this.executablePath;
                    Eth2OverwatchSettings.Default.HideCommandPrompt_Eth1 = this.hideCommandPrompt;
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

            

            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    this.processIdentifier = "validator";
                    this.fileName = "cmd.exe";
                    string validatorpath =  this.keyPath.Replace("\\", "\\\\");
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    this.commands[0] = "prysm validator --datadir=" + this.dataDir + " --keymanageropts=\"{\\\"path\\\":\\\"" + validatorpath + "\\\",\\\"passphrase\\\":\\\"" + Secure.SecureStringToString(this.password) + "\\\"}\"";
                    break;
                case PROCESS_TYPES.BEACON_CHAIN:
                    this.processIdentifier = "beacon";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[2];
                    this.commands[0] = String.Format(@"cd " + this.directory);
                    this.commands[1] = String.Format(@"prysm.bat beacon-chain --http-web3provider=$HOME/Goerli/geth.ipc --datadir=" + this.dataDir);
                    break;
                case PROCESS_TYPES.ETH_1:
                    this.processIdentifier = "geth";
                    this.fileName = "cmd.exe";
                    this.directory = this.executablePath;
                    this.commands = new string[1];
                    this.commands[0] = String.Format(@"geth --goerli --ipcpath=r=$HOME/Goerli/geth.ipc --rpc --datadir=" + this.dataDir);
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
                Trace.WriteLine(this.processIdentifier + " output>>" + e.Data);

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
        }

        public bool RequiresPassword()
        {
            return this.ProcessType == PROCESS_TYPES.VALIDATOR;
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
                        resultFunction(false, "Last block could not be loaded.");
                    }
                    break;
            }

        }
    }
}
