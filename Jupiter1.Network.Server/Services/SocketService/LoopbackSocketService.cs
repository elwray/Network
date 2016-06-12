using System;
using System.Net;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Server.Constants;

namespace Jupiter1.Network.Server.Services.SocketService
{
    internal class LoopbackSocketService : ISocketService
    {
        private readonly Loopback _client = new Loopback();
        private readonly Loopback _server = new Loopback();

        public bool Initialize()
        {
            return true;
        }

        public void SendPacket(NetworkSource networkSource, IPEndPoint to, byte[] data, int length)
        {
            var loopback = networkSource == NetworkSource.Client ? _server : _client;

            var index = loopback.Send & (ServerConstants.MaxLoopbackMessages - 1);
            ++loopback.Send;

            Buffer.BlockCopy(data, 0, loopback.Messages[index].Data, 0, length);
            loopback.Messages[index].Length = length;
        }

        public bool GetPacket(NetworkSource networkSource, IPEndPoint from, byte[] data, out int length)
        {
            var loopback = networkSource == NetworkSource.Client ? _client : _server;

            //if (loop->send - loop->get > MAX_LOOPBACK)
            //    loop->get = loop->send - MAX_LOOPBACK;

            //if (loop->get >= loop->send)
            //    return qfalse;

            var index = loopback.Get & (ServerConstants.MaxLoopbackMessages - 1);
            ++loopback.Get;

            Buffer.BlockCopy(loopback.Messages[index].Data, 0, data, 0, loopback.Messages[index].Length);
            length = loopback.Messages[index].Length;

            from = null;

            return true;
        }
    }
}