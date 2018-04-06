using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class ChunkGenerator
{
    NoiseGenerator _noiseGenerator;
    TileMapGenerator _tileMapGenerator;


    public ChunkGenerator(uint inChunkSize, Noise.Parameters[] inNoiseParameters)
    {
        _noiseGenerator   = new NoiseGenerator(inChunkSize, inNoiseParameters);
        _tileMapGenerator = new TileMapGenerator(inChunkSize);
    }


    public Chunk GenerateChunk(Vector2DInt inPosition)
    {
        Chunk newChunk = new Chunk(inPosition);

        NoiseGenerator.Output   noiseData = _noiseGenerator.Generate(inPosition);
        TileMapGenerator.Output tileMap   = _tileMapGenerator.Generate(inPosition, noiseData);

        newChunk.SetTiles(tileMap.tiles);

        return newChunk;
    }


    class NoiseGenerator
    {
        uint _noiseMapSize;
        Noise.Parameters[] _noiseParameters;


        public NoiseGenerator(uint inChunkSize, Noise.Parameters[] inNoiseParameters)
        {
            _noiseMapSize = inChunkSize;
            _noiseParameters = inNoiseParameters;
        }


        public Output Generate(Vector2DInt inChunkPosition) =>
            new Output() { heightMap = Noise.Generate(_noiseMapSize, _noiseParameters[0], inChunkPosition) };


        public class Output
        {
            public float[,] heightMap;
        }
    }

    class TileMapGenerator
    {
        uint _chunkSize;


        public TileMapGenerator(uint inChunkSize) =>
            _chunkSize = inChunkSize;


        public Output Generate(Vector2DInt inChunkPosition, NoiseGenerator.Output inNoiseData)
        {
            Output newOutput = new Output(_chunkSize);

            for (int y = 0; y < _chunkSize; y++)
                for (int x = 0; x < _chunkSize; x++)
                    newOutput.tiles[x, y] = new Tile(inLocalPosition: new Vector2DInt(x,y), 
                                                     inChunkPosition: inChunkPosition, 
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

