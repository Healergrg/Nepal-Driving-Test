using UnityEngine;

public class SpeedometerManager : MonoBehaviour
{
    [Header("Connections")]
    public ProManualCarController carController;
    public RectTransform needleTransform; // The UI needle

    [Header("Speedometer Calibration")]
    public float maxSpeedOnDial = 100f; // What is the highest speed on your picture?
    
    // Unity UI Rotation: Positive is counter-clockwise (Left), Negative is clockwise (Right)
    public float zeroSpeedAngle = 135f;   // Where the needle points at 0 (bottom left)
    public float maxSpeedAngle = -135f;  // Where the needle points at max speed (bottom right)

    void Update()
    {
        if (carController != null && needleTransform != null)
        {
            // 1. Get the absolute speed (so reversing still pushes the needle UP!)
            float currentSpeed = Mathf.Abs(carController.currentSpeed);

            // 2. Calculate the percentage of our speed (0.0 is stopped, 1.0 is maxed out)
            float speedPercentage = currentSpeed / maxSpeedOnDial;
            speedPercentage = Mathf.Clamp01(speedPercentage); // Prevents needle from breaking the dial

            // 3. Calculate the exact angle based on that percentage
            float targetAngle = Mathf.Lerp(zeroSpeedAngle, maxSpeedAngle, speedPercentage);

            // 4. Smoothly rotate the needle on the Z axis
            float smoothAngle = Mathf.LerpAngle(needleTransform.localEulerAngles.z, targetAngle, Time.deltaTime * 10f);
            needleTransform.localEulerAngles = new Vector3(0, 0, smoothAngle);
        }
    }
}
