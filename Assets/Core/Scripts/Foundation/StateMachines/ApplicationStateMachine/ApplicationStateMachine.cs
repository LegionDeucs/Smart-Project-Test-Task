using MyCore.StateMachine;
using Zenject;

public class ApplicationStateMachine : StateMachine<ApplicationStateMachineBaseState, ApplicationContext>
{
    public ApplicationStateMachine(ApplicationContext applicationContext, IInstantiator instantiator) : base(instantiator)
    {
        RegisterState<BootApplicationState>();
        RegisterState<LoadingSceneApplicationState>();
        RegisterState<GameApplicationState>();
    }

}
