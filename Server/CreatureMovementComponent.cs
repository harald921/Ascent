﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class Creature
{
    public class MovementComponent
    {
        readonly Creature _creature;

        Tile _currentTile;

        public Vector2DInt currentPosition => _currentTile.worldPosition;

        public event Action<Vector2DInt> OnChunkEnter;             // Params: newChunkPosition
        public event Action<Vector2DInt, Vector2DInt> OnChunkExit; // Params: newChunkPosition, oldChunkPosition


        public MovementComponent(Creature inCreature, Tile inSpawnTile)
        {
            _creature = inCreature;

            _currentTile = inSpawnTile;

            OnChunkEnter += (Vector2DInt inNewChunkPos) => Console.WriteLine("Entered chunk: " + inNewChunkPos.ToString());
        }


        public void MoveInDirection(Vector2DInt inDirection) =>
            Move(_currentTile, _currentTile.GetNearbyTile(inDirection));

        public void Teleport(Vector2DInt inChunkCoords, Vector2DInt inLocalTileCoords) =>
            Move(_currentTile, World.instance.chunkManager.GetChunk(inChunkCoords).GetTile(inLocalTileCoords));


        void Move(Tile inFromTile, Tile inToTile)
        {
            _currentTile = inToTile;

            inFromTile.CharacterExit(_creature);
            inToTile.CharacterEnter(_creature);


            if (inFromTile.chunkPosition != inToTile.chunkPosition)
            {
                OnChunkEnter?.Invoke(inToTile.chunkPosition);
                OnChunkExit?.Invoke(inToTile.chunkPosition, inFromTile.chunkPosition);
            }
                
            Console.WriteLine("Moved from " + inFromTile.localPosition.x + "," + inFromTile.localPosition.y + 
                              " to "        + inToTile.localPosition.x   + "," + inToTile.localPosition.y   +
                              "   Chunk: "  + World.ChunkManager.WorldPosToChunkPos(inToTile.worldPosition).ToString());
        }
    }
}
