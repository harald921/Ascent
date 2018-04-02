using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class World
{
    public class CreatureHolder
    {
        readonly Dictionary<Guid, Creature> _liveCreatures = new Dictionary<Guid, Creature>();
        public Creature GetCreature(Guid inGuid) => _liveCreatures[inGuid];


        public Creature SpawnCreature(Species.Type inSpeciesType, Tile inSpawnTile)
        {
            Creature newCreature = new Creature(inSpeciesType, inSpawnTile);
            _liveCreatures.Add(newCreature.guid, newCreature);

            Console.WriteLine("Spawning new " + inSpeciesType);

            return newCreature;
        }
    }
}
