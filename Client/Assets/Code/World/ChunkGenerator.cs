using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    static int _chunkSize;

    DataGenerator _dataGenerator;
    ViewGenerator _viewGenerator;


    public ChunkGenerator(Noise.Parameters[] inNoiseParameters)
    {
        _chunkSize = Constants.TerrainGeneration.CHUNK_SIZE; 
    }


    class DataGenerator
    {
        class NoiseGenerator
        {
            Noise.Parameters[] _noiseParamters;
           

            public class Output
            {
                float[,] heightMap;
            }
        }

        class TileMapGenerator
        {

        }


        public class Output
        {
            Tile[] chunkTiles;
        }
    }


    class ViewGenerator
    {
        class MeshGenerator
        {

        }

        class TextureGenerator
        {

        }

        
        public class Output
        {
            public GameObject _chunkView;
        }
    }
}