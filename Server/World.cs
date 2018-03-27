using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public partial class World
{
    Data _data = new Data();

    static Dictionary<Vector2DInt, Chunk> _worldChunks = new Dictionary<Vector2DInt, Chunk>();
    public static Chunk GetChunk(Vector2DInt inChunkPos) => _worldChunks[inChunkPos];

    public ChunkGenerator chunkGenerator { get; private set; }


    public World()
    {
        chunkGenerator = new ChunkGenerator(Data.chunkSize, _data.parameters);

        // DEBUG:
        _worldChunks.Add(Vector2DInt.Zero, chunkGenerator.GenerateChunk(Vector2DInt.Zero));

        Creature newCreature = new Creature(Species.Type.Human, _worldChunks[Vector2DInt.Zero].data.GetTile(Vector2DInt.Zero));

        newCreature.movementComponent.MoveInDirection(new Vector2DInt(0, 1));

        // NetworkManager.OnClientConnected += (NetConnection inConnection) =>
        //     Network.Send(_data, EDataPacketTypes.WorldData, inConnection, NetDeliveryMethod.ReliableUnordered);
    }


    class Data : IInPackable
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

        // Networking
        public int GetPacketSize()
        {
            int bitsNeeded = 0;
            bitsNeeded += NetUtility.BitsToHoldUInt(chunkSize);
            bitsNeeded += NetUtility.BitsToHoldUInt((uint)parameters.Length);

            for (int i = 0; i < parameters.Length; i++)
                bitsNeeded += parameters[i].GetPacketSize();

            return bitsNeeded;
        }

        public void PackInto(NetOutgoingMessage inMsg)
        {
            inMsg.WriteVariableUInt32(chunkSize);
            inMsg.WriteVariableUInt32((uint)parameters.Length);

            for (int i = 0; i < parameters.Length; i++)
                parameters[i].PackInto(inMsg);
        }
    }
}
