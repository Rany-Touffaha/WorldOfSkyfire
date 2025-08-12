using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfSkyfire
{
    public abstract class EnemyBase : Entity
    {
        // Stats
        public int MaxHealth { get; protected set; } = 50;
        public int Health { get; protected set; } = 50;

        // Movement/attack
        public float Speed { get; protected set; } = 1.2f;
        public float AttackRange { get; protected set; } = 40f;
        public float AttackCooldown { get; protected set; } = 0.75f;

        protected IEnemyBehavior behavior;
        protected Player target;

        // When to switch to defensive
        protected float LowHealthFrac = 0.30f;

        // Prefer aggressive if within this distance
        protected float AggressiveWithin = 5f;

        // Prefer ranged if within this distance 
        protected bool PrefersRanged = false;
        protected float RangedPreferredMin = 7f;

        // Damage per strategy
        protected int AggressiveDamage = 16;
        protected int DefensiveDamage = 6;
        protected int RangedDamage = 10;

        // Cooldown timer to avoid having the enemy attack every frame
        float cooldownTimer;

        protected EnemyBase(IEnemyBehavior initialBehavior)
        {
            behavior = initialBehavior;
            Size = new Vector2(40, 40);
            // Color to be overriden in each class
            Color = Color.DarkRed;
        }

        public void SetBehavior(IEnemyBehavior newBehavior) => behavior = newBehavior;
        public void SetTarget(Player player) => target = player;
        public void TakeDamage(int dmg) => Health = Math.Max(0, Health - Math.Max(0, dmg));

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            cooldownTimer = Math.Max(0, cooldownTimer - dt);

            if (target is null) return;

            // Move toward player
            var toPlayer = target.Position - Position;
            float dist = toPlayer.Length();
            if (dist > 1f)
                Position += Vector2.Normalize(toPlayer) * Speed;

            // Attack if in range and cooled down
            if (dist <= AttackRange && cooldownTimer <= 0f)
            {
                behavior.ExecuteAttack(target);
                cooldownTimer = AttackCooldown;
            }

            EvaluateStrategy(dist);
        }

        protected virtual void EvaluateStrategy(float distanceToPlayer)
        {
            // Defensive when HP is low
            if (Health <= MaxHealth * LowHealthFrac)
            {
                if (behavior is not DefensiveBehavior)
                    SetBehavior(new DefensiveBehavior(DefensiveDamage));
                return;
            }

            // Prefer ranged when far
            if (PrefersRanged && distanceToPlayer >= RangedPreferredMin)
            {
                if (behavior is not RangedBehavior)
                    SetBehavior(new RangedBehavior(RangedDamage));
                return;
            }

            // Switch to aggressive/defensive when close
            if (distanceToPlayer <= AggressiveWithin)
            {
                if (behavior is not AggressiveBehavior)
                    SetBehavior(new AggressiveBehavior(AggressiveDamage));
            }
            else
            {
                if (behavior is not DefensiveBehavior)
                    SetBehavior(new DefensiveBehavior(DefensiveDamage));
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.ToPoint(), Size.ToPoint()), Color);
        }
        
        public override string ToString() => GetType().Name;
    }
}
