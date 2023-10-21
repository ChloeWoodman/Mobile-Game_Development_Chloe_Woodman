using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 50.0f;
    public float jumpForce = 20.0f;
    private Rigidbody rb;
    private bool isJumping = false;
    private bool isRunning = false;

    private Animator animator; // Reference to the Animator component

    // Define the minimum and maximum allowed x and z positions
    public float minX = -200f;
    public float maxX = 300.0f;
    public float minZ = -50.0f;
    public float maxZ = 1100f;

    private static readonly string IsJumpingParam = "IsJumping";
    private static readonly string IsRunningParam = "IsRunning";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rigidbody from rotating
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }

        Movement();

        // Update the animator parameters
        animator.SetBool(IsJumpingParam, isJumping);
        animator.SetBool(IsRunningParam, isRunning);
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

        // Apply the x-axis restriction
        float newPosX = Mathf.Clamp(transform.position.x + targetVelocity.x * Time.fixedDeltaTime, minX, maxX);

        // Apply the z-axis restriction
        float newPosZ = Mathf.Clamp(transform.position.z + targetVelocity.z * Time.fixedDeltaTime, minZ, maxZ);

        // Set the x and z positions within the allowed ranges
        transform.position = new Vector3(newPosX, transform.position.y, newPosZ);

        rb.velocity = targetVelocity;

        isRunning = true; // You can update the isRunning variable here.
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isJumping = true;
        animator.SetBool(IsJumpingParam, isJumping); // Update the animator parameter
    }
}
