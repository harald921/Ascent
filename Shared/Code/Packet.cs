﻿using System.Collections;
using System.Collections.Generic;

using Lidgren.Network;

[System.Serializable]
public class Packet
{
    public void PackInto(NetOutgoingMessage outMsg)
    {
        outMsg.PackObjectInto(this);
    }
}
