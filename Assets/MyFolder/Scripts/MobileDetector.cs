// using UnityEngine;

// public class MobileDetector : MonoBehaviour
// {
//     void Start()
//     {
//         // Unity automatically checks if the device is a phone/tablet
//         if (Application.isMobilePlatform)
//         {
//             // If it IS a phone, keep this object turned ON
//             gameObject.SetActive(true);
//         }
//         else
//         {
//             // If it is a PC or Mac, turn this object completely OFF
//             gameObject.SetActive(false);
//         }
//     }
// }
using UnityEngine;

public class MobileDetector : MonoBehaviour
{
    [Header("Check this box to test UI on your PC")]
    public bool forceShowInEditor = true;

    void Start()
    {
        // 1. If we are testing in the Unity Editor and the box is checked, show the buttons!
        #if UNITY_EDITOR
        if (forceShowInEditor)
        {
            gameObject.SetActive(true);
            return; // Stop reading the rest of the script
        }
        #endif

        // 2. Normal check for when the game is actually published to the web
        if (Application.isMobilePlatform)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}