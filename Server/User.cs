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

        public CreatureManager(User inUser) => 
            _user = inUser;


        Dictionary<Creature, Vector2DInt[]> _chunksVisibleToCreatures = new Dictionary<Creature, Vector2DInt[]>();

        public void AddCreature(Creature inCreature)
        {
            // Recalculate creature visible chunks
            inCreature.movementComponent.OnChunkEnter += (Vector2DInt inNewChunkPosition) => {
                _chunksVisibleToCreatures[inCreature] = CalculateVisibleChunkPositions(inNewChunkPosition);
            };

            Vector2DInt[] visibleChunkPositions = CalculateVisibleChunkPositions(inCreature.movementComponent.currentPosition);
            _chunksVisibleToCreatures.Add(inCreature, visibleChunkPositions);

            Console.WriteLine(inCreature.guid.ToString());

            new Command.Client.GiveCreatureOwnership(new Command.Client.GiveCreatureOwnership.Data()
            {
                creatureGuid = inCreature.guid,
            }).Send(NetworkManager.instance.server, _user.connection, NetDeliveryMethod.ReliableOrdered);

            new Command.Client.SendVisibleChunks(new Command.Client.SendVisibleChunks.Data()
            {
                creatureGuid = inCreature.guid,
                visibleChunkPositions = visibleChunkPositions,
            }).Send(NetworkManager.instance.server, _user.connection, NetDeliveryMethod.ReliableOrdered);
        }

        public Creature[] GetCreatures() => 
            _chunksVisibleToCreatures.Keys.ToArray();

        public Vector2DInt[] GetVisibleChunkPositions(Creature inCreature) => 
            _chunksVisibleToCreatures[inCreature];
        


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




