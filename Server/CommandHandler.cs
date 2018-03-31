using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

class CommandHandler
{
    static Dictionary<Command.Type, Command> _netCommands = new Dictionary<Command.Type, Command>()
    {
        { Command.Type.MovePlayer, new Command.Server.MovePlayer() },
        { Command.Type.TestCommand, new Command.Server.TestCommand() }
    };


    public static void ProcessCommand(NetIncomingMessage inMsg)
    {
        Command.Type commandType = (Command.Type)inMsg.ReadVariableInt32();
        _netCommands[commandType].RecieveAndExecute(inMsg);
    }
}

public partial class Command
{
    public partial class Server
    {
        public partial class MovePlayer : Command
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                // data.UnpackFrom(inMsg);
                
                World.instance.creatureHolder.GetCreature(data.creatureGuid).movementComponent.MoveInDirection(data.direction);
            }
        }

        public partial class TestCommand : Command
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                // data.UnpackFrom(inMsg);

                
                Console.WriteLine(data.testInt);
            }
        }
    }
}


/*
 *         /// <summary>
        /// Serializes an object into the out going message.
        /// </summary>
        protected void EncodeObject(NetOutgoingMessage outMsg, object obj) {
            if (outMsg == null || obj == null) {
                return;
            }

            byte[] objBytes = SerializeUtils.SerializeObject(obj);

            outMsg.Write(objBytes.Length);
            outMsg.Write(objBytes);
        }

        /// <summary>
        /// Decodes the next object stored in the in message.
        /// </summary>
        protected object DecodeObject(NetIncomingMessage inMsg) {
            if (inMsg == null) {
                return null;
            }

            int objLen = inMsg.ReadInt32();
            byte[] objBytes = inMsg.ReadBytes(objLen);

            return SerializeUtils.DeserializeObject(objBytes);
        }

        /// <summary>
        /// Convert an object that has been marked as [Serializable] into
        /// a byte array.
        /// </summary>
        public static byte[] SerializeObject(object obj) {
            if (obj == null) {
                return null;
            }

            BinaryFormatter binFormatter = new BinaryFormatter();

            using (MemoryStream memStream = new MemoryStream()) {
                binFormatter.Serialize(memStream, obj);
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Convert a byte array back into the original object it was.
        /// </summary>
        public static object DeserializeObject(byte[] bytes) {
            if (bytes == null) {
                return null;
            }

            BinaryFormatter binFormatter = new BinaryFormatter();

            using (MemoryStream memStream = new MemoryStream()) {
                memStream.Write(bytes, 0, bytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return binFormatter.Deserialize(memStream);
            }
        }
 */
