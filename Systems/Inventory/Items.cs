namespace WorldOfSkyfire
{
    public class Weapon(string name, int damage, float weight) : IItem
    {
        public string Name { get; } = name;
        public int Damage { get; } = damage;
        public float Weight { get; } = weight;
        public override string ToString() => $"Weapon: {Name} with {Damage} DMG ";
    }

    public class Potion(string name, int healAmount, float weight) : IItem
    {
        public string Name { get; } = name;
        public int HealAmount { get; } = healAmount;
        public float Weight { get; } = weight;
        public override string ToString() => $"Potion: {Name} with {HealAmount} HP";
    }
}
