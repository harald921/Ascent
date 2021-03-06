﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public readonly Vector2DInt localPosition; // The chunk position of the tile
    public readonly Vector2DInt chunkPosition; // The world position of the chunk
    public readonly Vector2DInt worldPosition;

    List<Creature> _characters = new List<Creature>();
    public readonly Terrain terrain;


    public Tile(Vector2DInt inLocalPosition, Vector2DInt inChunkPosition, Terrain inTerrain)
    {
        localPosition = inLocalPosition;
        chunkPosition = inChunkPosition;
        worldPosition = localPosition + (chunkPosition * Constants.TerrainGeneration.CHUNK_SIZE);

        terrain = inTerrain;
    }


    public Tile GetNearbyTile(Vector2DInt inDirection) =>
        World.instance.chunkManager.GetTile(worldPosition + inDirection);

    public void CharacterEnter(Creature inCharacter) =>
        _characters.Add(inCharacter);

    public void CharacterExit(Creature inCharacter) =>
        _characters.Remove(inCharacter);
}
