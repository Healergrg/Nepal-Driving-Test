using UnityEngine;

public class IndicatorOffCheckZone : MonoBehaviour
{
    public GameManager gameManager;
    
    private bool trapTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        // Only grade the car once so we don't double-penalize the back tires!
        if (trapTriggered) return; 

        CarIndicators indicators = other.GetComponentInParent<CarIndicators>();
        
        if (indicators != null)
        {
            if (gameManager == null) 
            {
                Debug.LogError("🚨 FATAL ERROR: GameManager is missing in the Inspector!");
                return; 
            }

            // Check if EITHER the left or right indicator is still turned on
            if (indicators.leftOn == true || indicators.rightOn == true)
            {
                Debug.Log("🔴 PENALTY: Player forgot to turn off the indicator!");
                gameManager.DeductMarks(10, "Failed to turn off side light after turn!");
                trapTriggered = true; 
            }
            else
            {
                Debug.Log("🟢 SAFE: Indicators were successfully turned off.");
                trapTriggered = true; 
            }
        }
    }
}
