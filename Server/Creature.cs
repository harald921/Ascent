using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 

public partial class Creature
{
    public readonly Components components;
    public readonly Data data;


    public Creature(Species inSpecies)                                                                                                                                                 
    {
        data = SpeciesManager.GetSpeciesData(inSpecies);

        components = new Components(this);
    }


    public class Data
    {
        public Species species;

        // Health
        public int maxHealth;

        // Movement
        public int baseMoveSpeed;
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

    public enum Species
    {
        Human,
        Cow,
    }
}

