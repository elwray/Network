using System.Net;
using Jupiter1.Network.Common.Enums;

namespace Jupiter1.Network.Common.Services.SocketService
{
    public interface ISocketService
    {
        bool Initialize();
        void SendPacket(NetworkSource networkSource, IPEndPoint to, byte[] data, int length);
    }
}