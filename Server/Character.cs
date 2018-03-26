using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Character
{
    Components _components;
    public Components components => _components;

    public Character(Components inComponents)
    {
        _components = inComponents;
    }

    public struct Components
    {
        public HealthComponent  healthComponent;
        public MovementComponent movementComponent;
    }
}


partial class Character
{
    public static class Presets
    {
        public static Components Human()
        {
            return new Components()
            {
                healthComponent = new HealthComponent(new HealthComponent.Stats() {
                    maxHealth = 100
                }),

                movementComponent = new MovementComponent(new MovementComponent.Stats() {
                    baseMoveSpeed = 10
                })
            };
        }
    }
}