using Eth2Overwatch.Models;
using Eth2Overwatch.OverwatchUtils;
using LockMyEthTool.Controllers;
using LockMyEthTool.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Eth2Overwatch.Controllers
{
    class Eth1Controller : BaseProcessController, IReportContributor
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
            this.fileName = Path.Combine(this.executablePath, this.GetExecutableFileName());
            this.directory = this.executablePath;
            var ipc = useLocalEth1Node ? " --http --http.api eth,net,engine,admin" : "";
            this.commands = null;
            this.arguments = "--datadir=\"" + this.dataDir + "\"" + ipc + goerli + add;
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

            try
            {
                resultFunction(true, BuildEth1StateText());
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

        private string BuildEth1StateText()
        {
            ReportData reportData = new ReportData();
            PopulateEth1Status(reportData);

            if (reportData.Eth1Syncing == false)
            {
                return reportData.Eth1CurrentBlock.HasValue
                    ? "Sync state: synced\nLatest block number: " + reportData.Eth1CurrentBlock.Value
                    : "Sync state: synced\nLatest block number: unavailable";
            }

            if (reportData.Eth1Syncing == true)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Sync state: syncing");
                builder.AppendLine("Current block: " + (reportData.Eth1CurrentBlock?.ToString() ?? "unknown"));
                builder.AppendLine("Highest block: " + (reportData.Eth1HighestBlock?.ToString() ?? "unknown"));
                if (reportData.Eth1CurrentBlock.HasValue && reportData.Eth1HighestBlock.HasValue && reportData.Eth1HighestBlock.Value > 0)
                {
                    decimal progress = Math.Min(100m, Math.Round((decimal)reportData.Eth1CurrentBlock.Value / reportData.Eth1HighestBlock.Value * 100m, 2));
                    builder.Append("Progress: " + progress.ToString(CultureInfo.InvariantCulture) + "%");
                }
                else
                {
                    builder.Append("Progress: unknown");
                }

                return builder.ToString();
            }

            throw new InvalidOperationException("eth_syncing returned an unsupported result format.");
        }

        public void AddToReport(ReportData data)
        {
            if (data == null)
            {
                return;
            }

            try
            {
                PopulateEth1Status(data);
            }
            catch
            {
                data.Eth1SyncState = "unavailable";
                data.Eth1Syncing = null;
                data.Eth1CurrentBlock = null;
                data.Eth1HighestBlock = null;
            }
        }

        private void PopulateEth1Status(ReportData data)
        {
            const string syncingPayload = "{\"jsonrpc\":\"2.0\",\"method\":\"eth_syncing\",\"params\":[],\"id\":1}";
            string syncingResponse = WebUtils.PostJson("http://localhost:8545", syncingPayload);
            JObject syncingObject = JObject.Parse(syncingResponse);
            JToken syncingResult = syncingObject["result"];
            if (syncingResult == null)
            {
                throw new InvalidOperationException("eth_syncing did not return a result.");
            }

            if (syncingResult.Type == JTokenType.Boolean)
            {
                bool syncing = syncingResult.Value<bool>();
                data.Eth1Syncing = syncing;
                data.Eth1SyncState = syncing ? "syncing" : "synced";
                if (!syncing)
                {
                    const string blockPayload = "{\"jsonrpc\":\"2.0\",\"method\":\"eth_blockNumber\",\"params\":[],\"id\":2}";
                    string blockResponse = WebUtils.PostJson("http://localhost:8545", blockPayload);
                    JObject blockObject = JObject.Parse(blockResponse);
                    data.Eth1CurrentBlock = ParseHexToLong(blockObject["result"]?.Value<string>());
                    data.Eth1HighestBlock = null;
                }
                return;
            }

            if (syncingResult.Type == JTokenType.Object)
            {
                data.Eth1Syncing = true;
                data.Eth1SyncState = "syncing";
                data.Eth1CurrentBlock = ParseHexToLong(syncingResult["currentBlock"]?.Value<string>());
                data.Eth1HighestBlock = ParseHexToLong(syncingResult["highestBlock"]?.Value<string>());
                return;
            }

            throw new InvalidOperationException("eth_syncing returned an unsupported result format.");
        }

        private static long? ParseHexToLong(string hexValue)
        {
            if (String.IsNullOrWhiteSpace(hexValue))
            {
                return null;
            }

            string normalized = hexValue.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                ? hexValue.Substring(2)
                : hexValue;

            if (long.TryParse(normalized, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long value))
            {
                return value;
            }

            return null;
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
            Eth2OverwatchSettings.Default.AdditionalCommands_Eth1 = this.additionalCommands;

            Eth2OverwatchSettings.Default.Save();
            this.UpdateConfig();
        }
    }
}
