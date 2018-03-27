using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Creature
{
    public class HealthComponent
    {
        readonly Creature _character;

        int _currentHealth;


        public HealthComponent(Creature inCharacter)
        {
            _character = inCharacter;
            _currentHealth = _character.species.data.baseHealth;
        }
    }
}
