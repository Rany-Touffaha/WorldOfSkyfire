using System;
using Microsoft.Xna.Framework;

namespace WorldOfSkyfire
{
    public class GhoulEnemyFactory : AbstractEntityFactory
    {
        public override Entity CreateEntity()
        {
            var ghoulEnemy = new GhoulEnemy();

            int hour = DateTime.Now.Hour;
            if (hour >= 6 && hour < 18)
            {
                ghoulEnemy.Color = Color.Blue;
            }
            else
            {
                ghoulEnemy.Color = Color.Black;
            }

            return ghoulEnemy;
        }
    }
}
