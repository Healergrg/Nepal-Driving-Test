using UnityEngine;

public class IndicatorCheckZone : MonoBehaviour
{
    public GameManager gameManager;
    public bool requireLeftIndicator = true; // Check this box for Left, uncheck for Right
    
    private bool penaltyGiven = false;

    void OnTriggerEnter(Collider other)
    {
        // Don't deduct marks twice if they reverse back into the box
        if (penaltyGiven) return; 

        CarIndicators indicators = other.GetComponentInParent<CarIndicators>();
        
        if (indicators != null)
        {
            if (requireLeftIndicator && indicators.leftOn == false)
            {
                gameManager.DeductMarks(10, "Failed to use Left side light!");
                penaltyGiven = true;
            }
            else if (!requireLeftIndicator && indicators.rightOn == false)
            {
                gameManager.DeductMarks(10, "Failed to use Right side light!");
                penaltyGiven = true;
            }
        }
    }
}
