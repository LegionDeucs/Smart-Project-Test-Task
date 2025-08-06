using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveLerpStrength = 7;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool freezeX;
    [SerializeField] private bool freezeY;
    [SerializeField] private bool freezeZ;
    [SerializeField] private float ySizeMultiplier = 1.1f;
    [SerializeField] private float zSizeMultiplier = - 0.25f;

    private Transform target;

    public Vector3 Forward => transform.forward;
    public Vector3 Right => transform.right;

    public Camera Camera => mainCamera;

    public void SetupTarget(Transform target, bool teleport = true)
    {
        this.target = target;
        if(teleport)
            transform.position = target.position;
    }

    //Temporary camera offset solution
    public void SetupOffset(float mazeSize)
    {
        mainCamera.transform.localPosition = new Vector3(0, mazeSize* ySizeMultiplier, mazeSize* zSizeMultiplier);
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 targetPosition = new Vector3(freezeX ? 0 : target.position.x, freezeY ? 0 : target.position.y, freezeZ ? 0 : target.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveLerpStrength * Time.deltaTime);
    }
}
