using System.Net;
using Jupiter1.Network.Common.Enums;

namespace Jupiter1.Network.Common.Structures
{
    public sealed class NetworkAddress
    {
        public IPEndPoint EndPoint { get; set; }
        public NetworkAddressType AddressType { get; set; }
    }
}