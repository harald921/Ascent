﻿using System.Collections;
using System.Collections.Generic;

using Lidgren.Network;

public interface IPackable : IInPackable, IUnPackable { }

public interface IInPackable
{
    int GetPacketSize();
    void PackInto(NetOutgoingMessage inMsg);
}

public interface IUnPackable
{
    void UnpackFrom(NetIncomingMessage inMsg); 
}