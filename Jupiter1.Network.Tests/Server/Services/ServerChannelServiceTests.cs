using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jupiter1.Network.Tests.Server.Services
{
    [TestClass]
    public class ServerChannelServiceTests : BaseServerServiceTests
    {
        private IChannelService _channelService;

        [TestInitialize]
        public void BeforeEachMethod()
        {
            _channelService = GetSingleton<IChannelService>();
        }

        [TestMethod, TestCategory("Unit")]
        public void TransmitTest()
        {
            _channelService.Transmit(null, null, 0);
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