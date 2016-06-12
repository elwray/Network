using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Server.Services.SocketService
{
    internal sealed class LoopbackMessage : Message
    {
        public LoopbackMessage()
        {
            Data = new byte[Common.Constants.MaxPacketLength];
        }
    }
}