using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class World
{
	public class ChunkManager
    {
        Dictionary<Vector2DInt, Chunk> _chunks = new Dictionary<Vector2DInt, Chunk>();

        public readonly ChunkGenerator chunkGenerator;

        // Holds all chunks that have been generated
        // Responsible for deleting non-visible chunks
    }
}