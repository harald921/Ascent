using System.Collections;
using System.Collections.Generic;
using Lidgren.Network;

public struct Vector2DInt : IPackable
{
    public int x, y;

    public static Vector2DInt Zero => new Vector2DInt(0, 0);


    public Vector2DInt(int inX, int inY)
    {
        x = inX;
        y = inY;
    }

    public static Vector2DInt operator+ (Vector2DInt inVector1, Vector2DInt inVector2)
    {
        return new Vector2DInt()
        {
            x = inVector1.x + inVector2.x,
            y = inVector1.y + inVector2.y
        };
    }


    public int GetPacketSize() => NetUtility.BitsToHoldUInt(x) +
                                  NetUtility.BitsToHoldUInt(y) +
                                  Constants.BOOL_SIZE_IN_BITS * 2;

    public void PackInto(NetOutgoingMessage inMsg)
    {
        inMsg.WriteVariableInt32(x);
        inMsg.WriteVariableInt32(y);
    }

    public void UnpackFrom(NetIncomingMessage inMsg)
    {
        x = inMsg.ReadVariableInt32();
        y = inMsg.ReadVariableInt32();
    }
}
