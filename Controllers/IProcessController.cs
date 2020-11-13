﻿using Eth2Overwatch.Models;
using System;
using System.Collections.Generic;

namespace LockMyEthTool.Views
{
    enum PROCESS_TYPES
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

        void DownloadExecutable(string path = null);

        void ImportKeys(string keyPath);

        void SetPassword(string pw);
        bool RequiresPassword();

        bool RequiresDataDir();

        bool RequiresWalletPath();
        bool SupportsEth1Connection();
        bool SupportsGoerliTestnet();
        string GetPrysmVersion();
        string GetLastVersion();

        bool CheckExecutable(string path = null);

        Dictionary<string, ValidatorBo> ValidatorsByKey
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
    }
}

