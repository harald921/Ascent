using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren;

public class Tile
{
    public readonly Vector2DInt localPosition; // The chunk position of the tile
    public readonly Vector2DInt chunkPosition; // The world position of the chunk
    public readonly Vector2DInt worldPosition;

    List<Creature> _characters = new List<Creature>();
    Terrain _terrain;


    public Tile(Vector2DInt inLocalPosition, Vector2DInt inChunkPosition, Terrain inTerrain)
    {
        localPosition = inLocalPosition;
        chunkPosition = inChunkPosition;
        worldPosition = localPosition + (chunkPosition * Constants.TerrainGeneration.CHUNK_SIZE);

        _terrain = inTerrain;
    }


    public Tile GetNearbyTile(Vector2DInt inDirection) =>
        World.GetTile(worldPosition + inDirection);

    public void CharacterEnter(Creature inCharacter) =>
        _characters.Add(inCharacter);

    public void CharacterExit(Creature inCharacter) => 
        _characters.Remove(inCharacter);
}
