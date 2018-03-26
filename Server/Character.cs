using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Character
{
    Components _components;
    public Components components => _components;

    public static Guid _guid;

    public Character(Species inSpecies)
    {
        _guid = Guid.NewGuid();
        
        _components = TempCharacterPreset.GetCharacterPreset(inSpecies, this);
    }

    public struct Components
    {
        public HealthComponent   healthComponent;
        public MovementComponent movementComponent;
    }

    public enum Species
    {
        Human
    }
}


partial class Character
{
    public static class TempCharacterPreset
    {
        public static Components GetCharacterPreset(Species inSpecies, Character inCharacter)
        {
            switch (inSpecies)
            {
                case Species.Human:
                    return new Components() {
                        healthComponent = new HealthComponent(new HealthComponent.Stats() {
                            maxHealth = 100
                        }),

                        movementComponent = new MovementComponent(inCharacter, new MovementComponent.Stats() {
                            baseMoveSpeed = 10
                        })
                    };
            }

            Console.WriteLine("Catastrophe happened in TempCharacterPreset.GetCharacterPreset()");
            return new Components();
        }
    }
}