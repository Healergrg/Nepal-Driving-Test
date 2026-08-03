using UnityEngine;

public class CarEngineAudio : MonoBehaviour
{
    [Header("Connections")]
    public ProManualCarController carController;
    public AudioSource engineAudioSource;

    [Header("Engine Sound Settings")]
    public float idlePitch = 0.5f; 
    public float maxPitch = 2.0f;
    public float minVolume = 0.2f; // Quiet when stopped
    public float maxVolume = 1.0f; // Loud when driving

    void Start()
    {
        if (engineAudioSource != null && !engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
    }

    void Update()
    {
        if (carController != null && engineAudioSource != null)
        {
            // 1. Calculate exact RPM percentage (0.0 is Idle, 1.0 is Max RPM)
            float currentRPM = carController.currentRPM;
            float rpmRange = carController.maxRPM - carController.idleRPM;
            float rpmPercentage = (currentRPM - carController.idleRPM) / rpmRange;
            
            // Ensure the percentage never drops below 0 or goes above 1
            rpmPercentage = Mathf.Clamp01(rpmPercentage);

            // 2. PITCH CALCULATION (How high/low the sound is)
            float targetPitch = Mathf.Lerp(idlePitch, maxPitch, rpmPercentage);
            
            // REVERSE GEAR EFFECT: Real cars whine at a higher pitch when reversing!
            if (carController.currentGear == 0 && Mathf.Abs(carController.currentSpeed) > 1f)
            {
                targetPitch += 0.3f; // Adds a distinct reverse sound
            }

            // Smoothly change the pitch so it doesn't snap instantly
            engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, targetPitch, Time.deltaTime * 5f);

            // 3. VOLUME CALCULATION (How loud the car is)
            bool isPressingGas = Input.GetKey(KeyCode.W);
            float targetVolume = minVolume; // Default to quiet idle
            
            if (isPressingGas)
            {
                // Engine roars loudly when pressing the gas pedal
                targetVolume = Mathf.Lerp(minVolume + 0.3f, maxVolume, rpmPercentage);
            }
            else if (Mathf.Abs(carController.currentSpeed) > 1f)
            {
                // Coasting: Car is moving, but foot is off the gas (Engine Braking sound)
                float speedPercentage = Mathf.Abs(carController.currentSpeed) / 100f;
                targetVolume = Mathf.Lerp(minVolume, maxVolume * 0.6f, speedPercentage);
            }
            
            // Smoothly fade the volume
            engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, targetVolume, Time.deltaTime * 5f);
        }
    }
}
