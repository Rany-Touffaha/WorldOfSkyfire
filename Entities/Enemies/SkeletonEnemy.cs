using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public class SkeletonEnemy : EnemyBase
    {
        public SkeletonEnemy() : base(new DefensiveBehavior(6))
        {
            var rng = new Random();
            Position = new Vector2(rng.Next(50, 750), rng.Next(50, 450));
            Size = new Vector2(50, 50);
            Color = Color.Yellow;

            //Stats
            MaxHealth = Health = 60;
            Speed = 1.2f;
            AttackRange = 45f;
            AttackCooldown = 0.9f;

            //Behavior
            LowHealthFrac = 0.30f;
            AggressiveWithin = 55f;
            PrefersRanged = false;
            AggressiveDamage = 14;
            DefensiveDamage = 6;
        }
        
        public override string ToString() => "Skeleton";
    }
}
