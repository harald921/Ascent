using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class ChunkGenerator
{

    // Constructor
    public ChunkGenerator(uint inChunkSize, Noise.Parameters[] inNoiseParameters)
    {
        NoiseGenerator.Initialize(inChunkSize, inNoiseParameters);
        TileMapGenerator.Initialize(inChunkSize);
    }


    public Chunk GenerateChunk(Vector2DInt inPosition)
    {
        Chunk.Data newData = new Chunk.Data(inPosition);

        NoiseGenerator.Output noiseData = NoiseGenerator.Generate(inPosition);
        TileMapGenerator.Output tileMap = TileMapGenerator.Generate(inPosition, noiseData);

        newData.SetTiles(tileMap.tiles);

        return new Chunk(newData);
    }


    static class NoiseGenerator
    {
        static uint _noiseMapSize;
        static Noise.Parameters[] _noiseParameters;

        public static void Initialize(uint inChunkSize, Noise.Parameters[] inNoiseParameters)
        {
            _noiseMapSize = inChunkSize;
            _noiseParameters = inNoiseParameters;
        }

        public static Output Generate(Vector2DInt inChunkPosition) =>
            new Output() { heightMap = Noise.Generate(_noiseMapSize, _noiseParameters[0], inChunkPosition) };

        public class Output
        {
            public float[,] heightMap;
        }
    }

    static class TileMapGenerator
    {
        static uint _chunkSize;

        public static void Initialize(uint inChunkSize) =>
            _chunkSize = inChunkSize;

        public static Output Generate(Vector2DInt inChunkPosition, NoiseGenerator.Output inNoiseData)
        {
            Output newOutput = new Output(_chunkSize);

            for (int y = 0; y < _chunkSize; y++)
                for (int x = 0; x < _chunkSize; x++)
                    newOutput.tiles[x, y] = new Tile(inLocalPosition: new Vector2DInt(x,y), 
                                                     inChunkWorldPosition: inChunkPosition, 
                                                     inTerrain:       new Terrain(TerrainGenerator.GetTerrainType(inNoiseData.heightMap[x,y])));

            return newOutput;
        }

        public class Output
        {
            public Tile[,] tiles;

            public Output(uint inChunkSize) =>
                tiles = new Tile[inChunkSize, inChunkSize];
        }
    }
}

