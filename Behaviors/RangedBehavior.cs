using System;

namespace WorldOfSkyfire
{
    public class RangedBehavior(int damage = 10) : IEnemyBehavior
    {
        private readonly int dmg = damage;

        public void ExecuteAttack(Player player)
        {
            player.TakeDamage(dmg);
        }
        public override string ToString() => "Ranged";
    }
}
