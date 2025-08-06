using Zenject;
namespace MyCore.StateMachine
{
   public class GameplayStateMachine : StateMachine<GameplayStateMachineBaseState, GameplayContext>
    {
        public GameplayStateMachine(IInstantiator instantiator) : base(instantiator)
        {
            states = new System.Collections.Generic.Dictionary<System.Type, GameplayStateMachineBaseState>();

            RegisterState<GameState>();
            RegisterState<WinGameState>();
            RegisterState<LoseGameState>();
        }
    }
}