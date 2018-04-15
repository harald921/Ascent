using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class World : MonoBehaviour
{
    public static World instance { get; private set; }

    public ChunkManager    chunkManager    { get; private set; }
    public CreatureManager creatureManager { get; private set; }


    void Awake()
    {
        instance = this;

        chunkManager    = new ChunkManager();
        creatureManager = new CreatureManager();
    }

    public void Update()
    {
        chunkManager.ManualUpdate();
    }
}
