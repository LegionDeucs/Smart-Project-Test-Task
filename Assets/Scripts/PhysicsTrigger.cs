using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayer;
    public event System.Action OnInteractionEnter;
    private void OnTriggerEnter(Collider other)
    {
        if (interactionLayer.Includes(other.gameObject.layer))
        {
            OnInteractionEnter?.Invoke();
        }
    }
}
