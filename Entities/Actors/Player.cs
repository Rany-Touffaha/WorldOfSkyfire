using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfSkyfire
{
    public class Player : Entity, IObservable<int>
    {
        // Store mutiple elements to observe player's health
        // Use HashSet because it is O(1) time
        private readonly HashSet<IObserver<int>> observers = [];
        private int health;
        public int MaxHealth { get; private set; } = 100;

        public Player(Vector2 startPosition)
        {
            Position = startPosition;
            Size = new Vector2(50, 50);
            Color = Color.Green;
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.ToPoint(), Size.ToPoint()), Color);
        }

        public int Health
        {
            get => health;
            private set
            {
                if (health == value) return;
                health = Math.Clamp(value, 0, MaxHealth);
                NotifyHealthChanged(health);
                if (health <= 0) NotifyCompleted();
            }
        }

        public void TakeDamage(int dmg) => Health = Health - Math.Max(0, dmg);
        public void Heal(int amt)       => Health = Health + Math.Max(0, amt);

        private void NotifyHealthChanged(int newHealth)
        {
            foreach (var observer in observers.ToArray())
                observer.OnNext(newHealth);
        }

        private void NotifyCompleted()
        {
            foreach (var observer in observers.ToArray())
                observer.OnCompleted();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            if (observers.Add(observer))
            {
                observer.OnNext(Health);
                return new DisposableAction(() => observers.Remove(observer));
            }

            return DisposableAction.Empty;
        }
    }
}