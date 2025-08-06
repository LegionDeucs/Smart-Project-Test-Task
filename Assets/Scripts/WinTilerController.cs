using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WinTilerController : MonoBehaviour
{
    [Inject] private GameplayContext gameplayContext;
    [SerializeField] private PhysicsTrigger interactionTrigger;
    [SerializeField] private ParticleSystem winEffect;

    private void Start()
    {
        interactionTrigger.OnInteractionEnter += InteractionTrigger_OnInteractionEnter;
    }

    private void InteractionTrigger_OnInteractionEnter()
    {
        winEffect.Play();
        gameplayContext.OnWinGameTriggerEnter();
    }
}
