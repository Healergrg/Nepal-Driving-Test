using UnityEngine;

public class Figure8Manager : MonoBehaviour
{
    [Header("Connections")]
    public GameManager gameManager;
    
    [Header("1.5 Loop Settings")]
    [Tooltip("The exact order of checkpoints they must hit. Example: 1, 2, 3, 4, 1, 2")]
    public int[] requiredSequence = { 1, 2, 3, 4, 1, 2 }; 
    
    [Header("Live Data (View Only)")]
    public int currentProgress = 0;

    // Called automatically by the invisible boxes
   // Called automatically by the invisible boxes
    public void HitCheckpoint(int boxNumber)
    {
        if (currentProgress >= requiredSequence.Length) return;

        if (boxNumber == requiredSequence[currentProgress])
        {
            currentProgress++;
            Debug.Log("Good! Hit Checkpoint " + boxNumber + ". Progress: " + currentProgress + "/" + requiredSequence.Length);
        }
        else
        {
            // They hit the wrong box (went backward or skipped one)
            Debug.Log("Wrong way! Expected Box " + requiredSequence[currentProgress] + " but hit " + boxNumber);
            
            // NEW: Instantly fail them for driving out of sequence!
            if (gameManager != null)
            {
                gameManager.DeductMarks(100, "Wrong direction in Figure 8!"); 
            }
        }
    }

    // Called automatically when they touch the Exit Line (the trigger at the way out)
    public void CheckExit()
    {
        if (currentProgress >= requiredSequence.Length)
        {
            Debug.Log("Successfully completed 1.5 rounds of the Figure 8!");
            // You can add a success sound or UI popup here!
        }
        else
        {
            Debug.Log("Player exited the Figure 8 too early! Instant Fail.");
            // Deducting 100 marks instantly drops them below 70, triggering the Fail Screen!
            if (gameManager != null)
            {
                gameManager.DeductMarks(100, "Figure 8 Incomplete!"); 
            }
        }
    }
}