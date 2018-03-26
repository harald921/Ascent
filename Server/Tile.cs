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

    List<Character> _characters = new List<Character>();
    Terrain _terrain;


    public Tile(Vector2DInt inLocalPosition, Vector2DInt inChunkPosition, Terrain inTerrain)
    {
        localPosition = inLocalPosition;
        chunkPosition = inChunkPosition;

        _terrain = inTerrain;
    }


    // TODO: Write this one, but properly
    public Tile GetNearbyTile(Vector2DInt inDirection) =>
        World.GetChunk(chunkPosition).data.GetTile(localPosition + inDirection);

    // public Tile GetNearbyTile(Vector2DInt inDirection) =>
    //    World.TilePositionToChunkPosition(worldPosition).data.GetTile(worldPosition + inDirection);


    public void CharacterEnter(Character inCharacter) =>
        _characters.Add(inCharacter);

    public void CharacterExit(Character inCharacter) => 
        _characters.Remove(inCharacter);
}
