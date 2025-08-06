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
    AbstractEntityFactory ghoulFactory = new GhoulEnemyFactory();
    Entity ghoulEnemy;

    private ICommand moveUp;
    private ICommand moveDown;
    private ICommand moveLeft;
    private ICommand moveRight;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        player = EntityFactory.CreateEntity(EntityType.Player);
        ghoulEnemy = ghoulFactory.CreateEntity();
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

        moveUp = new MovePlayerCommand((Player)player, new Vector2(0, -1));
        moveLeft = new MovePlayerCommand((Player)player, new Vector2(-1, 0));
        moveDown = new MovePlayerCommand((Player)player, new Vector2(0, 1));
        moveRight = new MovePlayerCommand((Player)player, new Vector2(1, 0));

        texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        ghoulEnemy.Update(gameTime);

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.W)) moveUp.Execute();
        if (keyboardState.IsKeyDown(Keys.A)) moveLeft.Execute();
        if (keyboardState.IsKeyDown(Keys.S)) moveDown.Execute();
        if (keyboardState.IsKeyDown(Keys.D)) moveRight.Execute();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        player.Draw(_spriteBatch, texture);
        ghoulEnemy.Draw(_spriteBatch, texture);
        foreach (var entity in enemies)
        {
            entity.Draw(_spriteBatch, texture);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
