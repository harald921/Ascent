using System;
using System.Collections.Generic;
using Lidgren.Network;

class GameServer
{
    static NetServer _server;


    public GameServer(string inAppName)
    {
        var peerConfiguration = new NetPeerConfiguration(inAppName) {
            Port = Constants.Networking.PORT
        };

        _server = new NetServer(peerConfiguration);

        _server.Start();
    }


    public void ProcessMessages()
    {
        NetIncomingMessage message;
        while ((message = _server.ReadMessage()) != null)
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


    void OnClientStatusChanged(NetConnectionStatus inNewStatus, NetIncomingMessage inMsg)
    {
        Console.WriteLine(inMsg.SenderConnection + ": " + inNewStatus.ToString());
    }

    void ProcessDataMessage(NetIncomingMessage inMsg)
    {
        DataMessageType messageType = (DataMessageType)inMsg.ReadByte();

        if (messageType == DataMessageType.Command)
            NetCommandHandler.ProcessCommand(inMsg);
    }
}





