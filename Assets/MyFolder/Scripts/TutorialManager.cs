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

        // Step 0: Shift to 1st Gear
        if (tutorialStep == 0)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "कार्य १: [Left Shift] होल्ड गर्नुहोस् र पहिलो गियरमा जान [E] थिच्नुहोस्।" : 
                "TASK 1: Hold [Left Shift] and press [E] to shift into 1st Gear.";
            
            if (playerCar.currentGear == 2) tutorialStep++;
        }
        // Step 1: Accelerate
        else if (tutorialStep == 1)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "कार्य २: अगाडि बढ्न [W] थिच्नुहोस्। फिगर-८ ट्र्याकमा प्रवेश गर्नुहोस्!" : 
                "TASK 2: Press [W] to accelerate. Enter the Figure-8 track!";
            
            if (playerCar.currentSpeed > 2f) tutorialStep++; // Automatically moves to Step 2 when driving
        }
        // Step 2: The Figure-8 Rules (Triggered automatically after accelerating)
        else if (tutorialStep == 2)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "नियम: कोनलाई ठक्कर नदिई फिगर-८ को १.५ चक्कर पूरा गर्नुहोस्।" : 
                "RULES: Complete 1.5 loops of the Figure-8 without hitting cones.";
        }
        // Step 3: Exiting the 8 / Traffic Light / Indicators
        else if (tutorialStep == 3)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "नियम: मोड्नु अघि साइड लाइट बाल्नुहोस् र रातो बत्तीमा रोक्नुहोस्।" : 
                "RULES: Turn ON your side indicator before turning, and stop for Red lights.";
        }
        // Step 4: The Slope Test
        else if (tutorialStep == 4)
        {
            objectiveText.text = LanguageManager.isNepali ? 
                "नियम: उकालोमा रोक्नुहोस् र गाडी पछाडि नझारी ५ सेकेन्ड कुर्नुहोस्।" : 
                "RULES: Stop on the incline line and wait 5 seconds without rolling back.";
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