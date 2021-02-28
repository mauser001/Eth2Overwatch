using Eth2Overwatch.Models;
using System;
using System.Collections.Generic;

namespace LockMyEthTool.Views
{
    public enum PROCESS_TYPES
    {
        VALIDATOR = 0,
        BEACON_CHAIN = 1,
        ETH_1 = 2
    }
    public interface IProcessController
    {
        void Start(bool skipCheck = false, bool showCommandPrompt = false, bool dontStop = false);
        void Stop();

        void CheckState(Func<bool, string, string> resultFunction);

        string GetLogText();

        void UpdateConfig();

        void DownloadExecutable(string path = null, string version = null);

        bool RequiresPassword();

        bool RequiresDataDir();

        bool RequiresWalletPath();
        bool SupportsVersion();
        string GetPrysmVersion();
        string GetLastVersion();
        int GetInitialDelay();

        bool CheckExecutable(string path = null);
        bool CheckExecutablePath(string path = null);

        Dictionary<string, ValidatorBo> ValidatorsByKey
        {
            get;
        }

        PROCESS_TYPES ProcessType
        {
            get;
        }
        bool Autostart
        {
            get;
            set;
        }
        bool HideCommandPrompt
        {
            get;
            set;
        }
        string DataDir
        {
            get;
            set;
        }
        string ExecutablePath
        {
            get;
            set;
        }
        string KeyPath
        {
            get;
            set;
        }
        string WalletPath
        {
            get;
            set;
        }
        string AdditionalCommands
        {
            get;
            set;
        }
        string ReportPath
        {
            get;
            set;
        }
        string ReportKey
        {
            get;
            set;
        }
        string ReportLabel
        {
            get;
            set;
        }
        string CurrentVersion
        {
            get;
            set;
        }
        bool UseLatestVersion
        {
            get;
            set;
        }

        /**
         * Filter debug messages
         */
        bool ShowError
        {
            get;
            set;
        }
        bool ShowInfo
        {
            get;
            set;
        }
        bool ShowWarning
        {
            get;
            set;
        }
        bool NewVersionAvailable
        {
            get;
            set;
        }

        bool DownloadingExecutables
        {
            get;
            set;
        }
    }
}

