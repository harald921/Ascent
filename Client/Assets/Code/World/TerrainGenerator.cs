using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator 
{
    // Maybe share this file?
    public static Terrain.Type GetTerrainType(float inHeight)
    {
        // TODO: Implement this properly
        if (inHeight == 0) return Terrain.Type.Grass;
        else return Terrain.Type.Sand;
    }
}