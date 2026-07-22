using Eth2Overwatch.Models;
using Eth2Overwatch.OverwatchUtils;
using Ethereum.Eth.v1alpha1;
using Grpc.Net.Client;
using LockMyEthTool.Controllers;
using LockMyEthTool.Views;
using System;
using System.Collections.Generic;
using System.IO;
using static Ethereum.Eth.v1alpha1.Deposit.Types;

namespace Eth2Overwatch.Controllers
{
    class ValidatorController : PrysmController, IReportContributor
    {
        private string lastControllerStatus = "";

        public override PROCESS_TYPES ProcessType
        {
            get
            {
                return PROCESS_TYPES.VALIDATOR;
            }
        }

        protected override string ProcessIdentifier
        {
            get
            {
                return "validator";
            }
        }

        protected override void InitConfig()
        {

            if (this.dryMode && this.executablePath.Length > 0)
            {
                return;
            }

            this.autoStart = Eth2OverwatchSettings.Default.Autostart_Validator;
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
        }

        protected override void SetupProcessConfig()
        {
            var add = this.additionalCommands.Length > 0 ? " " + this.additionalCommands : "";
            var testNet = string.Empty != this.eth2TestNet ? " --" + this.eth2TestNet : "";

            this.fileName = Path.Combine(this.executablePath, this.GetExecutableFileName());
            this.directory = this.executablePath;
            this.commands = null;
            this.arguments = "--accept-terms-of-use --wallet-dir=\"" + this.walletPath + "\" --wallet-password-file=\"" + this.keyPath + "\"" + testNet + add;

        }

        public override string GetLastVersion()
        {
            return this.latestVersion;
        }

        public override bool RequiresDataDir()
        {
            return false;
        }
        public override bool RequiresPassword()
        {
            return true;
        }
        public override bool RequiresWalletPath()
        {
            return true;
        }

        public override bool SupportsVersion()
        {
            return true;
        }
        public override int GetInitialDelay()
        {
            return 2;
        }

        public override void CheckState(Func<bool, string, string> resultFunction)
        {
            if (this.downloadingExecutables)
            {
                resultFunction(false, "Downloading executables");
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
            try
            {
                if (this.validatorsByKey.Count == 0)
                {
                    string metricsText = WebUtils.FetchInfo("http://localhost:8081/metrics");
                    string[] lines = metricsText.Split("\n");
                    foreach (string line in lines)
                    {
                        if (line.IndexOf("validator_statuses{") == 0)
                        {
                            string publicKey = line.Split("\"")[3];
                            ValidatorBo bo = new ValidatorBo(publicKey);
                            if (!this.validatorsByKey.ContainsKey(bo.PublicKeyBase64))
                            {
                                this.validatorsByKey[bo.PublicKeyBase64] = bo;
                            }
                        }
                    }
                }

                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                using var channel = GrpcChannel.ForAddress("http://127.0.0.1:4000");
                var validatorClient = new BeaconNodeValidator.BeaconNodeValidatorClient(channel);
                var beaconClient = new BeaconChain.BeaconChainClient(channel);
                string result = "";
                ValidatorPerformanceRequest performanceRequest = new ValidatorPerformanceRequest();
                MultipleValidatorStatusRequest statusRequest = new MultipleValidatorStatusRequest();

                foreach (KeyValuePair<string, ValidatorBo> keyValue in this.validatorsByKey)
                {
#pragma warning disable CS0612
                    performanceRequest.PublicKeys.Add(keyValue.Value.PublicKeyByteString);
#pragma warning restore CS0612
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
                    if (keyValue.Value > 0)
                    {
                        result += "State " + keyValue.Key + ": " + keyValue.Value + "\n";
                    }
                }

                result += WebUtils.FetchInfo("http://localhost:8081/healthz");
                this.lastControllerStatus = result;
                resultFunction(true, result);
                return;
            }
            catch
            {
                this.validatorsByKey.Clear();
                if (this.ProcessIsRunning())
                {
                    this.lastControllerStatus = "Could not get healthz state, but process is still running.";
                    resultFunction(true, "Could not get healthz state, but process is still running.");
                }
                else
                {
                    this.lastControllerStatus = "Validator is not working properly";
                    resultFunction(false, "Validator is not working properly");
                }
            }
        }


        public override bool CheckWalletPath()
        {
            return !String.IsNullOrWhiteSpace(this.walletPath) && Directory.Exists(this.walletPath);
        }

        protected override void SaveConfig()
        {

            if (this.dryMode)
            {
                InitConfig();
                return;
            }
            Eth2OverwatchSettings.Default.Autostart_Validator = this.autoStart;
            Eth2OverwatchSettings.Default.ExecutablePath_Validator = this.executablePath;
            Eth2OverwatchSettings.Default.KeyPath_Validator = this.keyPath;
            Eth2OverwatchSettings.Default.AdditionalCommands_Validator = this.additionalCommands;
            Eth2OverwatchSettings.Default.WalletPath_Validator = this.walletPath;
            Eth2OverwatchSettings.Default.ReportPath = this.reportPath;
            Eth2OverwatchSettings.Default.ReportLabel = this.reportLabel;
            Eth2OverwatchSettings.Default.ReportKey = this.reportKey;
            Eth2OverwatchSettings.Default.UseLatestVersion_Validator = this.useLatestVersion;
            Eth2OverwatchSettings.Default.CurrentPrysmVersion_Validator = this.currentVersion;

            Eth2OverwatchSettings.Default.Save();
            this.UpdateConfig();
        }
        public void ImportKeys(string keyPath)
        {
            switch (this.ProcessType)
            {
                case PROCESS_TYPES.VALIDATOR:
                    var testNet = string.Empty != this.eth2TestNet ? " --" + this.eth2TestNet : "";
                    this.logOutput = false;
                    this.fileName = this.ExecutablePath + "\\" + this.GetExecutableFileName();
                    this.directory = this.ExecutablePath;
                    this.commands = null;
                    this.arguments = "accounts import --keys-dir=\"" + keyPath + "\" --wallet-dir=\"" + this.walletPath + "\"" + testNet;
                    this.Start(true, true, true);
                    break;
            }
        }

        public void AddToReport(ReportData data)
        {
            if (data == null)
            {
                return;
            }

            data.Version = this.currentVersion;
            data.LatestVersion = this.latestVersion;
            data.ControllerStatus = this.lastControllerStatus;
            data.Validators = new List<ReportValidatorInfo>();
            foreach (KeyValuePair<string, ValidatorBo> keyValue in this.ValidatorsByKey)
            {
                data.Validators.Add(keyValue.Value.ReportInfo);
            }
        }

    }
}
