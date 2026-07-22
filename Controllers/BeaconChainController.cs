using Eth2Overwatch.Models;
using Eth2Overwatch.OverwatchUtils;
using LockMyEthTool.Views;
using System;
using System.IO;

namespace Eth2Overwatch.Controllers
{
    class BeaconChainController : PrysmController, IReportContributor
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

            this.fileName = Path.Combine(this.executablePath, this.GetExecutableFileName());
            this.directory = this.executablePath;
            var connectTo = useLocalEth1Node ? " --execution-endpoint=//./pipe/geth.ipc" : "";
            this.commands = null;
            this.arguments = "--accept-terms-of-use --datadir=\"" + this.dataDir + "\"" + connectTo + testNet + add;

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
                resultFunction(true, WebUtils.FetchInfo("http://localhost:8080/healthz"));
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

        public void AddToReport(ReportData data)
        {
            if (data == null)
            {
                return;
            }

            try
            {
                string healthText = WebUtils.FetchInfo("http://localhost:8080/healthz");
                data.BeaconHealth = healthText;
                data.BeaconHealthy = !String.IsNullOrWhiteSpace(healthText)
                    && healthText.IndexOf("error", StringComparison.OrdinalIgnoreCase) < 0;
            }
            catch
            {
                data.BeaconHealthy = false;
                data.BeaconHealth = "unavailable";
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
            Eth2OverwatchSettings.Default.AdditionalCommands_BeaconChain = this.additionalCommands;
            Eth2OverwatchSettings.Default.UseLatestVersion_BeaconChain = this.useLatestVersion;
            Eth2OverwatchSettings.Default.CurrentPrysmVersion_BeaconChain = this.currentVersion;

            Eth2OverwatchSettings.Default.Save();
            this.UpdateConfig();
        }
    }
}
