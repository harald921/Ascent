using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class Program : MonoBehaviour
{
    NetworkManager _networkManager;

    void Awake()
    {
        _networkManager = new NetworkManager();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            new Command.Server.TestCommand(new Command.Server.TestCommand.Data()
            {
                direction = new Vector2DInt(0, 21)
            }).Send(_networkManager.client, _networkManager.client.ServerConnection);

        if (Input.GetKeyDown(KeyCode.D))
            new Command.Server.TestCommand(new Command.Server.TestCommand.Data()
            {
                direction = new Vector2DInt(123, -40)
            }).Send(_networkManager.client, _networkManager.client.ServerConnection);

    }
}