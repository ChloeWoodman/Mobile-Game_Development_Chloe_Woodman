using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(AudioSource))]
public class AirGlideController : MonoBehaviour
{
    public float glidingSpeed = 5.0f;
    public float tiltSpeed = 2.0f;
    //public float blowThreshold = 0.1f;
    public float glideHeight = 50.0f;
    public string groundTag = "Ground"; 
    public float accelerometerSensitivity = 2000.0f;
    private float currentForwardForce;

    public TouchMovement touchMovementScript;
    public GameObject[] dandelions; // Array to hold dandelion GameObjects

    private Rigidbody rb;
    private bool isGliding = false;
    //private AudioSource audioSource;
    //private float[] audioData;
    //private bool isMicInitialized = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentForwardForce = glidingSpeed;

        //audioSource = GetComponent<AudioSource>();
        //audioSource.loop = true;
        //audioSource.mute = true;

        //audioData = new float[256];
    }

    void FixedUpdate()
    {   
        if (isGliding)
        {
            HandleGliding();
            HandleTilting();
            CheckBounds();
        }

        //if (isMicInitialized)
        //{
        //    ProcessMicrophoneInput();
        //    HandleGliding();
        //    HandleTilting();
        //}
    }

    void CheckBounds()
    {
        // Check if the player's position is outside the specified bounds
        if (transform.position.x < -27f || transform.position.x > 27f ||
            transform.position.z < 5f || transform.position.z > 72f)
        {
            Debug.Log("Player has reached the bounds, stopping glide");
            StopGliding(); // Stop gliding and let the player fall
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion") && !isGliding)
        {
            Debug.Log("Collided with dandelion, starting glide");
            StartGliding();
            collision.gameObject.SetActive(false); // Hide the collided dandelion
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collided with ground, stopping glide");
            StopGliding();
            SetDandelionsVisible(); // Make all dandelions visible again
        }
    }

    void SetDandelionsVisible()
    {
        foreach (var dandelion in dandelions)
        {
            if (dandelion != null)
            {
                dandelion.SetActive(true);
            }
        }
    }

    //void StartMicrophone()
    //{
    //    if (!isMicInitialized)
    //    {
    //        Debug.Log("Attempting to start microphone");
    //        audioSource.clip = Microphone.Start(null, true, 10, 48000);
    //        int waitTime = 0;
    //        while (!(Microphone.GetPosition(null) > 0))
    //        {
    //            waitTime++;
    //            if (waitTime > 500) // 500 is an arbitrary number of loops. Adjust as needed.
    //            {
    //                Debug.LogError("Microphone not initializing");
    //                return;
    //            }
    //        }
    //        audioSource.Play();
    //        isMicInitialized = true;
    //        Debug.Log("Microphone started successfully");
    //    }
    //}

    //void ProcessMicrophoneInput()
    //{
    //    audioSource.GetOutputData(audioData, 0);
    //    float levelSum = 0f;

    //    foreach (var sample in audioData)
    //    {
    //        levelSum += Mathf.Abs(sample);
    //    }

    //    float avgLevel = levelSum / audioData.Length;
    //    Debug.Log("Average microphone level: " + avgLevel);
    //    if (avgLevel > blowThreshold && !isGliding && IsAboveGround())
    //    {
    //        Debug.Log("Noise is loud enough, beginning glide");
    //        StartGliding();
    //    }
    //}

    //bool IsAboveGround()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit))
    //    {
    //        return (transform.position.y - hit.point.y) < glideHeight;
    //    }
    //    return false;
    //}

    void StartGliding()
    {
        isGliding = true;
        rb.useGravity = false;  // Disable gravity

        if (touchMovementScript != null)
        {
            touchMovementScript.enabled = false;  // Disable touch movement control
        }
    }

    void StopGliding()
    {
        isGliding = false;
        rb.useGravity = true;  // Re-enable gravity

        currentForwardForce = glidingSpeed; // Reset forward force

        if (touchMovementScript != null)
        {
            touchMovementScript.enabled = true;  // Enable touch movement control
        }
    }

    void HandleGliding()
    {
        Debug.Log($"Y Position: {transform.position.y}, Upward Force: {Vector3.up * glidingSpeed}");

        // Check if the Rigidbody's current velocity is less than what you'd expect for the gliding speed
        if (rb.velocity.y < (glidingSpeed * Time.fixedDeltaTime))
        {
            // Apply a more controlled force
            rb.AddForce(Vector3.up * glidingSpeed, ForceMode.Acceleration);
        }

        // Apply a gentle downward force to simulate gliding descent
        float descentFactor = Mathf.Clamp((transform.position.y / glideHeight), 0.1f, 1.0f);
        rb.AddForce(Vector3.down * glidingSpeed * descentFactor, ForceMode.Acceleration);

        // Incrementally increase forward movement
        currentForwardForce += Time.fixedDeltaTime; // Increase forward force over time
        Vector3 forwardMovement = transform.forward * currentForwardForce;
        rb.AddForce(forwardMovement, ForceMode.Acceleration);
    }

    void HandleTilting()
    {
        float tiltLeftRight = Input.acceleration.x; // Tilting device left and right

        // Clamping the value to prevent extreme movements
        tiltLeftRight = Mathf.Clamp(tiltLeftRight, -1.0f, 1.0f);

        // Creating a tilt vector for the Rigidbody, affecting only the horizontal movement (along the X-axis)
        Vector3 tilt = new Vector3(tiltLeftRight, 0, 0) * accelerometerSensitivity;

        // Applying the tilt force
        rb.AddForce(tilt * tiltSpeed);
    }
}