using Ethereum.Eth.v1alpha1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eth2Overwatch.Models
{
    public class ValidatorBo
    {
        private string publicKey;
        private string publicKeyBase64;
        private Google.Protobuf.ByteString publicKeyByteString;
        private ValidatorStatus state = ValidatorStatus.UnknownStatus;
        private ulong balance = 0;
        private ulong currentEffectiveBalance;
        private bool correctlyVoted;

        public ValidatorBo(string publicKey)
        {
            this.publicKey = publicKey;
            this.publicKeyByteString = Google.Protobuf.ByteString.CopyFrom(Utils.StringToByteArray(publicKey.Replace("0x", "")));
            this.publicKeyBase64 = this.publicKeyByteString.ToBase64();
        }

        public string PublicKey
        {
            get
            {
                return this.publicKey;
            }
        }
        public string PublicKeyBase64
        {
            get
            {
                return this.publicKeyBase64;
            }
        }

        public Google.Protobuf.ByteString PublicKeyByteString
        {
            get
            {
                return this.publicKeyByteString;
            }
        }

        public ValidatorStatus State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public ulong Balance
        {
            get
            {
                return this.balance;
            }
            set
            {
                this.balance = value;
            }
        }

        public ulong CurrentEffectiveBalance {

            get
            {
                return this.currentEffectiveBalance;
            }

            set
            {
                this.currentEffectiveBalance = value;
            }
        }

        public bool CorrectlyVoted
        {
            get
            {
                return this.correctlyVoted;
            }

            set
            {
                this.correctlyVoted = value;
            }
        }

        public ReportValidatorInfo ReportInfo
        {
            get
            {
                ReportValidatorInfo info = new ReportValidatorInfo();
                info.Balance = Utils.GWeiToEthRounded(this.balance, 3);
                info.CorrectlyVoted = this.correctlyVoted;
                info.CurrentEffectiveBalance = Utils.GWeiToEthRounded(this.currentEffectiveBalance, 3);
                info.StateText = this.state.ToString();
                return info;
            }
        }
    }
}
