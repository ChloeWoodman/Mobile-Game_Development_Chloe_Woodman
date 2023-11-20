using UnityEngine;
using UnityEngine.InputSystem;

public class TouchMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float minX = -200f;
    [SerializeField] private float maxX = 200f;
    [SerializeField] private float minZ = -50f;
    [SerializeField] private float maxZ = 50f;

    [Header("On-Screen Controls")]
    [SerializeField] private Joystick joystick; 

    private Rigidbody rb;
    private Animator animator;
    private bool isJumping = false;
    private int jumpCount = 0;
    private const int maxJumps = 2;

    // PlayerInput component and actions
    private PlayerInput playerInput;
    private InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;

        // Initialize the PlayerInput component
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Read input from the joystick or the New Input System
        Vector2 inputVector = joystick.Direction;

        Vector3 movement = new Vector3(inputVector.x, 0f, inputVector.y) * runSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        // Clamp the player's position
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, minX, maxX),
            rb.position.y,
            Mathf.Clamp(rb.position.z, minZ, maxZ)
        );
    }

    private void HandleJumpInput()
    {
        if( jumpCount < maxJumps)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isGrounded() || jumpCount < maxJumps)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            jumpCount++;
            animator.SetBool("isJumping", true);
        }
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }

    public void OnJumpButtonPressed()
    {
        if (isGrounded() || jumpCount < maxJumps)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }
    }
}
