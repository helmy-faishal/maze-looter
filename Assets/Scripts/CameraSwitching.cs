using Cinemachine;
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

    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.canMove = false;

        cameraChest.Priority = 20;
        cameraAbove.Priority = 10;
        cameraFreeLook.Priority = 10;
        StartCoroutine(MoveToAboveCoroutine());
    }

    IEnumerator MoveToAboveCoroutine()
    {
        yield return new WaitForSeconds(moveToAboveDelay);
        cameraChest.Priority = 10;
        cameraAbove.Priority = 20;
        cameraFreeLook.Priority = 10;

        StartCoroutine(MoveToFreeLookCoroutine());
    }

    IEnumerator MoveToFreeLookCoroutine()
    {
        yield return new WaitForSeconds(moveToFreeLookDelay);
        cameraChest.Priority = 10;
        cameraAbove.Priority = 10;
        cameraFreeLook.Priority = 20;

        playerMovement.canMove = true;
    }
}
