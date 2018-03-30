using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public partial class World
{
    public static World instance;

    Data _data = new Data();

    static Dictionary<Vector2DInt, Chunk> _worldChunks = new Dictionary<Vector2DInt, Chunk>();
    public static Chunk GetChunk(Vector2DInt inChunkPos) => _worldChunks[inChunkPos];

    public readonly ChunkGenerator chunkGenerator;
    public readonly CreatureHolder creatureHolder;

    public World()
    {
        instance = this;

        chunkGenerator = new ChunkGenerator(Data.chunkSize, _data.parameters);
        creatureHolder = new CreatureHolder();

        // DEBUG:
        _worldChunks.Add(Vector2DInt.Zero, chunkGenerator.GenerateChunk(Vector2DInt.Zero));

        Creature newCreature = creatureHolder.SpawnCreature(Species.Type.Human, _worldChunks[Vector2DInt.Zero].data.GetTile(Vector2DInt.Zero));
        newCreature.movementComponent.MoveInDirection(new Vector2DInt(0, 1));

        // NetworkManager.OnClientConnected += (NetConnection inConnection) =>
        //     Network.Send(_data, EDataPacketTypes.WorldData, inConnection, NetDeliveryMethod.ReliableUnordered);
    }


    class Data
    {
        public const uint chunkSize = 64;

        public readonly Noise.Parameters[] parameters = new Noise.Parameters[]
        {
                // Height map
                new Noise.Parameters()
                {
                    scale       = 50,
                    octaves     = 7,
                    persistance = 1.01f,
                    lacunarity  = 1.01f,
                    seed        = 0
                }
        };
    }
}
