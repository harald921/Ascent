using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

partial class Creature
{
    public class ChunkComponent
    {
        readonly Creature _creature;

        Vector2DInt[] _visibleChunkPositions;

        readonly int _renderDistance;


        public ChunkComponent(Vector2DInt inChunkPos, int inRenderDistance)
        {
            _renderDistance = inRenderDistance;
            _visibleChunkPositions = new Vector2DInt[_renderDistance * _renderDistance];

            CalculateVisibleChunkPositions(inChunkPos);

            _creature.movementComponent.OnChunkEnter += CalculateVisibleChunkPositions;
        }


        void CalculateVisibleChunkPositions(Vector2DInt inNewChunkPosition)
        {
            int chunksToSide = (_renderDistance - 1) / 2;

            for (int y = 0; y < _renderDistance; y++)
                for (int x = 0; x < _renderDistance; x++)
                    _visibleChunkPositions[y * _renderDistance + x] = new Vector2DInt(x - chunksToSide + inNewChunkPosition.x,
                                                                                      y - chunksToSide + inNewChunkPosition.y);
        }
    }
}
