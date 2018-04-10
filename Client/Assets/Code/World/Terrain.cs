using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Terrain
{
    static Dictionary<Type, Data> _staticTerrainData = new Dictionary<Type, Data>()
    {
        { Type.Grass, new Data(inTextureID: 2, inMoveSpeedModifier: 0.9f) },
        { Type.Sand,  new Data(inTextureID: 1, inMoveSpeedModifier: 0.8f) },
    };

    readonly Type _type;
    public Data data => _staticTerrainData[_type];


    public Terrain(Type inType)
    {
        _type = inType;
    }

    public class Data // Packable
    {
        public readonly int textureID;
        public readonly float moveSpeedModifier;

        public Data(int inTextureID, float inMoveSpeedModifier)
        {
            textureID = inTextureID;
            moveSpeedModifier = inMoveSpeedModifier;
        }
    }

    public enum Type
    {
        Grass,
        Sand
    }
}