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

                for (int i = 0; i < 2; i++)
                {
                    Creature newCreature = SpawnCreature(Species.Type.Human, spawnTile);
                    inUser.creatureManager.AddCreature(newCreature);
                }
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
