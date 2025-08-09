using Stateless;

namespace WorldOfSkyfire
{
    public class GameStateService
    {
        public StateMachine<GameState, Trigger> finiteStateMachine { get; }

        public GameStateService()
        {
            finiteStateMachine = new StateMachine<GameState, Trigger>(GameState.MainMenu);

            finiteStateMachine.Configure(GameState.MainMenu)
               .Permit(Trigger.StartGame, GameState.Playing);

            finiteStateMachine.Configure(GameState.Playing)
               .Permit(Trigger.PlayerDied, GameState.GameOver);

            finiteStateMachine.Configure(GameState.GameOver)
               .Permit(Trigger.Restart, GameState.Playing);
        }
    }
}
