using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public readonly Guid guid;

    public readonly GameObject viewGO;


    public Creature(Guid inGuid, GameObject inViewGO)
    {
        viewGO = inViewGO;
    }
}