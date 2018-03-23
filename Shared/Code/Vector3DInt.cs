using System.Collections;
using System.Collections.Generic;
using Lidgren.Network;

public struct Vector3DInt : IPackable
{
    public int x, y, z;

    public Vector3DInt Zero => new Vector3DInt(0, 0, 0);


    public Vector3DInt(int inX, int inY, int inZ)
    {
        x = inX;
        y = inY;
        z = inZ;
    }


    public int GetPacketSize() => NetUtility.BitsToHoldUInt(x) + 
                                  NetUtility.BitsToHoldUInt(y) +
                                  NetUtility.BitsToHoldUInt(z) +
                                  Constants.BOOL_SIZE_IN_BITS * 3;

    public void PackInto(NetOutgoingMessage inMsg)
    {
        inMsg.WriteVariableUInt32(x);
        inMsg.WriteVariableUInt32(y);
        inMsg.WriteVariableUInt32(z);

        inMsg.Write(x < 0);
        inMsg.Write(y < 0);
        inMsg.Write(z < 0);
    }

    public void UnpackFrom(NetIncomingMessage inMsg)
    {
        x = (int)inMsg.ReadUInt32();
        y = (int)inMsg.ReadUInt32();
        z = (int)inMsg.ReadUInt32();

        x = inMsg.ReadBoolean() ? -x : x;
        y = inMsg.ReadBoolean() ? -y : y;
        z = inMsg.ReadBoolean() ? -z : z;
    }
}