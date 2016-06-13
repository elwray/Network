using System;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Server.Constants;

namespace Jupiter1.Network.Server.Services.LoopbackService
{
    internal sealed class LoopbackService : ILoopbackService
    {
        private readonly Loopback _client = new Loopback();
        private readonly Loopback _server = new Loopback();

        public void SendPacket(NetworkSource networkSource, byte[] data, int length)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (length <= 0)
                throw new ArgumentNullException(nameof(length));

            var loopback = networkSource == NetworkSource.Client ? _server : _client;

            var index = loopback.Send & (ServerConstants.MaxLoopbackMessages - 1);
            ++loopback.Send;

            Buffer.BlockCopy(data, 0, loopback.Messages[index].Data, 0, length);
            loopback.Messages[index].Length = length;
        }

        public bool GetPacket(NetworkSource networkSource, byte[] data, out int length)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            length = 0;

            var loopback = networkSource == NetworkSource.Client ? _client : _server;

            // TODO:
            //if (loop->send - loop->get > MAX_LOOPBACK)
            //    loop->get = loop->send - MAX_LOOPBACK;

            // If no more get messages available.
            if (loopback.Get >= loopback.Send)
                return false;

            var index = loopback.Get & (ServerConstants.MaxLoopbackMessages - 1);
            ++loopback.Get;

            Buffer.BlockCopy(loopback.Messages[index].Data, 0, data, 0, loopback.Messages[index].Length);
            length = loopback.Messages[index].Length;

            return true;
        }
    }
}