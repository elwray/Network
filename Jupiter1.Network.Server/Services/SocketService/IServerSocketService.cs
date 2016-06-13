using System.Net;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Services.SocketService;

namespace Jupiter1.Network.Server.Services.SocketService
{
    internal interface IServerSocketService : ISocketService
    {
        bool GetPacket(NetworkSource networkSource, IPEndPoint from, byte[] data, out int length);
    }
}