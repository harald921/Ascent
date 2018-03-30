using System;
using System.Collections.Generic;
using Lidgren.Network;

public class NetworkManager
{
    public static NetworkManager instance;
    public readonly NetServer server;

    public static event Action<NetConnection> OnClientConnected;

    public NetworkManager(string inAppName)
    {
        var peerConfiguration = new NetPeerConfiguration(inAppName) {
            Port = Constants.Networking.PORT
        };

        server = new NetServer(peerConfiguration);

        server.Start();
    }


    public void Update()
    {
        ProcessMessages();
    }

    public void ProcessMessages()
    {
        NetIncomingMessage message;
        while ((message = server.ReadMessage()) != null)
        {
            switch (message.MessageType)
            {
                case NetIncomingMessageType.ErrorMessage:
                    Console.WriteLine(message.ReadString());
                    break;

                case NetIncomingMessageType.WarningMessage:
                    Console.WriteLine(message.ReadString());
                    break;

                case NetIncomingMessageType.StatusChanged:
                    OnClientStatusChanged((NetConnectionStatus)message.ReadByte(), message);
                    break;


                case NetIncomingMessageType.Data:
                    ProcessDataMessage(message);
                    break;
            }
        }
    }

    public void Send(NetOutgoingMessage inMsg, NetConnection inTargetConnection, NetDeliveryMethod inDeliveryMethod = NetDeliveryMethod.ReliableUnordered) =>
        server.SendMessage(inMsg, inTargetConnection, inDeliveryMethod);

    void OnClientStatusChanged(NetConnectionStatus inNewStatus, NetIncomingMessage inMsg)
    {
        Console.WriteLine(inMsg.SenderConnection + ": " + inNewStatus.ToString());
    }

    void ProcessDataMessage(NetIncomingMessage inMsg)
    {
        DataMessageType messageType = (DataMessageType)inMsg.ReadVariableUInt32();

        if (messageType == DataMessageType.Command)
            CommandHandler.ProcessCommand(inMsg);
    }
}





