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

            newChunkData.SetTiles(_tileMapGenerator.Generate(_noiseGenerator.Generate(inPosition)).tiles);

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
            public Output Generate(Vector2DInt inChunkPos, NoiseGenerator.Output inNoiseData)
            {
                Output newOutput = new Output();

                for (int y = 0; y < _chunkSize; y++)
                    for (int x = 0; x < _chunkSize; x++)
                        newOutput.tiles[x, y] = new Tile(new Vector2DInt(x, y), inChunkPos, new Terrain(TerrainGenerator.GetTerrainType(inNoiseData.heightMap[x, y])));

                return newOutput;
            }

            public class Output
            {
                public Tile[,] tiles;
            }
        }
    }


    class ViewGenerator
    {
        Material _chunkMaterial;

        public ViewGenerator()
        {
            _chunkMaterial = (Material)Resources.Load("Material_Chunk", typeof(Material));
        }

        public GameObject Generate(Vector3 inPosition, Chunk.Data inChunkData)
        {
            GameObject   newChunkView = new GameObject("Chunk");

            MeshFilter   meshFilter   = newChunkView.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = newChunkView.AddComponent<MeshRenderer>();

            meshRenderer.material = _chunkMaterial;

            newChunkView.transform.position = new Vector3(inPosition.x, 0, inPosition.z);

            // Generate and apply mesh to GO
            // Generate and apply texture to GO

            return newChunkView;
        }


        class MeshGenerator
        {
            
        }

        class TextureGenerator
        {

        }
    }
}