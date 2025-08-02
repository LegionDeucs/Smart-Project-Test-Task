using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputType : BaseState<InputSystemProcessorContext>
{
    protected InputType(InputSystemProcessorContext context) : base(context)
    {
    }
}
