using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerProvider : MonoBehaviour
{
    [Inject] private LevelGenerator levelGenerator;

    [SerializeField] private PlayerController player;

    public PlayerController PlayerController => player;

    private void Start()
    {
        player.transform.position = levelGenerator.transform.position;
    }
}
