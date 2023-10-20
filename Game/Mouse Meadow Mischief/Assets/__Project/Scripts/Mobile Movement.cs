using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    public Joystick joystick;
    public float runSpeed = 10.0f;
    public float jumpForce = 20.0f;
    private Rigidbody rb;
    private bool isJumping = false;
    private bool isGrounded = true; // New variable to track if the player is grounded

    // Define the minimum and maximum allowed x and z positions
    public float minX = -70f;
    public float maxX = 170f;
    public float minZ = -50f;
    public float maxZ = 1100f;

    public float accelerometerSensitivity = 5.0f; // Adjust this value

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;
        movementDirection.Normalize();

        Vector3 targetVelocity = movementDirection * runSpeed;
        targetVelocity.y = rb.velocity.y;

        float newPosX = Mathf.Clamp(transform.position.x + targetVelocity.x * Time.fixedDeltaTime, minX, maxX);
        float newPosZ = Mathf.Clamp(transform.position.z + targetVelocity.z * Time.fixedDeltaTime, minZ, maxZ);

        transform.position = new Vector3(newPosX, transform.position.y, newPosZ);

        rb.velocity = targetVelocity;

        if (Input.GetButtonDown("Jump") && !isJumping && isGrounded)
        {
            Jump();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isGrounded = true; // The player is on the ground
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // The player is no longer on the ground
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isJumping = true;
        isGrounded = false; // The player is no longer on the ground after jumping
    }

    void FixedUpdate()
    {
        // Get accelerometer input
        float accelerometerX = Input.acceleration.x;
        float accelerometerY = Input.acceleration.y;

        // Apply accelerometer input to movement
        Vector3 accelerometerMovement = new Vector3(accelerometerX, 0, accelerometerY) * accelerometerSensitivity;
        rb.AddForce(accelerometerMovement, ForceMode.Acceleration);
    }
}
