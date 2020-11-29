using System;
using System.Linq;

namespace Eth2Overwatch.Models
{
    class Utils
    {
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string GWeiToEthLabel(ulong gwei)
        {
            return GWeiToEth(gwei).ToString();
        }

        public static decimal GWeiToEth(ulong gwei)
        {
            return (decimal)gwei / 1000000000;
        }

        public static decimal GWeiToEthRounded(ulong gwei, int decimals)
        {
            return Math.Round(GWeiToEth(gwei), decimals);
        }
    }
}
