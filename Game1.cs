using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace WorldOfSkyfire;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Entity player;
    List<Entity> enemies;
    Texture2D texture;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        player = EntityFactory.CreateEntity(EntityType.Player);
        enemies = new List<Entity>();

        for (int i = 0; i < 10; i++)
        {
            int type = (i % 2 == 0) ? 2 : 1;
            Entity enemy = EntityFactory.CreateEntity((EntityType)type);
            enemies.Add(enemy);
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        player.Draw(_spriteBatch, texture);
        foreach (var entity in enemies)
        {
            entity.Draw(_spriteBatch, texture);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
