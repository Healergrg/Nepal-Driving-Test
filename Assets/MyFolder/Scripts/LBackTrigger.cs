using UnityEngine;

public class LBackTrigger : MonoBehaviour
{
    public LBackManager manager;
    public bool isExitTrap = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object or its parent has the Player tag OR the car script
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player") || other.GetComponentInParent<ProManualCarController>() != null)
        {
            if (isExitTrap)
            {
                Debug.Log("EXIT TRAP HIT BY CAR!"); // Adding a loud debug message
                manager.HitExitTrap(); 
            }
            else
            {
                Debug.Log("L-BACK ZONE HIT BY CAR!");
                manager.HitLBackZone(); 
            }
        }
    }
}