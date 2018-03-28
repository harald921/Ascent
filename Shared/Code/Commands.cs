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

    public static void SendCommand(NetClient inSourceClient, INetCommand inNetCommand)
    {
        NetOutgoingMessage newMessage = inSourceClient.CreateMessage();

        newMessage.WriteVariableInt32((int)inNetCommand.type);
        inNetCommand.data.PackInto(newMessage);
    }
}



public class NetCommand
{
    public struct MovePlayer : INetCommand
    {
        public Type type => Type.MovePlayer;

        Data _data;
        public IPackable data => _data;


        public MovePlayer(Data inData) =>
            _data = inData;


        public void RecieveAndExecute(NetIncomingMessage inMsg)
        {
            data.UnpackFrom(inMsg);

            World.instance.creatureHolder.GetCreature(_data.creatureGuid).movementComponent.MoveInDirection(_data.direction);
        }


        public struct Data : IPackable
        {
            public Guid        creatureGuid;
            public Vector2DInt direction;


            public int GetPacketSize() =>
                creatureGuid.GetPacketSize() + direction.GetPacketSize();

            public void PackInto(NetOutgoingMessage inMsg)
            {
                creatureGuid.PackInto(inMsg);
                direction.PackInto(inMsg);

            }

            public void UnpackFrom(NetIncomingMessage inMsg)
            {
                creatureGuid.UnpackFrom(inMsg);
                direction.UnpackFrom(inMsg);
            }
        }
    }

    public enum Type
    {
        MovePlayer
    }
}

public interface INetCommand
{
    NetCommand.Type type { get; }
    IPackable data { get; }
    void RecieveAndExecute(NetIncomingMessage inMsg);
}
