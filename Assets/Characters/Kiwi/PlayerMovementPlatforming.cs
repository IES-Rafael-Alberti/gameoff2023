using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerMovementPlatforming : MonoBehaviour
{
    public delegate void DeathCallback();
    public static event DeathCallback OnDeath;

    private static int health = 3;
    public static int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
    private bool invulnerable = false;
    public float invulnerabilityLength = 2f;

    private PlatformingControls input = null;
    private Vector3 moveVector = Vector3.zero;
    private Rigidbody rb = null;
    public float jumpForce = 5f;
    public GroundChecking groundCheck;
    public Rigidbody WallScan;
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
    [HideInInspector]
    public bool gliding = false;

    private void Awake()
    {
        input = new PlatformingControls();
        rb = GetComponent<Rigidbody>();
        moveSpeed = groundSpeed;
        _animator = gameObject.GetComponent<Animator>();
        SBShuffleBoardScript.OnReturn += DestroySelf;
    }

    private void Start()
    {
        GroundKiwi();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
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

        NormalUpdate(); //Where your old code went.
    }

    //This is your old update function Rubus.
    private void NormalUpdate()
    {
        Vector3 move = new Vector3(moveVector.x * moveSpeed, rb.velocity.y, moveVector.z * moveSpeed);
        rb.velocity = move;

        Vector3 movewSweep = new Vector3(moveVector.x * moveSpeed, 2f, moveVector.z * moveSpeed);
        float distanceEstimate = movewSweep.magnitude * Time.fixedDeltaTime;
        RaycastHit hit;
        if (WallScan.SweepTest(movewSweep, out hit, distanceEstimate))
        {
            if(hit.collider.gameObject.layer != LayerMask.NameToLayer("TriggerLayer"))rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        Vector3 moveDirection = new Vector3(moveVector.x * Time.deltaTime, 0.0f, moveVector.z * Time.deltaTime) * -1f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * moveSpeed);
        rb.AddForce(Vector3.down * (_isHolding ? gravity * 0.2f : gravity * 2f) * rb.mass); //TODO Separar en metodo.

        //Fixes a niche animation issue.
        if (_animator.GetBool("isFloating") && isGrounded())
        {
            _animator.SetBool("isJumping", false);
        }

        if (_isHolding && !isGrounded())
        {
            _animator.SetBool("isFloating", true);
        }
        else
        {
            _animator.SetBool("isFloating", false);
        }

        if(moveVector == Vector3.zero)
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void GroundKiwi()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the distance from the player to the ground
            float distanceToGround = hit.distance;

            // Teleport the player to the ground position
            Vector3 teleportPosition = transform.position - new Vector3(0f, distanceToGround - 1f, 0f);
            transform.position = teleportPosition;
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

    public void Damage()
    {
        if (!invulnerable)
        {
            Health--;
            StartCoroutine(IFrames());
        }
    }

    public IEnumerator IFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityLength);
        invulnerable = false;
        yield break;
    }
}
