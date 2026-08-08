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
            objectiveText.text = LanguageManager.isNepali ? 
                "कार्य १: [Left Shift] होल्ड गर्नुहोस् र पहिलो गियरमा जान [E] थिच्नुहोस्।" : 
                "TASK 1: Hold [Left Shift] and press [E] to shift into 1st Gear.";
            
            if (playerCar.currentGear == 2) 
            {
                tutorialStep++;
            }
        }
        // Step 1: Teach them to accelerate
        else if (tutorialStep == 1)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "कार्य २: अगाडि बढ्न [W] थिच्नुहोस्। फिगर-८ ट्र्याकमा प्रवेश गर्नुहोस्!" : 
                "TASK 2: Press [W] to accelerate. Enter the Figure-8 track!";
            
            if (playerCar.currentSpeed > 2f) 
            {
                tutorialStep++;
            }
        }
        // Step 2: Teach them the rules of the track
        else if (tutorialStep == 2)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "नियम: कोनहरूलाई ठक्कर नदिई फिगर-८ पूरा गर्नुहोस् (-१० अंक)।" : 
                "RULES: Complete the Figure-8 without hitting cones (-10 Marks).";
        }
        // Step 3: Stop at the traffic light
        else if (tutorialStep == 3)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "नियम: टी-जक्सनमा पुग्नुहोस्। रातो बत्ती बलेको छ भने रोक्नै पर्छ!" : 
                "RULES: Approach the T-Junction. You MUST stop if the light is Red!";
        }
    }

    public void StartTutorial()
    {
        tutorialActive = true;
    }

    public void AdvanceTutorialStep(int stepNumber)
    {
        tutorialStep = stepNumber;
    }
}