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
        creatureManager = new CreatureManager(this);

        connection = inConnection;
    }


    public class CreatureManager
    {
        readonly User _user;

        Dictionary<Creature, Vector2DInt[]> _chunkPositionsVisibleToCreatures = new Dictionary<Creature, Vector2DInt[]>();


        public CreatureManager(User inUser) => 
            _user = inUser;


        public void AddCreature(Creature inCreature)
        {
            // Recalculate creature visible chunks
            Vector2DInt[] visibleChunkPositions = CalculateVisibleChunkPositions(inCreature.movementComponent.currentPosition);
            _chunkPositionsVisibleToCreatures.Add(inCreature, visibleChunkPositions);

            // Give ownership of the creature to the user
            new Command.Client.GiveCreatureOwnership(new Command.Client.GiveCreatureOwnership.Data()
            {
                creatureGuid = inCreature.guid,
            }).Send(NetworkManager.instance.server, _user.connection, NetDeliveryMethod.ReliableOrdered);

            new Command.Client.CreateCreature(new Command.Client.CreateCreature.Data()
            {
                creatureGuid = inCreature.guid,
                spawnWorldPosition = inCreature.movementComponent.currentPosition
            }).Send(NetworkManager.instance.server, _user.connection);

            // Send the chunks visible to this creature to the user
            new Command.Client.SendVisibleChunks(new Command.Client.SendVisibleChunks.Data()
            {
                creatureGuid = inCreature.guid,
                visibleChunkPositions = visibleChunkPositions,
            }).Send(NetworkManager.instance.server, _user.connection, NetDeliveryMethod.ReliableOrdered);

            // Make the creature send what chunks it can see every time it enters a new chunk
            inCreature.movementComponent.OnChunkEnter += (Vector2DInt inNewChunkPosition) => 
            {
                _chunkPositionsVisibleToCreatures[inCreature] = CalculateVisibleChunkPositions(inNewChunkPosition);

                new Command.Client.SendVisibleChunks(new Command.Client.SendVisibleChunks.Data()
                {
                    creatureGuid = inCreature.guid,
                    visibleChunkPositions = _chunkPositionsVisibleToCreatures[inCreature],
                }).Send(NetworkManager.instance.server, _user.connection, NetDeliveryMethod.ReliableOrdered);
            };
        }

        public Creature[] GetCreatures() => 
            _chunkPositionsVisibleToCreatures.Keys.ToArray();

        public Vector2DInt[] GetVisibleChunkPositions(Creature inCreature) => 
            _chunkPositionsVisibleToCreatures[inCreature];
        


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




