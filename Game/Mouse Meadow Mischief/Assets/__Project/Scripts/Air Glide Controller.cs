using UnityEngine;

public class AirGlideController : MonoBehaviour
{
    public float glidingSpeed = 5.0f;
    public float tiltSpeed = 2.0f;
    public float blowThreshold = 0.1f;
    public float glideHeight = 2.0f;

    private Rigidbody rb;
    //private AudioSource audioSource;
    //private float[] audioData = new float[128];
    private MicrophoneManager microphoneManager;

    private bool isGliding = false;
    private bool canGlide = false;
    //private bool isGlideAnimationPlaying = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        microphoneManager = FindObjectOfType<MicrophoneManager>();
        microphoneManager.HandleMicrophonePermissionDialog();
    }

    private void Update()
    {
        microphoneManager.HandleMicrophonePermissionDialog();

        float[] audioData = new float[128]; // or adjust the array size as needed

        // Create an AudioSource if you don't have one already
        AudioSource audioSource = GetComponent<AudioSource>(); // You may need to configure the audio source as needed

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
            // Check if the player is falling
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float heightAboveGround = transform.position.y - hit.point.y;
                if (heightAboveGround < glideHeight && !isGliding)
                {
                    canGlide = false;
                    //audioSource.Stop();
                }
            }

            //    if (!isGlideAnimationPlaying)
            //    {
            //        isGlideAnimationPlaying = true;
            //        audioSource.Play();
            //    }
            //}
            //else
            //{
            //    if (isGlideAnimationPlaying)
            //    {
            //        isGlideAnimationPlaying = false;
            //        audioSource.Stop();
            //    }
        }

        if (isGliding)
        {
            // Update the existing 'tilt' variable based on accelerometer input
            tilt = Input.acceleration;
            tilt = Quaternion.Euler(0, 0, -90) * tilt;

            // Use the microphone input to control gliding speed
            float adjustedSpeed = glidingSpeed * (1.0f + microphoneInputLevel);

            rb.AddTorque(tilt * tiltSpeed);
            rb.AddForce(transform.forward * adjustedSpeed);
        }
    }

    void StartGliding()
    {
        UnityEngine.Debug.Log("StartGliding called");
        isGliding = true;
    }

    private void StopGliding()
    {
        isGliding = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion"))
        {
            UnityEngine.Debug.Log("Collision with dandelion");
            canGlide = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dandelion"))
        {
            UnityEngine.Debug.Log("Collision ended with dandelion");
            canGlide = false;
        }
    }
}