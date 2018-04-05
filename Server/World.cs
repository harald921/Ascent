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

    static Dictionary<Vector2DInt, Chunk> _chunks = new Dictionary<Vector2DInt, Chunk>();

    public readonly ChunkGenerator chunkGenerator;
    public readonly CreatureHolder creatureHolder;


    public World()
    {
        instance = this;

        chunkGenerator = new ChunkGenerator(Constants.TerrainGeneration.CHUNK_SIZE, _data.parameters);
        creatureHolder = new CreatureHolder();

        // DEBUG:
        for (int y = 0; y < ServerConstants.TerrainGeneration.WORLD_SIZE; y++)
            for (int x = 0; x < ServerConstants.TerrainGeneration.WORLD_SIZE; x++)
                _chunks.Add(new Vector2DInt(x, y), chunkGenerator.GenerateChunk(new Vector2DInt(x, y)));
    }


    public static Chunk GetChunk(Vector2DInt inChunkPos) => _chunks[inChunkPos];

    public static Vector2DInt WorldPosToChunkPos(Vector2DInt inWorldPosition) =>
        inWorldPosition / Constants.TerrainGeneration.CHUNK_SIZE;

    public static Vector2DInt WorldPosToLocalTilePos(Vector2DInt inWorldPosition) =>
        inWorldPosition % Constants.TerrainGeneration.CHUNK_SIZE;

    public static Tile GetTile(Vector2DInt inWorldPosition)
    {
        Vector2DInt chunkPosition = WorldPosToChunkPos(inWorldPosition);
        Vector2DInt tilePosition  = WorldPosToLocalTilePos(inWorldPosition);

        return _chunks[chunkPosition].data.GetTile(tilePosition);
    }


    class Data : IInPackable
    {
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

        // Networking
        public int GetPacketSize()
        {
            int bitsNeeded = 0;
            bitsNeeded += NetUtility.BitsToHoldUInt((uint)parameters.Length);

            for (int i = 0; i < parameters.Length; i++)
                bitsNeeded += parameters[i].GetPacketSize();

            return bitsNeeded;
        }

        public void PackInto(NetOutgoingMessage inMsg)
        {
            inMsg.WriteVariableUInt32((uint)parameters.Length);

            for (int i = 0; i < parameters.Length; i++)
                parameters[i].PackInto(inMsg);
        }
    }
}
