using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public static class EntityFactory
    {
        public static Entity CreateEntity()
        {
            return new Player
            {

            };
        }

        public static Entity CreateEntity(EntityType entity)
        {
            switch (entity)
            {
                case EntityType.Player:
                    return new Player { };
                case EntityType.SkeletonEnemy:
                    return new SkeletonEnemy { };
                case EntityType.ZombieEnemy:
                    return new ZombieEnemy { };
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Unsupported entity type: {entity}");
            }
        }
    }
}
