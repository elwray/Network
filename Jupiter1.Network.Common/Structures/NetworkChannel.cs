using Jupiter1.Network.Common.Enums;

namespace Jupiter1.Network.Common.Structures
{
    public sealed class NetworkChannel
    {
        public NetworkSource NetworkSource { get; set; }

        // Between last packet and previous.
        public int Dropped { get; set; }

        public NetworkAddress RemoteAddress { get; set; }
        // Qport value to write when transmitting.
        public ushort QPort { get; set; }

        // Sequencing variables.
        public int IncomingSequence { get; set; }
        public int OutgoingSequence { get; set; }

        // Incoming fragment assembly buffer.
        public int FragmentSequence { get; set; }
        public int FragmentLength { get; set; }
        public byte[] FragmentBuffer { get; set; }

        // Outgoing fragment buffer we need to space out the sending of large fragmented messages.
        public bool HasUnsentFragments { get; set; }
        public int UnsentFragmentStart { get; set; }
        public int UnsentLength { get; set; }
        public byte[] UnsentBuffer { get; set; }
    }
}