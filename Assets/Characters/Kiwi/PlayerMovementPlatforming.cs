using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementPlatforming : MonoBehaviour
{
    private PlatformingControls input = null;
    private Vector3 moveVector = Vector3.zero;
    private Rigidbody rb = null;
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public GroundChecking groundCheck;
    private Vector3 kiwiDirection;

    private bool doubleJump;

    private void Awake()
    {
        input = new PlatformingControls();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Jump.performed += OnJumpPerformed;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveVector.x * moveSpeed, rb.velocity.y, moveVector.z * moveSpeed);
        Vector3 moveDirection = new Vector3(moveVector.x, 0.0f, moveVector.z) * -1f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * moveSpeed);
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    //XZ Axis movement.
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector3>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector3.zero;
    }

    //Jumping.
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        if (isGrounded())
        {
            doubleJump = false;
        }

        // Checks if we are on the ground or we have doubleJump.
        if (isGrounded() || doubleJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // We disable doubleJump.
            doubleJump = !doubleJump;
        }
    }

    bool isGrounded()
    {
        return groundCheck.GroundCheck();
    }
}
