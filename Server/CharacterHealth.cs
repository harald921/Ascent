using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Character
{
    public class HealthComponent
    {
        Stats _stats;
        public Stats stats => _stats;

        int _currentHealth;


        public HealthComponent(Stats inStats)
        {
            _stats = inStats;

            _currentHealth = _stats.maxHealth;
        }


        public struct Stats
        {
            public int maxHealth;
        }
    }
}
