using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfSkyfire
{
    public class Player : Entity
    {
        public Player()
        {
            Position = new Vector2(100, 100);
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

    }
}