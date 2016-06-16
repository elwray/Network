using System.Net;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Server.Services.ChannelService;
using Jupiter1.Network.Server.Services.SocketService;
using Jupiter1.Network.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Jupiter1.Network.Tests.Server.Services
{
    [TestClass]
    public class ServerChannelServiceTests : BaseServerServiceTests
    {
        private IServerChannelService _serverChannelService;
        private byte[] _sendPacketData;
        private int _sendPacketLength;

        [TestInitialize]
        public void BeforeEachMethod()
        {
            var serverSocketService = new Mock<IServerSocketService>();
            serverSocketService.Setup(x => x
                .SendPacket(It.IsAny<NetworkSource>(), It.IsAny<IPEndPoint>(), It.IsAny<byte[]>(), It.IsAny<int>()))
                .Callback((NetworkSource networkSource, IPEndPoint to, byte[] data, int length) =>
                {
                    _sendPacketData = data;
                    _sendPacketLength = length;
                });
            RegisterSingleton(serverSocketService.Object);

            _serverChannelService = GetSingleton<IServerChannelService>();
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitShouldWork()
        {
            _serverChannelService.Transmit(null, null);
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitNextShouldWork()
        {
            _serverChannelService.TransmitNext(null);
        }

        [TestMethod, TestCategory("Unit")]
        public void ProcessShouldWork()
        {
            _serverChannelService.Process(null, null);
        }
    }
}