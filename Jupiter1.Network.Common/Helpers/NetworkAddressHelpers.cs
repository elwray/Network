using System;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Common.Helpers
{
    public sealed class NetworkAddressHelpers
    {
        public static bool IsLanAddress(NetworkAddress networkAddress)
        {
            if (networkAddress == null)
                throw new ArgumentNullException(nameof(networkAddress));

            return false;
        }
    }
}