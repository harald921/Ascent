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
        GameObject chunkView = _viewGenerator.Generate(inPosition, chunkData);

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

            newChunkData.SetTiles(_tileMapGenerator.Generate(inPosition, _noiseGenerator.Generate(inPosition)).tiles);

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
                Output newOutput = new Output(_chunkSize);

                for (int y = 0; y < _chunkSize; y++)
                    for (int x = 0; x < _chunkSize; x++)
                        newOutput.tiles[x, y] = new Tile(new Vector2DInt(x, y), inChunkPos, new Terrain(TerrainGenerator.GetTerrainType(inNoiseData.heightMap[x, y])));

                return newOutput;
            }

            public class Output
            {
                public Tile[,] tiles;

                public Output(  int inChunkSize)
                {
                    tiles = new Tile[inChunkSize, inChunkSize];
                }
            }
        }
    }


    class ViewGenerator
    {
        readonly MeshGenerator _meshGenerator;

        Material _chunkMaterial;


        public ViewGenerator()
        {
            _chunkMaterial = (Material)Resources.Load("Material_Chunk", typeof(Material));

            _meshGenerator = new MeshGenerator();
        }


        public GameObject Generate(Vector2DInt inPosition, Chunk.Data inChunkData)
        {
            GameObject   newChunkView = new GameObject("Chunk");

            MeshFilter   meshFilter   = newChunkView.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = newChunkView.AddComponent<MeshRenderer>();

            meshRenderer.material = _chunkMaterial;

            newChunkView.transform.position = new Vector3(inPosition.x, 0, inPosition.y) * Constants.TerrainGeneration.CHUNK_SIZE;


            MeshGenerator.Output meshData = _meshGenerator.Generate(inChunkData);
            ApplyMesh(meshFilter, meshData);

            return newChunkView;
        }


        void ApplyMesh(MeshFilter inFilter, MeshGenerator.Output inMeshData)
        {
            inFilter.mesh.vertices  = inMeshData.vertices;
            inFilter.mesh.triangles = inMeshData.triangles;
            inFilter.mesh.uv2       = inMeshData.uv2;
        }


        class MeshGenerator
        {
            readonly int _vertexSize;
            readonly int _vertexCount;

            readonly int[]     _triangles;
            readonly Vector3[] _vertices;


            // Constructor
            public MeshGenerator()
            {
                // Calculate sizes and counts
                _vertexSize = _chunkSize * 2;
                _vertexCount = _vertexSize * _vertexSize * 4;

                // Generate vertices
                _vertices = GenerateVertices();

                // Generate triangle ID's
                _triangles = GenerateTriangleIDs();
            }


            public Output Generate(Chunk.Data inChunkData)
            {
                Output newOutput = new Output(_vertices, _triangles);

                newOutput.uv2 = GenerateUV2(inChunkData);

                return newOutput;
            }


            int[] GenerateTriangleIDs()
            {
                int[] triangles = new int[_chunkSize * _chunkSize * 6];
                int currentQuad = 0;
                for (int y = 0; y < _vertexSize; y += 2)
                    for (int x = 0; x < _vertexSize; x += 2)
                    {
                        int triangleOffset = currentQuad * 6;
                        int currentVertex = y * _vertexSize + x;

                        triangles[triangleOffset + 0] = currentVertex + 0;                 // Bottom - Left
                        triangles[triangleOffset + 1] = currentVertex + _vertexSize + 1;   // Top    - Right
                        triangles[triangleOffset + 2] = currentVertex + 1;                 // Bottom - Right

                        triangles[triangleOffset + 3] = currentVertex + 0;                 // Bottom - Left
                        triangles[triangleOffset + 4] = currentVertex + _vertexSize + 0;   // Top    - Left
                        triangles[triangleOffset + 5] = currentVertex + _vertexSize + 1;   // Top    - Right

                        currentQuad++;
                    }

                return triangles;
            }

            Vector3[] GenerateVertices()
            {
                Vector3[] vertices = new Vector3[_vertexCount];
                int vertexID = 0;
                for (int y = 0; y < _chunkSize; y++)
                {
                    for (int x = 0; x < _chunkSize; x++)
                    {
                        // Generate a quad 
                        vertices[vertexID + 0].x = x;
                        vertices[vertexID + 0].z = y;

                        vertices[vertexID + 1].x = x + 1;
                        vertices[vertexID + 1].z = y;

                        vertices[vertexID + _vertexSize + 0].x = x;
                        vertices[vertexID + _vertexSize + 0].z = y + 1;

                        vertices[vertexID + _vertexSize + 1].x = x + 1;
                        vertices[vertexID + _vertexSize + 1].z = y + 1;

                        vertexID += 2;
                    }
                    vertexID += _vertexSize;
                }

                return vertices;
            }

            Vector2[] GenerateUV2(Chunk.Data inChunkData)
            {
                Vector2[] newUV2s = new Vector2[_vertexCount];
                int vertexID = 0;
                for (int y = 0; y < _chunkSize; y++)
                {
                    for (int x = 0; x < _chunkSize; x++)
                    {

                        int tileTextureID = inChunkData.GetTile(new Vector2DInt(x, y)).terrain.data.textureID;

                        newUV2s[vertexID + 0] = new Vector2(tileTextureID, tileTextureID);
                        newUV2s[vertexID + 1] = new Vector2(tileTextureID, tileTextureID);
                        newUV2s[vertexID + _vertexSize + 0] = new Vector2(tileTextureID, tileTextureID);
                        newUV2s[vertexID + _vertexSize + 1] = new Vector2(tileTextureID, tileTextureID);

                        vertexID += 2;
                    }
                    vertexID += _vertexSize;
                }


                return newUV2s;
            }


            public class Output
            {
                // Cached data
                readonly public Vector3[] vertices;
                readonly public int[] triangles;

                // Data
                public Vector2[] uv2;

                // Constructor
                public Output(Vector3[] inVertices, int[] inTriangles)
                {
                    vertices = inVertices;
                    triangles = inTriangles;
                }
            }
        }
    }
}