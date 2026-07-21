using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace Eth2Overwatch.OverwatchUtils
{

    public class TimeSyncService
    {
        private readonly Timer _syncTimer;
        public DateTime? LastSyncedTime { get; private set; }

        public TimeSyncService(double intervalInMilliseconds = 60000)
        {
            _syncTimer = new Timer(intervalInMilliseconds);
            _syncTimer.Elapsed += (s, e) => SyncTime();
            _syncTimer.AutoReset = true;
            _syncTimer.Start();
        }

        private void SyncTime()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("syncing");
                DateTime networkTime = GetNetworkTime();
                LastSyncedTime = networkTime;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[TimeSyncService] Sync failed: {ex.Message}");
            }
        }

        private DateTime GetNetworkTime()
        {
            const string ntpServer = "time.windows.com";
            var ntpData = new byte[48];
            ntpData[0] = 0x1B;

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;
            var ipEndPoint = new IPEndPoint(addresses[0], 123);

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);
                socket.Send(ntpData);
                socket.Receive(ntpData);
            }

            const byte serverReplyTime = 40;
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            var networkDateTime = new DateTime(1900, 1, 1).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }

        private static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000FF) << 24) +
                          ((x & 0x0000FF00) << 8) +
                          ((x & 0x00FF0000) >> 8) +
                          ((x & 0xFF000000) >> 24));
        }
    }
}
