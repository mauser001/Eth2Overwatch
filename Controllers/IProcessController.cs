using System;

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

        void ImportKeys(string medallaKeyPath);

        void SetPassword(string pw);
        bool RequiresPassword();

        bool RequiresDataDir();

        bool RequiresWalletPath();
        bool SupportsEth1Connection();
        bool SupportsGoerliTestnet();
        string GetPrysmVersion();
        string GetLastVersion();

        bool CheckExecutable(string path = null);

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
        bool UseLocalEth1Node
        {
            get;
            set;
        }
        bool UseGoerliTestnet
        {
            get;
            set;
        }
    }
}

