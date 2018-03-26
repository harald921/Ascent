using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class Creature
{
    public class MovementComponent
    {
        readonly Creature _character;

        Tile _currentTile;


        public MovementComponent(Creature inCharacter)
        {
            _character = inCharacter;
        }


        public void MoveInDirection(Vector2DInt inDirection) =>
            Move(_currentTile, _currentTile.GetNearbyTile(inDirection));

        public void Teleport(Vector2DInt inChunkCoords, Vector2DInt inLocalTileCoords) =>
            Move(_currentTile, World.GetChunk(inChunkCoords).data.GetTile(inLocalTileCoords));


        void Move(Tile inFromTile, Tile inToTile)
        {
            _currentTile = inToTile;

            inFromTile.CharacterExit(_character);
            inToTile.CharacterEnter(_character);
        }
    }
}
