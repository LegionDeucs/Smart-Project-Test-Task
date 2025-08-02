using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveLerpStrength = 7;
    [SerializeField] private Camera mainCamera;
    public Transform target;

    public Vector3 Forward => transform.forward;
    public Vector3 Right => transform.right;

    public Camera Camera => mainCamera;

    public void SetupTarget(Transform target)
    {
        //this.target = target;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, moveLerpStrength * Time.deltaTime);
    }
}
