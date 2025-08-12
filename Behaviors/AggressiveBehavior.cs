using System;

namespace WorldOfSkyfire
{
    public class AggressiveBehavior(int damage = 20) : IEnemyBehavior
    {
        private readonly int dmg = damage;

        public void ExecuteAttack(Player player)
        {
            player.TakeDamage(dmg);
        }

        public override string ToString() => "Aggressive";

    }
}
