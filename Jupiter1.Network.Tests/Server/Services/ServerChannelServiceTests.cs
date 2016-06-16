using System;
using System.Net;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Services.SocketService;
using Jupiter1.Network.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Jupiter1.Network.Tests.Server.Services
{
    [TestClass]
    public class ServerChannelServiceTests : BaseServerServiceTests
    {
        private IChannelService _channelService;
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

            _channelService = GetSingleton<IChannelService>();
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitClientToServerDataShouldWork()
        {
            var data = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            var channel = new NetworkChannel
            {
                NetworkSource = NetworkSource.Client,
                OutgoingSequence = 13,
                QPort = 4321,
                RemoteAddress = new NetworkAddress
                {
                    AddressType = NetworkAddressType.Ip
                }
            };
            _channelService.Transmit(channel, data, 6);

            var expected = new byte[1400];
            Array.Copy(new byte[] { 0x0D, 0x00, 0x00, 0x00, 0xE1, 0x10, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF }, expected, 12);
            CollectionAssert.AreEqual(expected, _sendPacketData);
            Assert.AreEqual(12, _sendPacketLength);
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitServerToClientDataShouldWork()
        {
            var data = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            var channel = new NetworkChannel
            {
                NetworkSource = NetworkSource.Server,
                OutgoingSequence = 13,
                RemoteAddress = new NetworkAddress
                {
                    AddressType = NetworkAddressType.Ip
                }
            };
            _channelService.Transmit(channel, data, 6);

            var expected = new byte[1400];
            Array.Copy(new byte[] { 0x0D, 0x00, 0x00, 0x00, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF }, expected, 10);
            CollectionAssert.AreEqual(expected, _sendPacketData);
            Assert.AreEqual(10, _sendPacketLength);
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitNextShouldWork()
        {
            _channelService.TransmitNext(null);
        }

        [TestMethod, TestCategory("Unit")]
        public void ProcessClientToServerDataShouldWork()
        {
            _channelService.Process(null, null);
        }

        [TestMethod, TestCategory("Unit")]
        public void ProcessServerToClientDataShouldWork()
        {
            _channelService.Process(null, null);
        }
    }
}