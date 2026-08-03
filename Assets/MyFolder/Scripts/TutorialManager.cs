using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Connections")]
    public TextMeshProUGUI objectiveText;
    public ProManualCarController playerCar;
    
    private int tutorialStep = 0;
    private bool tutorialActive = false;

    void Update()
    {
        if (!tutorialActive) return;

        // Step 0: Teach them how to use the clutch and shift into 1st Gear
        if (tutorialStep == 0)
        {
            objectiveText.text = "TASK 1: Hold [Left Shift] and press [E] to shift into 1st Gear.";
            
            // If they successfully shift into 1st gear (Gear 2 in the array)
            if (playerCar.currentGear == 2) 
            {
                tutorialStep++;
            }
        }
        // Step 1: Teach them to accelerate
        else if (tutorialStep == 1)
        {
            objectiveText.text = "TASK 2: Press [W] to accelerate. Enter the Figure-8 track!";
            
            // If the car starts moving faster than 2 speed
            if (playerCar.currentSpeed > 2f) 
            {
                tutorialStep++;
            }
        }
        // Step 2: Teach them the rules of the track
        else if (tutorialStep == 2)
        {
            objectiveText.text = "RULES: Complete the Figure-8 without hitting cones (-10 Marks).";
        }
        // Step 3: Stop at the traffic light (Can be triggered later by a road trigger!)
        else if (tutorialStep == 3)
        {
            objectiveText.text = "RULES: Approach the T-Junction. You MUST stop if the light is Red!";
        }
    }

    // You can call this from your GameManager right after the "3..2..1.. GO!" countdown finishes!
    public void StartTutorial()
    {
        tutorialActive = true;
    }

    // You can call this from a Trigger Box on the road to change the text as they drive
    public void AdvanceTutorialStep(int stepNumber)
    {
        tutorialStep = stepNumber;
    }
}
