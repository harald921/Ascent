using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public ChunkManager    chunkManager    { get; private set; }
    public CreatureManager creatureManager { get; private set; }


    void Awake()
    {
        chunkManager    = new ChunkManager();
        creatureManager = new CreatureManager();
    }
}


// Temp

public class ChunkManager 
{
    
}

public class CreatureManager
{

}


public class Chunk
{
    class Data
    {
        // The actual data (e.g tile types, etc)
        // This will be what is used to generate the view
    }

    class View
    {
        // The mesh, model, etc.
    }
}