using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;
using System;

public class Program : MonoBehaviour
{
    NetworkManager _networkManager;


    void Awake()
    {
        _networkManager = new NetworkManager();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NetworkManager.instance.client.Connect(host: Constants.Networking.HOST_ADRESS,
                port: Constants.Networking.PORT);

        if (Input.GetKeyDown(KeyCode.E))
            new Command.Server.TestCommand(new Command.Server.TestCommand.Data()
            {
                testInt = 5
            }).Send(_networkManager.client, _networkManager.client.ServerConnection);

    }
}