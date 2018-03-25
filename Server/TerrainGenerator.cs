using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TerrainGenerator
{
    public static Terrain.Type GetTerrainType(float inHeight)
    {
        // TODO: Implement this properly
        if (inHeight == 0) return Terrain.Type.Grass;
        else return Terrain.Type.Sand;
    }
}
