using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AndroidInputType : InputType
{
    private float minRelativeInputLength = 0.1f;
    private readonly float minInputLength;

    private Vector2 currentPartialInput;

    public AndroidInputType(InputSystemProcessorContext context) : base(context)
    {
        minInputLength = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height) * minRelativeInputLength;
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

    private void MoveAction_canceled(InputAction.CallbackContext context)
    {
        currentPartialInput = Vector2.zero;
    }

    private void MoveAction_performed(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        currentPartialInput += rawInput;

        if(currentPartialInput.magnitude >= minInputLength)
        {
            Vector2 processedInput = Mathf.Abs(currentPartialInput.x) > Mathf.Abs(currentPartialInput.y) ?
                                    new Vector2(Mathf.Sign(currentPartialInput.x), 0) :
                                    new Vector2(0, Mathf.Sign(currentPartialInput.y));

            currentPartialInput = Vector2.zero;
            OnMoveInputStarted?.Invoke(new Vector3(processedInput.x, 0, processedInput.y));
        }
    }

    public override void OnStateExit()
    {

        Context.InputSystemProcessor.InputSystem.BaseGameplay.MoveAction.performed -= MoveAction_performed;
        Context.InputSystemProcessor.InputSystem.BaseGameplay.MoveAction.canceled -= MoveAction_canceled;
    }
}
