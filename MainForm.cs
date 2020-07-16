using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace LockMyEthTool
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            Trace.AutoFlush = true;

            InitializeCustomComponents();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
