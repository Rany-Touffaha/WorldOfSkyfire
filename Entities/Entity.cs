using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfSkyfire
{
    public abstract class Entity
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Color Color{ get; set; }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, Texture2D texture);

    }
}