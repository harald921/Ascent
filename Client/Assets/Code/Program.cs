using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class Program : MonoBehaviour
{
    static public User user;

    NetworkManager _networkManager;

    void Awake()
    {
        _networkManager = new NetworkManager();

        user = new User();
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
                direction = moveDirection
            }).Send(_networkManager.client, _networkManager.client.ServerConnection);
        }
    }
}