using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Constants;

namespace Jupiter1.Network.Server.Structures
{
    internal sealed class Snapshot
    {
        public int SentTime { get; set; }    // Time the message was transmitted.
        public int AckedTime { get; set; }   // Time the message was acked.
        public int MessageSize { get; set; } // Used to rate drop packets.

        public int EntitiesCount { get; set; }
        public int FirstEntity { get; set; }    // Into the circular sv_packet_entities[] the entities MUST be in
                                                // increasing state number order, otherwise the delta compression will
                                                // fail.
        public PlayerState PlayerState { get; set; }

        public int AreaBytes { get; set; }
        public byte[] AreaBits { get; set; } // Portalarea visibility bits. 
                                             // sizeof(byte) * ServerConstants.MaxMapAreaBytes = 256

        public Snapshot()
        {
            AreaBits = new byte[ServerConstants.MaxMapAreaBytes];
        }
    }
}