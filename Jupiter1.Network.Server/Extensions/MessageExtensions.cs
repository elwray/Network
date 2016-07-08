using System;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Server.Extensions
{
    internal static class MessageExtensions
    {
        public static void WriteDeltaPlayerState(this Message message, object from, object to)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (to == null)
                throw new ArgumentNullException(nameof(to));

            // TODO: implement this.
            throw new NotImplementedException();
        }
    }
}