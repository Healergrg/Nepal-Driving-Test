using UnityEngine;
using UnityEngine.UI;

public class CarCameraManager : MonoBehaviour
{
    [Header("Camera Switching (Press C)")]
    public GameObject[] drivingCameras; 
    private int currentCamIndex = 0;

    [Header("Reverse Screen System")]
    public GameObject reverseMirrorUI; 
    public ProManualCarController carController; // <-- NEW: Connects to the car's gears!

    void Start()
    {
        for (int i = 0; i < drivingCameras.Length; i++)
        {
            drivingCameras[i].SetActive(i == currentCamIndex);
        }
        
        if (reverseMirrorUI != null) reverseMirrorUI.SetActive(false);
    }

    void Update()
    {
        // 1. SWITCH CAMERA LOGIC (Press C)
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCamIndex++;
            if (currentCamIndex >= drivingCameras.Length) 
            {
                currentCamIndex = 0; 
            }

            for (int i = 0; i < drivingCameras.Length; i++)
            {
                drivingCameras[i].SetActive(i == currentCamIndex);
            }
        }

        // 2. NEW REVERSE MIRROR LOGIC (Reads the Gearbox)
        if (reverseMirrorUI != null && carController != null)
        {
            // Gear 0 is Reverse in our manual transmission
            if (carController.currentGear == 0)
            {
                if (!reverseMirrorUI.activeSelf) reverseMirrorUI.SetActive(true);
            }
            else
            {
                if (reverseMirrorUI.activeSelf) reverseMirrorUI.SetActive(false);
            }
        }
    }
}
