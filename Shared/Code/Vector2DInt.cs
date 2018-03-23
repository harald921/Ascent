using System.Collections;
using System.Collections.Generic;
using Lidgren.Network;

public struct Vector2DInt : IPackable
{
    public int x, y;

    public Vector2DInt Zero => new Vector2DInt(0, 0);


    public Vector2DInt(int inX, int inY)
    {
        x = inX;
        y = inY;
    }


    public int GetPacketSize() => NetUtility.BitsToHoldUInt(x) +
                                  NetUtility.BitsToHoldUInt(y) +
                                  Constants.BOOL_SIZE_IN_BITS * 2;

    public void PackInto(NetOutgoingMessage inMsg)
    {
        inMsg.WriteVariableUInt32(x);
        inMsg.WriteVariableUInt32(y);

        inMsg.Write(x < 0);
        inMsg.Write(y < 0);
    }

    public void UnpackFrom(NetIncomingMessage inMsg)
    {
        x = (int)inMsg.ReadUInt32();
        y = (int)inMsg.ReadUInt32();

        x = inMsg.ReadBoolean() ? -x : x;
        y = inMsg.ReadBoolean() ? -y : y;
    }
}
