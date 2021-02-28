using LockMyEthTool.Controllers;
using LockMyEthTool.Views;
using Nethereum.Web3;
using System;

namespace Eth2Overwatch.Controllers
{
    class Eth1Controller : BaseProcessController
    {
        public override PROCESS_TYPES ProcessType
        {
            get
            {
                return PROCESS_TYPES.ETH_1;
            }
        }

        protected override string ProcessIdentifier
        {
            get
            {
                return "geth";
            }
        }

        protected override void InitConfig()
        {

            if (this.dryMode && this.executablePath.Length > 0)
            {
                return;
            }

            this.autoStart = Eth2OverwatchSettings.Default.AutoStart_Eth1;
            this.hideCommandPrompt = Eth2OverwatchSettings.Default.HideCommandPrompt_Eth1;
            this.dataDir = Eth2OverwatchSettings.Default.DataDir_Eth1;
            this.executablePath = Eth2OverwatchSettings.Default.ExecutablePath_Eth1;
            this.additionalCommands = Eth2OverwatchSettings.Default.AdditionalCommands_Eth1;
            this.eth2TestNet = Eth2OverwatchSettings.Default.Eth2_TestNet;
            this.useLocalEth1Node = Eth2OverwatchSettings.Default.UseLocalEth1Node;

        }

        protected override void SetupProcessConfig()
        {
            var add = this.additionalCommands.Length > 0 ? " " + this.additionalCommands : "";
            var goerli = string.Empty != this.eth2TestNet ? " --goerli" : "";
            this.fileName = "cmd.exe";
            this.directory = this.executablePath;
            this.commands = new string[1];
            var ipc = useLocalEth1Node ? " --ipcpath=http://127.0.0.1:8545/ --rpc" : "";
            this.commands[0] = String.Format(@"geth --datadir=" + this.dataDir + ipc + goerli + add);
        }

        public override string GetLastVersion()
        {
            return "";
        }

        public override bool RequiresDataDir()
        {
            return true;
        }
        public override bool RequiresPassword()
        {
            return this.ProcessType == PROCESS_TYPES.VALIDATOR;
        }
        public override bool RequiresWalletPath()
        {
            return this.ProcessType == PROCESS_TYPES.VALIDATOR;
        }
        public override bool SupportsVersion()
        {
            return false;
        }
        public override int GetInitialDelay()
        {
            return 0;
        }
        public override string GetPrysmVersion()
        {
            return "";
        }

        public override void CheckState(Func<bool, string, string> resultFunction)
        {
            if (!this.CheckDataDir())
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
        }


        public override void DownloadExecutable(string path = null, string version = null)
        {
        }

        protected override void SaveConfig()
        {

            if (this.dryMode)
            {
                InitConfig();
                return;
            }
            Eth2OverwatchSettings.Default.AutoStart_Eth1 = this.autoStart;
            Eth2OverwatchSettings.Default.DataDir_Eth1 = this.dataDir;
            Eth2OverwatchSettings.Default.ExecutablePath_Eth1 = this.executablePath;
            Eth2OverwatchSettings.Default.HideCommandPrompt_Eth1 = this.hideCommandPrompt;
            Eth2OverwatchSettings.Default.AdditionalCommands_Eth1 = this.additionalCommands;

            Eth2OverwatchSettings.Default.Save();
        }
    }
}
