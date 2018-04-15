using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public Guid ownedCreatureID;

    public event Action OnVisibleChunksChange;

    Dictionary<Guid, Vector2DInt[]> _chunksVisibleToCreatures = new Dictionary<Guid, Vector2DInt[]>();


    public Vector2DInt[] GetAllVisibleChunkPositions()
    {
        List<Vector2DInt> visibleChunkPositions = new List<Vector2DInt>();

        foreach (KeyValuePair<Guid, Vector2DInt[]> item in _chunksVisibleToCreatures)
            visibleChunkPositions.AddRange(item.Value);

        return visibleChunkPositions.ToArray();
    }

    public void UpdateVisibleChunks(Guid inTargetCreatureGuid, Vector2DInt[] inNewVisibleChunks)
    {
        _chunksVisibleToCreatures[inTargetCreatureGuid] = (Vector2DInt[])inNewVisibleChunks.Clone();

        OnVisibleChunksChange?.Invoke();
    }
}