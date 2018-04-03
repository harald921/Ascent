using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class NetworkManager
{
    public static NetworkManager instance;

    public readonly NetClient client;

    public NetworkManager()
    {
        instance = this;

        NetPeerConfiguration config = new NetPeerConfiguration(Constants.APPLICATION_NAME);

        client = new NetClient(config);

        client.Start();

        client.Connect(host: Constants.Networking.HOST_ADRESS,
                        port: Constants.Networking.PORT);
    }

    public void ManualUpdate()
    {
        ProcessMessages();
    }

    public void ProcessMessages()
    {
        NetIncomingMessage message;
        while ((message = client.ReadMessage()) != null)
        {
            switch (message.MessageType)
            {
                case NetIncomingMessageType.ErrorMessage:
                    Debug.Log(message.ReadString());
                    break;

                case NetIncomingMessageType.WarningMessage:
                    Debug.Log(message.ReadString());
                    break;

                case NetIncomingMessageType.Data:
                    ProcessDataMessage(message);
                    break;
            }
        }
    }

    void ProcessDataMessage(NetIncomingMessage inMsg)
    {
        DataMessageType messageType = (DataMessageType)inMsg.ReadVariableUInt32();

        if (messageType == DataMessageType.Command)
            CommandHandler.ProcessCommand(inMsg);
    }

    public void Send(NetOutgoingMessage inMsg, NetConnection inTargetConnection, NetDeliveryMethod inDeliveryMethod = NetDeliveryMethod.ReliableUnordered) =>
        client.SendMessage(inMsg, inTargetConnection, inDeliveryMethod);

}