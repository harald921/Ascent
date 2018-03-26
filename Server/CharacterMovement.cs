using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Character
{
    public class MovementComponent
    {
        Stats _stats;
        public Stats stats => _stats;

        Tile _currentTile;


        public MovementComponent(Stats inStats)
        {
            _stats = inStats;
        }


        public void Move(Vector2DInt inDirection)
        {
            
        }


        public struct Stats
        {
            public float baseMoveSpeed;
        }
    }
}
