using UnityEngine;
using System.Collections;

public class TrafficLightController : MonoBehaviour
{
    [Header("Light Objects")]
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    [Header("Timers (in seconds)")]
    public float redDuration = 5f;
    public float greenDuration = 5f;
    public float yellowDuration = 2f;

    [Header("Status (Used by other scripts)")]
    public bool isRedLight = true;

    void Start()
    {
        // Start the infinite light cycle when the game begins
        StartCoroutine(LightCycle());
    }

    IEnumerator LightCycle()
    {
        while (true) // This makes the cycle repeat forever
        {
            // 1. RED LIGHT ON
            isRedLight = true;
            redLight.SetActive(true);
            yellowLight.SetActive(false);
            greenLight.SetActive(false);
            
            // Wait for the Red Duration
            yield return new WaitForSeconds(redDuration);

            // 2. GREEN LIGHT ON
            isRedLight = false;
            redLight.SetActive(false);
            yellowLight.SetActive(false);
            greenLight.SetActive(true);
            
            // Wait for the Green Duration
            yield return new WaitForSeconds(greenDuration);

            // 3. YELLOW LIGHT ON
            isRedLight = false; 
            redLight.SetActive(false);
            yellowLight.SetActive(true);
            greenLight.SetActive(false);
            
            // Wait for the Yellow Duration
            yield return new WaitForSeconds(yellowDuration);
        }
    }
}
