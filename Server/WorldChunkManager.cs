using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

partial class World
{
    public class ChunkManager
    {
        public readonly ChunkGenerator chunkGenerator;

        Dictionary<Vector2DInt, Chunk> _chunks = new Dictionary<Vector2DInt, Chunk>();


        public ChunkManager()
        {
            chunkGenerator = new ChunkGenerator(Constants.TerrainGeneration.CHUNK_SIZE, LoadNoiseParameters());

            GenerateWorld();
        }


        public static Vector2DInt WorldPosToChunkPos(Vector2DInt inWorldPosition) =>
            inWorldPosition / Constants.TerrainGeneration.CHUNK_SIZE;

        public static Vector2DInt WorldPosToLocalTilePos(Vector2DInt inWorldPosition) =>
            inWorldPosition % Constants.TerrainGeneration.CHUNK_SIZE;


        public Chunk GetChunk(Vector2DInt inChunkPos) => _chunks[inChunkPos];

        public Tile GetTile(Vector2DInt inWorldPosition)
        {
            Vector2DInt chunkPosition = WorldPosToChunkPos(inWorldPosition);
            Vector2DInt tilePosition = WorldPosToLocalTilePos(inWorldPosition);

            return _chunks[chunkPosition].GetTile(tilePosition);
        }



        void GenerateWorld()
        {
            for (int y = 0; y < ServerConstants.TerrainGeneration.WORLD_SIZE; y++)
                for (int x = 0; x < ServerConstants.TerrainGeneration.WORLD_SIZE; x++)
                    _chunks.Add(new Vector2DInt(x, y), chunkGenerator.GenerateChunk(new Vector2DInt(x, y)));
        }

        Noise.Parameters[] LoadNoiseParameters()
        {
            // Load this from disk
            return new Noise.Parameters[]
            {
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
}
