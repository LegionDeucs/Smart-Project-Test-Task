using System;
using UnityEngine;
using Zenject;

public class InputSystemProcessor : StateMachine<InputType, InputSystemProcessorContext>
{
    private const float inputTickDelay = 0.2f;
    private readonly StandaloneCoroutineRunner standaloneCoroutineRunner;
    private CoroutineItem recallInputRoutine;

    public InputSystem InputSystem { get; private set; }

    public InputSystemProcessor(IInstantiator instantiator, StandaloneCoroutineRunner standaloneCoroutineRunner) : base(instantiator)
    {
        InputSystem = new InputSystem();

        states = new System.Collections.Generic.Dictionary<Type, InputType>();
        RegisterState<PCInputType>();
        this.standaloneCoroutineRunner = standaloneCoroutineRunner;
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

    private void CurrentState_OnMoveInput(Vector3 direction)
    {
        //TODO adjust for camera
        recallInputRoutine?.Stop();

        OnMoveInputStarted?.Invoke(direction.FloorToInt());
        recallInputRoutine = standaloneCoroutineRunner.WaitAndDoCoroutine(inputTickDelay, () => CurrentState_OnMoveInput(direction));
    }

    private void CurrentState_OnMoveInputCompleted()
    {
        recallInputRoutine?.Stop();
        OnMoveInputCompleted?.Invoke();
    }

    private void UnsubToInput()
    {
        currentState.OnMoveInputStarted -= CurrentState_OnMoveInput;
        currentState.OnMoveInputCompleted -= CurrentState_OnMoveInputCompleted;
    }
}
