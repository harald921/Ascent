using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public List<Guid> ownedCreatureID = new List<Guid>();

    public event Action OnVisibleChunksChange;

    Dictionary<Guid, Vector2DInt[]> _chunksVisibleToCreatures = new Dictionary<Guid, Vector2DInt[]>();


    public Vector2DInt[] GetAllVisibleChunkPositions()
    {
        HashSet<Vector2DInt> visibleChunkPositions = new HashSet<Vector2DInt>();

        foreach (KeyValuePair<Guid, Vector2DInt[]> item in _chunksVisibleToCreatures)
            foreach (Vector2DInt visiblePosition in item.Value)
                visibleChunkPositions.Add(visiblePosition);

        Vector2DInt[] visiblePositions = new Vector2DInt[visibleChunkPositions.Count];
        visibleChunkPositions.CopyTo(visiblePositions);

        return visiblePositions;
    }

    public void UpdateVisibleChunks(Guid inTargetCreatureGuid, Vector2DInt[] inNewVisibleChunks)
    {
        _chunksVisibleToCreatures[inTargetCreatureGuid] = (Vector2DInt[])inNewVisibleChunks.Clone();

        OnVisibleChunksChange?.Invoke();
    }
}