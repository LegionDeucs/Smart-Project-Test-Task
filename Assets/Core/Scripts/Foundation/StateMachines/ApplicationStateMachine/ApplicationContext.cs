using Zenject;

public class ApplicationContext : IStateMachineContext
{
    [Inject]
    public ApplicationStateMachine StateMachine;

    public ApplicationContext(IInstantiator instantiator)
    {

    }
}
