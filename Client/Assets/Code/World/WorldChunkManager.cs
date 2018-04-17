using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class World
{
	public class ChunkManager
    {
        public readonly ChunkGenerator chunkGenerator;

        Dictionary<Vector2DInt, Chunk> _chunks           = new Dictionary<Vector2DInt, Chunk>();
        List<Vector2DInt>              _chunksToDelete   = new List<Vector2DInt>();
        List<Vector2DInt>              _chunksToGenerate = new List<Vector2DInt>();



        public ChunkManager()
        {
            chunkGenerator = new ChunkGenerator(new Noise.Parameters[]
            {
                new Noise.Parameters()
                {
                    scale       = 50,
                    octaves     = 7,
                    persistance = 1.01f,
                    lacunarity  = 1.01f,
                    seed        = 0
                }
            });

            Program.user.OnVisibleChunksChange += () =>
            {
                Vector2DInt[] visibleChunksPositions = Program.user.GetAllVisibleChunkPositions();

                _chunksToDelete.Clear();
                _chunksToDelete.AddRange(_chunks.Keys);

                // Queue up coords to generate chunks on
                for (int i = 0; i < visibleChunksPositions.Length; i++)
                {
                    // If chunks doesn't contain the visible chunk position, add it to the generate queue
                    if (!_chunks.ContainsKey(visibleChunksPositions[i]))
                    {
                        if (!_chunksToGenerate.Contains(visibleChunksPositions[i]))
                            _chunksToGenerate.Add(visibleChunksPositions[i]);
                    }

                    // If chunksToDelete contains the visible chunk position, remove it from chunksToDelete
                    else if (_chunksToDelete.Contains(visibleChunksPositions[i]))
                        _chunksToDelete.Remove(visibleChunksPositions[i]);
                }
            };
        }


        public void ManualUpdate()
        {
            while (_chunksToDelete.Count > 0)
            {
                Destroy(_chunks[_chunksToDelete[0]].viewGO);

                _chunks.Remove(_chunksToDelete[0]);

                _chunksToDelete.RemoveAt(0);
            }

            while (_chunksToGenerate.Count > 0)
            {
                Vector2DInt newChunkPosition = _chunksToGenerate[0];

                _chunks.Add(newChunkPosition, chunkGenerator.GenerateChunk(newChunkPosition));

                _chunksToGenerate.RemoveAt(0); 
            }
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

            return _chunks[chunkPosition].data.GetTile(tilePosition);
        }
        
        // Holds all chunks that have been generated
        // Responsible for deleting non-visible chunks
    }
}