using System;
using System.Net;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Server.Services.ServerConfiguration;

namespace Jupiter1.Network.Server.Services.SocketService
{
    internal class ServerSocketService : IServerSocketService
    {
        private readonly IServerConfiguration _configuration;

        public ServerSocketService(IServerConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public bool Initialize()
        {
            return true;
        }

        public void SendPacket(NetworkSource networkSource, IPEndPoint to, byte[] data, int length)
        {
        }

        public bool GetPacket(NetworkSource networkSource, IPEndPoint from, byte[] data, out int length)
        {
            length = 0;

            return true;
        }
    }
}