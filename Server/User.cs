using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

public class User
{
    public readonly NetConnection connection;

    public readonly CreatureManager creatureManager;


    public User(NetConnection inConnection)
    {
        creatureManager = new CreatureManager();

        connection = inConnection;
    }


    public class CreatureManager
    {
        Dictionary<Creature, Vector2DInt[]> _creatures = new Dictionary<Creature, Vector2DInt[]>();

        public void AddCreature(Creature inCreature)
        {
            // Recalculate creature visible chunks
            inCreature.movementComponent.OnChunkEnter += (Vector2DInt inNewChunkPosition) => {
                _creatures[inCreature] = CalculateVisibleChunkPositions(inNewChunkPosition);
            };

            Vector2DInt[] visibleChunkPositions = CalculateVisibleChunkPositions(inCreature.movementComponent.currentPosition);
            _creatures.Add(inCreature, visibleChunkPositions);
        }

        public Creature[] GetCreatures() => 
            _creatures.Keys.ToArray();

        public Vector2DInt[] GetVisibleChunkPositions(Creature inCreature) => 
            _creatures[inCreature];
        


        Vector2DInt[] CalculateVisibleChunkPositions(Vector2DInt inViewOrigin)
        {
            int renderDistance = ServerConstants.TerrainGeneration.CHUNK_RENDER_DISTANCE;
            int chunksToSide = (renderDistance - 1) / 2;

            Vector2DInt[] visibleChunks = new Vector2DInt[renderDistance * renderDistance];

            for (int y = 0; y < renderDistance; y++)
                for (int x = 0; x < renderDistance; x++)
                    visibleChunks[y * renderDistance + x] = new Vector2DInt(x - chunksToSide + inViewOrigin.x,
                                                                            y - chunksToSide + inViewOrigin.y);

            return visibleChunks;
        }
    }
}




