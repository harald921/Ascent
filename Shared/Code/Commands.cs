using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Lidgren.Network;

public static class NetCommandHandler
{
    public static void ProcessCommand(NetIncomingMessage inMsg)
    {
        NetCommandType commandType = (NetCommandType)inMsg.ReadByte();
        switch (commandType)
        {
            case NetCommandType.MovePlayer:
                System.Console.WriteLine("Move player");
                break;
        }
    }
}

public static class NetCommand
{
    public static class MovePlayer
    {
        public static void Send(/*Player inPlayer, Vector2DInt inDirection*/)
        {
            // Create message object
            // Serialize ECommandType.MovePlayer
            // Serialize Player ID
            // Serialize Direction
    
            // Ask Lidgren to send message object 
        }
    
        public static void RecieveAndExecute(NetIncomingMessage inMsg)
        {
            // Deserialize Player ID
            // Deserialize Direction
    
            // Invoke methods
        }
    }
}

public enum NetCommandType
{
    MovePlayer
}