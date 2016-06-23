using System;
using Jupiter1.Network.Common.Enums;
using Jupiter1.Network.Common.Extensions;
using Jupiter1.Network.Common.Services.ChannelService;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ChannelService
{
    internal class ServerChannelService : IServerChannelService
    {
        private readonly IChannelService _channelService;

        public ServerChannelService(IChannelService channelService)
        {
            if (channelService == null)
                throw new ArgumentNullException(nameof(channelService));

            _channelService = channelService;
        }

        #region IServerChannelService
        // TTimo
        // https://zerowing.idsoftware.com/bugzilla/show_bug.cgi?id=462
        // If there are some unsent fragments(which may happen if the snapshots and the gamestate are fragmenting, and
        // collide on send for instance) then buffer them and make sure they get sent in correct order.
        public void Transmit(Client client, Message message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            message.WriteByte((byte) ServerToClientMessage.EOF);

            if (client.Channel.HasUnsentFragments)
            {
                //    netchan_buffer_t* netbuf;
                //    Com_DPrintf("#462 SV_Netchan_Transmit: unsent fragments, stacked\n");
                //    netbuf = (netchan_buffer_t*) Z_Malloc(sizeof(netchan_buffer_t));
                //    // store the msg, we can't store it encoded, as the encoding depends on stuff we still have to finish sending
                //    MSG_Copy(&netbuf->msg, netbuf->msgBuffer, sizeof(netbuf->msgBuffer), msg);
                //    netbuf->next = NULL;
                //    // insert it in the queue, the message will be encoded and sent later
                //    *client->netchan_end_queue = netbuf;
                //    client->netchan_end_queue = &(*client->netchan_end_queue)->next;
                //    // emit the next fragment of the current message for now
                //    Netchan_TransmitNextFragment(&client->netchan);
            }
            else
            {
                Encode(client, message);
                _channelService.Transmit(client.Channel, message.Data, message.Length);
            }
        }

        public void TransmitNext(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _channelService.TransmitNext(client.Channel);
            if (!client.Channel.HasUnsentFragments)
            {
                //    // make sure the netchan queue has been properly initialized (you never know)
                //    if (!client->netchan_end_queue)
                //    {
                //        Com_Error(ERR_DROP, "netchan queue is not properly initialized in SV_Netchan_TransmitNextFragment\n");
                //    }
                //    // the last fragment was transmitted, check wether we have queued messages
                //    if (client->netchan_start_queue)
                //    {
                //        netchan_buffer_t* netbuf;
                //        Com_DPrintf("#462 Netchan_TransmitNextFragment: popping a queued message for transmit\n");
                //        netbuf = client->netchan_start_queue;
                //        SV_Netchan_Encode(client, &netbuf->msg);
                //        Netchan_Transmit(&client->netchan, netbuf->msg.cursize, netbuf->msg.data);
                //        // pop from queue
                //        client->netchan_start_queue = netbuf->next;
                //        if (!client->netchan_start_queue)
                //        {
                //            Com_DPrintf("#462 Netchan_TransmitNextFragment: emptied queue\n");
                //            client->netchan_end_queue = &client->netchan_start_queue;
                //        }
                //        else
                //            Com_DPrintf("#462 Netchan_TransmitNextFragment: remaining queued message\n");
                //        Z_Free(netbuf);
                //    }
            }
        }

        public bool Process(Client client, Message message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var process = _channelService.Process(client.Channel, message);
            if (!process)
                return false;

            Decode(client, message);

            return true;
        }
        #endregion

        // First four bytes of the data are always:
        // long reliableAcknowledge;
        private void Encode(Client client, Message message)
        {
            //long reliableAcknowledge, i, index;
            //byte key, *string;
            //int srdc, sbit, soob;

            //if (msg->cursize < SV_ENCODE_START)
            //{
            //    return;
            //}

            //srdc = msg->readcount;
            //sbit = msg->bit;
            //soob = msg->oob;

            //msg->bit = 0;
            //msg->readcount = 0;
            //msg->oob = 0;

            //reliableAcknowledge = MSG_ReadLong(msg);

            //msg->oob = soob;
            //msg->bit = sbit;
            //msg->readcount = srdc;

            //string = (byte*) client->lastClientCommandString;
            //index = 0;
            //// xor the client challenge with the netchan sequence number
            //key = client->challenge ^ client->netchan.outgoingSequence;
            //for (i = SV_ENCODE_START; i < msg->cursize; i++)
            //{
            //    // modify the key with the last received and with this message acknowledged client command
            //    if (!string[index])
            //        index = 0;
            //    if (string[index] > 127 || string[index] == '%')
            //    {
            //        key ^= '.' << (i & 1);
            //    }
            //    else
            //    {
            //        key ^= string[index] << (i & 1);
            //    }
            //    index++;
            //    // encode the data with this key
            //    *(msg->data + i) = *(msg->data + i) ^ key;
            //}
        }

        // First 12 bytes of the data are always:
        // long serverId;
        // long messageAcknowledge;
        // long reliableAcknowledge;
        private void Decode(Client client, Message message)
        {
            //int serverId, messageAcknowledge, reliableAcknowledge;
            //int i, index, srdc, sbit, soob;
            //byte key, *string;

            //srdc = msg->readcount;
            //sbit = msg->bit;
            //soob = msg->oob;

            //msg->oob = 0;

            //serverId = MSG_ReadLong(msg);
            //messageAcknowledge = MSG_ReadLong(msg);
            //reliableAcknowledge = MSG_ReadLong(msg);

            //msg->oob = soob;
            //msg->bit = sbit;
            //msg->readcount = srdc;

            //string = (byte*) client->reliableCommands[reliableAcknowledge & (MAX_RELIABLE_COMMANDS - 1)];
            //index = 0;
            ////
            //key = client->challenge ^ serverId ^ messageAcknowledge;
            //for (i = msg->readcount + SV_DECODE_START; i < msg->cursize; i++)
            //{
            //    // modify the key with the last sent and acknowledged server command
            //    if (!string[index])
            //        index = 0;
            //    if (string[index] > 127 || string[index] == '%')
            //    {
            //        key ^= '.' << (i & 1);
            //    }
            //    else
            //    {
            //        key ^= string[index] << (i & 1);
            //    }
            //    index++;
            //    // decode the data with this key
            //    *(msg->data + i) = *(msg->data + i) ^ key;
            //}
        }
    }
}