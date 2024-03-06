using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool isTestScene = false;
    public bool canMove = true;
    [SerializeField] float moveSpeed = 200f;
    [SerializeField] Transform cameraTransform;

    CameraSwitching cameraSwitching;
    Rigidbody rb;
    PlayerInputAction action;
    InputAction MoveInput;

    float horizontal, vertical;
    float speedMultiplier = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        action = new PlayerInputAction();
    }

    private void OnEnable()
    {
        action.Player.Enable();

        cameraSwitching = FindObjectOfType<CameraSwitching>();

        if (cameraSwitching != null )
        {
            cameraSwitching.OnCameraStartMove += PlayerCannotMove;
            cameraSwitching.OnCameraEndMove += PlayerCanMove;
        }
    }

    private void OnDisable()
    {
        action.Player.Disable();

        if (cameraSwitching != null)
        {
            cameraSwitching.OnCameraStartMove -= PlayerCannotMove;
            cameraSwitching.OnCameraEndMove -= PlayerCanMove;
        }
    }

    public void PlayerCanMove()
    {
        canMove = true;
    }

    public void PlayerCannotMove()
    {
        rb.velocity = Vector3.zero;
        canMove = false;
    }

    void Start()
    {
        if (isTestScene) return;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MoveInput = action.Player.Move;
    }

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
        Vector2 move = MoveInput.ReadValue<Vector2>();
        horizontal = move.x;
        vertical = move.y;
    }

    void MovePlayer()
    {
        Vector3 xDirection = horizontal * cameraTransform.right;
        Vector3 zDirection = vertical * cameraTransform.forward;

        Vector3 move = xDirection + zDirection;

        rb.velocity = moveSpeed * speedMultiplier * Time.fixedDeltaTime * move;
    }

    public void SetSpeedMultiplier(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
    }
}
