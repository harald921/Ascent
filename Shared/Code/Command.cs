using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Lidgren.Network;

public abstract partial class Command
{
    public abstract Type      type         { get; }
    public abstract IPackable dataAsPacket { get; }
    public virtual void RecieveAndExecute(NetIncomingMessage inMsg) { }

    public void Send(NetPeer inSourcePeer, NetConnection inTargetConnection, NetDeliveryMethod inDeliveryMethod = NetDeliveryMethod.ReliableUnordered)
    {
        NetOutgoingMessage newMessage = inSourcePeer.CreateMessage();

        newMessage.WriteVariableInt32((int)DataMessageType.Command);
        newMessage.WriteVariableInt32((int)type);

        dataAsPacket.PackInto(newMessage);

        NetworkManager.instance.Send(newMessage, inTargetConnection, inDeliveryMethod);
    }


    public partial class Server
    {
        public partial class MovePlayer : Command
        {
            public readonly Data data = new Data();

            public override Type type => Type.MovePlayer;
            public override IPackable dataAsPacket => data;


            public MovePlayer() { }
            public MovePlayer(Data inData)
            {
                data = inData;
            }


            public class Data : IPackable
            {
                public Guid creatureGuid;
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
                    creatureGuid = creatureGuid.UnpackFrom(inMsg);
                    direction.UnpackFrom(inMsg);
                }
            }
        }
    }
    

    public enum Type
    {
        MovePlayer,
    }
}
