using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cameraChest;
    [SerializeField] CinemachineVirtualCamera cameraAbove;
    [SerializeField] CinemachineFreeLook cameraFreeLook;

    [SerializeField] float moveToAboveDelay = 2f;
    [SerializeField] float moveToFreeLookDelay = 2f;

    public Action OnCameraStartMove;
    public Action OnCameraEndMove;

    [HideInInspector]
    public bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        ResetCameraPriority();
        cameraChest.Priority = 20;
        OnCameraStartMove?.Invoke();
        StartCoroutine(MoveToAboveCoroutine());
    }

    void ResetCameraPriority()
    {
        cameraChest.Priority = 10;
        cameraAbove.Priority = 10;
        cameraFreeLook.Priority = 10;
    }

    IEnumerator MoveToAboveCoroutine()
    {
        yield return new WaitForSeconds(moveToAboveDelay);
        ResetCameraPriority();
        cameraAbove.Priority = 20;

        StartCoroutine(MoveToFreeLookCoroutine());
    }

    IEnumerator MoveToFreeLookCoroutine()
    {
        yield return new WaitForSeconds(moveToFreeLookDelay);
        ResetCameraPriority();
        cameraFreeLook.Priority = 20;
        OnCameraEndMove?.Invoke();
        isMoving = false;
    }
}
