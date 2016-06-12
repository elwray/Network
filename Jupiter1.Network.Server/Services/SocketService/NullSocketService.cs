using System.Net;
using Jupiter1.Network.Common.Enums;

namespace Jupiter1.Network.Server.Services.SocketService
{
    internal sealed class NullSocketService : ISocketService
    {
        public bool Initialize()
        {
            return true;
        }

        public void SendPacket(NetworkSource networkSource, IPEndPoint to, byte[] data, int length)
        {
        }

        public bool GetPacket(NetworkSource networkSource, IPEndPoint @from, byte[] data, out int length)
        {
            length = 0;

            return true;
        }
    }
}