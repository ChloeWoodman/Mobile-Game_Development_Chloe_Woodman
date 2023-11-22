using System.Collections;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 50.0f;
    public float jumpForce = 20.0f;
    private Rigidbody rb;
    private bool isJumping = false;
    private int jumpCount = 0; // Variable to track number of jumps
    private const int maxJumps = 2; // Maximum number of jumps

    private float sensitivity = 1.0f; // Sensitivity setting
    private Animator animator; // Reference to the Animator component

    // Define the minimum and maximum allowed x and z positions
    public float minX = -200f;
    public float maxX = 300.0f;
    public float minZ = -50.0f;
    public float maxZ = 1100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rigidbody from rotating
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumps))
        {
            Jump();
        }

        Movement();
    }

    public void SetSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;
    }

    void Movement()
    {
        if (OptionsMenu.Instance != null)
        {
            sensitivity = OptionsMenu.Instance.GetCurrentSensitivity();
        }

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Adjust input using sensitivity
        horizontalInput *= sensitivity;
        verticalInput *= sensitivity;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Calculate movement direction
        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;
        movementDirection.Normalize();

        // Check if there is significant movement to update rotation
        if (movementDirection.magnitude > 0.1f)
        {
            // Create a rotation looking in the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            // Increase the rotation speed multiplier for quicker rotation
            float rotationSpeed = sensitivity * 5.0f; // Adjust this value as needed

            // Smoothly rotate the player towards the target rotation more quickly
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Calculate the target velocity based on the input
        Vector3 targetVelocity = movementDirection * runSpeed;
        targetVelocity.y = rb.velocity.y;

        // Check if movement is detected and set the "isRunning" parameter accordingly
        bool isMoving = movementDirection.magnitude > 0;
        animator.SetBool("isRunning", isMoving);

        // Apply the x-axis and z-axis restrictions
        float newPosX = Mathf.Clamp(transform.position.x + targetVelocity.x * Time.fixedDeltaTime, minX, maxX);
        float newPosZ = Mathf.Clamp(transform.position.z + targetVelocity.z * Time.fixedDeltaTime, minZ, maxZ);

        // Set the x and z positions within the allowed ranges
        transform.position = new Vector3(newPosX, transform.position.y, newPosZ);

        rb.velocity = targetVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpCount = 0; // Reset jump count when touching the ground
            animator.SetBool("isJumping", false);

            // Check if the collided object is a mushroom
            if (collision.gameObject.name.Contains("Mushroom"))
            {
                jumpForce *= 10; // Multiply jump force by 10
                StartCoroutine(ResetJumpForce()); // Start coroutine to reset jump force
            }
        }
    }

    private IEnumerator ResetJumpForce()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay as needed
        jumpForce /= 10; // Reset the jump force back to original
    }

    public void Jump()
    {
        if (jumpCount >= maxJumps)
        {
            return; // Prevent jumping if the max number of jumps has been reached
        }

        animator.SetBool("isJumping", true);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isJumping = true;
        jumpCount++; // Increment jump count each time the player jumps

        StartCoroutine(ResetJumpAnimation());
    }

    private IEnumerator ResetJumpAnimation()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay as needed
        animator.SetBool("isJumping", false); // Reset the jump animation parameter
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure OptionsMenu is initialized
        if (OptionsMenu.Instance != null)
        {
            SetSensitivity(OptionsMenu.Instance.GetCurrentSensitivity());
        }
    }
}
