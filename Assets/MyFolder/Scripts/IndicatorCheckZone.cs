using UnityEngine;

public class IndicatorCheckZone : MonoBehaviour
{
    public GameManager gameManager;
    public bool requireLeftIndicator = true; 
    
    private bool trapTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (trapTriggered) return; 

        CarIndicators indicators = other.GetComponentInParent<CarIndicators>();
        
        if (indicators != null)
        {
            if (gameManager == null) return; 

            // 🕵️ THIS IS OUR SPYGLASS! It tells us EXACTLY what the box sees.
            Debug.Log("🕵️ BOX SAYS -> Left Blinker is: " + indicators.leftOn + " | Right Blinker is: " + indicators.rightOn);

            if (requireLeftIndicator && indicators.leftOn == false)
            {
                Debug.Log("🔴 PENALTY: Left indicator was OFF.");
                gameManager.DeductMarks(10, "Failed to use Left side light!");
                trapTriggered = true; 
            }
            else if (!requireLeftIndicator && indicators.rightOn == false)
            {
                Debug.Log("🔴 PENALTY: Right indicator was OFF.");
                gameManager.DeductMarks(10, "Failed to use Right side light!");
                trapTriggered = true; 
            }
            else
            {
                Debug.Log("🟢 SAFE: The correct indicator was turned on!");
                trapTriggered = true; 
            }
        }
    }
}
