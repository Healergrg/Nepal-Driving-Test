using UnityEngine;

public class FinishLineRule : MonoBehaviour
{
    private GameManager gameManager;
    private bool trialFinished = false;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // We check the object's tag OR its root parent's tag (just like the cones!)
        if ((other.CompareTag("Player") || other.transform.root.CompareTag("Player")) && !trialFinished)
        {
            trialFinished = true;
            Debug.Log("Car crossed the finish line!"); // This will show in the console
            gameManager.TrialPassed(); 
        }
    }
}