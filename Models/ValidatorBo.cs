using Ethereum.Eth.v1alpha1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eth2Overwatch.Models
{
    class ValidatorBo
    {
        private string publicKey;
        private Google.Protobuf.ByteString publicKeyByteString;
        private ValidatorStatus state = ValidatorStatus.UnknownStatus;
        private float balance = 0;

        public ValidatorBo(string publicKey)
        {
            this.publicKey = publicKey;
            this.publicKeyByteString = Google.Protobuf.ByteString.CopyFrom(Utils.StringToByteArray(publicKey.Replace("0x", "")));
        }

        public string PublicKey
        {
            get
            {
                return this.publicKey;
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

        public float Balance
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
    }
}
