using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootSceneContext : MonoBehaviour
{
    [Inject] private ApplicationStateMachine ApplicationStateMachine;

    void Start()
    {
        ApplicationStateMachine.EnterState<BootApplicationState>();
    }
}
