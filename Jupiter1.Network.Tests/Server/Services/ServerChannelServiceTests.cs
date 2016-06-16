using System.Net;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Services.SocketService;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Jupiter1.Network.Tests.Server.Services
{
    [TestClass]
    public class ServerChannelServiceTests : BaseServerServiceTests
    {
        private IChannelService _channelService;

        [TestInitialize]
        public void BeforeEachMethod()
        {
            var socketService = new Mock<ISocketService>();
            socketService.Setup(x => x
                .SendPacket(It.IsAny<NetworkSource>(), It.IsAny<IPEndPoint>(), It.IsAny<byte[]>(), It.IsAny<int>()));
            RegisterSingleton(socketService.Object);

            _channelService = GetSingleton<IChannelService>();
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitServerDataTest()
        {
            var data = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, };
            var channel = new NetworkChannel
            {
                NetworkSource = NetworkSource.Server,
                RemoteAddress = new NetworkAddress
                {
                    AddressType = NetworkAddressType.Ip
                }
            };
            _channelService.Transmit(channel, data, 6);
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitClientDataTest()
        {
            var data = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            var channel = new NetworkChannel
            {
                NetworkSource = NetworkSource.Client,
                RemoteAddress = new NetworkAddress
                {
                    AddressType = NetworkAddressType.Ip
                }
            };
            _channelService.Transmit(channel, data, 6);
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitNextTest()
        {
            _channelService.TransmitNext(null);
        }

        [TestMethod, TestCategory("Unit")]
        public void Process()
        {
            _channelService.Process(null, null);
        }
    }
}