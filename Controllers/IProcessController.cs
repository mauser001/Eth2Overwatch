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
        void Start();
        void Stop();

        void CheckState(Func<bool, string, string> resultFunction);

        void UpdateConfig();

        void SetPassword(string pw);

        bool RequiresPassword();

        bool CheckPassword();

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
    }
}

