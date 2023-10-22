using UnityEngine;

public class AirGlideController : MonoBehaviour
{
    public float glidingSpeed = 5.0f;
    public float tiltSpeed = 2.0f;
    public float blowThreshold = 0.1f;
    public float glideHeight = 2.0f;

    private Rigidbody rb;
    private MicrophoneManager microphoneManager;
    private bool canGlide = false;
    private AudioSource audioSource;
    private float[] audioData = new float[128]; // Adjust the array size as needed

    private Animator animator; // Reference to the Animator component
    private bool isGliding = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent <Rigidbody>();
        rb.freezeRotation = true;

        microphoneManager = FindObjectOfType<MicrophoneManager>();
        microphoneManager.RequestMicrophonePermission();

        // Create an AudioSource if you don't have one already
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 1, AudioSettings.outputSampleRate);
        audioSource.loop = true;

        while (!(Microphone.GetPosition(null) > 0))
        {
            audioSource.Play();
        }
    }
    private void Update()
    {
        float microphoneInputLevel = microphoneManager.GetMicrophoneInputLevel(audioSource, audioData);

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
        tilt = Quaternion.Euler(0, 0, -90) * tilt;
        rb.AddTorque(tilt * tiltSpeed);

        if (canGlide)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float heightAboveGround = transform.position.y - hit.point.y;
                if (heightAboveGround < glideHeight && !isGliding)
                {
                    canGlide = false;
                }
            }
        }

        if (isGliding)
        {
            tilt = Input.acceleration;
            tilt = Quaternion.Euler(0, 0, -90) * tilt;

            float adjustedSpeed = glidingSpeed * (1.0f + microphoneInputLevel);

            rb.AddTorque(tilt * tiltSpeed);
            rb.AddForce(transform.forward * adjustedSpeed);
        }
    }

    void StartGliding()
    {
        isGliding = true;
        animator.SetBool("isGliding", true);
    }

    private void StopGliding()
    {
        isGliding = false;
        animator.SetBool("isGliding", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion"))
        {
            UnityEngine.Debug.Log("Collision prepare to glide"); canGlide = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion"))
        {
            UnityEngine.Debug.Log("Collision ended glide until ground"); canGlide = false;
        }
    }
}
