using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 

public partial class Creature
{
    public readonly Guid    guid;    

    public readonly Species species; 

    public readonly HealthComponent   healthComponent;      
    public readonly MovementComponent movementComponent;


    public Creature(Species.Type inSpeciesType, Tile inSpawnTile)                                                                                                                                                 
    {
        guid    = Guid.NewGuid();
        species = SpeciesManager.GetSpecies(inSpeciesType);

        healthComponent   = new HealthComponent(this);
        movementComponent = new MovementComponent(this, inSpawnTile);
    }
}

