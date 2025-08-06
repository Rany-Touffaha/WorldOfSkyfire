using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public static class EntityFactory
    {
        public static Entity CreateEntity()
        {
            return new Player(new Vector2(100, 100))
            {

            };
        }

        public static Entity CreateEntity(EntityType entity)
        {
            switch (entity)
            {
                case EntityType.Player:
                    return new Player(new Vector2(100, 100)) { };
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
