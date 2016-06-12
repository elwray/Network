namespace Jupiter1.Network.Server.Structures
{
    internal sealed class Snapshot
    {
        public int SentTime { get; set; } // time the message was transmitted
        public int AckedTime { get; set; } // time the message was acked
        public int MessageSize { get; set; } // used to rate drop packets
    }
}