using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Tests.Infrastructure;
using NUnit.Framework;

namespace Jupiter1.Network.Tests.Server.Services
{
    [TestFixture]
    [Category("Unit")]
    public class ServerChannelServiceTests : BaseServerServiceTests
    {
        private IChannelService _channelService;

        [SetUp]
        public void ForEachSetup()
        {
            _channelService = GetSingleton<IChannelService>();
        }

        [Test]
        public void TransmitTest()
        {
            _channelService.Transmit(null, null, 0);
        }

        [Test]
        public void TransmitNextTest()
        {
            _channelService.TransmitNext(null);
        }

        [Test]
        public void Process()
        {
            _channelService.Process(null, null);
        }
    }
}