using System;
using Jupiter1.Network.Common.Constants;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Extensions;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Common.Services.ChannelService
{
    /*
     * Transmit:
     * +----------------------+
     * | OutgoingSequence     |
     * +----------------------+
     * | QPort                | - (present if message was sent from client to server).
     * +----------------------+
     * | Data ...             |
     * +----------------------+
     * 
     * TransmitNext:
     * +----------------------+
     * | OutgoingSequence &   |
     * | FRAGMENT_BIT         |
     * +----------------------+
     * | QPort                | - (present if message was sent from client to server).
     * +----------------------+
     * | UnsentFragmentStart  |
     * +----------------------+
     * | UnsentFragmentLength |
     * +----------------------+
     * | Data ...             |
     * +----------------------+
     */
    public abstract class BaseChannelService : IChannelService
    {
        public void Transmit(NetworkChannel channel, byte[] data, int length)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (length <= 0)
                throw new ArgumentException(nameof(length));

            var message = new Message { Data = new byte[CommonConstants.MaxPacketLength] };

            if (length > CommonConstants.MaxMessageLength)
            {
                // TODO: log too long packet.
            }

            channel.UnsentFragmentStart = 0;

            if (length >= CommonConstants.FragmentSize)
            {
                channel.HasUnsentFragments = true;
                channel.UnsentLength = length;
                Buffer.BlockCopy(data, 0, channel.UnsentBuffer, 0, length);

                // Only send the first fragment now.
                TransmitNext(channel);

                return;
            }

            // Write the packet header.
            message.WriteInt32(channel.OutgoingSequence);
            ++channel.OutgoingSequence;

            // Send the qport if we are a client.
            if (channel.NetworkSource == NetworkSource.Client)
                WriteClientData(message);

            message.WriteData(data, 0, length);

            // Send the datagram
            SendPacket(channel.NetworkSource, channel.RemoteAddress, message);
        }

        public void TransmitNext(NetworkChannel channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel));

            var message = new Message { Data = new byte[CommonConstants.MaxPacketLength] };

            message.WriteInt32(channel.OutgoingSequence & CommonConstants.FragmentBit);

            // Send the qport if we are a client.
            if (channel.NetworkSource == NetworkSource.Client)
                WriteClientData(message);

            // Copy the reliable message to the packet first.
            var fragmentLength = CommonConstants.FragmentSize;
            if ((channel.UnsentFragmentStart + fragmentLength) > channel.UnsentLength)
                fragmentLength = channel.UnsentLength - channel.UnsentFragmentStart;

            message.WriteInt16((short) channel.UnsentFragmentStart);
            message.WriteInt16((short) fragmentLength);
            message.WriteData(channel.UnsentBuffer, channel.UnsentFragmentStart, fragmentLength);

            // Send the datagram
            SendPacket(channel.NetworkSource, channel.RemoteAddress, message);

            channel.UnsentFragmentStart += fragmentLength;

            // This exit condition is a little tricky, because a packet that is exactly the fragment length still needs
            // to send a second packet of zero length so that the other side can tell there aren't more to follow.
            if (channel.UnsentFragmentStart == channel.UnsentLength && fragmentLength != CommonConstants.FragmentSize)
            {
                ++channel.OutgoingSequence;
                channel.HasUnsentFragments = false;
            }
        }

        public bool Process(NetworkChannel channel, Message message)
        {
            var sequence = message.ReadInt32();

            // Discard out of order or duplicated packets.
            if (sequence <= channel.IncomingSequence)
                return false;

            // Check for fragment information.
            var fragmented = false;
            if ((sequence & CommonConstants.FragmentBit) > 0)
            {
                sequence &= ~CommonConstants.FragmentBit;
                fragmented = true;
            }

            // Read the qport if we are a server.
            if (channel.NetworkSource == NetworkSource.Server)
            {
                var qport = message.ReadInt16();
            }

            // Read the fragment information.
            var fragmentStart = 0;
            var fragmentLength = 0;
            if (fragmented)
            {
                fragmentStart = message.ReadInt16();
                fragmentLength = message.ReadInt16();
            }

            // Dropped packets don't keep the message from being used.
            channel.Dropped = sequence - (channel.IncomingSequence + 1);
            if (channel.Dropped > 0)
            {
                // TODO: log dropped message.
            }

            //// If this is the final framgent of a reliable message, bump incoming_reliable_sequence.
            if (fragmented)
            {
                // TTimo
                // make sure we add the fragments in correct order either a packet was dropped, or we received this one
                // too soon we don't reconstruct the fragments. we will wait till this fragment gets to us again
                // (NOTE: we could probably try to rebuild by out of order chunks if needed)
                if (channel.FragmentSequence != sequence)
                {
                    channel.FragmentSequence = sequence;
                    channel.FragmentLength = 0;
                }

                // If we missed a fragment, dump the message.
                if (fragmentStart != channel.FragmentLength)
                {
                    // We can still keep the part that we have so far, so we don't need to clear channel.fragmentLength.
                    return false;
                }

                if (fragmentLength < 0 || (message.Length + fragmentLength > message.Length) ||
                    (channel.FragmentLength + fragmentLength > channel.FragmentBuffer.Length))
                    return false;

                Buffer.BlockCopy(channel.FragmentBuffer, channel.FragmentLength, message.Data, message.Length,
                    fragmentLength);

                channel.FragmentLength += fragmentLength;

                // If this wasn't the last fragment, don't process anything
                if (fragmentLength == CommonConstants.FragmentSize)
                    return false;

                if (channel.FragmentLength > message.Data.Length)
                    return false;

                // Copy the full message over the partial fragment

                // Make sure the sequence number is still there.
                message.RewriteInt32(0, sequence);

                Buffer.BlockCopy(channel.FragmentBuffer, 0, message.Data, 4, channel.FragmentLength);

                var data = message.Data;
                Array.Resize(ref data, channel.FragmentLength + 4);
                message.Data = data;

                message.Length = 4; // Past the sequence number.
                channel.FragmentLength = 0;
            }

            // The message can now be read from the current message pointer.

            channel.IncomingSequence = sequence;

            return true;
        }

        public abstract void SendPacket(NetworkSource networkSource, NetworkAddress to, Message message);
        public abstract void WriteClientData(Message message);
    }
}