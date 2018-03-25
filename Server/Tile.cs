using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren;

public class Tile
{
    public readonly Vector2DInt localPosition;
    public readonly Vector2DInt chunkPosition;
    // public Vector2DInt worldPosition 

    Terrain terrain;

    public Tile(Vector2DInt inLocalPosition, Vector2DInt inChunkPosition, Terrain inTerrain)
    {
        localPosition = inLocalPosition;
        chunkPosition = inChunkPosition;

        terrain = inTerrain;
    }
}
