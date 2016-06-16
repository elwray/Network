using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Constants;
using Jupiter1.Network.Server.Enums;

namespace Jupiter1.Network.Server.Structures
{
    internal sealed class Client
    {
        public ClientState State { get; set; }
        public ClientType Type { get; set; }
        public int LastPacketTime { get; set; }
        public int TimeoutCount { get; set; }
        public int NextSnapshotTime { get; set; }
        public int Ping { get; set; }
        public int Rate { get; set; }
        public Snapshot[] Snapshots { get; }
        public NetworkChannel Channel { get; set; }

        public Client()
        {
            Snapshots = new Snapshot[ServerConstants.PacketsBackup];
        }
    }
}