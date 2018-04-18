using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class CommandHandler
{
    static Dictionary<Command.Type, Command> _clientCommands = new Dictionary<Command.Type, Command>()
    {
        { Command.Type.GiveCreatureOwnership, new Command.Client.GiveCreatureOwnership() },
        { Command.Type.SendVisibleChunks,     new Command.Client.SendVisibleChunks()     },
        { Command.Type.CreateCreature,        new Command.Client.CreateCreature()        },
        { Command.Type.MoveCreature,          new Command.Client.MoveCreature()          }
    };

    public static void ProcessCommand(NetIncomingMessage inMsg)
    {
        Command.Type commandType = (Command.Type)inMsg.ReadVariableInt32();
        _clientCommands[commandType].RecieveAndExecute(inMsg);
    }
}

public partial class Command
{
    public partial class Client
    {
        public partial class GiveCreatureOwnership
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                dataAsPacket.UnpackFrom(inMsg);

                Program.user.ownedCreatureID.Add(data.creatureGuid);
            }
        }

        public partial class SendVisibleChunks
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                dataAsPacket.UnpackFrom(inMsg);

                Program.user.UpdateVisibleChunks(data.creatureGuid, data.visibleChunkPositions);
            }
        }

        public partial class CreateCreature
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                dataAsPacket.UnpackFrom(inMsg);

                World.instance.creatureManager.SpawnCreature(data.creatureGuid, data.spawnWorldPosition);
            }
        }

        public partial class MoveCreature
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                dataAsPacket.UnpackFrom(inMsg);

                World.instance.creatureManager.GetCreature(data.creatureGuid).viewGO.transform.position += new Vector3(data.moveDirection.x, 0, data.moveDirection.y);
            }
        }
    }
}
