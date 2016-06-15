using System.Net;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Services.SocketService;
using Jupiter1.Network.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Jupiter1.Network.Tests.Server.Services
{
    [TestClass]
    public class ServerChannelServiceTests : BaseServerServiceTests
    {
        private IChannelService _channelService;
        private byte[] _data;
        private int _length;
        private IPEndPoint _endPoint = new IPEndPoint(59886677396, 44309); // 223.186.45.148:44309

        [TestInitialize]
        public void BeforeEachMethod()
        {
            _channelService = GetSingleton<IChannelService>();

            var socketService = new Mock<ISocketService>();
            RegisterSingleton(socketService.Object);
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitTest()
        {
            var data = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
            _channelService.Transmit(null, data, 6);
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