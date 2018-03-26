using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class Character
{
    public class MovementComponent
    {
        readonly Character _character;

        Stats _stats;
        public Stats stats => _stats;

        Tile _currentTile;


        public MovementComponent(Character inCharacter, Stats inStats)
        {
            _character = inCharacter;
            _stats = inStats;
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


        public struct Stats
        {
            public float baseMoveSpeed;
        }
    }
}
