using UnityEngine;

public class AccelerometerInput : MonoBehaviour
{
    public float accelerometerSensitivity = 2000.0f; // Adjust this value to control sensitivity.

    private Rigidbody rb;
    public float runSpeed = 50.0f;
    public float jumpForce = 20.0f;
    private bool isJumping = false;

    public float minX = -200f;
    public float maxX = 300.0f;
    public float minZ = -50.0f;
    public float maxZ = 1100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get accelerometer input
        float accelerometerX = Input.acceleration.x;
        float accelerometerY = Input.acceleration.y;

        // Apply accelerometer input to movement
        Vector3 accelerometerMovement = new Vector3(accelerometerX, 0, accelerometerY) * accelerometerSensitivity;

        // Apply the accelerometer input to the Rigidbody for movement
        rb.AddForce(accelerometerMovement, ForceMode.Acceleration);

        // Use the accelerometerMovement vector in your game logic as needed.

        // Enforce screen boundaries
        float newPosX = Mathf.Clamp(transform.position.x, minX, maxX);
        float newPosZ = Mathf.Clamp(transform.position.z, minZ, maxZ);

        transform.position = new Vector3(newPosX, transform.position.y, newPosZ);

        // Detect and handle jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isJumping = true;
    }
}
