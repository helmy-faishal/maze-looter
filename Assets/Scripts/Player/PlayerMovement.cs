using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isTestScene = false;
    public bool canMove = true;
    [SerializeField] float moveSpeed = 200f;
    [SerializeField] Transform cameraTransform;

    Rigidbody rb;

    float horizontal, vertical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isTestScene) return;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove || isTestScene) return;
        GetInput();
    }

    private void FixedUpdate()
    {
        if (!canMove || isTestScene) return;
        MovePlayer();
    }

    void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {
        Vector3 xDirection = horizontal * cameraTransform.right;
        Vector3 zDirection = vertical * cameraTransform.forward;

        Vector3 move = xDirection + zDirection;

        rb.velocity = moveSpeed * Time.fixedDeltaTime * move;
    }
}