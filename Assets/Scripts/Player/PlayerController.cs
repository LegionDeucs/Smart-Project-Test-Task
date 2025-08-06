using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMoveController moveController;


    public event System.Action OnStep;
    private void Start()
    {
        moveController.OnStep += OnStep;
    }

    internal void DisableMovement()
    {
        moveController.DisableMovement();
    }

    internal int GetStepsCount() => moveController.MoveCount;
}
