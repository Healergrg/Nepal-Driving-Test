using UnityEngine;

public class ZebraCrossingRule : MonoBehaviour
{
    [Header("Connections")]
    public TrafficLightController trafficLight;
    public GameManager gameManager;

    private bool alreadyPenalized = false;

    void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object that entered the line is the car
        if (other.CompareTag("Player"))
        {
            // 2. Check if the light is currently red
            if (trafficLight.isRedLight)
            {
                // 3. Make sure we only penalize them once per crossing
                if (!alreadyPenalized)
                {
                    gameManager.DeductMarks(gameManager.redLightPenalty, "Ran a Red Light at Zebra Crossing");
                    alreadyPenalized = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset the penalty flag after the car leaves the crossing area completely
        if (other.CompareTag("Player"))
        {
            alreadyPenalized = false;
        }
    }
}