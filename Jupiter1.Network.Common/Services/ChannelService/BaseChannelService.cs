using System;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Extensions;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Common.Services.ChannelService
{
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

            if (length > CommonConstants.MaxPacketLength)
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
                message.WriteUInt16(channel.QPort); // TODO: message.WriteUInt16(qport->integer)

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
                message.WriteUInt16(channel.QPort); // TODO: message.WriteUInt16(qport->integer)

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
            //int sequence;
            //int qport;
            //int fragmentStart, fragmentLength;
            //qboolean fragmented;

            //// XOR unscramble all data in the packet after the header
            ////	Netchan_UnScramblePacket( msg );

            //// get sequence numbers		
            //MSG_BeginReadingOOB(msg);
            //sequence = MSG_ReadLong(msg);

            ////
            //// discard out of order or duplicated packets
            ////
            //if (sequence <= chan->incomingSequence)
            //{
            //    if (showdrop->integer || showpackets->integer)
            //    {
            //        Com_Printf("%s:Out of order packet %i at %i\n"
            //            , NET_AdrToString(chan->remoteAddress)
            //            , sequence
            //            , chan->incomingSequence);
            //    }
            //    return qfalse;
            //}

            //// check for fragment information
            //if (sequence & FRAGMENT_BIT)
            //{
            //    sequence &= ~FRAGMENT_BIT;
            //    fragmented = qtrue;
            //}
            //else {
            //    fragmented = qfalse;
            //}

            //// read the qport if we are a server
            //if (chan->sock == NS_SERVER)
            //{
            //    qport = MSG_ReadShort(msg);
            //}

            //// read the fragment information
            //if (fragmented)
            //{
            //    fragmentStart = MSG_ReadShort(msg);
            //    fragmentLength = MSG_ReadShort(msg);
            //}
            //else {
            //    fragmentStart = 0;      // stop warning message
            //    fragmentLength = 0;
            //}

            //if (showpackets->integer)
            //{
            //    if (fragmented)
            //    {
            //        Com_Printf("%s recv %4i : s=%i fragment=%i,%i\n"
            //            , netsrcString[chan->sock]
            //            , msg->cursize
            //            , sequence
            //            , fragmentStart, fragmentLength);
            //    }
            //    else {
            //        Com_Printf("%s recv %4i : s=%i\n"
            //            , netsrcString[chan->sock]
            //            , msg->cursize
            //            , sequence);
            //    }
            //}

            ////
            //// dropped packets don't keep the message from being used
            ////
            //chan->dropped = sequence - (chan->incomingSequence + 1);
            //if (chan->dropped > 0)
            //{
            //    if (showdrop->integer || showpackets->integer)
            //    {
            //        Com_Printf("%s:Dropped %i packets at %i\n"
            //        , NET_AdrToString(chan->remoteAddress)
            //        , chan->dropped
            //        , sequence);
            //    }
            //}


            ////
            //// if this is the final framgent of a reliable message,
            //// bump incoming_reliable_sequence 
            ////
            //if (fragmented)
            //{
            //    // TTimo
            //    // make sure we add the fragments in correct order
            //    // either a packet was dropped, or we received this one too soon
            //    // we don't reconstruct the fragments. we will wait till this fragment gets to us again
            //    // (NOTE: we could probably try to rebuild by out of order chunks if needed)
            //    if (sequence != chan->fragmentSequence)
            //    {
            //        chan->fragmentSequence = sequence;
            //        chan->fragmentLength = 0;
            //    }

            //    // if we missed a fragment, dump the message
            //    if (fragmentStart != chan->fragmentLength)
            //    {
            //        if (showdrop->integer || showpackets->integer)
            //        {
            //            Com_Printf("%s:Dropped a message fragment\n"
            //            , NET_AdrToString(chan->remoteAddress)
            //            , sequence);
            //        }
            //        // we can still keep the part that we have so far,
            //        // so we don't need to clear chan->fragmentLength
            //        return qfalse;
            //    }

            //    // copy the fragment to the fragment buffer
            //    if (fragmentLength < 0 || msg->readcount + fragmentLength > msg->cursize ||
            //        chan->fragmentLength + fragmentLength > sizeof(chan->fragmentBuffer) ) {
            //        if (showdrop->integer || showpackets->integer)
            //        {
            //            Com_Printf("%s:illegal fragment length\n"
            //            , NET_AdrToString(chan->remoteAddress));
            //        }
            //        return qfalse;
            //    }

            //    Com_Memcpy(chan->fragmentBuffer + chan->fragmentLength,
            //        msg->data + msg->readcount, fragmentLength);

            //    chan->fragmentLength += fragmentLength;

            //    // if this wasn't the last fragment, don't process anything
            //    if (fragmentLength == FRAGMENT_SIZE)
            //    {
            //        return qfalse;
            //    }

            //    if (chan->fragmentLength > msg->maxsize)
            //    {
            //        Com_Printf("%s:fragmentLength %i > msg->maxsize\n"
            //            , NET_AdrToString(chan->remoteAddress),
            //            chan->fragmentLength);
            //        return qfalse;
            //    }

            //    // copy the full message over the partial fragment

            //    // make sure the sequence number is still there
            //    *(int*) msg->data = LittleLong(sequence);

            //    Com_Memcpy(msg->data + 4, chan->fragmentBuffer, chan->fragmentLength);
            //    msg->cursize = chan->fragmentLength + 4;
            //    chan->fragmentLength = 0;
            //    msg->readcount = 4; // past the sequence number
            //    msg->bit = 32;  // past the sequence number

            //    // TTimo
            //    // clients were not acking fragmented messages
            //    chan->incomingSequence = sequence;

            //    return qtrue;
            //}

            ////
            //// the message can now be read from the current message pointer
            ////
            //chan->incomingSequence = sequence;

            //return qtrue;

            return false;
        }

        public abstract void SendPacket(NetworkSource networkSource, NetworkAddress to, Message message);
    }
}