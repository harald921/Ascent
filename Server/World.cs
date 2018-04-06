using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public partial class World
{
    public static World instance;

    public readonly ChunkManager    chunkManager;
    public readonly CreatureManager creatureManager;


    public World()
    {
        instance = this;

        creatureManager = new CreatureManager();
        chunkManager = new ChunkManager();
    }   
}
