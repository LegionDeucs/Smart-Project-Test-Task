using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputType
{
    event Action<Vector3> OnMoveInputStarted;
    event Action OnMoveInputCompleted;
}
