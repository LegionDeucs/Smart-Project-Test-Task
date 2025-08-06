using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputType : BaseState<InputSystemProcessorContext>, IInputType
{
    protected InputType(InputSystemProcessorContext context) : base(context)
    {
    }

    public abstract event Action<Vector3> OnMoveInputStarted;
    public abstract event Action OnMoveInputCompleted;
}
