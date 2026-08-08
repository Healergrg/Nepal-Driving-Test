using UnityEngine;

public class LBackManager : MonoBehaviour
{
    [Header("Connections")]
    public GameManager gameManager;
    
    [Header("Live Data (View Only)")]
    public bool hasDoneLBack = false;

    // Called when they hit Box 1 (Inside the L-Back)
    public void HitLBackZone()
    {
        hasDoneLBack = true;
        Debug.Log("Player entered the L-Back zone!");
    }

    // Called when they hit Box 2 (The road after L-Back)
    public void HitExitTrap()
    {
        if (hasDoneLBack == false)
        {
            Debug.Log("Player skipped the L-Back! Instant Fail.");
            if (gameManager != null)
            {
                // Instantly deduct 100 points to trigger the Fail Screen!
                gameManager.DeductMarks(100, LanguageManager.isNepali ? "एल-ब्याक (L-Back) पूरा गर्नुभएन!" : "Skipped the L-Back!"); 
            }
        }
        else
        {
            Debug.Log("Player successfully completed L-Back and moved on.");
        }
    }
}