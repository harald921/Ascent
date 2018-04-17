using System;
using System.Collections;
using System.Collections.Generic;
using Lidgren.Network;

public static class SystemGuidExtensions
{
	const int GUID_NUM_BYTES = 16;

    public static int GetPacketSize(this Guid inGuid) =>
        GUID_NUM_BYTES * 8;

    public static void PackInto(this Guid inGuid, NetOutgoingMessage inMsg)
    {
        byte[] guidBytes = inGuid.ToByteArray();

        inMsg.Write(guidBytes, 0, GUID_NUM_BYTES);
    }

	public static Guid UnpackFrom(this Guid inGuid, NetIncomingMessage inMsg)
    {
        byte[] guidBytes = inMsg.ReadBytes(GUID_NUM_BYTES);

        return new Guid(guidBytes);
    }

    public static void AddTo(this int inInt, int inAmountToAdd)
    {
        inInt += inAmountToAdd;
    }
}
