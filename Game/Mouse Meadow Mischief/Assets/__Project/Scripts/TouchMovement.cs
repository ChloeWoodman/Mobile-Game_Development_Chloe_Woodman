using System.Diagnostics;
using System.Collections;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 50.0f;
    public float jumpForce = 20.0f;
    private Rigidbody rb;
    private bool isJumping = false;

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

        // Initialize the animator in the Start method
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }

        Movement();
    }

    void Movement()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 cameraForward = Camera.main.transform.forward; // Use the camera's forward direction
        Vector3 cameraRight = Camera.main.transform.right;

        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;
        movementDirection.Normalize();

        // Calculate the target velocity based on the input
        Vector3 targetVelocity = movementDirection * runSpeed;
        targetVelocity.y = rb.velocity.y;

        // Check if movement is detected and set the "isRunning" parameter accordingly
        bool isMoving = movementDirection.magnitude > 0;
        animator.SetBool("isRunning", isMoving);

        // Apply the x-axis restriction
        float newPosX = Mathf.Clamp(transform.position.x + targetVelocity.x * Time.fixedDeltaTime, minX, maxX);

        // Apply the z-axis restriction
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
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    public void Jump()
    {
        animator.SetBool("isJumping", true); // Set the isJumping parameter to true
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isJumping = true;

        // Add a coroutine to reset the jump animation parameter
        StartCoroutine(ResetJumpAnimation());
    }

    private IEnumerator ResetJumpAnimation()
    {
        yield return new WaitForSeconds(2.0f); // Adjust the delay as needed
        animator.SetBool("isJumping", false); // Reset the jump animation parameter
    }
}
