using System;
using System.Collections.Generic;
using System.Text;

namespace Eth2Overwatch.Models
{
    public class ReportValidatorInfo
    {
        public string StateText;
        public decimal Balance = 0;
        public decimal CurrentEffectiveBalance;
        public bool CorrectlyVoted;
        public bool CorrectlyVotedTarget;
        public bool CorrectlyVotedSource;
        public bool CorrectlyVotedHead;
    }
}
