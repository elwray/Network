using System;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Services.LoopbackService;
using Jupiter1.Network.Server.Services.SocketService;

namespace Jupiter1.Network.Server.Services.ChannelService
{
    internal sealed class ChannelService : BaseChannelService
    {
        private readonly IServerSocketService _socketService;
        private readonly ILoopbackService _loopbackService;

        public ChannelService(IServerSocketService socketService, ILoopbackService loopbackService)
        {
            if (socketService == null)
                throw new ArgumentNullException(nameof(socketService));
            if (loopbackService == null)
                throw new ArgumentNullException(nameof(loopbackService));

            _socketService = socketService;
            _loopbackService = loopbackService;
        }

        #region BaseChannelService
        public override void SendPacket(NetworkSource networkSource, NetworkAddress to, Message message)
        {
            if (to == null)
                throw new ArgumentNullException(nameof(to));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (to.AddressType == NetworkAddressType.Loopback)
            {
                _loopbackService.SendPacket(networkSource, message.Data, message.Length);
                return;
            }

            if (to.AddressType == NetworkAddressType.Bot)
                return;

            if (to.AddressType == NetworkAddressType.Bad)
                return;

            _socketService.SendPacket(networkSource, to.EndPoint, message.Data, message.Length);
        }

        public override void WriteClientData(Message message)
        {
            // Only client should write own qport.
        }
        #endregion
    }
}