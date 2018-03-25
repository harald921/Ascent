using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class Chunk
{
    readonly public Data data;


    public Chunk(Data inData)
    {
        data = inData;

        Console.WriteLine("New Chunk: " + data.position.x + "," + data.position.y);
    }


    public class Data
    {
        public readonly Vector2DInt position;

        Tile[,] _tiles;


        public Data(Vector2DInt inPosition)
        {
            position = inPosition;
        }


        public Tile GetTile(Vector2DInt inTileCoords) => 
            _tiles[inTileCoords.x, inTileCoords.y];

        public void SetTile(Vector2DInt inTileCoords, Tile inTile) => 
            _tiles[inTileCoords.x, inTileCoords.y] = inTile;

        public void SetTiles(Tile[,] inTiles) =>
            _tiles = inTiles;
    }
}
