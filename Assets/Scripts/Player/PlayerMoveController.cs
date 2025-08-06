using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Zenject;

public enum MoveType { Stay, Move, FadeMove }
public class PlayerMoveController : MonoBehaviour
{
    [Inject] private InputSystemProcessor inputSystemProcessor;
    [Inject] private LevelGenerator levelGenerator;

    [SerializeField] private float moveDuration;
    [SerializeField] private float jumpPower;
    [SerializeField] private int maxInputCount = 2;

    private Tween moveTween;
    private int moveCount;

    public int MoveCount => moveCount;

    public event Action OnStep;

    private List<Vector3Int> inputQueue;
    private void Awake()
    {
        inputQueue = new List<Vector3Int>();
    }

    private void Start()
    {
        inputSystemProcessor.OnMoveInputStarted += InputSystemProcessor_OnMoveInputStarted;
        inputSystemProcessor.OnMoveInputCompleted += InputSystemProcessor_OnMoveInputCompleted;
    }

    private void InputSystemProcessor_OnMoveInputCompleted()
    {
    }

    private void InputSystemProcessor_OnMoveInputStarted(Vector3Int direction)
    {
        inputQueue.Add(direction);
        if (inputQueue.Count > maxInputCount)
            inputQueue.RemoveAt(0);

        if (moveTween != null)
            return;
        PerformMovement();
    }

    private void PerformMovement()
    {
        Vector3Int input = inputQueue[0];
        inputQueue.RemoveAt(0);

        switch (NextMovePoint(input, out Vector3 nextPosition))
        {
            case MoveType.FadeMove:
            case MoveType.Move:
                moveTween = transform.DOJump(nextPosition, jumpPower, 1, moveDuration).OnComplete(() =>
                {
                    moveTween = null;
                    if (inputQueue.Count > 0)
                    {
                        PerformMovement();
                    }
                });
                moveCount++;
                OnStep?.Invoke();
                break;
            case MoveType.Stay:
                if (inputQueue.Count > 0)
                    PerformMovement();
                break;
        }
    }

    private MoveType NextMovePoint(Vector3Int moveDirection, out Vector3 nextPosition)
    {
        Cell currentCell = levelGenerator.Grid.GetCell(transform.position);
        Cell nextCell = levelGenerator.Grid.GetNextCell(currentCell, moveDirection);

        nextPosition = currentCell.VisualPosition;

        if (nextCell == null)
            return MoveType.Stay;
        bool isDirOpened = currentCell.IsDirectionOpened(moveDirection);
        if (!isDirOpened)
            return MoveType.Stay;

        nextPosition = nextCell.VisualPosition;
        return MoveType.Move;
    }

    internal void DisableMovement()
    {
        inputSystemProcessor.OnMoveInputStarted -= InputSystemProcessor_OnMoveInputStarted;
        inputSystemProcessor.OnMoveInputCompleted -= InputSystemProcessor_OnMoveInputCompleted;
    }
}
