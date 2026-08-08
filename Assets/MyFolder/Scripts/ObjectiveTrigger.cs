using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public TutorialManager tutorialManager;
    [Tooltip("Which step should load when the car hits this box? (e.g., 3 for Indicators, 4 for Slope)")]
    public int stepToTrigger;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object passing through is the car
        if (other.GetComponentInParent<ProManualCarController>() != null)
        {
            if (tutorialManager != null)
            {
                tutorialManager.AdvanceTutorialStep(stepToTrigger); // Change the text!
            }
        }
    }
}