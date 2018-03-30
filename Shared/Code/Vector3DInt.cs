using System.Collections;
using System.Collections.Generic;
using Lidgren.Network;

public struct Vector3DInt
{
    public int x, y, z;

    public Vector3DInt Zero => new Vector3DInt(0, 0, 0);


    public Vector3DInt(int inX, int inY, int inZ)
    {
        x = inX;
        y = inY;
        z = inZ;
    }
}