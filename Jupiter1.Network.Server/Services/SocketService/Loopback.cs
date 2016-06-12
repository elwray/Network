using Jupiter1.Network.Server.Constants;

namespace Jupiter1.Network.Server.Services.SocketService
{
    internal sealed class Loopback
    {
        public LoopbackMessage[] Messages { get; }

        public int Send { get; set; }

        public int Get { get; set; }

        public Loopback()
        {
            Messages = new LoopbackMessage[ServerConstants.MaxLoopbackMessages];
        }
    }
}