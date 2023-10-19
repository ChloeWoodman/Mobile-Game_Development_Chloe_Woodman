using UnityEngine;

public class AirGlideController : MonoBehaviour
{
    public float glidingSpeed = 5.0f;
    public float tiltSpeed = 2.0f;
    public float blowThreshold = 0.1f; // Adjust this value based on your microphone sensitivity
    public float glideHeight = 2.0f; // Height above the ground at which gliding is allowed
    private Animator animator; // Reference to the Animator component
    private bool isGlideAnimationPlaying = false; // Flag to track whether the glide animation is playing
    private Rigidbody rb;
    private bool isGliding = false;
    private bool canGlide = false; // Flag to check if the player can start gliding
    private AudioSource audioSource;
    private float[] audioData = new float[128]; // Initialize an array to store audio data

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Make sure the Z-axis is the forward axis

        audioSource = GetComponent<AudioSource>(); // Reference the AudioSource component

        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for microphone input (blow detection) and the ability to glide
        float microphoneInputLevel = GetMicrophoneInputLevel();

        if (microphoneInputLevel > blowThreshold && canGlide)
        {
            StartGliding();
        }
        else if (microphoneInputLevel <= blowThreshold && isGliding)
        {
            StopGliding();
        }

        // Tilt the glider based on accelerometer input
        Vector3 tilt = Input.acceleration;
        tilt = Quaternion.Euler(0, 0, -90) * tilt; // Adjust for device orientation
        rb.AddTorque(tilt * tiltSpeed);

        // Check if the player is falling (height above the ground < glideHeight)
        if (canGlide)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float heightAboveGround = transform.position.y - hit.point.y;
                if (heightAboveGround < glideHeight && !isGliding)
                {
                    canGlide = false; // Prevent further gliding until the player lands
                    audioSource.Stop(); // Stop audio when gliding ends
                }
            }

            if (!isGlideAnimationPlaying)
            {
                // Start the glide animation
                //animator.SetBool("IsGliding", true);
                isGlideAnimationPlaying = true;
                audioSource.Play(); // Start audio when gliding begins
            }
        }
        else
        {
            if (isGlideAnimationPlaying)
            {
                // Stop the glide animation
                //animator.SetBool("IsGliding", false);
                isGlideAnimationPlaying = false;
                audioSource.Stop(); // Stop audio when gliding animation ends
            }
        }

        // Apply forward gliding force while gliding
        if (isGliding)
        {
            rb.AddForce(transform.forward * glidingSpeed);
        }
    }

    float GetMicrophoneInputLevel()
    {
        audioSource.GetOutputData(audioData, 0); // Get audio data from the microphone
        float microphoneLevel = 0;

        // Calculate the microphone input level
        for (int i = 0; i < audioData.Length; i++)
        {
            microphoneLevel += Mathf.Abs(audioData[i]);
        }

        return microphoneLevel / audioData.Length;
    }

    void StartGliding()
    {
        isGliding = true;
        audioSource.Play(); // Start audio when gliding begins
        // Add any specific setup when gliding starts
    }

    void StopGliding()
    {
        isGliding = false;
        audioSource.Stop(); // Stop audio when gliding ends
        // Add any specific cleanup when gliding stops
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion"))
        {
            Debug.Log("Dandelion collision detected");
            canGlide = true; // Allow gliding when the player lands on a dandelion
            // Start the glide animation when the player lands on a dandelion
            //animator.SetBool("IsGliding", true);
            isGlideAnimationPlaying = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion"))
        {
            Debug.Log("Left Dandelion");
            canGlide = false; // Disallow gliding when the player leaves the dandelion
            audioSource.Stop(); // Stop the microphone when leaving the dandelion
        }
    }
}
