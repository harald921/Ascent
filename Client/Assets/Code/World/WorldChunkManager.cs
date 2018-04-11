using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class World
{
	public class ChunkManager
    {
        Dictionary<Vector2DInt, Chunk> _chunks = new Dictionary<Vector2DInt, Chunk>();

        public readonly ChunkGenerator chunkGenerator;

        public static Vector2DInt WorldPosToChunkPos(Vector2DInt inWorldPosition) =>
            inWorldPosition / Constants.TerrainGeneration.CHUNK_SIZE;

        public static Vector2DInt WorldPosToLocalTilePos(Vector2DInt inWorldPosition) =>
            inWorldPosition % Constants.TerrainGeneration.CHUNK_SIZE;


        public Chunk GetChunk(Vector2DInt inChunkPos) => _chunks[inChunkPos];

        public Tile GetTile(Vector2DInt inWorldPosition)
        {
            Vector2DInt chunkPosition = WorldPosToChunkPos(inWorldPosition);
            Vector2DInt tilePosition = WorldPosToLocalTilePos(inWorldPosition);

            return _chunks[chunkPosition].data.GetTile(tilePosition);
        }
        
        // Holds all chunks that have been generated
        // Responsible for deleting non-visible chunks
    }
}