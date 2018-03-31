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



