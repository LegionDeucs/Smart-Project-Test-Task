using MyCore.StateMachine;
using UnityEngine;
using Zenject;

public class GameplayContext : MonoBehaviour, IStateMachineContext
{
    [Inject] private GameplayStateMachine gameplayStateMachine;
    [Inject] private CameraController cameraController;

    private void Start()
    {
        gameplayStateMachine.EnterState<GameState>();
    }
}
