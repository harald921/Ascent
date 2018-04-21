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

        public Creature[] GetCreatures() =>
            _chunkPositionsVisibleToCreatures.Keys.ToArray();

        public Vector2DInt[] GetVisibleChunkPositions(Creature inCreature) =>
            _chunkPositionsVisibleToCreatures[inCreature];

        public Vector2DInt[] GetVisibleChunkPositions()
        {
            List<Vector2DInt> visibleChunkPositions = new List<Vector2DInt>();
            foreach (KeyValuePair<Creature, Vector2DInt[]> item in _chunkPositionsVisibleToCreatures)
                visibleChunkPositions.AddRange(item.Value);
            return visibleChunkPositions.ToArray();
        }
            

        public CreatureManager(User inUser) => 
            _user = inUser;


        public void AddCreature(Creature inCreature)
        {
            // Calculate creature visible chunks
            Vector2DInt[] visibleChunkPositions = CalculateVisibleChunkPositions(inCreature.movementComponent.currentPosition);
            _chunkPositionsVisibleToCreatures.Add(inCreature, visibleChunkPositions);

            Command[] commandsToSend = new Command[]
            {
                new Command.Client.GiveCreatureOwnership(new Command.Client.GiveCreatureOwnership.Data() {
                    creatureGuid = inCreature.guid,
                }),

                new Command.Client.CreateCreature(new Command.Client.CreateCreature.Data() {
                    creatureGuid = inCreature.guid,
                    spawnWorldPosition = inCreature.movementComponent.currentPosition
                }),

                new Command.Client.SendVisibleChunks(new Command.Client.SendVisibleChunks.Data() {
                    creatureGuid = inCreature.guid,
                    visibleChunkPositions = _chunkPositionsVisibleToCreatures[inCreature],
                }),
            };

            Command.SendMultiple(commandsToSend, NetworkManager.instance.server, _user.connection, NetDeliveryMethod.ReliableOrdered);


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

            inCreature.movementComponent.OnChunkEnter += AddWitnessToVisibleChunks;
            inCreature.movementComponent.OnChunkExit += RemoveWitnessFromExitedChunks;
        }





        Vector2DInt[] CalculateVisibleChunkPositions(Vector2DInt inViewOrigin)
        {
            int renderDistance = ServerConstants.TerrainGeneration.CHUNK_RENDER_DISTANCE;
            int chunksToSide = (renderDistance - 1) / 2;

            List<Vector2DInt> visibleChunks = new List<Vector2DInt>();

            for (int y = 0; y < renderDistance; y++)
                for (int x = 0; x < renderDistance; x++)
                {
                    Vector2DInt possibleChunkPosition = new Vector2DInt(x - chunksToSide + inViewOrigin.x,
                                                                        y - chunksToSide + inViewOrigin.y);

                    if (possibleChunkPosition.x >= 0 && possibleChunkPosition.y >= 0)
                        visibleChunks.Add(possibleChunkPosition);
                }

            return visibleChunks.ToArray();
        }

        void AddWitnessToVisibleChunks(Vector2DInt inViewPosition)
        {
            Vector2DInt[] visibleChunksPositions = CalculateVisibleChunkPositions(inViewPosition);

            foreach (Vector2DInt visibleChunkPos in visibleChunksPositions)
                World.instance.chunkManager.GetChunk(visibleChunkPos).AddWitness(_user);
        }

        void RemoveWitnessFromExitedChunks(Vector2DInt inNewChunkPosition, Vector2DInt inOldChunkPosition)
        {
            Vector2DInt[] visibleChunkPositions = GetVisibleChunkPositions();
            Vector2DInt[] oldChunkPositionsVisibleToCreature = CalculateVisibleChunkPositions(inOldChunkPosition);

            foreach (Vector2DInt oldVisibleChunkPosition in oldChunkPositionsVisibleToCreature)
                if (!visibleChunkPositions.Contains(oldVisibleChunkPosition))
                {
                    World.instance.chunkManager.GetChunk(oldVisibleChunkPosition).RemoveWitness(_user);
                    Console.WriteLine("User lost sight of: " + oldVisibleChunkPosition.ToString());
                }
        }
    }
}




