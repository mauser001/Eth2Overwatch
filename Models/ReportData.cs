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
    }
}
