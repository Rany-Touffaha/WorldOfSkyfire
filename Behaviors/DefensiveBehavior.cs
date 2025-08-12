using System;

namespace WorldOfSkyfire
{
    public class DefensiveBehavior(int damage = 5) : IEnemyBehavior
    {
        private readonly int dmg = damage;

        public void ExecuteAttack(Player player)
        {
            player.TakeDamage(dmg);
        }
        
        public override string ToString() => "Defensive";
    }
}
