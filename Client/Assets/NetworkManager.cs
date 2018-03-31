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

    public void Send(NetOutgoingMessage inMsg, NetConnection inTargetConnection, NetDeliveryMethod inDeliveryMethod = NetDeliveryMethod.ReliableUnordered) =>
        client.SendMessage(inMsg, inTargetConnection, inDeliveryMethod);

}