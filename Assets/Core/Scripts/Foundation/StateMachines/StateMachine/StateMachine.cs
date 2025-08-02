using System;
using System.Collections.Generic;
using Zenject;

public abstract class StateMachine<TBaseState, TContext> where TBaseState : BaseState<TContext> where TContext : IStateMachineContext
{
    protected readonly IInstantiator instantiator;
    protected Dictionary<Type, TBaseState> states;

    protected TBaseState currentState;

    public StateMachine(IInstantiator instantiator) 
    {
        states = new Dictionary<Type, TBaseState>();
        this.instantiator = instantiator;
    }

    public virtual TState EnterState<TState>() where TState : BaseState<TContext>
    {
        currentState?.OnStateExit();
        currentState = null;

        Type type = typeof(TState);

        if (states.ContainsKey(type))
            currentState = states[type];

        currentState?.OnStateEnter();
        return currentState as TState;
    }

    protected void RegisterState<TState>()
        where TState : TBaseState
    {
        states.Add(typeof(TState), instantiator.Instantiate<TState>());
    }
}
