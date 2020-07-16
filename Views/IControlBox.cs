using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LockMyEthTool.Views
{
    interface IControlBox
    {
        void StartProcess();
        void StopProcess();

        void CheckState();
    }
}
