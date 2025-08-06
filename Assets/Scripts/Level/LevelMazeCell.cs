using NoctisDev.Pooling.Runtime.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelMazeCell : MonoBehaviour
{
    [Inject] private IPoolService poolService;

    [SerializeField] private Transform wallN;
    [SerializeField] private Transform wallW;
    [SerializeField] private Transform wallE;
    [SerializeField] private Transform wallS;

    [SerializeField] private Transform winObjectPrefab;

    public Transform WallN => wallN;
    public Transform WallW => wallW;
    public Transform WallE => wallE;
    public Transform WallS => wallS;

    internal void SetAsWin()
    {
        poolService.Spawn(winObjectPrefab, parent: transform);
    }
}
