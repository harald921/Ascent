using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lidgren.Network;

public class CommandHandler
{
    static Dictionary<Command.Type, Command> _clientCommands = new Dictionary<Command.Type, Command>()
    {
        { Command.Type.SendPlayerData, new Command.Client.SendPlayerData() },
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
        public partial class SendPlayerData
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                dataAsPacket.UnpackFrom(inMsg);
                Program.playerCreature = data.creatureGuid;
                Debug.Log("Player creature recieved");
            }
        }
    }
}
