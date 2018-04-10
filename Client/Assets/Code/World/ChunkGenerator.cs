using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkGenerator
{
    static int _chunkSize;

    readonly DataGenerator _dataGenerator;
    readonly ViewGenerator _viewGenerator;


    public ChunkGenerator(Noise.Parameters[] inNoiseParameters)
    {
        _chunkSize = Constants.TerrainGeneration.CHUNK_SIZE;

        _dataGenerator = new DataGenerator(inNoiseParameters);
        _viewGenerator = new ViewGenerator();
    }


    public Chunk GenerateChunk(Vector2DInt inPosition)
    {
        Chunk.Data chunkData = _dataGenerator.Generate(inPosition);
        GameObject chunkView = _viewGenerator.Generate(chunkData);

        Chunk newChunk = new Chunk(inPosition, chunkData, chunkView);

        return newChunk;
    }


    class DataGenerator
    {
        readonly NoiseGenerator   _noiseGenerator;
        readonly TileMapGenerator _tileMapGenerator;


        // Constructor
        public DataGenerator(Noise.Parameters[] inNoiseParameters)
        {
            _noiseGenerator   = new NoiseGenerator(inNoiseParameters);
            _tileMapGenerator = new TileMapGenerator();
        }


        public Chunk.Data Generate(Vector2DInt inPosition)
        {
            Chunk.Data newChunkData = new Chunk.Data();

            newChunkData.SetTiles(_tileMapGenerator.Generate(_noiseGenerator.Generate(inPosition)).tileMap);

            return newChunkData;
        }


        class NoiseGenerator
        {
            readonly Noise.Parameters[] _noiseParamters;


            // Constructor
            public NoiseGenerator(Noise.Parameters[] inNosieParameters)
            {
                _noiseParamters = inNosieParameters;
            }


            public Output Generate(Vector2DInt inOffset)
            {
                Output newOutput = new Output();

                newOutput.heightMap = Noise.Generate((uint)_chunkSize, _noiseParamters[0], inOffset);

                return newOutput;
            }


            public class Output
            {
                public float[,] heightMap;
            }
        }

        class TileMapGenerator
        {
            public Output Generate(NoiseGenerator.Output inNoiseData)
            {
                Output newOutput = new Output();

                // Generate Tilemap from noise data

                return newOutput;
            }

            public class Output
            {
                public Tile[,] tileMap;
            }
        }
    }


    class ViewGenerator
    {
        public GameObject Generate(Chunk.Data inChunkData)
        {
            GameObject newView = new GameObject();

            // Use MeshGenerator and TextureGenerator to generate view

            return newView;
        }


        class MeshGenerator
        {
            
        }

        class TextureGenerator
        {

        }
    }
}