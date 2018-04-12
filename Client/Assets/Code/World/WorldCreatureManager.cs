using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class World
{

	public class CreatureManager
    {
        ChunkGenerator _chunkGenerator;


        public CreatureManager()
        {
            _chunkGenerator = new ChunkGenerator(new Noise.Parameters[]
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

            _chunkGenerator.GenerateChunk(Vector2DInt.Zero);
        }

        // Simply holds a collection of all creatures and their position in the world
    }
}