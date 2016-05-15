using System.Net;
using System.Net.Sockets;

namespace Jupiter1.Network.Common
{
    /*
    typedef struct {
        netsrc_t	sock;

        int			dropped;			// between last packet and previous

        netadr_t	remoteAddress;
        int			qport;				// qport value to write when transmitting

        // sequencing variables
        int			incomingSequence;
        int			outgoingSequence;

        // incoming fragment assembly buffer
        int			fragmentSequence;
        int			fragmentLength;	
        byte		fragmentBuffer[MAX_MSGLEN];

        // outgoing fragment buffer
        // we need to space out the sending of large fragmented messages
        qboolean	unsentFragments;
        int			unsentFragmentStart;
        int			unsentLength;
        byte		unsentBuffer[MAX_MSGLEN];
    } netchan_t;
    */

    public sealed class NetworkChannelInfo
    {
        public Socket Socket { get; set; }

        public int Dropped { get; set; }

        public IPEndPoint RemoteAddress { get; set; }
        public int QPort { get; set; }

        public int IncomingSequence { get; set; }
        public int OutgoingSequence { get; set; }

        public int FragmentSequence { get; set; }
        public int FragmentLength { get; set; }
        public byte[] FragmentBuffer { get; set; } // [MAX_MSGLEN]

        public bool UnsentFragments { get; set; }
        public int UnsentFragmentStart { get; set; }
        public int UnsentLength { get; set; }
        public byte[] UnsentBuffer { get; set; } // [MAX_MSGLEN]
    }
}