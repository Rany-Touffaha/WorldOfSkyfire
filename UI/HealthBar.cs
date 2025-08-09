using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfSkyfire
{
    public class HealthBar : IObserver<int>
    {
        private readonly Texture2D texture;
        private readonly Vector2 position;
        private readonly int maxHealth;
        private readonly int barWidth;
        private readonly int barHeight;

        private int currentHealth;

        private bool emptyHealthBar = false;

        public bool IsEmpty => emptyHealthBar;

        public HealthBar(Texture2D startTexture, Vector2 startPosition, int maxHealth, int width = 200, int height = 12)
        {
            texture = startTexture;
            position = startPosition;
            maxHealth = Math.Max(1, maxHealth);
            barWidth = Math.Max(1, width);
            barHeight = Math.Max(1, height);
            currentHealth = maxHealth;
        }

        public void OnNext(int value)      => currentHealth = Math.Clamp(value, 0, maxHealth);
        public void OnCompleted()
        {
            emptyHealthBar = true;
            currentHealth = 0;
        }
        public void OnError(Exception error) { }


        public void Draw(SpriteBatch spriteBatch)
        {
            var backgroundRect = new Rectangle((int)position.X, (int)position.Y, barWidth, barHeight);
            spriteBatch.Draw(texture, backgroundRect, Color.Black * 0.5f);

            Color fgColor = emptyHealthBar ? Color.DarkGray : Color.Red;

            float percentage = currentHealth / (float)maxHealth;
            var foregroundRect = new Rectangle((int)position.X, (int)position.Y, (int)(barWidth * percentage), barHeight);
            spriteBatch.Draw(texture, foregroundRect, Color.Red);
        }
    }
}
