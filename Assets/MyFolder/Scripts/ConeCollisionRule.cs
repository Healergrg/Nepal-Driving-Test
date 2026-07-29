using UnityEngine;

public class ConeCollisionRule : MonoBehaviour
{
    [Header("Penalty Settings")]
    public int conePenalty = 10;
    
    private GameManager gameManager;
    private bool alreadyHit = false; 

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // 1. DEBUG TRICK: This will print a white message telling us exactly what touched the cone
        Debug.Log("Cone was hit by: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);

        // 2. We check the object's tag OR its root parent's tag (fixes compound collider issues)
        if (collision.gameObject.CompareTag("Player") || collision.transform.root.CompareTag("Player"))
        {
            if (!alreadyHit)
            {
                gameManager.DeductMarks(conePenalty, "Hit a traffic cone");
                alreadyHit = true; 
            }
        }
    }
    
    // Just in case the cone is still set as a Trigger
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cone was passed through by: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);

        if (other.gameObject.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            if (!alreadyHit)
            {
                gameManager.DeductMarks(conePenalty, "Hit a traffic cone (Trigger)");
                alreadyHit = true; 
            }
        }
    }
}
