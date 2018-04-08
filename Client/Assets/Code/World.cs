using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class World : MonoBehaviour
{
    public ChunkManager    chunkManager    { get; private set; }
    public CreatureManager creatureManager { get; private set; }


    void Awake()
    {
        chunkManager    = new ChunkManager();
        creatureManager = new CreatureManager();
    }
}
