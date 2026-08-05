using UnityEngine;

public class IndicatorCheckZone : MonoBehaviour
{
    public GameManager gameManager;
    public bool requireLeftIndicator = true; 
    
    private bool penaltyGiven = false;

    void OnTriggerEnter(Collider other)
    {
        // 1. Tell us WHAT hit the box
        Debug.Log("⚠️ SOMETHING TOUCHED THE INVISIBLE BOX: " + other.gameObject.name);

        if (penaltyGiven) return; 

        // 2. Try to find the Car script
        CarIndicators indicators = other.GetComponentInParent<CarIndicators>();
        
        if (indicators == null)
        {
            Debug.Log("❌ ERROR: The box hit something, but couldn't find the 'CarIndicators' script on it!");
            return; // Stop here if it's not the car
        }

        Debug.Log("✅ SUCCESS: Found the car! Checking indicators now...");

        // 3. Check the GameManager connection
        if (gameManager == null)
        {
            Debug.LogError("🚨 FATAL ERROR: The GameManager slot is EMPTY in the Inspector! I cannot deduct marks!");
            return;
        }

        // 4. Do the actual math
        if (requireLeftIndicator && indicators.leftOn == false)
        {
            Debug.Log("🔴 PENALTY: Left indicator was OFF. Deducting marks.");
            gameManager.DeductMarks(10, "Failed to use Left side light!");
            penaltyGiven = true;
        }
        else if (!requireLeftIndicator && indicators.rightOn == false)
        {
            Debug.Log("🔴 PENALTY: Right indicator was OFF. Deducting marks.");
            gameManager.DeductMarks(10, "Failed to use Right side light!");
            penaltyGiven = true;
        }
        else
        {
            Debug.Log("🟢 SAFE: The correct indicator was turned on!");
        }
    }
}
