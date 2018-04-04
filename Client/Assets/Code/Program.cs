using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class Program : MonoBehaviour
{
    static public System.Guid playerCreature;

    NetworkManager _networkManager;

    void Awake()
    {
        _networkManager = new NetworkManager();
    }

    void Update()
    {
        _networkManager.ManualUpdate();

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
            Debug.Log(moveDirection.x + ", " + moveDirection.y);
            new Command.Server.MovePlayer(new Command.Server.MovePlayer.Data()
            {
                creatureGuid = playerCreature,
                direction = moveDirection
            }).Send(_networkManager.client, _networkManager.client.ServerConnection);
        }
    }
}