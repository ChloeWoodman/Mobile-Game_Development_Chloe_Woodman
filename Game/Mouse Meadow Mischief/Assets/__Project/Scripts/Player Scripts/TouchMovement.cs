using System.Collections;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public GameObject[] climbableObjects;
    public GameObject[] mushroomObjects;

    public Joystick joystick;
    public float runSpeed = 50.0f;
    public float jumpForce = 20.0f;
    public float climbSpeed = 5.0f; // Speed of climbing
    private Rigidbody rb;
    private bool isJumping = false;
    private bool isClimbing = false; // Flag to check if the player is climbing
    private int jumpCount = 0; // Variable to track number of jumps
    private const int maxJumps = 2; // Maximum number of jumps

    public float gyroSensitivity = 100.0f;

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
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure OptionsMenu is initialized
        if (OptionsMenu.Instance != null)
        {
            SetSensitivity(OptionsMenu.Instance.GetCurrentSensitivity());
        }
    }

    void FixedUpdate()
    {
        if (!isClimbing)
        {
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                Jump();
            }

            Movement();
        }
        else
        {
            Climb();
        }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.z, q.y, -q.w);
    }

    public void SetSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;
    }

    void Movement()
    {
        float horizontalInput = joystick.Horizontal * sensitivity;
        float verticalInput = joystick.Vertical * sensitivity;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        if (movementDirection.magnitude > 0.1f)
        {
            movementDirection.Normalize();

            // Use gyroscope data for rotation
            Quaternion gyroRotation = GyroToUnity(Input.gyro.attitude);
            transform.rotation = Quaternion.Slerp(transform.rotation, gyroRotation, Time.deltaTime * gyroSensitivity); // Use a variable for gyro sensitivity

            // Directly set the velocity for more responsive control
            Vector3 velocity = movementDirection * runSpeed;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        animator.SetBool("isRunning", movementDirection.magnitude > 0.1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpCount = 0; // Reset jump count when touching the ground
            animator.SetBool("isJumping", false);

            // Reset Gyroscope Orientation
            ResetGyroOrientation();

            foreach (var mushroomObject in mushroomObjects)
            {
                // Check if the collided object is a mushroom
                if (collision.gameObject == mushroomObject)
                {
                    jumpForce *= 10; // Multiply jump force by 10
                    StartCoroutine(ResetJumpForce()); // Start coroutine to reset jump force
                }
            }
        }

        Debug.Log("Collision detected with: " + collision.gameObject.name);

        foreach (var climbableObject in climbableObjects)
        {
            if (collision.gameObject == climbableObject)
            {
                isClimbing = true;
                rb.useGravity = false;
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("Plant"))
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }

    void ResetGyroOrientation()
    {
        // Reset the player's rotation to a default orientation
        // This can be adjusted based on your game's default orientation
        transform.rotation = Quaternion.identity;
    }

    void Climb()
    {
        Debug.Log("Climbing method called. Joystick Vertical value: " + joystick.Vertical);
        float climbInput = joystick.Vertical; // Replace with your input method
        if (Mathf.Abs(climbInput) > 0.1f)
        {
            // Use Rigidbody to move for consistent physics interaction
            Vector3 climbMovement = Vector3.up * climbSpeed * climbInput * Time.deltaTime;
            rb.MovePosition(rb.position + climbMovement);
        }
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

    private IEnumerator ResetJumpForce()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay as needed
        jumpForce /= 10; // Reset the jump force back to original
    }
}
