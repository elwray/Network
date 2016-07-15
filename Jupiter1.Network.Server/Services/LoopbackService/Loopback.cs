using Jupiter1.Network.Common.Constants;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Core.Extensions;
using Jupiter1.Network.Server.Constants;

namespace Jupiter1.Network.Server.Services.LoopbackService
{
    internal sealed class Loopback
    {
        public Message[] Messages { get; }

        public int Send { get; set; }
        public int Get { get; set; }

        public Loopback()
        {
            Messages = new Message[ServerConstants.MaxLoopbackMessages];
            Messages.Assign(() => new Message
            {
                Data = new byte[CommonConstants.MaxPacketLength]
            });
        }
    }
}