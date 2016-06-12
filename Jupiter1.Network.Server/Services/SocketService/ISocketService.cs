using System.Net;
using Jupiter1.Network.Common.Enums;

namespace Jupiter1.Network.Server.Services.SocketService
{
    internal interface ISocketService
    {
        bool Initialize();
        void SendPacket(NetworkSource networkSource, IPEndPoint to, byte[] data, int length);
        bool GetPacket(NetworkSource networkSource, IPEndPoint from, byte[] data, out int length);
    }
}