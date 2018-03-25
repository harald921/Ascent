using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class Noise
{
    public static float[,] Generate(uint inSize, Parameters inParameters, Vector2DInt inOffset)
    {
        // TODO: Find a perlin noise lib and use it

        System.Random rng = new System.Random((int)inParameters.seed);

        float[,] noiseMap = new float[inSize, inSize];

        for (int y = 0; y < inSize; y++)
            for (int x = 0; x < inSize; x++)
                noiseMap[x, y] = rng.Next(0, 2);

        return noiseMap;
    }

    public struct Parameters : IPackable
    {
        public uint  scale;
        public uint  octaves;
        public float persistance;
        public float lacunarity;
        public uint  seed;

        // Networking
        public int GetPacketSize()
        {
            int bitsNeeded = 0;
            bitsNeeded += NetUtility.BitsToHoldUInt(scale);
            bitsNeeded += NetUtility.BitsToHoldUInt(octaves);
            bitsNeeded += 32;
            bitsNeeded += 32;
            bitsNeeded += NetUtility.BitsToHoldUInt(seed);
            return bitsNeeded;
        }

        public void PackInto(NetOutgoingMessage inMsg)
        {
            inMsg.WriteVariableUInt32(scale);
            inMsg.WriteVariableUInt32(octaves);
            inMsg.Write(persistance);
            inMsg.Write(lacunarity);
            inMsg.WriteVariableUInt32(seed);
        }

        public void UnpackFrom(NetIncomingMessage inMsg)
        {
            inMsg.ReadVariableUInt32(out scale);
            inMsg.ReadVariableUInt32(out octaves);
            inMsg.ReadSingle(out persistance);
            inMsg.ReadSingle(out lacunarity);
            inMsg.ReadVariableUInt32(out seed);
        }
    }
}