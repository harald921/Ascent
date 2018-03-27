using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 

public partial class Creature
{
    public readonly Components components;
    public readonly Species species;


    public Creature(Species.Type inSpeciesType)                                                                                                                                                 
    {
        species = SpeciesManager.GetSpecies(inSpeciesType);

        components = new Components(this);
    }


    public class Components
    {
        public readonly HealthComponent   healthComponent;
        public readonly MovementComponent movementComponent;

        public Components(Creature inCharacter)
        {
            healthComponent   = new HealthComponent(inCharacter);
            movementComponent = new MovementComponent(inCharacter);
        }
    }
}

