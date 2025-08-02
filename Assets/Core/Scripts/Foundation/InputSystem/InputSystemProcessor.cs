using UnityEngine;
using Zenject;

public class InputSystemProcessor : StateMachine<InputType, InputSystemProcessorContext>, IInputType
{
    public InputSystemProcessor(IInstantiator instantiator) : base(instantiator)
    {
    }

    public override TState EnterState<TState>()
    {
        if(currentState != null)
            UnsubToInput();

        var state  = base.EnterState<TState>();
        SubToInput();

        return state;
    }

    private void SubToInput()
    {

    }
    private void UnsubToInput()
    {

    }
}
