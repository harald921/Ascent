using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class Chunk
{
    public readonly Vector2DInt position;

    List<User> _witnesses = new List<User>();

    Tile[,] _tiles;


    public Chunk(Vector2DInt inPosition)
    {
        position = inPosition;
        Console.WriteLine("New Chunk at: " + position.x + "," + position.y);
    }

    public void AddWitness(User inUser) =>
        _witnesses.Add(inUser);

    public void RemoveWitness(User inUser) =>
        _witnesses.Remove(inUser);

    public User[] GetWitnesses() =>
        _witnesses.ToArray();

    public Tile GetTile(Vector2DInt inTileCoords) =>
        _tiles[inTileCoords.x, inTileCoords.y];

    public void SetTile(Vector2DInt inTileCoords, Tile inTile) =>
        _tiles[inTileCoords.x, inTileCoords.y] = inTile;

    public void SetTiles(Tile[,] inTiles) =>
        _tiles = inTiles;
}
