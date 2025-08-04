using System;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfSkyfire
{
    public class ZombieEnemy : Entity
    {
        public ZombieEnemy()
        {
            Random rng = new Random();
            float x = rng.Next(50, 750);
            float y = rng.Next(50, 450);

            Position = new Vector2(x, y);
            Size = new Vector2(50, 50);
            Color = Color.Red;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.ToPoint(), Size.ToPoint()), Color);
        }
    }
}