using UnityEngine;

public class Figure8Manager : MonoBehaviour
{
    [Header("Connections")]
    public GameManager gameManager;
    
    [Header("Settings")]
    public int totalCheckpoints = 6; // How many boxes they must hit before leaving
    
    [Header("Live Data (View Only)")]
    public int currentProgress = 0;

    // Called automatically by the invisible boxes
    public void HitCheckpoint(int boxNumber)
    {
        // Only count it if it is EXACTLY the next box in the required sequence!
        // (This prevents them from driving backward or doing donuts in one circle)
        if (boxNumber == currentProgress + 1)
        {
            currentProgress++;
            Debug.Log("Good! Hit Figure-8 Checkpoint: " + currentProgress);
        }
    }

    // Called automatically when they touch the Exit Line
    public void CheckExit()
    {
        if (currentProgress >= totalCheckpoints)
        {
            Debug.Log("Successfully completed 1.5 rounds of the Figure 8!");
            currentProgress = 0; // Reset just in case
        }
        else
        {
            gameManager.DeductMarks(100, "Skipped parts of the Figure 8 track!");
            currentProgress = 0; 
        }
    }
}
