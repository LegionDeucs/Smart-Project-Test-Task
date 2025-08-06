using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInputType : InputType
{
    public PCInputType(InputSystemProcessorContext context) : base(context)
    {

    }

    public override event Action<Vector3> OnMoveInputStarted;
    public override event Action OnMoveInputCompleted;

    public override void Dispose()
    {
        
    }

    public override void OnStateEnter()
    {
        Context.InputSystemProcessor.InputSystem.BaseGameplay.Enable();
        Context.InputSystemProcessor.InputSystem.BaseGameplay.MoveAction.performed += MoveAction_performed;
        Context.InputSystemProcessor.InputSystem.BaseGameplay.MoveAction.canceled += MoveAction_canceled;
    }

    private void MoveAction_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnMoveInputCompleted?.Invoke();
    }

    private void MoveAction_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        OnMoveInputStarted?.Invoke(new Vector3(rawInput.x, 0, rawInput.y));
    }

    public override void OnStateExit()
    {
        Context.InputSystemProcessor.InputSystem.BaseGameplay.Disable();
        Context.InputSystemProcessor.InputSystem.BaseGameplay.MoveAction.performed -= MoveAction_performed;
        Context.InputSystemProcessor.InputSystem.BaseGameplay.MoveAction.canceled -= MoveAction_canceled;
    }
}
