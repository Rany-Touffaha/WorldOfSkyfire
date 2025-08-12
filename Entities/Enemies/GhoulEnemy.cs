using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public class GhoulEnemy : EnemyBase
    {
        public GhoulEnemy() : base(new AggressiveBehavior(18))
        {
            var rng = new Random();
            Position = new Vector2(rng.Next(50, 750), rng.Next(50, 450));
            Size = new Vector2(50, 50);
            Color = Color.Blue;

            //Stats
            MaxHealth = Health = 40;
            Speed = 1.8f;
            AttackRange = 35f;
            AttackCooldown = 0.5f;

            //Behavior
            LowHealthFrac = 0.30f;
            AggressiveWithin = 45f;
            PrefersRanged = true;
            AggressiveDamage = 18;
            DefensiveDamage = 6;
        }

        public override string ToString() => "Ghoul";
    }
}
