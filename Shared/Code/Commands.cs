using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Lidgren.Network;

public static class NetCommandHandler
{
    static Dictionary<NetCommand.Type, INetCommand> _netCommands = new Dictionary<NetCommand.Type, INetCommand>()
    {
        { NetCommand.Type.MovePlayer, new NetCommand.MovePlayer() },
    };

    public static void ProcessCommand(NetIncomingMessage inMsg)
    {
        NetCommand.Type commandType = (NetCommand.Type)inMsg.ReadByte();
        _netCommands[commandType].RecieveAndExecute(inMsg);
    }
}

public class NetCommand
{
    public class MovePlayer : INetCommand
    {
        public static void Send(NetClient inSourceClient, Guid inCreatureGuid, Vector2DInt inDirection)
        {
            NetOutgoingMessage newMessage = inSourceClient.CreateMessage();

            newMessage.WriteVariableUInt32((int)Type.MovePlayer);
            
            inDirection.PackInto(newMessage);

            inSourceClient.SendMessage(newMessage, NetDeliveryMethod.ReliableUnordered);
        }
    
        public void RecieveAndExecute(NetIncomingMessage inMsg)
        {
            // Deserialize Player ID
            // Deserialize Direction
    
            // Invoke methods
        }
    }
    
    public class RequestWorldData : INetCommand
    {
        public static void Send()
        {
    
        }
    
        public void RecieveAndExecute(NetIncomingMessage inMsg)
        {
    
        }
    
    }

    public enum Type
    {
        MovePlayer
    }
}

public interface INetCommand
{
    void RecieveAndExecute(NetIncomingMessage inMsg);
}
