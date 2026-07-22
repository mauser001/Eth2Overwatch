using System;
using System.Collections.Generic;
using System.Text;

namespace Eth2Overwatch.Models
{
    class ReportData
    {
        public long TS;
        public string Label;
        public string Version;
        public string LatestVersion;
        public List<ReportValidatorInfo> Validators;
        public string ControllerStatus;
        public string BeaconHealth;
        public bool? BeaconHealthy;
        public bool? Eth1Syncing;
        public long? Eth1CurrentBlock;
        public long? Eth1HighestBlock;
        public string Eth1SyncState;
    }
}
