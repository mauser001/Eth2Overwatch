using LockMyEthTool.Views;
using System;
using System.IO;
using System.Net;

namespace Eth2Overwatch.Controllers
{
    class BeaconChainController : PrysmController
    {        
        public override PROCESS_TYPES ProcessType
        {
            get
            {
                return PROCESS_TYPES.BEACON_CHAIN;
            }
        }

        protected override string ProcessIdentifier
        {
            get
            {
                return "beacon-chain";
            }
        }

        protected override void InitConfig()
        {

            if (this.dryMode && this.executablePath.Length > 0)
            {
                return;
            }

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
        }

        protected override void SetupProcessConfig()
        {
            var add = this.additionalCommands.Length > 0 ? " " + this.additionalCommands : "";
            var testNet = string.Empty != this.eth2TestNet ? " --" + this.eth2TestNet : "";

            this.fileName = "cmd.exe";
            this.directory = this.executablePath;
            this.commands = new string[2];
            this.commands[0] = String.Format(@"cd " + this.directory);
            var connectTo = useLocalEth1Node ? " --http-web3provider=http://127.0.0.1:8545/" : "";
            this.commands[1] = String.Format(this.GetExecutableFileName() + @" --accept-terms-of-use --datadir=" + this.dataDir + connectTo + testNet + add);

        }

        public override string GetLastVersion()
        {
            return this.latestVersion;
        }

        public override bool RequiresDataDir()
        {
            return true;
        }
        public override bool RequiresPassword()
        {
            return false;
        }
        public override bool RequiresWalletPath()
        {
            return false;
        }
        public override bool SupportsVersion()
        {
            return true;
        }
        public override int GetInitialDelay()
        {
            return 0;
        }

        public override void CheckState(Func<bool, string, string> resultFunction)
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
        }

        protected override void SaveConfig()
        {

            if (this.dryMode)
            {
                InitConfig();
                return;
            }
            Eth2OverwatchSettings.Default.Autostart_BeaconChain = this.autoStart;
            Eth2OverwatchSettings.Default.DataDir_BeaconChain = this.dataDir;
            Eth2OverwatchSettings.Default.ExecutablePath_BeaconChain = this.executablePath;
            Eth2OverwatchSettings.Default.HideCommandPrompt_BeaconChain = this.hideCommandPrompt;
            Eth2OverwatchSettings.Default.AdditionalCommands_BeaconChain = this.additionalCommands;
            Eth2OverwatchSettings.Default.UseLatestVersion_BeaconChain = this.useLatestVersion;
            Eth2OverwatchSettings.Default.CurrentPrysmVersion_BeaconChain = this.currentVersion;

            Eth2OverwatchSettings.Default.Save();
            this.UpdateConfig();
        }
    }
}
