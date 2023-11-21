using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class AirGlideController : MonoBehaviour
{
    public float glidingSpeed = 5.0f;
    public float tiltSpeed = 2.0f;
    public float blowThreshold = 0.1f;
    public float glideHeight = 2.0f;
    public string groundTag = "Ground"; // Tag for the ground object

    private Rigidbody rb;
    private AudioSource audioSource;
    private bool isGliding = false;
    private float[] audioData;
    private bool isMicInitialized = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.mute = true;

        audioData = new float[256];
    }

    void Update()
    {
        if (isMicInitialized)
        {
            ProcessMicrophoneInput();
            HandleGliding();
            HandleTilting();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion") && !isGliding)
        {
            Debug.Log("Collided with dandelion, blow into mic");
            StartMicrophone();
        }
        else if (collision.gameObject.CompareTag(groundTag))
        {
            Debug.Log("Collided with ground, stopping glide");
            StopGliding();
        }
    }

    void StartMicrophone()
    {
        if (!isMicInitialized)
        {
            Debug.Log("Attempting to start microphone");
            audioSource.clip = Microphone.Start(null, true, 10, 48000);
            int waitTime = 0;
            while (!(Microphone.GetPosition(null) > 0))
            {
                waitTime++;
                if (waitTime > 500) // 500 is an arbitrary number of loops. Adjust as needed.
                {
                    Debug.LogError("Microphone not initializing");
                    return;
                }
            }
            audioSource.Play();
            isMicInitialized = true;
            Debug.Log("Microphone started successfully");
        }
    }

    void ProcessMicrophoneInput()
    {
        audioSource.GetOutputData(audioData, 0);
        float levelSum = 0f;

        foreach (var sample in audioData)
        {
            levelSum += Mathf.Abs(sample);
        }

        float avgLevel = levelSum / audioData.Length;
        Debug.Log("Average microphone level: " + avgLevel);
        if (avgLevel > blowThreshold && !isGliding && IsAboveGround())
        {
            Debug.Log("Noise is loud enough, beginning glide");
            StartGliding();
        }
    }

    bool IsAboveGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            return (transform.position.y - hit.point.y) < glideHeight;
        }
        return false;
    }

    void HandleGliding()
    {
        if (isGliding)
        {
            Vector3 forwardMovement = transform.forward * glidingSpeed;
            rb.AddForce(forwardMovement, ForceMode.Acceleration);
        }
    }

    void HandleTilting()
    {
        if (isGliding)
        {
            Vector3 tilt = Input.acceleration;
            tilt = Quaternion.Euler(0, 0, -90) * tilt;
            rb.AddTorque(tilt * tiltSpeed);
        }
    }

    void StartGliding()
    {
        isGliding = true;
        // Add any animations or effects for starting the glide
    }

    void StopGliding()
    {
        isGliding = false;
        isMicInitialized = false; // Reset microphone initialization
        audioSource.Stop(); // Stop the microphone
        Microphone.End(null); // End microphone recording
        // Add any animations or effects for stopping the glide
    }
}
