using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public class ZombieEnemy : EnemyBase
    {
        public ZombieEnemy() : base(new AggressiveBehavior(10))
        {
            var rng = new Random();
            Position = new Vector2(rng.Next(50, 750), rng.Next(50, 450));
            Size = new Vector2(50, 50);
            Color = Color.Red;

            //Stats
            MaxHealth = Health = 90;
            Speed = 0.9f;
            AttackRange = 40f;
            AttackCooldown = 0.8f;

            //Behavior
            LowHealthFrac = 0.20f;
            AggressiveWithin = 50f;
            PrefersRanged = false;
            AggressiveDamage = 10;
            DefensiveDamage = 5;
        }
        
        public override string ToString() => "Zombie";
    }
}
