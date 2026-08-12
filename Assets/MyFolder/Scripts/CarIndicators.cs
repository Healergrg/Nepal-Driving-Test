// using UnityEngine;
// using System.Collections;

// public class CarIndicators : MonoBehaviour
// {
//     [Header("Light Connections")]
//     public GameObject[] leftLights;
//     public GameObject[] rightLights;
    
//     [Header("Live Status (View Only)")]
//     public bool leftOn = false;
//     public bool rightOn = false;

//     private bool isBlinking = false;
//     private bool lightsState = false;

//     void Start()
//     {
//         // Make sure all lights are OFF when the game starts
//         TurnOffAllLights();
//     }

//     void Update()
//     {
//         // Press Left Arrow to toggle left indicator
//         if (Input.GetKeyDown(KeyCode.LeftArrow))
//         {
//             leftOn = !leftOn; // Toggle on/off
//             if (leftOn) rightOn = false; // Turn off right if left turns on
//         }
        
//         // Press Right Arrow to toggle right indicator
//         if (Input.GetKeyDown(KeyCode.RightArrow))
//         {
//             rightOn = !rightOn; 
//             if (rightOn) leftOn = false; 
//         }

//         // If either light is on, start the blinking sequence!
//         if ((leftOn || rightOn) && !isBlinking)
//         {
//             StartCoroutine(BlinkRoutine());
//         }
//     }

//     IEnumerator BlinkRoutine()
//     {
//         isBlinking = true;
        
//         // Keep looping as long as one of the indicators is turned on
//         while (leftOn || rightOn)
//         {
//             lightsState = !lightsState; // Flashes the lights on and off

//             foreach (GameObject light in leftLights) 
//             {
//                 if (light != null) light.SetActive(leftOn && lightsState);
//             }
//             foreach (GameObject light in rightLights) 
//             {
//                 if (light != null) light.SetActive(rightOn && lightsState);
//             }

//             yield return new WaitForSeconds(0.4f); // How fast they blink
//         }
        
//         // When turned off, make sure the lights actually hide
//         TurnOffAllLights();
//         isBlinking = false;
//     }

//     private void TurnOffAllLights()
//     {
//         foreach (GameObject light in leftLights) if (light != null) light.SetActive(false);
//         foreach (GameObject light in rightLights) if (light != null) light.SetActive(false);
//     }
// }
using UnityEngine;
using System.Collections;

public class CarIndicators : MonoBehaviour
{
    [Header("Light Connections")]
    public GameObject[] leftLights;
    public GameObject[] rightLights;
    
    [Header("Live Status (View Only)")]
    public bool leftOn = false;
    public bool rightOn = false;

    private bool isBlinking = false;
    private bool lightsState = false;

    // <-- NEW: Tap memory for the mobile indicator buttons
    private bool wasLeftBtnPressed = false;
    private bool wasRightBtnPressed = false;

    void Start()
    {
        // Make sure all lights are OFF when the game starts
        TurnOffAllLights();
    }

    void Update()
    {
        // <-- NEW: Read the state of our mobile buttons
        bool mobileLeftBtn = MobileInputBridge.CheckButton("IndicatorLeft");
        bool mobileRightBtn = MobileInputBridge.CheckButton("IndicatorRight");

        // Press Left Arrow OR tap the mobile Left Indicator button
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (mobileLeftBtn && !wasLeftBtnPressed))
        {
            leftOn = !leftOn; // Toggle on/off
            if (leftOn) rightOn = false; // Turn off right if left turns on
        }
        
        // Press Right Arrow OR tap the mobile Right Indicator button
        if (Input.GetKeyDown(KeyCode.RightArrow) || (mobileRightBtn && !wasRightBtnPressed))
        {
            rightOn = !rightOn; 
            if (rightOn) leftOn = false; 
        }

        // <-- NEW: Save the button state for the next frame
        wasLeftBtnPressed = mobileLeftBtn;
        wasRightBtnPressed = mobileRightBtn;

        // If either light is on, start the blinking sequence!
        if ((leftOn || rightOn) && !isBlinking)
        {
            StartCoroutine(BlinkRoutine());
        }
    }

    IEnumerator BlinkRoutine()
    {
        isBlinking = true;
        
        // Keep looping as long as one of the indicators is turned on
        while (leftOn || rightOn)
        {
            lightsState = !lightsState; // Flashes the lights on and off

            foreach (GameObject light in leftLights) 
            {
                if (light != null) light.SetActive(leftOn && lightsState);
            }
            foreach (GameObject light in rightLights) 
            {
                if (light != null) light.SetActive(rightOn && lightsState);
            }

            yield return new WaitForSeconds(0.4f); // How fast they blink
        }
        
        // When turned off, make sure the lights actually hide
        TurnOffAllLights();
        isBlinking = false;
    }

    private void TurnOffAllLights()
    {
        foreach (GameObject light in leftLights) if (light != null) light.SetActive(false);
        foreach (GameObject light in rightLights) if (light != null) light.SetActive(false);
    }
}