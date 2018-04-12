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

    static int _chunkSize;


    public ChunkGenerator(uint inChunkSize, Noise.Parameters[] inNoiseParameters)
    {
        _noiseGenerator   = new NoiseGenerator(inNoiseParameters);
        _tileMapGenerator = new TileMapGenerator();
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
        Noise.Parameters[] _noiseParameters;


        public NoiseGenerator(Noise.Parameters[] inNoiseParameters) =>
            _noiseParameters = inNoiseParameters;


        public Output Generate(Vector2DInt inChunkPosition) =>
            new Output() { heightMap = Noise.Generate((uint)_chunkSize, _noiseParameters[0], inChunkPosition) };


        public class Output
        {
            public float[,] heightMap;
        }
    }

    class TileMapGenerator
    {
        public Output Generate(Vector2DInt inChunkPosition, NoiseGenerator.Output inNoiseData)
        {
            Output newOutput = new Output();

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

            public Output() =>
                tiles = new Tile[_chunkSize, _chunkSize];
        }
    }
}

