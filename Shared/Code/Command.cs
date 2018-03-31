using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Lidgren.Network;



public abstract partial class Command
{
    public abstract Type   type         { get; }
    public abstract Packet dataAsPacket { get; }

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
            public Data data { get; private set; }

            public override Type type => Type.MovePlayer;
            public override Packet dataAsPacket => data;

            public MovePlayer() { }
            public MovePlayer(Data inData)
            {
                data = inData;
            }


            [Serializable]
            public class Data : Packet
            {
                public Guid creatureGuid;
                public Vector2DInt direction;
            }
        }




        public partial class TestCommand : Command
        {
            public Data data { get; private set; }

            public override Type type => Type.TestCommand;
            public override Packet dataAsPacket => data;

            public TestCommand() { }
            public TestCommand(Data inData)
            {
                data = inData;
            }


            [Serializable]
            public class Data : Packet
            {
                public int testInt;
            }
        }
    }

    public enum Type
    {
        MovePlayer,
        TestCommand
    }
}
