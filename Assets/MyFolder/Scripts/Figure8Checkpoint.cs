using UnityEngine;

public class Figure8Checkpoint : MonoBehaviour
{
    public Figure8Manager manager;
    
    [Header("Box Settings")]
    public bool isExitLine = false; 
    public int checkpointNumber = 1; // 1, 2, 3, 4, etc.

    void OnTriggerEnter(Collider other)
    {
        ProManualCarController car = other.GetComponentInParent<ProManualCarController>();
        
        if (car != null)
        {
            if (isExitLine)
            {
                manager.CheckExit(); // Ask the judge if we passed
            }
            else
            {
                manager.HitCheckpoint(checkpointNumber); // Tell the judge we hit a box
            }
        }
    }
}
