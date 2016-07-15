using System;
using Jupiter1.Network.Common.Structures;
using Jupiter1.Network.Server.Structures;

namespace Jupiter1.Network.Server.Services.ClientService
{
    internal sealed class ClientService : IClientService
    {
        #region IClientService
        public void DropClient(Client client, string reason)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            throw new NotImplementedException();
        }

        public void WriteDownloadToClient(Client client, Message message)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            throw new NotImplementedException();
        }

        ///*
        //================
        //SV_SendClientGameState
        //Sends the first message from the server to a connected client.
        //This will be sent on the initial connection and upon each new map load.
        //It will be resent if the client acknowledges a later message but has
        //the wrong gamestate.
        //================
        //*/
        //void SV_SendClientGameState(client_t* client)
        //{
        //    int start;
        //    entityState_t * base, nullstate;
        //    msg_t msg;
        //    byte msgBuffer[MAX_MSGLEN];

        //    Com_DPrintf("SV_SendClientGameState() for %s\n", client->name);
        //    Com_DPrintf("Going from CS_CONNECTED to CS_PRIMED for %s\n", client->name);
        //    client->state = CS_PRIMED;
        //    client->pureAuthentic = 0;
        //    client->gotCP = qfalse;

        //    // when we receive the first packet from the client, we will
        //    // notice that it is from a different serverid and that the
        //    // gamestate message was not just sent, forcing a retransmit
        //    client->gamestateMessageNum = client->netchan.outgoingSequence;

        //    MSG_Init(&msg, msgBuffer, sizeof(msgBuffer));

        //    // NOTE, MRE: all server->client messages now acknowledge
        //    // let the client know which reliable clientCommands we have received
        //    MSG_WriteLong(&msg, client->lastClientCommand);

        //    // send any server commands waiting to be sent first.
        //    // we have to do this cause we send the client->reliableSequence
        //    // with a gamestate and it sets the clc.serverCommandSequence at
        //    // the client side
        //    SV_UpdateServerCommandsToClient(client, &msg);

        //    // send the gamestate
        //    MSG_WriteByte(&msg, svc_gamestate);
        //    MSG_WriteLong(&msg, client->reliableSequence);

        //    // write the configstrings
        //    for (start = 0; start < MAX_CONFIGSTRINGS; start++)
        //    {
        //        if (sv.configstrings[start][0])
        //        {
        //            MSG_WriteByte(&msg, svc_configstring);
        //            MSG_WriteShort(&msg, start);
        //            MSG_WriteBigString(&msg, sv.configstrings[start]);
        //        }
        //    }

        //    // write the baselines
        //    Com_Memset(&nullstate, 0, sizeof(nullstate));
        //    for (start = 0; start < MAX_GENTITIES; start++)
        //    {
        //        base = &sv.svEntities[start].baseline;
        //        if (!base->number)
        //        {
        //            continue;
        //        }
        //        MSG_WriteByte(&msg, svc_baseline);
        //        MSG_WriteDeltaEntity(&msg, &nullstate, base, qtrue);
        //    }

        //    MSG_WriteByte(&msg, svc_EOF);

        //    MSG_WriteLong(&msg, client - svs.clients);

        //    // write the checksum feed
        //    MSG_WriteLong(&msg, sv.checksumFeed);

        //    // deliver this to the client
        //    SV_SendMessageToClient(&msg, client);
        //}
        #endregion
    }
}