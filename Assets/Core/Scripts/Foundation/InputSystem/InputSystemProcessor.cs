using System;
using UnityEngine;
using Zenject;

public class InputSystemProcessor : StateMachine<InputType, InputSystemProcessorContext>
{
    public InputSystem InputSystem { get; private set; }

    public InputSystemProcessor(IInstantiator instantiator) : base(instantiator)
    {
        InputSystem = new InputSystem();

        states = new System.Collections.Generic.Dictionary<Type, InputType>();
        RegisterState<PCInputType>();
    }

    public event Action<Vector3Int> OnMoveInputStarted;
    public event Action OnMoveInputCompleted;

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
        currentState.OnMoveInputStarted += CurrentState_OnMoveInput;
        currentState.OnMoveInputCompleted += CurrentState_OnMoveInputCompleted;
    }

    private void CurrentState_OnMoveInputCompleted()
    {
        OnMoveInputCompleted?.Invoke();
    }

    private void CurrentState_OnMoveInput(Vector3 direction)
    {
        //TODO adjust for camera
        OnMoveInputStarted?.Invoke(direction.FloorToInt());
    }

    private void UnsubToInput()
    {
        currentState.OnMoveInputStarted -= CurrentState_OnMoveInput;
        currentState.OnMoveInputCompleted -= CurrentState_OnMoveInputCompleted;
    }
}
