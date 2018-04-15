using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public Guid ownedCreatureID;

    Dictionary<Guid, Vector2DInt[]> _chunksVisibleToCreatures = new Dictionary<Guid, Vector2DInt[]>();

    public void UpdateVisibleChunks(Guid inTargetCreatureGuid, Vector2DInt[] inNewVisibleChunks)
    {
        Debug.Log("TODO: Delete the non-visible chunks and generate the newly visible ones");
    }
}