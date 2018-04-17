using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class Program : MonoBehaviour
{
    static public User user = new User();

    NetworkManager _networkManager;

    void Awake()
    {
        _networkManager = new NetworkManager();
    }

    void Update()
    {
        _networkManager.ManualUpdate();

        DebugCreature1Movement();
        DebugCreature2Movement();
    }

    void DebugCreature1Movement()
    {
        Vector2DInt moveDirection = Vector2DInt.Zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDirection.y += 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDirection.y -= 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDirection.x -= 1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDirection.x += 1;

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            new Command.Server.MovePlayer(new Command.Server.MovePlayer.Data()
            {
                direction = moveDirection,
                creatureGuid = user.ownedCreatureID[0],
            }).Send(_networkManager.client, _networkManager.client.ServerConnection);
        }
    }

    void DebugCreature2Movement()
    {
        Vector2DInt moveDirection = Vector2DInt.Zero;

        if (Input.GetKeyDown(KeyCode.W))
            moveDirection.y += 1;
        if (Input.GetKeyDown(KeyCode.S))
            moveDirection.y -= 1;
        if (Input.GetKeyDown(KeyCode.A))
            moveDirection.x -= 1;
        if (Input.GetKeyDown(KeyCode.D))
            moveDirection.x += 1;

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            new Command.Server.MovePlayer(new Command.Server.MovePlayer.Data()
            {
                direction = moveDirection,
                creatureGuid = user.ownedCreatureID[1],
            }).Send(_networkManager.client, _networkManager.client.ServerConnection);
        }
    }
}