using MyCore.StateMachine;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GameplayContext : MonoBehaviour, IStateMachineContext
{
    [Inject] private GameplayStateMachine gameplayStateMachine;
    [Inject] private CameraController cameraController;
    [Inject] private InputSystemProcessor inputSystemProcessor;
    [Inject] private PlayerProvider playerProvider;
    [Inject] private LevelGenerator levelGenerator;

    private DateTime startedLevelTime;
    private DateTime finishLevelTime;

    public void OnWinGameTriggerEnter()
    {
        finishLevelTime = DateTime.Now;
        gameplayStateMachine.EnterState<WinGameState>();
    }

    private void Start()
    {
        gameplayStateMachine.EnterState<GameState>();
        inputSystemProcessor.OnMoveInputStarted += InputSystemProcessor_OnMoveInputStarted;

        cameraController.SetupTarget(playerProvider.PlayerController.transform);
    }

    private void InputSystemProcessor_OnMoveInputStarted(Vector3Int obj)
    {
        //Move to game state after creating Prepare state (game is on pause until player make his first move)
        inputSystemProcessor.OnMoveInputStarted -= InputSystemProcessor_OnMoveInputStarted;
        startedLevelTime = DateTime.Now;
    }

    internal TimeSpan GetGameplayDuration() => finishLevelTime - startedLevelTime;

    internal Vector2Int GetMazeSize()
    {
        return levelGenerator.GetMazeSize();
    }
}
