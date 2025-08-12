using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldOfSkyfire;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player player;
    List<Entity> enemies;
    Texture2D texture;
    AbstractEntityFactory ghoulFactory = new GhoulEnemyFactory();
    Entity ghoulEnemy;

    private ICommand moveUp;
    private ICommand moveDown;
    private ICommand moveLeft;
    private ICommand moveRight;

    readonly GameStateService stateService = new GameStateService();
    GameState lastState;

    private HealthBar healthBar;
    private IDisposable healthBarSub;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        lastState = stateService.finiteStateMachine.State;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);



        texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var keyboardState = Keyboard.GetState();

        switch (stateService.finiteStateMachine.State)
        {
            case GameState.MainMenu:
                if (keyboardState.IsKeyDown(Keys.Enter))
                    stateService.finiteStateMachine.Fire(Trigger.StartGame);
                break;

            case GameState.Playing:
                HandleGameInput(keyboardState);
                UpdateGameplay(gameTime);
                break;

            case GameState.GameOver:
                if (keyboardState.IsKeyDown(Keys.Enter))
                    stateService.finiteStateMachine.Fire(Trigger.Restart);
                break;
        }

        if (stateService.finiteStateMachine.State != lastState)
        {
            if (stateService.finiteStateMachine.State == GameState.Playing)
                StartPlaying();
            if (lastState == GameState.Playing)
                StopPlaying();
            lastState = stateService.finiteStateMachine.State;
        }

        base.Update(gameTime);
    }

    void StartPlaying()
    {
        // Create player
        player = (Player)EntityFactory.CreateEntity(EntityType.Player);
        healthBar = new HealthBar(texture,
                           new Vector2(20, 20),
                           player.MaxHealth,
                           width: 200, height: 12);

        healthBarSub = player.Subscribe(healthBar);

        //Add player movement
        moveUp = new MovePlayerCommand((Player)player, new Vector2(0, -1));
        moveLeft = new MovePlayerCommand((Player)player, new Vector2(-1, 0));
        moveDown = new MovePlayerCommand((Player)player, new Vector2(0, 1));
        moveRight = new MovePlayerCommand((Player)player, new Vector2(1, 0));

        //Create enemies
        enemies = new List<Entity>();

        for (int i = 0; i < 10; i++)
        {
            int type = (i % 2 == 0) ? 2 : 1;
            Entity enemy = EntityFactory.CreateEntity((EntityType)type);
            enemies.Add(enemy);
        }

        var ghoulEnemy = ghoulFactory.CreateEntity();
        enemies.Add(ghoulEnemy);

        foreach (var enemy in enemies.OfType<EnemyBase>())
            enemy.SetTarget(player);
    }

    void StopPlaying()
    {
        enemies?.Clear();
        player = null;
        healthBar = null;
        healthBarSub?.Dispose();
        healthBarSub = null;
    }

    void HandleGameInput(KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(Keys.W)) moveUp.Execute();
        if (keyboardState.IsKeyDown(Keys.A)) moveLeft.Execute();
        if (keyboardState.IsKeyDown(Keys.S)) moveDown.Execute();
        if (keyboardState.IsKeyDown(Keys.D)) moveRight.Execute();
    }

    void UpdateGameplay(GameTime gameTime)
    {
        player?.Update(gameTime);
        foreach (Entity enemy in enemies)
            enemy.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        switch (stateService.finiteStateMachine.State)
        {
            case GameState.MainMenu:
                DrawTextBox("Press ENTER to start");
                break;
            case GameState.Playing:
                healthBar?.Draw(_spriteBatch);
                player?.Draw(_spriteBatch, texture);
                foreach (var enemy in enemies)
                    enemy.Draw(_spriteBatch, texture);
                break;
            case GameState.GameOver:
                DrawTextBox("Game Over – press Enter to restart");
                break;
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    void DrawTextBox(string txt)
        {
            var rect = new Rectangle(
                GraphicsDevice.Viewport.Width  / 2 - 100,
                GraphicsDevice.Viewport.Height / 2 - 20,
                200, 100);
            _spriteBatch.Draw(texture, rect, Color.Black * 0.7f);
        }
}
