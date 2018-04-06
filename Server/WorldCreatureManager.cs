using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class World
{
    public class CreatureManager
    {
        readonly Dictionary<Guid, Creature> _liveCreatures = new Dictionary<Guid, Creature>();
        public Creature GetCreature(Guid inGuid) => _liveCreatures[inGuid];


        public CreatureManager()
        {
            UserManager.OnUserLogin += (User inUser) =>
            {
                // TODO: Make it read from disk when Users are permanent
                Tile spawnTile = instance.chunkManager.GetChunk(Vector2DInt.Zero).GetTile(Vector2DInt.Zero);
                Creature newCreature = SpawnCreature(Species.Type.Human, spawnTile);

                inUser.creatureManager.AddCreature(newCreature);

                // Send the id of the creature to the fresh client
                new Command.Client.SendPlayerData(new Command.Client.SendPlayerData.Data()
                {
                    creatureGuid = newCreature.guid
                }).Send(NetworkManager.instance.server, inUser.connection);
            };
        }


        public Creature SpawnCreature(Species.Type inSpeciesType, Tile inSpawnTile)
        {
            Creature newCreature = new Creature(inSpeciesType, inSpawnTile);
            _liveCreatures.Add(newCreature.guid, newCreature);

            Console.WriteLine("Spawning new " + inSpeciesType);

            return newCreature;
        }
    }
}
