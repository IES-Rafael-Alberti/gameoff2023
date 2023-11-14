using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovementPlatforming : MonoBehaviour
{
    private PlatformingControls input = null;
    private Vector3 moveVector = Vector3.zero;
    private Rigidbody rb = null;
    public float jumpForce = 5f;
    public GroundChecking groundCheck;
    private Vector3 kiwiDirection;
    public float groundSpeed = 15f;

    private bool doubleJump;
    public float gravity;
    public bool _isHolding;

    private float moveSpeed;

    private Animator _animator;

    //SliderDropVariables (DON'T TOUCH THESE PLZ RUBUS)
    [Header("Glider Variables")]
    public float glideSpeed = 80f;
    public bool freeFall = true;
    public float glideDropSpeed = 5;
    public float releaseDropSpeed = 25;
    private float dropSpeedGoal = -5;
    private float dropSpeed = -5;
    private float glideVelocityX;
    [HideInInspector]
    public bool gliding = false;



    private void Awake()
    {
        input = new PlatformingControls();
        rb = GetComponent<Rigidbody>();
        moveSpeed = groundSpeed;
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        input.Enable();
        EnableNormalControls();
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);

        if (gliding)
        {
            GlideUpdate();
        } else
        {
            NormalUpdate(); //Where your old code went.
        }
    }

    //This is your old update function Rubus.
    private void NormalUpdate()
    {
        rb.velocity = new Vector3(moveVector.x * moveSpeed, rb.velocity.y, moveVector.z * moveSpeed);
        Vector3 moveDirection = new Vector3(moveVector.x * Time.deltaTime, 0.0f, moveVector.z * Time.deltaTime) * -1f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * moveSpeed);
        rb.AddForce(Vector3.down * (_isHolding ? gravity * 0.2f : gravity * 2f) * rb.mass); //TODO Separar en metodo.

        if(_isHolding && !isGrounded())
        {
            _animator.SetBool("isFloating", true);
        }
        else
        {_animator.SetBool("isFloating", false);}

        if(moveVector == Vector3.zero)
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void OnDisable()
    {
        input.Disable();
        DisableNormalControls();
    }

    //XZ Axis movement.
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _animator.SetBool("isRunning", true);
        moveVector = value.ReadValue<Vector3>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector3.zero;
    }

    //Jumping.
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        _isHolding = true;
        if (isGrounded())
        {
            doubleJump = false;
        }

        // Checks if we are on the ground or we have doubleJump.
        if (isGrounded() || doubleJump)
        {
            _animator.SetBool("isJumping", true);
            rb.velocity = new Vector3(moveVector.x * moveSpeed, 0f, moveVector.z * moveSpeed);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // We disable doubleJump.
            doubleJump = !doubleJump;
        }
    }

    private void OnJumpCancelled(InputAction.CallbackContext value)
    {
        _animator.SetBool("isJumping", false);
        _isHolding = false;
    }

    bool isGrounded()
    {
        return groundCheck.GroundCheck();
    }

    private void EnableNormalControls()
    {
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Jump.performed += OnJumpPerformed;
        input.Player.Jump.canceled += OnJumpCancelled;
    }
    private void DisableNormalControls()
    {
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.Jump.performed -= OnJumpPerformed;
        input.Player.Jump.canceled -= OnJumpCancelled;
    }

    #region Glide
    private void GlideUpdate()
    {
        dropSpeed = dropSpeed * 0.9f  + dropSpeedGoal * 0.1f;
        if (moveVector.x > 0.1) glideVelocityX = glideSpeed;
        if (moveVector.x < -0.1) glideVelocityX = -glideSpeed;
        if (freeFall) glideVelocityX *= 0.1f;
        rb.velocity = new Vector3(glideVelocityX, dropSpeed, 0);
        //Ends gliding when you land.
        if (isGrounded())
        {
            StopDrop();
        }
    }
    private void EnableGlideControls()
    {
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Jump.performed += SlowFall;
        input.Player.Jump.canceled += NormalFall;
    }
    private void DisableGlideControls()
    {
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.Jump.performed -= SlowFall;
        input.Player.Jump.canceled -= NormalFall;
    }

    private void SlowFall(InputAction.CallbackContext context)
    {
        dropSpeedGoal = -glideDropSpeed;
        freeFall = false;
    }

    private void NormalFall(InputAction.CallbackContext context)
    {
        dropSpeedGoal = -releaseDropSpeed;
        freeFall = true;
    }

    public void DropKiwi()
    {
        glideVelocityX = glideSpeed;
        dropSpeed = -glideDropSpeed;
        dropSpeedGoal = -releaseDropSpeed;
        gliding = true;
        freeFall = true;
        moveSpeed = glideSpeed;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | rb.constraints;
        DisableNormalControls();
        EnableGlideControls();
    }

    public void StopDrop()
    {
        gliding = false;
        moveSpeed = groundSpeed;
        rb.constraints = ~RigidbodyConstraints.FreezePositionZ & rb.constraints;
        DisableGlideControls();
        EnableNormalControls();
    }
    #endregion
}
