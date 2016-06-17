using System;
using System.Net;
using System.Net.Sockets;
using Jupiter1.Network.Client.Services.ClientConfiguration;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Services.SocketService;

namespace Jupiter1.Network.Client.Services.SocketService
{
    internal class ClientSocketService : ISocketService
    {
        private readonly IClientConfiguration _configuration;

        private Socket _socket;

        public ClientSocketService(IClientConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public bool Initialize()
        {
            if (_socket != null)
                throw new InvalidOperationException();

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            return true;
        }

        public void SendPacket(NetworkSource networkSource, IPEndPoint to, byte[] data, int length)
        {
        }
    }
}