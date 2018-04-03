using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Lidgren.Network;
using System.Text;

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

        public partial class UserLogin : Command
        {
            public readonly Data data = new Data();

            public override Type type => Type.UserLogin;
            public override IPackable dataAsPacket => data;


            public UserLogin() { }
            public UserLogin(Data inData)
            {
                data = inData;
            }


            public class Data : IPackable
            {
                public bool   registerElseLogin;
                public string providedUsername;
                public string providedPassword;


                public int GetPacketSize()
                {
                    throw new NotImplementedException("Not implemented for packets including strings");
                }

                public void PackInto(NetOutgoingMessage outMsg)
                {
                    outMsg.Write(registerElseLogin);
                    outMsg.Write(providedUsername);
                    outMsg.Write(providedPassword);
                }

                public void UnpackFrom(NetIncomingMessage inMsg)
                {
                    registerElseLogin = inMsg.ReadBoolean();
                    providedUsername  = inMsg.ReadString();
                    providedPassword  = inMsg.ReadString();
                }
            }
        }
    }
    
    public partial class Client
    {
        public partial class SendPlayerData : Command
        {
            public readonly Data data = new Data();

            public override Type type => Type.SendPlayerData;
            public override IPackable dataAsPacket => data;


            public SendPlayerData() { }
            public SendPlayerData(Data inData)
            {
                data = inData;
            }


            public class Data : IPackable
            {
                public Guid creatureGuid;

                public int GetPacketSize() =>
                    creatureGuid.GetPacketSize();

                public void PackInto(NetOutgoingMessage inMsg) =>
                    creatureGuid.PackInto(inMsg);

                public void UnpackFrom(NetIncomingMessage inMsg) =>
                    creatureGuid = creatureGuid.UnpackFrom(inMsg);
            }
        }
    }

    public enum Type
    {
        // ClientToServer
        MovePlayer,
        UserLogin,

        // ServerToClient
        SendPlayerData
    }
}
