﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

class CommandHandler
{
    static Dictionary<Command.Type, Command> _serverCommands = new Dictionary<Command.Type, Command>()
    {
        { Command.Type.MovePlayer, new Command.Server.MovePlayer() },
        { Command.Type.UserLogin,  new Command.Server.UserLogin()  },
    };


    public static void ProcessCommand(NetIncomingMessage inMsg)
    {
        Command.Type commandType = (Command.Type)inMsg.ReadVariableInt32();
        _serverCommands[commandType].RecieveAndExecute(inMsg);
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
                dataAsPacket.UnpackFrom(inMsg);

                World.instance.creatureHolder.GetCreature(data.creatureGuid).movementComponent.MoveInDirection(data.direction);
            }
        }
    }

    public partial class Server
    {
        public partial class UserLogin : Command
        {
            public override void RecieveAndExecute(NetIncomingMessage inMsg)
            {
                dataAsPacket.UnpackFrom(inMsg);

                User newUser;

                if (data.registerElseLogin)
                    newUser = User.Register(inUsername:   data.providedUsername,
                                            inPassword:   data.providedUsername,
                                            inConnection: inMsg.SenderConnection);
                else
                    newUser = User.Login(inUsername:   data.providedUsername,
                                         inPassword:   data.providedUsername,
                                         inConnection: inMsg.SenderConnection);
                
                if (newUser != null)
                {
                    // Send back User data 
                }

                else
                {
                    // Send back error message
                }
            }
        }
    }
}
